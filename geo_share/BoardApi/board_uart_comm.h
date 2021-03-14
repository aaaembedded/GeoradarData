/*
 * board_uart_comm.h
 *
 *  Created on: Dec 27, 2020
 *      Author: seags
 */

#ifndef BOARD_UART_COMM_H_
#define BOARD_UART_COMM_H_

#include "stm32f4xx_hal.h"

HAL_StatusTypeDef board_uart_send_data(int16_t * pi16_buffer, int16_t ui16_command, int16_t ui16_command_parameter);
void board_uart_u16_crc(uint8_t * pu8_data, uint16_t size, uint16_t *pu16_crc);






#endif /* BOARD_UART_COMM_H_ */
