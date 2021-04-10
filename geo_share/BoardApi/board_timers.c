/*
 * board_timers.c
 *
 *  Created on: Mar 5, 2019
 *      Author: VNVolokitin
 */


#include "board_timers.h"
#include "board_states.h"

uint32_t u32_pulse_number = 3; // TODO: remove 3

extern TIM_HandleTypeDef htim2;
extern TIM_HandleTypeDef htim3;
extern void HAL_TIM_MspPostInit(TIM_HandleTypeDef *htim);

/*
 * This function used for start/restart motion timer.
 * Should be part of motion-Queue send function.
 * */
void board_timers_restart(TIM_TypeDef * pTimer, uint32_t u32_current_prescaler, uint32_t u32_current_period)
{
	TIM_HandleTypeDef htim;

	htim.Instance = pTimer;
	htim.Init.Prescaler = u32_current_prescaler;
	htim.Init.CounterMode = TIM_COUNTERMODE_UP;
	htim.Init.Period = u32_current_period;
	HAL_TIM_Base_Init(&htim);
	HAL_TIM_Base_Start_IT(&htim);
}


void board_clock_start(uint32_t u32_clock_number, uint32_t u32_ticks_delay)
{

	// Set DATA_ACQUISION_FLAG to 1, DATA_ACQUISION busy
	board_set_uint32_state(DATA_ACQUISITION_FLAG, 1);

	u32_ticks_delay = u32_ticks_delay + 4;/// 4 is hardware value to compensate start delay.
	                                      /// Now, if input value u32_ticks_delay is 0, TIM2 and TIM3
	                                      /// will start at same time.

	if(u32_clock_number > 0)
	{
		u32_pulse_number = u32_clock_number - 1;
	}
	else
	{
		u32_pulse_number = 0;
	}

    htim2.Instance->CNT = htim2.Instance->CCR1 + 1 +  u32_ticks_delay; // +1 to avoid wrong pulse from tim2
    htim3.Instance->CNT = htim3.Instance->CCR3+5;

    //Reset counter of ADC clock pulses:
    board_set_uint32_state(PULSE_NUMBER_IN_BANCH, 0);

	HAL_GPIO_WritePin(GPIOF, GPIO_PIN_6, GPIO_PIN_SET);

    HAL_TIM_PWM_Start_IT(&htim3, TIM_CHANNEL_3);
    HAL_TIM_PWM_Start_IT(&htim2, TIM_CHANNEL_1);
}


void board_tx_pulse_width_set(uint32_t u32_tx_pulse_width, uint32_t u32_scan_period )
{
	  /* USER CODE BEGIN TIM2_Init 0 */
	  TIM_MasterConfigTypeDef sMasterConfig = {0};
	  TIM_OC_InitTypeDef sConfigOC = {0};
	  htim2.Instance = TIM2;
	  htim2.Init.Prescaler = 2;
	  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
	  htim2.Init.Period = u32_scan_period; //0x7fff;//2400*3;
	  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
	  if (HAL_TIM_PWM_Init(&htim2) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  sMasterConfig.MasterOutputTrigger = TIM_TRGO_ENABLE;
	  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_ENABLE;
	  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  sConfigOC.OCMode = TIM_OCMODE_PWM1;
	  sConfigOC.Pulse = u32_tx_pulse_width;
	  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	  if (HAL_TIM_PWM_ConfigChannel(&htim2, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  /* USER CODE BEGIN TIM2_Init 2 */

	  /* USER CODE END TIM2_Init 2 */
	  HAL_TIM_MspPostInit(&htim2);



	  /* USER CODE BEGIN TIM3_Init 0 */
	  TIM_SlaveConfigTypeDef sSlaveConfig = {0};
	  htim3.Instance = TIM3;
	  htim3.Init.Prescaler = 2;
	  htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
	  htim3.Init.Period = u32_scan_period; //0x7fff;//2400*3;
	  htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	  htim3.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
	  if (HAL_TIM_Base_Init(&htim3) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  if (HAL_TIM_PWM_Init(&htim3) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  sSlaveConfig.SlaveMode = TIM_SLAVEMODE_GATED;
	  sSlaveConfig.InputTrigger = TIM_TS_ITR1;
	  if (HAL_TIM_SlaveConfigSynchro(&htim3, &sSlaveConfig) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  sMasterConfig.MasterOutputTrigger = TIM_TRGO_ENABLE;
	  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
	  if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  sConfigOC.OCMode = TIM_OCMODE_PWM1;
	  sConfigOC.Pulse = u32_tx_pulse_width;
	  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	  if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_3) != HAL_OK)
	  {
	    Error_Handler();
	  }
	  HAL_TIM_MspPostInit(&htim3);
}



