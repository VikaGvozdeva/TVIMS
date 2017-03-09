using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Lab1
{

    public struct ticket
    {
        public double M;
        public double N;
        public int r;
        public int n;

        public ticket(int _M, int _N, int _r, int _n)
        {
            M = _M;
            N = _N;
            r = _r;
            n = _n;
        }
    }
    class Probability
    {
        
        public int[] ksiValue;
        public double[] ksiRasp;
        public int[] ksiNumb;
        public double[] ksiProbability;
        public int ksiCount;


        private double factorial(double n)
        {
            return (n == 0) ? 1 : n * factorial(n - 1);

        }

        public void SetKsiCount(ticket T)
        {
            ksiCount = Math.Min((int)T.M, T.r);
        }

        public int GetKsiCount(ticket T)
        {
            return ksiCount;
        }

        public void FillKsiValue()
        {
            ksiValue = new int[ksiCount + 1];

            for (int j = 0; j <= ksiCount; j++)
            {
                ksiValue[j] = j;
            }
        }

        public void FillKsiProbability(ticket T)
        {
            SetKsiCount(T);
            double r = T.r;
            ksiCount = Math.Min((int)T.M, T.r);
            ksiProbability = new double[ksiCount + 1];
            for (int i = 0; i <= ksiCount; i++)
            {
                //ksiProbability[i] = Convert.ToDouble((Math.Pow((1 - (T.M / T.N)), T.r - i)) * (Math.Pow((T.M / T.N), i)));
                if ((i == 0) || (i == T.r))
                {
                    ksiProbability[i] = ((Math.Pow((1 - (T.M / T.N)), r - i)) * (Math.Pow((T.M / T.N), i)));
                }
                else
                {
                    double coef = (factorial(r) / (factorial((double)(i)) * factorial((double)(r - i))));
                    ksiProbability[i] = coef*((Math.Pow((1 - (T.M / T.N)), T.r - i)) * (Math.Pow((T.M / T.N), i)));
                }


            }
        }

        public void FillKsiRasp()
        {

            ksiRasp = new double[ksiCount + 2];
            for (int j = 0; j < (ksiCount + 2); j++)
            {
                ksiRasp[j] = 0;
            }
            ksiRasp[1] = ksiProbability[0];
            for (int k = 2; k < (ksiCount + 2); k++)
            {
                ksiRasp[k] =ksiRasp[k - 1] + ksiProbability[k - 1];

            }

        }

        public void Exp(int count)
        {

            double U;
            ksiNumb = new int[ksiCount + 1];
            for (int k = 0; k <= ksiCount; k++)
            {
                ksiNumb[k] = 0;
            }
            for (int i = 0; i < count; i++)
            {
                Random rand = new Random();
                U = rand.NextDouble();

                for (int j = 1; j < ksiCount; j++)
                {
                    if ((U >= ksiRasp[j]) && (U < ksiRasp[j + 1]))
                    {
                        ksiNumb[j - 1]++;
                        break;
                    }
                    break;
                }
            }
        }





        //        do
        //        {
        //            if (U >= ksiRasp[j + 1])
        //            {
        //                j++;
        //            }
        //            else
        //                break;
        //        }
        //        while ((U >= ksiRasp[j]) && (U < ksiRasp[j + 1]));

        //        ksiNumb[j]++;
        //    }
        //}



            //for (int i = 0; i < count; i++)
            //{
            //    U = rnd.Next(0, 1);
            //    for (int j = 1; j < (ksiCount + 1); j++)
            //    {
            //        if (U > a)
            //        {
            //            a = ksiRasp[j + 1];
            //        }
            //        else
            //        {
            //            ksiNumb[j - 1]++;
            //            break;
            //        }
            //        //break;
            //    }
            //}
     //   }

        public void Raspr(ticket T)
        {
            SetKsiCount(T);
            FillKsiValue();
            FillKsiProbability(T);
            FillKsiProbability(T);
            FillKsiRasp();
            Exp(T.n);
        }


    }
}
    