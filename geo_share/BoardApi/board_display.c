/*
 * board_display.c
 *
 *  Created on: 22. 4. 2019
 *      Author: VNVolokitin
 */

#include "board_display.h"
#include "board_configuration.h"
#include <stdio.h>

void board_display_init()
{
	/* Initialize the graphical hardware */
	  GRAPHICS_HW_Init();

	  /* Initialize the graphical stack engine */
	  GRAPHICS_Init();

	  /* Graphic application */
	  GUI_Clear();
	  GUI_SetColor(GUI_WHITE);
	  GUI_SetFont(&GUI_Font32_1);

}


/* Put value to LCD:  */
void board_display_put_value_to_lcd(int16_t i16_value)
{
	char ch_numbers[50];

	GUI_SetColor(GUI_WHITE);
	/* Write to print: */
	sprintf(ch_numbers,"%d           ", i16_value);
	GUI_DispStringAt(ch_numbers, (LCD_GetXSize()-150)/2-30, (LCD_GetYSize()-20)/2 + 128);//68
}


void board_display_graph(int16_t i16_old_value, int16_t i16_new_value, uint32_t u32_x_possition)
{
	int16_t i16_y0 = 0;

	i16_y0 = -1 * i16_old_value/2 + 150;
	CONSTRAIN(i16_y0, 0,320);
	GUI_SetColor(GUI_BLACK);
	GUI_DrawPixel(u32_x_possition, i16_y0);

	i16_y0 = -1 * i16_new_value/2 + 150;
	CONSTRAIN(i16_y0, 0,320);
	GUI_SetColor(GUI_WHITE);
	GUI_DrawPixel(u32_x_possition, i16_y0);

}


void board_display_graph_line(int16_t i16_value, uint32_t u32_x_possition,uint32_t u32_y_possition)
{
	int16_t i16_y0 = 0;
	int32_t u32_color = 0;
	int32_t u32_color_b = 0;
	int32_t u32_color_g = 0;
	int32_t u32_color_r = 0;

#if 1
	if(i16_value >= 0)
	{
  		     //CONSTRAIN(i16_value, 0,256);
		constrain_16bit(&i16_value, 0,255);
		i16_y0 =255 - i16_value;
		u32_color = 0xFF000000 + (i16_y0<<16);
	}
	else
	{
		i16_value = -1 * i16_value;
		     //CONSTRAIN(i16_value, 0,256);
		constrain_16bit(&i16_value, 0,255);
		i16_y0 =255 - i16_value;
		u32_color = 0xFF000000 + (i16_y0<<8);
	}
#endif

#if 0

	i16_y0 = 1 * (i16_value) + 128;

	CONSTRAIN(i16_y0, 0,256);
	//board_i16_constrain(&i16_y0, 0, 255);

	u32_color = 0xFF000000 + (i16_y0<<16);// + (i16_y0<<8) + (i16_y0<<0);
	//u32_color = 0xFF000000 + (i16_y0<<8);
#endif



#if 0
	i16_y0 = -1* i16_value + 33023;
#if 0
	//CONSTRAIN(i16_y0, 0,768);
	//board_i16_constrain(&i16_y0, 0, 767);
	if(i16_y0 < 256)
	{
		u32_color_r = i16_y0;
		u32_color_g = 0;
		u32_color_b = 0;
	}
	else if ((i16_y0 >= 256) && (i16_y0 < 512))
	{
		u32_color_r = 0;
		u32_color_g = i16_y0;
		u32_color_b = 0;
	}
	else
	{
		u32_color_r = 0;
		u32_color_g = 0;
		u32_color_b = i16_y0;
	}

	u32_color = 0xFF000000 + (u32_color_r<<16) + (u32_color_g<<8) + (u32_color_b<<0);
#endif
	u32_color = 0xFF000000 + i16_y0;

#endif
	GUI_SetColor(u32_color);
	GUI_DrawPixel(u32_x_possition, u32_y_possition);
}


void board_display_put_dot(int16_t i16_value, uint32_t u32_x_possition,uint32_t u32_y_possition)
{
	int16_t i16_y0 = 0;
	int32_t u32_color = 0;
	int32_t u32_color_b = 0;
	int32_t u32_color_g = 0;
	int32_t u32_color_r = 0;

#if 1
	constrain_16bit(&i16_value, 0,255);
	i16_y0 = i16_value;
	u32_color = 0xFF000000 + (i16_y0<<16) + (i16_y0<<8) + (i16_y0<<0);
#endif

#if 0
	if(i16_y0 < 256)
	{
		u32_color_r = i16_y0;
		u32_color_g = 0;
		u32_color_b = 0;
	}
	else if ((i16_y0 >= 256) && (i16_y0 < 512))
	{
		u32_color_r = 0;
		u32_color_g = i16_y0;
		u32_color_b = 0;
	}
	else
	{
		u32_color_r = 0;
		u32_color_g = 0;
		u32_color_b = i16_y0;
	}

	u32_color = 0xFF000000 + (u32_color_r<<16) + (u32_color_g<<8) + (u32_color_b<<0);
#endif
	GUI_SetColor(u32_color);
	GUI_DrawPixel(u32_x_possition, u32_y_possition);
}


void constrain_16bit(int16_t * pi16_value, int16_t i16_min, int16_t i16_max)
{
	if (*pi16_value < i16_min )
	{
		*pi16_value = i16_min;
	}

	if (*pi16_value > i16_max )
	{
		*pi16_value = i16_max;
	}
}








