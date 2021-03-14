# README #

# TOUCHSCREEN

To get access to touchad you need copy BSP component of stmpe811 to your project. It is  like LL driver. 
Next step is to  copy STM429i TS support files from Utilities\STM32F429I-Discovery_2.1.4
( from http://mazsola.iit.uni-miskolc.hu/~drdani/docs_arm/st/sw/  or soethere else)
to BoardApi dirrectory. Do not copy it to default directory because it will be deleted after CUBE generating procedure. 
That are: 
stm32f429i_discovery.c
stm32f429i_discovery.h
stm32f429i_discovery_ts.c
stm32f429i_discovery_ts.h
and
stm32f429i_discovery_io.h

Add path to BSP components to Core and BoardApi branches.
Remove void LCD_Delay(uint32_t Delay) function. It defined in STemwin_wrapper.c


STemwin_wrapper.c
add after generation:

#include "board_ts.h"

in function 
LCD_X_Config(void) 
add at the end  call:
board_ts_init();


TIMERS:
Prescaller = 2 ,
Input freq. 84000000 / (2 + 1) is one tick , One tick is 1/28000000 = 35.7nS
To set correct shift time, we need set u32_ticks_delay value + 4 in function board_clock_start()

Delay time between FALLING EDGE of PWM of TIMER 3, if pulse width is 70ns,  and start of EXT3 int is 10uS
ADC read processing time for one pulse is 10 uS
ADC input is +-5V for 12 bits, so 1 bit is 5V/2048 = 2.44mV



Delay time in delay line minimum 300nS and maximim is  300nS + (256 + 256 + 256 ) * 0.25nS = 300nS + 192nS  

 
board_config.h    include main config parameters, like a number of pulses in one bunch, mininum delay time
maximum delay time... and so on.






















  