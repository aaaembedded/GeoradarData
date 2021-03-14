/*
 * board_gui_task.h
 *
 *  Created on: Mar 4, 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_GUI_TASK_H_
#define BOARD_GUI_TASK_H_

#include "cmsis_os.h"
#include "stm32f429i_discovery_ts.h"


void board_init_gui_task();
void board_get_gui_task_handle(osThreadId * task_handle);

void board_gui_task(void const * argument);
void board_service_task(void const * argument);
void board_ts_task(void const * argument);









#endif /* BOARD_GUI_TASK_H_ */
