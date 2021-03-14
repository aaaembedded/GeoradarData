/*
 * board_tasks.c
 *
 *  Created on: Feb 12, 2019
 *      Author: VNVolokitin
 */


#include "board_tasks.h"
#include "board_gui_task.h"


/* This function initialize all board tasks: */
void board_tasks_init()
{
	board_init_gui_task();
}

