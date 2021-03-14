/*
 * board_gui_task.c
 *
 *  Created on: Mar 4, 2019
 *      Author: VNVolokitin
 */


#include "board_gui_task.h"
#include "board_states.h"
#include "cmsis_os.h"
#include "GUI.h"
#include "board_delay_line.h"

osThreadId guiTaskHandle;


#define GUI_TASK_STACK_SIZE			1024
#define SERVICE_TASK_STACK_SIZE		1024


void board_init_gui_task()
{
	/* Create the GUI Task here: */
//	osThreadDef(guiTask, board_gui_task, osPriorityNormal, 0, GUI_TASK_STACK_SIZE); // 4096 == 16384 in stack
//	guiTaskHandle = osThreadCreate(osThread(guiTask), NULL);

	/* Save GUI task handler for some case: */
//	board_set_task_handler(GUI_TASK, guiTaskHandle);

	osThreadDef(tsTask, board_ts_task, osPriorityNormal, 0, 128);
	guiTaskHandle = osThreadCreate(osThread(tsTask), NULL);

	/* Save GUI task handler for some case: */
	board_set_task_handler(TS_TASK, guiTaskHandle);



	osThreadDef(serviceTask, board_service_task, osPriorityNormal, 0, SERVICE_TASK_STACK_SIZE);
	guiTaskHandle = osThreadCreate(osThread(serviceTask), NULL);

	/* Save GUI task handler for some case: */
	board_set_task_handler(SERVICE_TASK, guiTaskHandle);


	//osThreadDef(graphicTask, GRAPHICS_MainTask, osPriorityNormal, 0, 1024);
	//guiTaskHandle = osThreadCreate(osThread(graphicTask), NULL);

	/* Save GUI task handler for some case: */
	//board_set_task_handler(GUI_TASK, guiTaskHandle);



}


/* Here is a board GUI task: */
void board_gui_task(void const * argument)
{
	while(1)
	{

		uint32_t i = 0;
		uint32_t j = 0;
		for(i = 0; i < 320 ; i++)
		{
			for(i = 0; i < 240 ; i++)
			{
				board_delay_line_set_delay_time(250 * j);
			}
		}


		//board_delay_line_set_delay_time(uint32_t u32_delay_time);




	      osDelay(100);
	}
}

void board_service_task(void const * argument)
{
	//TIM_HandleTypeDef htim2;
	//HAL_TIM_PWM_Start_IT(&htim2, TIM_CHANNEL_1);
	TS_StateTypeDef TsState;
	uint32_t u32_value;
	GUI_PID_STATE State;
	while(1)
	{
		//BSP_TS_GetState(&TsState);
	    GUI_TOUCH_GetState(&State);
		if(State.Pressed == 1)
		{
			board_get_uint32_state(BTN_0_VALUE, &u32_value);
			if(State.x > 120)
			{
				u32_value++;
			}
			else
			{
				u32_value--;
			}
			board_set_uint32_state(BTN_0_VALUE, u32_value);
		}
        osDelay(100);
	}
}


void board_ts_task(void const * argument)
{
	while(1)
	{
		GUI_TOUCH_Exec();
        osDelay(100);
	}
}
