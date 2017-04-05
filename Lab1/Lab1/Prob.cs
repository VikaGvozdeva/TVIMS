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
        public double[] prob;
        public double[] fun;
        public double[] fun_;


        public double[] n;
        public double maxProb;
        public double x_;
        public double disp_S2;
        public double disp;
        public double mw;
        public double S2;
        public double mw_x;
        public double R;
        public double Me;
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
            ksiN = new double[ksiCount + 1];
            prob = new double[ksiCount + 1];
            for (int i = 0; i < ksiCount + 1; i++)
            {
                ksiN[i] = (double)ksiNumb[i] / n;
                prob[i] = (double)ksiNumb[i] / n;
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
            for (int k = 2; k < (ksiCount + 1); k++)
            {
                ksiRasp[k] = ksiRasp[k - 1] + ksiProbability[k - 1];

            }
            ksiRasp[ksiCount + 1] = 1;

        }

        public void MaxProb(double[] prob, double[] ksiProb)
        {
            maxProb = Math.Abs(ksiProb[0] - prob[0]);
            for (int i = 1; i < ksiCount+1-min; i++)
            {
                if ((ksiProb[i] - prob[i]) < maxProb)
                {
                    maxProb = Math.Abs(ksiProb[i] - prob[i]);
                }
            }

        }
        public double[] Generation(int count)
        {
            Random rand = new Random();
            n = new double[count];
            ksiV = new double[count];
            for (int i = 0; i < count; i++)
            {

                n[i] = (double)(rand.Next(0, 100000)) / 100000.0;
                ksiV[i] = n[i];

            }
            if (count % 2 == 0)
            {
                Me = (ksiV[count / 2] + ksiV[count / 2 + 1])/2;
            }
            else
            {
                Me = ksiV[count / 2 + 1];
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
       
    public void Sort(double[] mas, int n)
        {
            double temp;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (mas[i] > mas[j])
                    {
                        temp = mas[i];
                        mas[i] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
      
        }
        public void function()
        {
            fun = new double[ksiCount+2];
            fun[0] = 0;
            for (int i=1;i<ksiCount+2;i++)
            {
                fun[i] = fun[i - 1] + ksiProbability[i-1];
            }

        }
        public void function_(ticket T)
        {
            fun_ = new double[ksiCount + 2];
            fun_[0] = 0;
            for (int i = 1; i < ksiCount + 2; i++)
            {
                fun_[i] = fun[i-1] + (double)ksiNumb[i-1] / T.n;
            }
        }
        public void R_(ticket T)
        {
            Sort(ksiV, T.n);
            R = ksiV[T.n-1] - ksiV[0];
        }
        public void x(double[] n, int exp)
        {
            x_ = 0;
            for (int i = 0; i < exp; i++)
            {
                x_ += n[i];
            }
            x_ = x_ / exp;

        }

        public void S(double[] n, int exp, double x_)
        {
            S2 = 0;
            for (int i = 0; i < exp; i++)
            {
                S2 += (n[i] - x_) * (n[i] - x_);
            }
            S2 = S2 / exp;

        }

        public void MathWait(double[] ksiProbability, int[] ksiValue)
        {
            mw = 0;
            for (int i = 0; i < ksiCount + 1; i++)
            {
                mw += ksiProbability[i] * ksiValue[i];
            }

        }

        public void MathWait_x_(double x_, double mw)
        {
            mw_x = Math.Abs(mw - x_);
        }

        public void disp_(double disp, double S2)
        {
            disp_S2 = Math.Abs(disp - S2);
        }

        public void Disp(double mw, int[] ksiValue, double[] ksiProbability)
        {
            disp = 0;
            for (int i = 0; i < ksiCount + 1; i++)
            {
                disp += ksiProbability[i] * (ksiValue[i] - mw);
            }

        }


        public void Raspr(ticket T)
        {
            
            FillKsiProbability(T);
            FillKsiProbability(T);
            FillKsiRasp();
            ksiNumb = Exp(T.n);
            FillksiN(T.n);
            MaxProb(prob, ksiProbability);
            MathWait(ksiProbability, ksiValue);
            x(n, T.n);
            MathWait_x_(x_, mw);
            S(n, T.n, x_);
            Disp(mw, ksiValue, ksiProbability);
            disp_(disp, S2);
            R_(T);
            function();
            function_(T);
        }
    }
}




