/*
 * AppUartCommunication.c
 *
 *  Created on: Dec 18, 2020
 *      Author: vadvol01
 */


#include "AppUartCommunication.h"
#include "board_states.h"
#include "queue.h"

extern UART_HandleTypeDef huart1;
extern 	SemaphoreHandle_t xTxItSemaphore;

uint8_t u8_rx_buff[RX_BUFF_SIZE] = {0,};
uint8_t u8_rx_packet[RX_BUFF_SIZE] = {0,};

uint32_t u32_counter = 0;
uint8_t u8_rx_byte;

uint16_t u16_request_response;

extern 	SemaphoreHandle_t xTxItSemaphore;


// START it for initialization of RX interrupt sequence:
void AppUartRxInit()
{
    //__HAL_UART_ENABLE_IT(&huart1, UART_IT_IDLE);
    HAL_UART_Receive_IT(&huart1, & u8_rx_byte, 1);
}

void AppUartGetResponseID(uint16_t *pu16_response_id)
{
	*pu16_response_id = u16_request_response;
}

void AppUartSetResponseID(uint16_t u16_response_id)
{
	u16_request_response = u16_response_id;
}




void HAL_UART_TxCpltCallback(UART_HandleTypeDef *huart)
{
	xSemaphoreGiveFromISR( xTxItSemaphore, pdTRUE);
}


// RX "receive byte" event and restart RX interrupt:
#if 1
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
//	static uint32_t u32_local_counter = 0;

	if(huart == &huart1)
	{
		u8_rx_buff[u32_counter] = u8_rx_byte;
		u32_counter++;
		if(u32_counter >= RX_BUFF_SIZE)
		{
			u32_counter = 0;
		}

		AppUartRxInit();
	}
}
#endif

// IDLE Callback, called after RX packet receiving done:
void AppUartIdleCallBack(UART_HandleTypeDef *huart)
{
	if(huart == &huart1)
	{
        if(__HAL_UART_GET_FLAG(huart,UART_FLAG_IDLE)!=RESET)
    	{
			__HAL_UART_CLEAR_IDLEFLAG(huart);
			AppUartPacketRxProcess();

			__HAL_UART_DISABLE_IT(huart, UART_IT_IDLE);
			__HAL_UART_ENABLE_IT(huart, UART_IT_IDLE);
			//__HAL_UART_ENABLE_IT(huart, UART_IT_IDLE);
    	}
	}
}


// Validating and parsing of RX packet
void AppUartPacketRxProcess()
{
	uint16_t u16_length;
	HAL_StatusTypeDef hal_return = HAL_OK;

	hal_return = api_get_packet(u8_rx_buff, u32_counter, u8_rx_packet, &u16_length);

	if(hal_return == HAL_OK)
	{
		api_packet_parsing(u8_rx_packet);
	}
	u32_counter = 0;
}


//Packet parsing:
HAL_StatusTypeDef api_packet_parsing(uint8_t u8_data_out[])
{
	HAL_StatusTypeDef hal_return = HAL_OK;
	uint16_t u16_packet_command = 0;
	uint16_t u16_packet_parameter = 0;

	uint32_t u32_scan_step = 0;
	uint32_t u32_depth_points = 0;
	uint32_t u32_average_points = 0;
	uint32_t u32_static_delay = 0;
	uint32_t u32_tx_pulse_width = 0;
	uint32_t u32_tx_pulse_period = 0;

	//Header check:
	if(u8_data_out[0]== HEADER_VALUE)
	{
		u16_packet_command = u8_data_out[3] + (u8_data_out[4] << 8);
		u16_packet_parameter = u8_data_out[5] + (u8_data_out[6] << 8);

		switch(u16_packet_command)
		{
			case DATA_FLASHED_DATA_REQUEST:
				u16_packet_parameter = 0;
				api_send_data(DATA_FLASHED_DATA_ANSWER, u16_packet_parameter);
				break;
			case DATA_FIRE_TEST_REQUEST:
				//TODO:
				// Create request for FIRE TEST FUNCTION
				u16_packet_parameter = 0;
				api_send_data(DATA_FIRE_TEST_ANSWER, u16_packet_parameter);
				break;
			case GEORADAR_LOAD_PARAMETERS_REQUEST_ID:
				//api_send_data(GEORADAR_LOAD_PARAMETERS_RESPONCE_ID , u16_packet_parameter);
				AppUartSetResponseID(GEORADAR_LOAD_PARAMETERS_RESPONCE_ID);
				break;

			case GEORADAR_SAVE_PARAMETERS_REQUEST_ID:

				u32_scan_step = (uint32_t)u8_data_out[7] + ((uint32_t)u8_data_out[8] << 8) + ((uint32_t)u8_data_out[9] << 16) + ((uint32_t)u8_data_out[10] << 24);
				u32_depth_points = 8 + (uint32_t)u8_data_out[11] + ((uint32_t)u8_data_out[12] << 8) + ((uint32_t)u8_data_out[13] << 16) + ((uint32_t)u8_data_out[14] << 24);
				u32_average_points = (uint32_t)u8_data_out[15] + ((uint32_t)u8_data_out[16] << 8) + ((uint32_t)u8_data_out[17] << 16) + ((uint32_t)u8_data_out[18] << 24);
				u32_static_delay = (uint32_t)u8_data_out[19] + ((uint32_t)u8_data_out[20] << 8) + ((uint32_t)u8_data_out[21] << 16) + ((uint32_t)u8_data_out[22] << 24);
				u32_tx_pulse_width = (uint32_t)u8_data_out[23] + ((uint32_t)u8_data_out[24] << 8) + ((uint32_t)u8_data_out[25] << 16) + ((uint32_t)u8_data_out[26] << 24);
				u32_tx_pulse_period = (uint32_t)u8_data_out[27] + ((uint32_t)u8_data_out[28] << 8) + ((uint32_t)u8_data_out[29] << 16) + ((uint32_t)u8_data_out[30] << 24);

				board_set_uint32_state(STEP_DELAY_PARAM, u32_scan_step);
				board_set_uint32_state(MAXIMUM_DIP_POINTS_PARAM, u32_depth_points);
				board_set_uint32_state(STATIC_DELAY_PARAM, u32_static_delay);
				board_set_uint32_state(TX_PULSE_WIDTH_PARAM, u32_tx_pulse_width);
				board_set_uint32_state(SCAN_TIMER_PERIOD_PARAM, u32_tx_pulse_period);
				board_set_uint32_state(MAXIMUM_SAMPLES_PER_POINT_PARAM, u32_average_points);

				//api_send_data(GEORADAR_SAVE_PARAMETERS_RESPONCE_ID , u16_packet_parameter);
				AppUartSetResponseID(GEORADAR_SAVE_PARAMETERS_RESPONCE_ID);

				break;

			case GEORADAR_START_REQUEST_ID:
				board_set_uint32_state(START_ON_FLAG_PARAM, 1);
				//api_send_data(GEORADAR_START_RESPONCE_ID , u16_packet_parameter);
				break;

			case GEORADAR_STOP_REQUEST_ID:
				board_set_uint32_state(START_ON_FLAG_PARAM, 0);
				//api_send_data(GEORADAR_STOP_RESPONCE_ID, u16_packet_parameter);
				break;


			default:
				hal_return = HAL_ERROR;
			    break;
		}
	}
	else
	{
		hal_return = HAL_ERROR;
	}

	return(hal_return);
}




HAL_StatusTypeDef api_packet_response(uint16_t u16_response_id)
{
	HAL_StatusTypeDef hal_return = HAL_OK;
	uint16_t u16_packet_parameter = 0;

	switch(u16_response_id)
	{
		case GEORADAR_LOAD_PARAMETERS_RESPONCE_ID:

			api_send_data(GEORADAR_LOAD_PARAMETERS_RESPONCE_ID , u16_packet_parameter);
			break;

		case GEORADAR_SAVE_PARAMETERS_RESPONCE_ID:
			api_send_data(GEORADAR_SAVE_PARAMETERS_RESPONCE_ID , u16_packet_parameter);
			HAL_Delay(500);
			break;

		case GEORADAR_START_REQUEST_ID:
			api_send_data(GEORADAR_START_RESPONCE_ID , u16_packet_parameter);
			break;

		case GEORADAR_STOP_REQUEST_ID:
			api_send_data(GEORADAR_STOP_RESPONCE_ID, u16_packet_parameter);
			break;


		default:
			hal_return = HAL_ERROR;
		    break;

	}
	return(hal_return);
}








/* Packet validation. Should be called from the Rx_IDLE_End interrupt: */
/* uint8_t *pu8_data_in - input data buffer. */
/* uint8_t *pu8_data_out return data buffer.  */
/* uint16_t * pu16_size Return size of RX data. */

HAL_StatusTypeDef api_get_packet(uint8_t *pu8_data_in, uint16_t u16_size_in, uint8_t *pu8_data_out, uint16_t * pu16_size_out)
{
	ePacketState pars_state = LOOKING_FOR_HEADER;
	uint8_t u8_in_byte = 0;
	uint16_t u16_counter = 0;
	uint16_t u16_data_length = 0;
	uint16_t u16_packet_crc = 0;
	uint16_t u16_local_packet_crc = 0;


	uint16_t u16_index = 0;
	HAL_StatusTypeDef hal_return = HAL_OK;


	u16_index = 0;
	while(hal_return == HAL_OK)
	{
		if(u16_index >= u16_size_in)
		{
			hal_return = HAL_ERROR;
			*pu16_size_out = 0;
		}
		/* Read byte from input buffer: */
		u8_in_byte = pu8_data_in[u16_index];
		u16_index++;

		if(hal_return == HAL_OK)
		{
			switch(pars_state)
			{
				case LOOKING_FOR_HEADER:
					if(u8_in_byte == HEADER_VALUE)
					{
						pars_state = GET_PACKET_LENGTH_LSB;
						pu8_data_out[0] = u8_in_byte;
					}
					break;

				case GET_PACKET_LENGTH_LSB:
					u16_data_length = u8_in_byte;
					pu8_data_out[1] = u8_in_byte;
					pars_state = GET_PACKET_LENGTH_MSB;

					break;
				case GET_PACKET_LENGTH_MSB:
					u16_data_length = u16_data_length + (u8_in_byte << 8);
					if(u16_data_length > MAXIMUM_RX_DATA_LENGTH)
					{
						pars_state = WRONG_PACKET;
					}
					else
					{
						pu8_data_out[2] = u8_in_byte;
						pars_state = GET_DATA;
						u16_counter = 0;
					}
					break;

				case GET_DATA:
					pu8_data_out[u16_counter + 3] = u8_in_byte;
					u16_counter++;
					if(u16_counter >= (u16_data_length) )
					{
						pars_state = GET_CRC_LSB;
					}
					break;

				case GET_CRC_LSB:
					u16_packet_crc  = u8_in_byte;
					pars_state = GET_CRC_MSB;

					break;
				case GET_CRC_MSB:
					u16_packet_crc  = u16_packet_crc + (u8_in_byte << 8);
					pars_state = GET_CRC_MSB;
					api_u16_crc(pu8_data_out, u16_data_length + 3, &u16_local_packet_crc);
					if(u16_local_packet_crc == u16_packet_crc)
					{
						*pu16_size_out = u16_data_length + 3;
						pars_state = CRC_OK;
					}
					else
					{
						*pu16_size_out = 0;
						pars_state = WRONG_PACKET;
					}
					break;
				case CRC_OK:


					break;

				case WRONG_PACKET:
					*pu16_size_out = 0;

					break;

				default:
					pars_state = LOOKING_FOR_HEADER;
					break;

			}/* end of switch*/

			if(pars_state == CRC_OK)
			{
				break;
			}

		}/* if(hal_return == HAL_OK)*/
		else
		{
			/* Here can be implemented check for HAL_BUSY. */
		}

	}/* end of while(hal_return == HAL_OK)*/

	return(hal_return);
}


#if 1

#define HEAD_LENGTH      		1
#define LENGTH_FIELD_LENGTH      2
#define COMMAND_LENGTH  		4    // command is uint32 or 2x uint16(u16 command, u16 parameter) -
                             	 	 // if request 0x43 , answer is request + 1 = 0x44

#define DATA_LENGTH 	6008 // 1000 * 3 * 2 bytes of Data array + 8 bytes of data pointer.
#define CRC_LENGTH 		2

uint8_t tx_buffer[HEAD_LENGTH + LENGTH_FIELD_LENGTH + COMMAND_LENGTH + DATA_LENGTH + CRC_LENGTH];


// Send Acc XYZ stored data:
// This function create a packet structure in tx_buffer[] memory buffer and send it.
HAL_StatusTypeDef api_send_data(uint16_t u16_command, uint16_t u16_parameter)
{
	HAL_StatusTypeDef hal_return = HAL_OK;
	uint16_t u16_data_length = 0;
	uint32_t u32_copy_counter = 0;
	uint16_t u16_local_crc = 0;

    // Build header for data packet:
	tx_buffer[0] = HEADER_VALUE;

	tx_buffer[3] = u16_command & 0xFF;
	tx_buffer[4] = (u16_command >> 8) & 0xFF;;
	tx_buffer[5] = u16_parameter & 0xFF;
	tx_buffer[6] = (u16_parameter >> 8) & 0xFF;;

	u32_copy_counter = 0;
	switch(u16_command)
	{
		case DATA_FLASHED_DATA_ANSWER:
		    // Copy flash data to Tx-packet buffer:
			while(u32_copy_counter < DATA_LENGTH)
			{
				//tx_buffer[HEAD_LENGTH + LENGTH_FIELD_LENGTH + COMMAND_LENGTH + u32_copy_counter] = ((uint8_t*)(FLASH_USER_START_ADDR))[u32_copy_counter];
				u32_copy_counter++;
			}
			u16_data_length = COMMAND_LENGTH + u32_copy_counter;
			break;

		case DATA_FIRE_TEST_ANSWER:
			u16_data_length = COMMAND_LENGTH;
			break;

		case GEORADAR_LOAD_PARAMETERS_RESPONCE_ID:
			u16_data_length = COMMAND_LENGTH;
			break;

		case GEORADAR_SAVE_PARAMETERS_RESPONCE_ID:
			u16_data_length = COMMAND_LENGTH;
			break;

		case GEORADAR_START_RESPONCE_ID:
			u16_data_length = COMMAND_LENGTH;
			break;

		case GEORADAR_STOP_RESPONCE_ID:
			u16_data_length = COMMAND_LENGTH;
			break;

		default:
			break;
	}

	// Add data length to packet:
	tx_buffer[1] = u16_data_length & 0xFF;
	tx_buffer[2] = (u16_data_length >> 8) & 0xFF;

	// Add CRC to flash data packet
	api_u16_crc(tx_buffer, HEAD_LENGTH + LENGTH_FIELD_LENGTH + COMMAND_LENGTH + u32_copy_counter, &u16_local_crc);
	tx_buffer[HEAD_LENGTH + LENGTH_FIELD_LENGTH + COMMAND_LENGTH + u32_copy_counter + 0] = u16_local_crc & 0xFF;
    tx_buffer[HEAD_LENGTH + LENGTH_FIELD_LENGTH + COMMAND_LENGTH + u32_copy_counter + 1] = (u16_local_crc >> 8) & 0xFF;

    // Send flash data packet to Frau Hauptmann:
    if( xTxItSemaphore != NULL )
    {
    	while(xSemaphoreTake( xTxItSemaphore, ( TickType_t ) 10 ))
    	{}
    	hal_return = HAL_UART_Transmit_IT(&huart1, tx_buffer, HEAD_LENGTH + LENGTH_FIELD_LENGTH + COMMAND_LENGTH + u32_copy_counter + CRC_LENGTH);
    }


    return(hal_return);
}
#endif

// CRC is simple SUM of all byte.
void api_u16_crc(uint8_t * pu8_data, uint16_t size, uint16_t *pu16_crc)
{
	uint16_t u16_crc = 0;
	uint16_t u16_counter = 0;

	while(u16_counter < size)
	{
		u16_crc = u16_crc + pu8_data[u16_counter];
		u16_counter++;
	}
	*pu16_crc = u16_crc;
}
