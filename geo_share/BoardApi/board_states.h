/*
 * board_states.h
 *
 *  Created on: Feb 12, 2019
 *      Author: VNVolokitin
 */

#ifndef __BOARD_STATES_H_
#define __BOARD_STATES_H_

#include "cmsis_os.h"
#include "stm32f4xx_hal.h"


/* INT32_T: board values:*/
#define MAX_BOARD_INT32_STATE 			3

#define INT32_STATE_0		 			0
#define INT32_STATE_1		 			1
#define ADC_AVERAGE		 			    2



/* UINT32_T: board values: */
#define MAX_BOARD_UINT32_STATE 			24

#define UINT32_STATE_0		 			0
#define UINT32_STATE_1		 			1
#define CURRENT_DEEP_POINT		 		2 /* Then we increase delay time, we increase deep length.  */
#define CURRENT_DEEP_STEP_INDEX_PARAM	3 /* Delay time index step, see in TABLE in board_delay_line.h .  */
#define MAXIMUM_DELAY_TIME_PARAM		4 /* Get it from define during initialization. */
#define DELAY_TIME_STEP_PARAM			5 /* Get it from define during initialization. */
#define MAXIMUM_SAMPLES_PER_POINT_PARAM	6 /* Get it from define during initialization. */
#define CURRENT_SAMPLE_PARAM			7
#define CURRENT_STEP_PARAM				8
#define RAW_ADC_VALUE    				9

#define BTN_0_VALUE     				10
#define BTN_1_VALUE     				11
#define BTN_2_VALUE     				12
#define BTN_3_VALUE     				13

#define PULSE_NUMBER_IN_BANCH           14
#define MAXIMUM_DIP_POINTS_PARAM        15
#define CURRENT_DIP_POINT_PARAM         16
#define FIXED_DELAY_PARAM               17
#define DATA_ACQUISITION_FLAG           18
#define START_ON_FLAG_PARAM             19
#define STATIC_DELAY_PARAM              20
#define STEP_DELAY_PARAM                21
#define TX_PULSE_WIDTH_PARAM            22
#define SCAN_TIMER_PERIOD_PARAM         23



/* DOUBLE: board values: */
#define MAX_BOARD_DOUBLE_STATE 			2

#define DOUBLE_STATE_0		 			0
#define DOUBLE_STATE_1		 			1


/* TASK HANDLERS: board values: */
#define MAX_BOARD_TASK_HANDLERS_STATE   4

#define GUI_TASK     		 			0
#define TS_TASK     		 			1
#define SERVICE_TASK     		 		2
#define ADC_TASK    		 			3



void board_states_init();
void board_get_int32_state(uint32_t state_number, int32_t * pi32_value);
void board_set_int32_state(uint32_t state_number, int32_t i32_value);

void board_get_uint32_state(uint32_t state_number, uint32_t * pu32_value);
void board_set_uint32_state(uint32_t state_number, uint32_t u32_value);

void board_get_double_state(uint32_t state_number, double * p_double_value);
void board_set_double_state(uint32_t state_number, double double_value);

void board_get_task_handler(uint32_t task_number, osThreadId * p_task_handler);
void board_set_task_handler(uint32_t task_number, osThreadId task_handler);




#endif /* __BOARD_STATES_H_ */
