/*
 * AppUartCommunication.h
 *
 *  Created on: Dec 18, 2020
 *      Author: vadvol01
 */

#ifndef APPUARTCOMMUNICATION_H_
#define APPUARTCOMMUNICATION_H_

#include "stm32f4xx_hal.h"

#define RX_BUFF_SIZE  6011 //100
#define MAXIMUM_TX_DATA_LENGTH		CONVERSION_COUNT * 2 //4096
#define MAXIMUM_RX_DATA_LENGTH 		(RX_BUFF_SIZE - 5)
#define HEADER_VALUE 				0x73
#define COMMUNICATION_LINE 			USART1

#define DATA_FLASHED_DATA_REQUEST	0x43
#define DATA_FLASHED_DATA_ANSWER	0x44
#define DATA_FIRE_TEST_REQUEST		0x45
#define DATA_FIRE_TEST_ANSWER		0x46

// ID of Georadar data packet:
#define GEORADAR_DATA_PACKET_ID               0x0048

#define GEORADAR_LOAD_PARAMETERS_REQUEST_ID   0x0049
#define GEORADAR_LOAD_PARAMETERS_RESPONCE_ID  0x004A

#define GEORADAR_SAVE_PARAMETERS_REQUEST_ID   0x004B
#define GEORADAR_SAVE_PARAMETERS_RESPONCE_ID  0x004C

#define GEORADAR_START_REQUEST_ID             0x004D
#define GEORADAR_START_RESPONCE_ID            0x004E

#define GEORADAR_STOP_REQUEST_ID              0x004F
#define GEORADAR_STOP_RESPONCE_ID             0x0050


typedef enum
{
	LOOKING_FOR_HEADER,
	GET_PACKET_LENGTH_LSB,
	GET_PACKET_LENGTH_MSB,
	GET_DATA,
	GET_CRC_LSB,
	GET_CRC_MSB,
	CRC_OK,
	WRONG_PACKET

} ePacketState;


extern uint8_t u8_rx_buff[];
extern uint32_t u32_counter;

void AppUartIdleCallBack(UART_HandleTypeDef *huart);
void AppUartRxInit(void);
void AppUartPacketRxProcess(void);

void AppUartSetResponseID(uint16_t u16_response_id);
void AppUartGetResponseID(uint16_t *pu16_response_id);
HAL_StatusTypeDef api_packet_response(uint16_t u16_response_id);


HAL_StatusTypeDef api_packet_parsing(uint8_t u8_data_out[]);
HAL_StatusTypeDef api_send_data(uint16_t u16_command, uint16_t u16_parameter);
             void api_u16_crc(uint8_t * pu8_data, uint16_t size, uint16_t *pu16_crc);
HAL_StatusTypeDef api_get_packet(uint8_t *pu8_data_in, uint16_t u16_size_in, uint8_t *pu8_data_out, uint16_t * pu16_size_out);


#endif /* APPUARTCOMMUNICATION_H_ */
