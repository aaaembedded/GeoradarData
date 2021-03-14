/*
 * board_tim_pwm_interrupts.c
 *
 *  Created on: 13. 4. 2019
 *      Author: VNVolokitin
 */

#include "board_tim_pwm_interrupts.h"

#include "board_delay_line.h"
#include "board_config.h"


TIM_HandleTypeDef *local_htim;

void board_tim_pwm_interrupt_init(TIM_HandleTypeDef* htim)
{
	local_htim = htim;

}

#if 1

void HAL_TIM_PWM_PulseFinishedCallback(TIM_HandleTypeDef* htim)
{
    static uint32_t pwm_value = 0;
    static uint32_t step = 1;

  //  static uint32_t u32_delay_counter = 0;

    if (htim->Instance == TIM2)
    {
        if(pwm_value == 60) step = 1;
        if(pwm_value == 2000) step = -1;
        pwm_value += step;
       // board_tim_pwm_setvalue(htim, pwm_value);

        /* Delay line test:  */
    //    board_delay_line_set_delay_time(u32_delay_counter);
    //    u32_delay_counter++;
	//	if(u32_delay_counter >= (DEFAULT_DELAY_TIME_STEP * 256 *3))
	//	{
	//		u32_delay_counter = 0;
	//	}


    }
}


void board_tim_pwm_setvalue(TIM_HandleTypeDef* htim, uint16_t value)
{
    TIM_OC_InitTypeDef sConfigOC;

    sConfigOC.OCMode = TIM_OCMODE_PWM1;
    sConfigOC.Pulse = 500;
    sConfigOC.OCPolarity = TIM_OCPOLARITY_LOW;
    sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
    htim->Init.Period = 1000 + value;
    HAL_TIM_PWM_Init(htim);
    HAL_TIM_PWM_ConfigChannel(htim, &sConfigOC, TIM_CHANNEL_1);
   // HAL_TIM_PWM_Start(htim, TIM_CHANNEL_1);
}

#endif
