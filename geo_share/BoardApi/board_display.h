/*
 * board_display.h
 *
 *  Created on: 22. 4. 2019
 *      Author:  VNVolokitin
 */

#ifndef BOARD_DISPLAY_H_
#define BOARD_DISPLAY_H_


#include "GUI_App.h"
#include "GUI.h"

#define CONSTRAIN(amt,low,high) ((amt)<(low)?(low):((amt)>(high)?(high):(amt)))


void board_display_init();
void board_display_put_value_to_lcd(int16_t i16_value);
void board_display_graph(int16_t i16_old_value, int16_t i16_new_value, uint32_t u32_x_possition);
void board_display_graph_line(int16_t i16_value, uint32_t u32_x_possition,uint32_t u32_y_possition);
void board_display_put_dot(int16_t i16_value, uint32_t u32_x_possition,uint32_t u32_y_possition);
void constrain_16bit(int16_t * pi16_value, int16_t i16_min, int16_t i16_max);

#endif /* BOARD_DISPLAY_H_ */
