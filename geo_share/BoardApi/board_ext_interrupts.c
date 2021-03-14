/*
 * board_ext_interrupts.c
 *
 *  Created on: 13. 4. 2019
 *      Author: VNVolokitin
 */

#include "board_ext_interrupts.h"
#include "board_data_processing.h"
#include "board_ext_adc.h"
#include "stm32f429i_discovery_ts.h"
#include "board_states.h"

 void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin)
{
//	static  uint32_t u32_counter = 0;
	 uint32_t u32_max_pulse_number = 0;
	 uint32_t u32_current_pulse_number = 0;
	 uint32_t u32_current_raw_adc_index = 0;
	 int32_t i32_average_value = 0;

	 int16_t i16_current_adc_value = 0;

    if(GPIO_Pin == GPIO_PIN_3)
    {
        //board_set_uint32_state(UINT32_STATE_0, u32_counter);
		//u32_counter++;
//    	board_ext_adc_clock();
  ////////////////////////////  	board_data_processing_read_adc();
 //   	HAL_GPIO_WritePin(GPIOF, GPIO_PIN_6, GPIO_PIN_SET);

    	// Test READING ADC
    	// 1) Read MAX PULSE NUMBER
    	board_get_uint32_state(MAXIMUM_SAMPLES_PER_POINT_PARAM, &u32_max_pulse_number);
    	// 2) Read current pulse number
    	board_get_uint32_state(PULSE_NUMBER_IN_BANCH, &u32_current_pulse_number);

    	// 3) Read current value of ADC:
    	board_ext_adc_one_time_read_sequence(&i16_current_adc_value);

    	// 4) Read saved average value:
    	board_get_int32_state(ADC_AVERAGE, &i32_average_value);
    	// 5) Add current ADC value to average:
    	i32_average_value = i32_average_value + i16_current_adc_value;

    	// 6.1) Calculate real average ADC value
    	//     	and reset it in memory if it is last pulse in bunch:
    	if(u32_current_pulse_number >= u32_max_pulse_number)
    	{
    		if(u32_max_pulse_number > 0)
    		{
    			i32_average_value = i32_average_value/u32_max_pulse_number;
        		// Write average to current ADC value;
    			board_get_uint32_state(CURRENT_DIP_POINT_PARAM, &u32_current_raw_adc_index);
    			if(u32_current_raw_adc_index >= 7) // 7 is a sample shift, see AD9226 manual.
    			{
    				board_ext_adc_write_to_raw_average_buffer((int16_t)i32_average_value, u32_current_raw_adc_index - 7);
    			}

    			// Reset average
        		board_set_int32_state(ADC_AVERAGE, 0);

        		// Reset DATA_ACQUISION_FLAG:
        		board_set_uint32_state(DATA_ACQUISITION_FLAG, 0);
    		}
    		//
    		//
    	}
    	else
    	{
        // 6.2) Save current average value to memory:
    		board_set_int32_state(ADC_AVERAGE, i32_average_value);
    	}


//        HAL_GPIO_WritePin(GPIOF, GPIO_PIN_6, GPIO_PIN_RESET);


	}

    if(GPIO_Pin == GPIO_PIN_15)
	{
    	BSP_TS_ITClear();
	}

}
