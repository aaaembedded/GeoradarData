using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoradarGui
{
    class FilterClass
    {
        int NCoef = 5;
        double[] y; //output samples
        double[] x; //input samples

        public FilterClass()
        {
            y = new double[NCoef + 1];
            x = new double[NCoef + 1 ];
        }


        public double iir(double NewSample , int param)
        {
            double[] ACoef;
            double[] BCoef;
            if (param == 0)
            {

                ACoef = new double[] {
                                         0.00111864642797306940,
                                         0.00559323213986534590,
                                         0.01118646427973069200,
                                         0.01118646427973069200,
                                         0.00559323213986534590,
                                         0.00111864642797306940
                                     };

                BCoef = new double[] {
                                         1.00000000000000000000,
                                         -2.79367066485982370000,
                                         3.30427613832456800000,
                                         -2.04390219144863220000,
                                         0.65598353623328531000,
                                         -0.08688997955300684100
                                     };
            }
            else
            {
                ACoef = new double[] {
                                         0.00000002699816382559,
                                         0.00000013499081912794,
                                         0.00000026998163825589,
                                         0.00000026998163825589,
                                         0.00000013499081912794,
                                         0.00000002699816382559
                                    };

                BCoef = new double[] {
                                        1.00000000000000000000,
                                        -4.76248828149975530000,
                                        9.07607354302641230000,
                                        -8.65166209646841720000,
                                        4.12511338965476960000,
                                        -0.78703568411325420000
                                    };

            }

            int n;

            //shift the old samples
            for (n = NCoef; n > 0; n--)
            {
                x[n] = x[n - 1];
                y[n] = y[n - 1];
            }

            //Calculate the new output
            x[0] = NewSample;
            y[0] = ACoef[0] * x[0];
            for (n = 1; n <= NCoef; n++)
                y[0] += ACoef[n] * x[n] - BCoef[n] * y[n];

            return (y[0]);
        }








    }
}
