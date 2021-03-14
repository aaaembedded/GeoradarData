/*
 * board_ts.c
 *
 *  Created on: Dec 12, 2020
 *      Author: seags
 */

#include "board_ts.h"
#include "GUI.h"
#include "stm32f4xx_hal.h"
#include "stm32f429i_discovery_ts.h"



/* Physical display size */
#define XSIZE_PHYS          240
#define YSIZE_PHYS          320

#define VXSIZE_PHYS         240
#define VYSIZE_PHYS         320


#define TOUCH_AD_LEFT 0x3c0
#define TOUCH_AD_RIGHT 0x034
#define TOUCH_AD_TOP 0x3b0
#define TOUCH_AD_BOTTOM 0x034



void board_ts_init()
{
	int x = 0;
	int y = 0;

    unsigned int TouchOrientation = (GUI_MIRROR_X * LCD_GetMirrorX()) |
	                                (GUI_MIRROR_Y * LCD_GetMirrorY()) |
	                                (GUI_SWAP_XY  * LCD_GetSwapXY()) ;
    GUI_TOUCH_SetOrientation(TouchOrientation);//Set TS orientation

  	/* Calibrate x */
	GUI_TOUCH_Calibrate(GUI_COORD_X, 0, XSIZE_PHYS-1, TOUCH_AD_LEFT, TOUCH_AD_RIGHT);
	/* Calibrate y */
	GUI_TOUCH_Calibrate(GUI_COORD_Y, 0, YSIZE_PHYS-1, TOUCH_AD_TOP, TOUCH_AD_BOTTOM);

	//    GUI_TOUCH_Calibrate(GUI_COORD_X, 0, 320, 0 , 4000);// X callibration
//    GUI_TOUCH_Calibrate(GUI_COORD_Y, 0, 240, 4000, 0); // Y callibration

    /* Touch-screen initialization */
    x = GUI_GetScreenSizeX();
    y = GUI_GetScreenSizeY();
    if (BSP_TS_Init(x, y) == HAL_ERROR)
	{
	    // Error hardFault
	}

    /* Interrupt initialization: */
    BSP_TS_ITConfig();
}


int GUI_TOUCH_X_MeasureX(void) //функция должна возвращать значение ацп по оси x
{
	GUI_PID_STATE GH_State;
	TS_StateTypeDef TS_State;

	BSP_TS_GetState(&TS_State);

	GH_State.Layer = GUI_TOUCH_GetLayer();
	GH_State.x = TS_State.X;
	GH_State.y = TS_State.Y;
	GH_State.Pressed = TS_State.TouchDetected;

	GUI_TOUCH_StoreStateEx (&GH_State);

	if (TS_State.TouchDetected == 1)
	{
		return TS_State.X;
	}
    else
    {
        return 0;
    }
}

int GUI_TOUCH_X_MeasureY(void) //функция должна возвращать значение ацп по оси y
{
	GUI_PID_STATE GH_State;
	TS_StateTypeDef TS_State;

	BSP_TS_GetState(&TS_State);

	GH_State.Layer = GUI_TOUCH_GetLayer();
	GH_State.x = TS_State.X;
	GH_State.y = TS_State.Y;
	GH_State.Pressed = TS_State.TouchDetected;

	GUI_TOUCH_StoreStateEx (&GH_State);

	if (TS_State.TouchDetected == 1)
	{
		return TS_State.Y;
	}
    else
    {
        return 0;
    }
}

void GUI_TOUCH_X_ActivateX(void)
{

}

void GUI_TOUCH_X_ActivateY(void)
{
}








