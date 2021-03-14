/*
 * board_states.c
 *
 *  Created on: Feb 12, 2019
 *      Author: VNVolokitin
 *
 *
 *  Board states storage subsystem.
 *  Here should be stored all important board states.
 *  It is possible to manage states by SET/GET functions.
 */

#include "board_states.h"


int32_t 	board_int32_state [MAX_BOARD_INT32_STATE];
uint32_t 	board_uint32_state[MAX_BOARD_UINT32_STATE];
double 		board_double_state[MAX_BOARD_DOUBLE_STATE];
osThreadId  board_task_handlers[MAX_BOARD_TASK_HANDLERS_STATE];




/* Function should be called during initialization process. */
void board_states_init()
{
	uint32_t u32_counter = 0;


	/* Reset board osThreadId values */
	while(u32_counter < MAX_BOARD_TASK_HANDLERS_STATE)
	{
		board_task_handlers[u32_counter] = 0;
		u32_counter++;
	}


	/* Reset board int32 values */
	u32_counter = 0;
	while(u32_counter < MAX_BOARD_INT32_STATE)
	{
		board_int32_state[u32_counter] = 0;
		u32_counter++;
	}

	/* Reset board uint32 values */
	u32_counter = 0;
	while(u32_counter < MAX_BOARD_UINT32_STATE)
	{
		board_uint32_state[u32_counter] = 0;
		u32_counter++;
	}

	/* Reset board double values */
	u32_counter = 0;
	while(u32_counter < MAX_BOARD_DOUBLE_STATE)
	{
		board_double_state[u32_counter] = 0;
		u32_counter++;
	}

}


void board_get_task_handler(uint32_t task_number, osThreadId * p_task_handler)
{
	if(task_number < MAX_BOARD_TASK_HANDLERS_STATE)
	{
		*p_task_handler = board_task_handlers[task_number];
	}
	else
	{
		while(1)
		{}
	}
}


void board_set_task_handler(uint32_t task_number, osThreadId task_handler)
{
  	if(task_number < MAX_BOARD_TASK_HANDLERS_STATE)
  	{
  		board_task_handlers[task_number] = task_handler;
  	}
  	else
  	{
  		while(1)
  		{}
  	}
}


void board_get_int32_state(uint32_t state_number, int32_t * pi32_value)
{
	if(state_number < MAX_BOARD_INT32_STATE)
	{
		*pi32_value = board_int32_state[state_number];
	}
	else
	{
		while(1)
		{}
	}
}


void board_set_int32_state(uint32_t state_number, int32_t i32_value)
{
	if(state_number < MAX_BOARD_INT32_STATE)
	{
		board_int32_state[state_number] = i32_value;
	}
	else
	{
		while(1)
		{}
	}
}


void board_get_uint32_state(uint32_t state_number, uint32_t * pu32_value)
{
	if(state_number < MAX_BOARD_UINT32_STATE)
	{
		*pu32_value = board_uint32_state[state_number];
	}
	else
	{
		while(1)
		{}
	}
}


void board_set_uint32_state(uint32_t state_number, uint32_t u32_value)
{
	if(state_number < MAX_BOARD_UINT32_STATE)
	{
		board_uint32_state[state_number] = u32_value;
	}
	else
	{
		while(1)
		{}
	}
}


void board_get_double_state(uint32_t state_number, double * p_double_value)
{
	if(state_number < MAX_BOARD_DOUBLE_STATE)
	{
		*p_double_value = board_double_state[state_number];
	}
	else
	{
		while(1)
		{}
	}
}


void board_set_double_state(uint32_t state_number, double double_value)
{
	if(state_number < MAX_BOARD_DOUBLE_STATE)
	{
		board_double_state[state_number] = double_value;
	}
	else
	{
		while(1)
		{}
	}
}



