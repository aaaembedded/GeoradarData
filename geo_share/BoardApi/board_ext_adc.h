/*
 * board_ext_adc.h
 *
 *  Created on: Mar 4, 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_EXT_ADC_H_
#define BOARD_EXT_ADC_H_


#include "cmsis_os.h"
#include "stm32f4xx_hal.h"
#include "board_config.h"



#define EXT_ADC_BUFFER 200
#define EXT_ADC_BUFFER 200
#define EXT_ADC_RAW_AVERAGE_BUFFER 		(MAXIMUM_DIP_POINTS + 8)

/* Define GPIO for external ADC here: */
#define READ_D0() 	HAL_GPIO_ReadPin(GPIOB, GPIO_PIN_7)
#define READ_D1() 	HAL_GPIO_ReadPin(GPIOB, GPIO_PIN_3)
#define READ_D2() 	HAL_GPIO_ReadPin(GPIOE, GPIO_PIN_3)
#define READ_D3() 	HAL_GPIO_ReadPin(GPIOG, GPIO_PIN_9)
#define READ_D4() 	HAL_GPIO_ReadPin(GPIOE, GPIO_PIN_2)
#define READ_D5() 	HAL_GPIO_ReadPin(GPIOD, GPIO_PIN_7)
#define READ_D6() 	HAL_GPIO_ReadPin(GPIOE, GPIO_PIN_5)
#define READ_D7() 	HAL_GPIO_ReadPin(GPIOD, GPIO_PIN_5)
#define READ_D8() 	HAL_GPIO_ReadPin(GPIOE, GPIO_PIN_4)
#define READ_D9() 	HAL_GPIO_ReadPin(GPIOD, GPIO_PIN_4)
#define READ_D10() 	HAL_GPIO_ReadPin(GPIOE, GPIO_PIN_6)
#define READ_D11() 	HAL_GPIO_ReadPin(GPIOD, GPIO_PIN_2)
#define READ_OTR() 	HAL_GPIO_ReadPin(GPIOB, GPIO_PIN_4)

//#define ADC_CLK_PORT	GPIOG
//#define ADC_CLK_PIN	GPIO_PIN_9
// out : 
// PC8, PC11, PC12, PC13

/* Generating ADC pulse : */
//#define CLOCK_LOW()      ADC_CLK_PORT->BSRR = ((uint32_t)ADC_CLK_PIN << 16U)
//#define CLOCK_HIGH()     ADC_CLK_PORT->BSRR = ADC_CLK_PIN


/* Graph buffers, primary and secondary: */
extern int16_t i16_ext_adc_first_buffer[EXT_ADC_BUFFER];
extern int16_t i16_ext_adc_second_buffer[EXT_ADC_BUFFER];
extern int16_t i16_ext_adc_raw_average_buffer[EXT_ADC_RAW_AVERAGE_BUFFER];



void board_init_external_adc();

void board_ext_adc_read_to_raw_average_buffer(int16_t * pi16_adc_value, uint32_t u32_index);
void board_ext_adc_write_to_raw_average_buffer(int16_t i16_adc_value, uint32_t u32_index);
void board_ext_adc_clear_raw_average_buffer();

void board_ext_adc_clear_buffer();
void board_ext_adc_read_adc_value();
void board_ext_adc_clock();
void board_ext_adc_print_bits();



void board_ext_adc_get_adc_value(int16_t * pi16_value);
void board_ext_adc_print_adc_value(int16_t i16_value);
void board_ext_adc_one_time_read_sequence(int16_t * pi16_value);


#endif /* BOARD_EXT_ADC_H_ */
