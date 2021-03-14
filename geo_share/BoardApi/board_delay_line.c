/*
 * board_delay_line.c
 *
 *  Created on: 14. 4. 2019
 *      Author: VNVolokitin
 */

#include "board_delay_line.h"
#include "board_config.h"
#include "cmsis_os.h"
#include "math.h"
#include "board_states.h"

#define CONFIGURATION_BITS_LENGTH 24


void __attribute__((optimize("O0"))) board_delay_line_local_clk_delay(uint32_t u32_delay_time);

/*
 * input is delay in pS, it will be aligned to 250pS:  range from 0 to  250 * 765.
 * But, minimum delay of delay-line is around 55nS
 * */
void board_delay_line_set_delay_time(uint32_t u32_delay_time)
{
	/* It is for 3x DS1023-250pS */

	uint32_t u32_delay = 0;
	uint32_t u32_configuration_word = 0;
	uint8_t u8_i = 0;

	u32_delay = u32_delay_time/DEFAULT_DELAY_TIME_STEP;
	if(u32_delay >= (256 *3))
	{
		u32_delay = (256 *3) -1;
	}
	if(u32_delay < 256)
	{
		u32_configuration_word = u32_delay;
	}
	else if((u32_delay >=256)&&(u32_delay < (256 * 2)))
	{
		u32_configuration_word = 0xFF + ((u32_delay - 0x100) << 8);
	}
	else
	{
		u32_configuration_word = 0xFFFF + ((u32_delay - 0x200) << 16);
	}


	/* Set LE to HIGH: */
	DELAY_LINE_LE_HIGH();

	/* In according with HW of delay line delay configuration word is 24 bits */
	for(u8_i = 0; u8_i < CONFIGURATION_BITS_LENGTH; u8_i++ )
	{
		if (0x00800000 & (u32_configuration_word << u8_i))
		{
			DELAY_LINE_DATA_HIGH();
		}
		else
		{
			DELAY_LINE_DATA_LOW();
		}

		DELAY_LINE_CLOCK_HIGH();
		board_delay_line_local_clk_delay(1);
		DELAY_LINE_CLOCK_LOW();
	}
	/* Set LE to LOW: */
	DELAY_LINE_LE_LOW();
}


void board_delay_line_set_delay_point(uint32_t u32_delay_point)
{
	board_delay_line_set_delay_time(250 * u32_delay_point); // Here 250 is time in pS, HW depended value
}


void __attribute__((optimize("O0"))) board_delay_line_local_clk_delay(uint32_t u32_delay_time)
{
	while(u32_delay_time)
	{
		u32_delay_time--;
	}

}


void board_get_one_step_value(uint32_t u32_step_index, StepParameters *pStepParameters)
{
	switch (u32_step_index)
	{
		case 0:
			pStepParameters->float_one_step_value_nS = STEP_0;
			break;
		case 1:
			pStepParameters->float_one_step_value_nS = STEP_1;
			break;
		case 2:
			pStepParameters->float_one_step_value_nS = STEP_2;
			break;
		case 3:
			pStepParameters->float_one_step_value_nS = STEP_3;
			break;
		case 4:
			pStepParameters->float_one_step_value_nS = STEP_4;
			break;
		case 5:
			pStepParameters->float_one_step_value_nS = STEP_5;
			break;
		case 6:
			pStepParameters->float_one_step_value_nS = STEP_6;
			break;
		case 7:
			pStepParameters->float_one_step_value_nS = STEP_7;
			break;
		case 8:
			pStepParameters->float_one_step_value_nS = STEP_8;
			break;
		case 9:
			pStepParameters->float_one_step_value_nS = STEP_9;
			break;
		case 10:
			pStepParameters->float_one_step_value_nS = STEP_10;
			break;
		case 11:
			pStepParameters->float_one_step_value_nS = STEP_11;
			break;
		case 12:
			pStepParameters->float_one_step_value_nS = STEP_12;
			break;
		case 13:
			pStepParameters->float_one_step_value_nS = STEP_13;
			break;
		case 14:
			pStepParameters->float_one_step_value_nS = STEP_14;
			break;

		default:
			pStepParameters->float_one_step_value_nS = STEP_0;
			break;
	}

}


// function set desired delay(one_step_value * point) for fixed step.
// Fixed Step is come from table:
HAL_StatusTypeDef board_calculation_of_delay_variables(uint32_t u32_step_index, uint32_t u32_delay_point, DelayParameters * pDelayParameters)
{
	HAL_StatusTypeDef hal_return = HAL_OK;
	StepParameters stepParameters;

	float float_atomic_large_delay = 1000.0/28.0; // atomic period, depend on HW timer
	float float_large_delay = float_atomic_large_delay * 5.0; // large period

	float float_desired_period = 0.0;
	float float_large_period_part = 0.0;
	float float_delay_remainder = 0.0;

	uint32_t u32_static_delay_value = 0;

	// Get static delay value, should be in pS:
	board_get_uint32_state(STATIC_DELAY_PARAM, &u32_static_delay_value);


	// 1) convert step index to float one-step-value:
	//    float_one_step_value = board_get_one_step_value(u32_step_index, &stepParameters);
	board_get_one_step_value(u32_step_index, &stepParameters);

	// 2) calculation of full delay time:
	float_desired_period = stepParameters.float_one_step_value_nS * (float)u32_delay_point +  ((float)u32_static_delay_value)/1000.0;

	// 3) calculation of how many large periods in desire-delay:
	//    It is important to keep * 5, because we looking for how many part of
	//    (float_atomic_large_delay * 5.0) in  float_desired_period
	//    Simple, dont't touch this line.
	pDelayParameters->u32_large_delay_number = (uint32_t)(float_desired_period / float_large_delay) * 5;

	// 4) calculation a delay-time builded from large-delay-periods:
	float_large_period_part = float_atomic_large_delay * (float)(pDelayParameters->u32_large_delay_number);

	// 5) get remainder:
	float_delay_remainder = float_desired_period - float_large_period_part;

	// 6) calculation of how many small periods in remainder-delay:
	pDelayParameters->u32_small_delay_number = (uint32_t)(roundf(float_delay_remainder /0.25));

    return(hal_return);
}

HAL_StatusTypeDef board_calculation_of_delay_variables_from_step_and_depth(uint32_t u32_step, uint32_t u32_delay_point, DelayParameters * pDelayParameters)
{
	HAL_StatusTypeDef hal_return = HAL_OK;
	StepParameters stepParameters;

	float float_atomic_large_delay = 1000.0/28.0; // atomic period, depend by HW timer.
	float float_large_delay = float_atomic_large_delay * 5.0; // large period

	float float_desired_period = 0.0;
	float float_large_period_part = 0.0;
	float float_delay_remainder = 0.0;

	uint32_t u32_static_delay_value = 0;

	// 0) Get static delay value, should be in pS:
	board_get_uint32_state(STATIC_DELAY_PARAM, &u32_static_delay_value);

	// 1) convert step pS to float one-step-value, nS:
	stepParameters.float_one_step_value_nS	= (float)u32_step/1000.0;

	// 2) calculation of full delay time, as measurement delay + HW static delay:
	float_desired_period = stepParameters.float_one_step_value_nS * (float)u32_delay_point + ((float)u32_static_delay_value)/1000.0;

	// 3) calculation of how many large periods in desire-delay:
	//    It is important to keep * 5, because we looking for how many part of
	//    (float_atomic_large_delay * 5.0) in  float_desired_period
	//    Simple, dont't touch this line.
	pDelayParameters->u32_large_delay_number = (uint32_t)(float_desired_period / float_large_delay) * 5;

	// 4) calculation a delay-time builded from large-delay-periods:
	float_large_period_part = float_atomic_large_delay * (float)(pDelayParameters->u32_large_delay_number);

	// 5) get remainder:
	float_delay_remainder = float_desired_period - float_large_period_part;

	// 6) calculation of how many small periods in remainder-delay:
	pDelayParameters->u32_small_delay_number = (uint32_t)(roundf(float_delay_remainder /0.25));

    return(hal_return);
}









