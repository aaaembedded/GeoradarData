/*
 * board_uart_comm.c
 *
 *  Created on: Dec 27, 2020
 *      Author: seags
 */

#include "board_uart_comm.h"
#include "AppUartCommunication.h"
#include "board_states.h"

extern UART_HandleTypeDef huart1;
extern 	SemaphoreHandle_t xTxItSemaphore;

#define HEADER_VALUE         0x73
#define HEAD_LENGTH          0x01
#define LENGTH_FIELD_LENGTH  0x02
#define COMMAND_FIELD_LENGTH 0x04

#define HEADER_LENGTH 	    (HEAD_LENGTH) + (LENGTH_FIELD_LENGTH) + (COMMAND_FIELD_LENGTH)

#define DATA_LENGTH 	(2 * (256 + 256 + 256)) // 768 * 2 bytes of ADC data array.
#define CRC_LENGTH 		2

uint8_t tx_buffer[HEADER_LENGTH + DATA_LENGTH + CRC_LENGTH];


// Send Acc XYZ stored data:
// This function create a packet structure in tx_buffer[] memory buffer and send it.
HAL_StatusTypeDef board_uart_send_data(int16_t * pi16_buffer, int16_t ui16_command, int16_t ui16_command_parameter)
{
	HAL_StatusTypeDef hal_return = HAL_OK;

	uint16_t u16_data_load_length = 0;
	uint32_t u32_copy_counter = 0;
	uint16_t u16_local_crc = 0;

	uint32_t u32_depth_points = 0;
	uint16_t u16_data_length = 0;

    board_get_uint32_state(MAXIMUM_DIP_POINTS_PARAM, &u32_depth_points);

    u16_data_length = (uint16_t)(2 * u32_depth_points);// 2 is because uint_16 is 2 bytes

    u16_data_load_length = u16_data_length + COMMAND_FIELD_LENGTH;

	// Set packet HEAD:
	tx_buffer[0] = HEADER_VALUE;
	// Set packet data_load length:
	tx_buffer[1] = u16_data_load_length & 0xFF;
	tx_buffer[2] = (u16_data_load_length >> 8) & 0xFF;
	// Set command ( part of data load)
	tx_buffer[3] = ui16_command & 0xFF;
	tx_buffer[4] = (ui16_command >> 8) & 0xFF;
	// Set command parameter ( part of data load)
	tx_buffer[5] = ui16_command_parameter & 0xFF;
	tx_buffer[6] = (ui16_command_parameter >> 8) & 0xFF;

	u32_copy_counter = 0;
	while(u32_copy_counter < u16_data_length)
	{
		tx_buffer[HEADER_LENGTH + u32_copy_counter] = ((uint8_t*)(pi16_buffer))[u32_copy_counter];
		u32_copy_counter++;
	}

	board_uart_u16_crc(tx_buffer, HEADER_LENGTH + u16_data_length, &u16_local_crc);
	tx_buffer[HEADER_LENGTH + u16_data_length + 0] = u16_local_crc & 0xFF;
    tx_buffer[HEADER_LENGTH + u16_data_length + 1] = (u16_local_crc >> 8) & 0xFF;

    if( xTxItSemaphore != NULL )
    {
    	while(xSemaphoreTake( xTxItSemaphore, ( TickType_t ) 10 ))
    	{}
        /* We were able to obtain the semaphore and can now access the
        shared resource. */
       	hal_return = HAL_UART_Transmit_IT(&huart1, tx_buffer, HEADER_LENGTH + u16_data_length + CRC_LENGTH);
       // while(u32_tx_cpl_flag == 1)
       // {
       //     AppUartGetTxCplFlag(&u32_tx_cpl_flag);
       // }
        /* We have finished accessing the shared resource.  Release the
        semaphore. */
        //xSemaphoreGive( xTxItSemaphore );
    }


	return(hal_return);
}

// CRC is simple SUM of all byte.
void board_uart_u16_crc(uint8_t * pu8_data, uint16_t size, uint16_t *pu16_crc)
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












