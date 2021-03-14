/*
 * board_timers.h
 *
 *  Created on: Mar 5, 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_TIMERS_H_
#define BOARD_TIMERS_H_


#include "stm32f4xx_hal.h"

extern void Error_Handler(void);

void board_timers_restart(TIM_TypeDef * pTimer, uint32_t u32_current_prescaler, uint32_t u32_current_period);
void board_clock_start(uint32_t u32_clock_number, uint32_t u32_ticks_delay);
void board_tx_pulse_width_set(uint32_t u32_tx_pulse_width, uint32_t u32_scan_period );




#endif /* BOARD_TIMERS_H_ */
