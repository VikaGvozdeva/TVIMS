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
        public int n;//количество экспериментов

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
        
        public int[] ksiValue;//значения, которые принимает
        public double[] ksiRasp;// узлы отрезков 
        public int[] ksiNumb;//массив, и-ая ячейка показывает сколько в интервал попало величин
        public double[] ksiProbability;//вероятности от значений случайной величины
        public double[] ksiN;// сколько попало величин в интервал / количесвто экмпериментов
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

        public void FillksiN(int n)
        {
            ksiN = new double[ksiCount + 1];
            for (int i = 0; i <= ksiCount; i++)
            {
                ksiN[i] = (double)ksiNumb[i] / n;
            }

        }

        public void FillKsiRasp()
        {

            ksiRasp = new double[ksiCount + 1];
            for (int j = 0; j < (ksiCount + 1); j++)
            {
                ksiRasp[j] = 0;
            }
            ksiRasp[1] = ksiProbability[0];
            for (int k = 2; k < (ksiCount+1 ); k++)
            {
                ksiRasp[k] =ksiRasp[k - 1] + ksiProbability[k-1];

            }

        }

        public double[] Generation(int count)
        {
            Random rand = new Random();
            double[] n = new double[count];
            for (int i=0; i< count; i++)
            {

                n[i] = (double)(rand.Next(100, 999)) / 1000.0;

            }
            return n;
        }

        public int[] Exp(int count)
        {
            int[] n = new int[ksiCount + 1];
            double[] itr = new double[ksiCount + 2];
            double[] g = new double[count + 1];
            for (int i = 0; i < ksiCount + 1; i++)
                n[i] = 0;
            itr = ksiRasp;
            g = Generation(count);

            for (int i = 0; i < count; i++)
                for (int j = 0; j < ksiCount ; j++)
                {
                    if ((g[i] < itr[j + 1]) && (g[i] >= itr[j]))
                        n[j]++;
                }
            return n;
        }
       

        public void Raspr(ticket T)
        {
            SetKsiCount(T);
            FillKsiValue();
            FillKsiProbability(T);
            FillKsiProbability(T);
            FillKsiRasp();
            ksiNumb = Exp(T.n);
            FillksiN(T.n);
        }
        

    }
}



