/*
 * board_tim_pwm_interrupts.h
 *
 *  Created on: 13. 4. 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_TIM_PWM_INTERRUPTS_H_
#define BOARD_TIM_PWM_INTERRUPTS_H_

#include "stm32f4xx_hal.h"

void board_tim_pwm_interrupt_init(TIM_HandleTypeDef* htim);
void board_tim_pwm_setvalue(TIM_HandleTypeDef* htim, uint16_t value);







#endif /* BOARD_TIM_PWM_INTERRUPTS_H_ */
