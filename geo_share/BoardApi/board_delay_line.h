/*
 * board_delay_line.h
 *
 *  Created on: 14. 4. 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_DELAY_LINE_H_
#define BOARD_DELAY_LINE_H_

#include "stm32f4xx_hal.h"


typedef struct
{
	float float_one_step_value_nS;
	uint32_t u32_atomic_delay_number;
	uint32_t u32_large_delay_step_number;
} StepParameters;



typedef struct
{
	uint32_t u32_large_delay_number;
	uint32_t u32_small_delay_number;

} DelayParameters;






#define DELAY_LINE_LE_PORT		GPIOC
#define DELAY_LINE_LE_PIN		GPIO_PIN_11
#define DELAY_LINE_CLK_PORT		GPIOC
#define DELAY_LINE_CLK_PIN		GPIO_PIN_12
#define DELAY_LINE_DATA_PORT	GPIOC
#define DELAY_LINE_DATA_PIN		GPIO_PIN_13



/* Generating DELAY LINE LATCH ENABLED : */
#define DELAY_LINE_LE_LOW()      	DELAY_LINE_LE_PORT->BSRR = ((uint32_t)DELAY_LINE_LE_PIN << 16U)
#define DELAY_LINE_LE_HIGH()     	DELAY_LINE_LE_PORT->BSRR = DELAY_LINE_LE_PIN

/* Generating DELAY LINE CLOCK pulse : */
#define DELAY_LINE_CLOCK_LOW()      DELAY_LINE_CLK_PORT->BSRR = ((uint32_t)DELAY_LINE_CLK_PIN << 16U)
#define DELAY_LINE_CLOCK_HIGH()     DELAY_LINE_CLK_PORT->BSRR = DELAY_LINE_CLK_PIN

/* Generating DELAY LINE DATA : */
#define DELAY_LINE_DATA_LOW()      	DELAY_LINE_DATA_PORT->BSRR = ((uint32_t)DELAY_LINE_DATA_PIN << 16U)
#define DELAY_LINE_DATA_HIGH()     	DELAY_LINE_DATA_PORT->BSRR = DELAY_LINE_DATA_PIN


void board_delay_line_set_delay_time(uint32_t u32_delay_time);
void board_delay_line_set_delay_point(uint32_t u32_delay_point);


#if 0
Large period is 1000/28 (nS) = 35.7...

STEP (nS):
0.25    1     714
0.5     2     357
1.5     6     119
1.75    7     102
3.5     14    51
4.25    17    42
8.5     34    21
10.5    42    17
#endif

#define STEP_0	0.25
#define STEP_1	0.5
#define STEP_2	1.0
#define STEP_3	1.5
#define STEP_4	2.0
#define STEP_5	2.5
#define STEP_6	3.0
#define STEP_7	3.5
#define STEP_8	4.5
#define STEP_9	5.0
#define STEP_10	6.0
#define STEP_11	7.0
#define STEP_12	8.0
#define STEP_13	9.0
#define STEP_14	150.0

void board_get_one_step_value(uint32_t u32_step_index, StepParameters *pStepParameters);
HAL_StatusTypeDef board_calculation_of_delay_variables(uint32_t u32_step_index, uint32_t u32_delay_point, DelayParameters * pDelayParameters);
HAL_StatusTypeDef board_calculation_of_delay_variables_from_step_and_depth(uint32_t u32_step, uint32_t u32_delay_point, DelayParameters * pDelayParameters);


#endif /* BOARD_DELAY_LINE_H_ */
