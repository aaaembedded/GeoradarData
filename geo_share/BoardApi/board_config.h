/*
 * board_config.h
 *
 *  Created on: 14. 4. 2019
 *      Author: VNVolokitin
 */

#ifndef BOARD_CONFIG_H_
#define BOARD_CONFIG_H_

/*
 * Maximum value of deep-steps depend on time-delay step, HW delay line and Tx/Rx noise rate
 *
 *
 * */

#define DEFAULT_MAXIMUM_DELAY_TIME  	100000 	/* 	pS , for speed 30cm/1000pS it is speed * time /2 = 15m,
												but we must pay respect to HW delay time around 50 nS
												*/
#define DEFAULT_DELAY_TIME_STEP  		250 	/* pS - depend on HW  */
#define MAXIMUM_SAMPLES  				1 	    /* Numbers of samples for averaging for one step point. */
#define MAXIMUM_DIP_POINTS              (256 + 256 + 256) /* It is maximum number of step that can do our delay line
                                                           * (each step is 0.25 nS)   */
#define FIXED_DELAY                     0     /* It is delay between start TIM3 and TIM3
                                                * One point is 1/(timer input clock / prescaler)
                                                *  Here:
                                                *   timer input clock = 84000000 (MCU CLOCK / 2)
                                                *   prescaler = 2 + 1 = 3
                                                *   So, one point is around 35.7nS
												*/
#define DEFAULT_STEP_VALUE_INDEX		14       /* 0-14, see table in board_delay_line.h */


#endif /* BOARD_CONFIG_H_ */
