/*
 * board_data_processing.h
 *
 *  Created on: 14. 4. 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_DATA_PROCESSING_H_
#define BOARD_DATA_PROCESSING_H_

#include "stm32f4xx_hal.h"

#define ADC_ARRAY 8000
#define REDUCE_VALUE 200


extern int16_t i16_aY_value[REDUCE_VALUE];

void board_data_processing_init(void);


void board_data_start_acquisition(uint32_t u32_max_dip_point);
void board_data_acquisition();














#endif /* BOARD_DATA_PROCESSING_H_ */
