/*
 * board_tasks.h
 *
 *  Created on: Feb 12, 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_SRC_BOARD_TASKS_H_
#define BOARD_SRC_BOARD_TASKS_H_

#include "FreeRTOS.h"
#include "cmsis_os.h"
//#include "board_motion_task.h"


extern osThreadId motion_task_handle;


void board_tasks_init();






#endif /* BOARD_SRC_BOARD_TASKS_H_ */
