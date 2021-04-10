/*
 *  board_configuration.c
 *
 *  Created on: 14. 4. 2019
 *      Author: VNVolokitin
 */


#include "board_configuration.h"


/* Function should be called during initialization process. */
void board_configuration_init()
{
	/* Clear configuration buffers: */
	board_states_init();

	/* Load configuration: */
	board_set_uint32_state(MAXIMUM_DELAY_TIME_PARAM, DEFAULT_MAXIMUM_DELAY_TIME);
	board_set_uint32_state(DELAY_TIME_STEP_PARAM, DEFAULT_DELAY_TIME_STEP);

	board_set_uint32_state(MAXIMUM_SAMPLES_PER_POINT_PARAM, MAXIMUM_SAMPLES);
	board_set_uint32_state(MAXIMUM_DIP_POINTS_PARAM, MAXIMUM_DIP_POINTS);
	board_set_uint32_state(FIXED_DELAY_PARAM, FIXED_DELAY);

	board_set_uint32_state(CURRENT_DEEP_STEP_INDEX_PARAM, DEFAULT_STEP_VALUE_INDEX);

	board_set_uint32_state(START_ON_FLAG_PARAM, 1);
	board_set_uint32_state(STEP_DELAY_PARAM, 250);
	board_set_uint32_state(TX_PULSE_WIDTH_PARAM, 15);// it is around 15 * 36nS
	board_set_uint32_state(SCAN_TIMER_PERIOD_PARAM, SCAN_TIMER_DEFAULT_PERIOD);// 0x7FFF * 36 it is 1.2mS
}
