/*
 * Board initialization file.
 * VNVolokitin
 * */

#include "board_init.h"
#include "board_queue.h"
#include "board_configuration.h"
#include "board_tasks.h"
#include "board_ext_adc.h"


/* Board main initialization function. Must be call from main: */

void board_main_init()
{
	board_init_external_adc();
	//board_queue_init();
    board_configuration_init();
	board_tasks_init();

}




