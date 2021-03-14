/*
 * board_queue.c
 *
 *  Created on: Feb 12, 2019
 *      Author: VNVolokitin
 */


#include "board_queue.h"



#define QUEUE_LENGTH    1
#define ITEM_SIZE       sizeof( int32_t )

/* The array to use as the queue's storage area.  This must be at least uxQueueLength * uxItemSize bytes. */
uint8_t X_QueueStorageArea[ QUEUE_LENGTH * ITEM_SIZE ];
uint8_t Y_QueueStorageArea[ QUEUE_LENGTH * ITEM_SIZE ];


static StaticQueue_t X_StaticQueueCore;
static StaticQueue_t Y_StaticQueueCore;

QueueHandle_t X_QueueCore;
QueueHandle_t Y_QueueCore;


void board_queue_init()
{

    /* Create a queue capable of containing QUEUE_LENGTH of  ITEM_SIZE values. */
	X_QueueCore = xQueueCreateStatic(
											QUEUE_LENGTH,
											ITEM_SIZE,
											X_QueueStorageArea,
											&X_StaticQueueCore
										 );

	xQueueReset( X_QueueCore );

	Y_QueueCore = xQueueCreateStatic(
											QUEUE_LENGTH,
											ITEM_SIZE,
											Y_QueueStorageArea,
											&Y_StaticQueueCore
										 );

	xQueueReset( Y_QueueCore );


}
