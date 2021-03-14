/*
 * board_data_processing.c
 *
 *  Created on: 14. 4. 2019
 *      Author: VNVolokitin
 */



#include "board_data_processing.h"
#include "board_ext_adc.h"
#include "board_states.h"
#include "board_config.h"
#include "board_delay_line.h"
#include "GUI_App.h"
#include "GUI.h"
#include <stdio.h>
#include "board_display.h"
#include "board_configuration.h"
#include "board_timers.h"

__attribute__((section(".ccmram.i32_adc_full_value"))) int32_t i32_adc_full_value;
__attribute__((section(".ccmram.i32_adc_average_value"))) int16_t i16_adc_average_value[ADC_ARRAY];

__attribute__((section(".ccmram.i32_adc_average_value"))) int16_t i16_aY_value[REDUCE_VALUE];
__attribute__((section(".ccmram.i32_adc_average_value"))) int16_t i16_aY_old_value[REDUCE_VALUE];


void board_data_processing_init(void)
{
	// TODO: Clean buffers.
	//
	//
	uint32_t u32_counter = 0;

	i32_adc_full_value = 0;
	for(u32_counter = 0; u32_counter < ADC_ARRAY ; u32_counter ++)
	{
		i16_adc_average_value[u32_counter] = 0;
	}
}

// Function to start data acquisition:
// Input parameter is max dip point in points (each point is 0.25nS)
void board_data_start_acquisition(uint32_t u32_max_dip_point)
{
	uint32_t u32_tx_pulse_width = 0;
	uint32_t u32_scan_timer_period = 0;
	// 1) Set max dip point in points (each point is 0.25nS)
	if(u32_max_dip_point > MAXIMUM_DIP_POINTS)
	{
		u32_max_dip_point = MAXIMUM_DIP_POINTS;
		board_set_uint32_state(MAXIMUM_DIP_POINTS_PARAM, u32_max_dip_point);
	}
	board_set_uint32_state(MAXIMUM_DIP_POINTS_PARAM, u32_max_dip_point);
	// 2) Reset current point number
	board_set_uint32_state(CURRENT_DIP_POINT_PARAM, 0);

	// 2.5) Set tx_pulse width and scan-timer period:
	board_get_uint32_state(TX_PULSE_WIDTH_PARAM, &u32_tx_pulse_width);
	board_get_uint32_state(SCAN_TIMER_PERIOD_PARAM, &u32_scan_timer_period);
	board_tx_pulse_width_set(u32_tx_pulse_width, u32_scan_timer_period);

	// 3) Start "one column of data" acquisition
	board_data_acquisition();
}


void board_data_acquisition()
{
	DelayParameters delayParameters;

	uint32_t u32_fixed_delay_value = 0;
	uint32_t u32_current_dip_point_number = 0;
	uint32_t u32_samples_per_point = 0;
	uint32_t u32_maximum_dip_point = 0;
	uint32_t u32_current_dip_step_index = 0;
	uint32_t u32_data_acquision_flag = 0;
	uint32_t u32_step_value = 0;


	// 1) Get all start parameters:
	// Get value of fixed delay:
    board_get_uint32_state(FIXED_DELAY_PARAM, &u32_fixed_delay_value);
    // Get start dip point number
    board_get_uint32_state(CURRENT_DIP_POINT_PARAM, &u32_current_dip_point_number);
    // Get maximum dip points
    board_get_uint32_state(MAXIMUM_DIP_POINTS_PARAM, &u32_maximum_dip_point);
    // Get samples_per_point value:
    board_get_uint32_state(MAXIMUM_SAMPLES_PER_POINT_PARAM, &u32_samples_per_point);


    // 2) Start acquisition cycle:
    while(u32_current_dip_point_number < u32_maximum_dip_point)
    {
		// Get value of current dip point number:
		board_get_uint32_state(CURRENT_DIP_POINT_PARAM, &u32_current_dip_point_number);
//
//		board_get_uint32_state(CURRENT_DEEP_STEP_INDEX_PARAM, &u32_step_value);
//		board_calculation_of_delay_variables(u32_current_dip_step_index, u32_current_dip_point_number, &delayParameters);
		board_get_uint32_state(STEP_DELAY_PARAM, &u32_step_value);
		board_calculation_of_delay_variables_from_step_and_depth(u32_step_value, u32_current_dip_point_number, &delayParameters);

		board_delay_line_set_delay_point(delayParameters.u32_small_delay_number);

//		board_get_uint32_state(TX_PULSE_WIDTH_PARAM, &u32_tx_pulse_width);
//		board_tx_pulse_width_set(u32_tx_pulse_width);

		board_clock_start(u32_samples_per_point, u32_fixed_delay_value + delayParameters.u32_large_delay_number);

		// Wait for acquisition ready:
		while(1)
		{
			board_get_uint32_state(DATA_ACQUISITION_FLAG, &u32_data_acquision_flag);
			if(u32_data_acquision_flag == 0)
			{
				break;
			}
		}
		//HAL_GPIO_WritePin(GPIOF, GPIO_PIN_6, GPIO_PIN_RESET);
        // TODO: May be add delay???
		//HAL_Delay(100);
	    // Go to the next point:
		u32_current_dip_point_number++;

		// Set number of the next point:
		board_set_uint32_state(CURRENT_DIP_POINT_PARAM, u32_current_dip_point_number);
    }
}











