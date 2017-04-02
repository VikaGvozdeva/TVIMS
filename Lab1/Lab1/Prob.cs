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
        public int M;
        public int N;
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
        
        public int[] ksiValue;//значения, которые принимает
        public double[] ksiRasp;// узлы отрезков 
        public int[] ksiNumb;//массив, и-ая ячейка показывает сколько в интервал попало величин
        public double[] ksiProbability;//вероятности от значений случайной величины
        public double[] ksiN;// сколько попало величин в интервал / количесвто экмпериментов
        public double[] ksiV;
        public double[] ksiV1;
        public double[] n;
        public int ksiCount;
        public int min;



        public double C(int i, int j)
        {
            double k = (factorial(j)) / (factorial(i) * factorial(j - i));
            return k;
        }

        public double factorial(int i)
        {
            double result;

            if (i < 1)
            {
                result = 1;
            }
            else
            {
                result = factorial(i - 1) * i;

            }
            return result;

        }

        public void FillKsiProbability(ticket T)
        {
            double k = 1;
            if (T.r > T.M)
            {
                if (T.r <= T.N - T.M)
                {
                    ksiCount = T.M;
                    //??
                    ksiProbability = new double[ksiCount + 1];
                    ksiValue = new int[ksiCount + 1];
                    for (int i = 0; i <= ksiCount; i++)
                    {
                        ksiValue[i] = i;
                        k = 1;
                        k *= (C(i, T.M) * C(T.r - i, T.N - T.M)) / (C(T.r, T.N));
                        if (k != 1)
                        {
                            ksiProbability[i] = k;

                        }

                    }
                }
                else
                {
                    ksiCount = T.M;
                    ksiProbability = new double[ksiCount + 1];
                    ksiValue = new int[ksiCount + 1];
                    min = T.r - (T.N - T.M);

                    for (int i = min; i <= ksiCount; i++)
                    {
                        ksiValue[i - min] = i;
                        k = 1;
                        k *= (C(i, T.M) * C(T.r - i, T.N - T.M)) / (C(T.r, T.N));
                        if ((k != 1) && (k != 0))
                        {
                            ksiProbability[i - min] = k;
                        }

                    }
                }
            }
            else
            {
                ksiCount = T.r;
                ksiProbability = new double[ksiCount + 1];
                ksiValue = new int[ksiCount + 1];

                for (int i = 0; i <= ksiCount; i++)
                {
                    ksiValue[i] = i;
                    k = 1;
                    k *= (C(i, T.M) * C(ksiCount - i, T.N - T.M)) / (C(T.r, T.N));
                    if (k != 1)
                    {
                        ksiProbability[i] = k;

                    }

                }

            }
        }

        public void FillksiN(int n)
        {
            ksiN = new double[ksiCount];
            for (int i = 0; i <ksiCount; i++)
            {
                ksiN[i] = (double)ksiNumb[i] / n;
            }

        }

        public void FillKsiRasp()
        {

            ksiRasp = new double[ksiCount+2];
            for (int j = 0; j < (ksiCount+2); j++)
            {
                ksiRasp[j] = 0;
            }
            ksiRasp[1] = ksiProbability[0];
            for (int k = 2; k < (ksiCount+1); k++)
            {
                ksiRasp[k] =ksiRasp[k - 1] + ksiProbability[k-1];

            }
            ksiRasp[ksiCount + 1] = 1;

        }

        public double[] Generation(int count)
        {
            Random rand = new Random();
            double[] n = new double[count];
            double[] ksiV = new double[count];
            for (int i=0; i< count; i++)
            {
                
                n[i] = (double)(rand.Next(0, 100000)) / 100000.0;
                ksiV[i] = n[i];

            }
            return n;
        }

       
        public int[] Exp(int count)
        {
            int[] n = new int[ksiCount + 1];
            double[] itr = new double[ksiCount + 2];
            double[] g = new double[count + 1];
            for (int i = 0; i < ksiCount + 1; i++)
            {
                n[i] = 0;
            }
            for (int i = 0; i < ksiCount + 2; i++)
            {
                itr[i] = ksiRasp[i];
            }
            g = Generation(count);

            for (int i = 0; i < count; i++)
                for (int j = 0; j < ksiCount + 1; j++)
                {
                    if ((g[i] < itr[j + 1]) && (g[i] >= itr[j]))
                        n[j]++;
                }
            return n;
        }

        public void Part2(double[] ksiV, int count)
        {
            int numb;
            List<double> l = new List<double>();
            bool c;
            for (int j = 0; j < count; j++)
            {
                c = false;
                for (int i = 0; i < count; i++)
                {
                    if (ksiV[i] == ksiV[j] && i != j)
                    {
                        c = true;
                        break;
                    }
                }
                if (!c) l.Add(ksiV[j]);
                // ksiV1[t++] = ksiV[j];
            }
            numb = l.Count;
            ksiV1 = new double[numb];
            for (int i = 0; i < numb; i++)
            {
                ksiV1[i] = l[i];
            }
            ///next step

            n = new double[numb]; 
            for (int i=0; i< numb;i++)
                for(int j=0; j< count;j++)
                {
                    if (ksiV[j]==ksiV1[i])
                    {
                        n[i]++;
                    }
                }



        }
       

        public void Raspr(ticket T)
        {
            //SetKsiCount(T);
            //FillKsiValue(T);
            FillKsiProbability(T);
            FillKsiProbability(T);
            FillKsiRasp();
            ksiNumb = Exp(T.n);
            FillksiN(T.n);
        }
        

    }
}



