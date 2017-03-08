using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public struct ticket
    {
        public int M;
        public int N;
        public int r;

        public ticket(int _M, int _N, int _r)
        {
            M = _M;
            N = _N;
            r = _r;
        }
    }
    class Puasson
    {
        public int[] ksiValue;
        public double[] ksiProbability;
        public int l = 1, ksiCount;

        private int factorial(int n)
        {
            return (n == 0) ? 1 : n * factorial(n - 1);

         }
 
        public void GetPuasson(ticket T)
        {
            ksiCount = Math.Min(T.M, T.r);
            ksiValue = new int[ksiCount+1];
            for (int j=0; j<(ksiCount+1); j++)
            {
                ksiValue[j] = j;
            }
            ksiProbability = new double[ksiCount+1];
            ksiProbability[0] = Math.Exp(-l);
            for (int i=1; i<(ksiCount+1); i++)
            {
                ksiProbability[i] = (double)((ksiProbability[0] * (Math.Pow(l, i))) / factorial(i));

            }

        }
    }
}
