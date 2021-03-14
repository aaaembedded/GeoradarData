/*
 * board_queue.h
 *
 *  Created on: Feb 12, 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_SRC_BOARD_QUEUE_H_
#define BOARD_SRC_BOARD_QUEUE_H_

#include "cmsis_os.h"

extern QueueHandle_t X_QueueCore;
extern QueueHandle_t Y_QueueCore;

void board_queue_init();

#endif /* BOARD_SRC_BOARD_QUEUE_H_ */
