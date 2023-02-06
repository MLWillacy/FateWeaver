using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;

namespace Fate1
{
    public static class UsefulMethods
    {
        public static string NextCell(this WorkSheet pWorksheet, string pThisCell)
        {
            char lastChar = pThisCell.Last();
            StringBuilder sb = new StringBuilder(pThisCell.Substring(0, pThisCell.Length - 1));

            if (lastChar == 'Z')
            {
                for (int i = sb.Length - 1; i >= 0; i--)
                {
                    char currentChar = sb[i];
                    if (currentChar == 'Z')
                    {
                        sb[i] = 'A';
                        if (i == 0)
                        {
                            sb.Insert(0, 'A');
                            break;
                        }
                    }
                    else
                    {
                        sb[i] = (char)(((int)currentChar) + 1);
                        break;
                    }
                }
            }
            else
            {
                sb.Append((char)(((int)lastChar) + 1));
            }

            return sb.ToString();
        }

        public static int BinomialCoefficient(int pSetSize, int pTargetSize)
        {
            int result = 1;
            for (int i = 1; i <= pTargetSize; i++)
            {
                result = result * (pSetSize - (pTargetSize - i)) / i;
            }
            return result;
        }

        public static double BinomialDistribution(int pSetSize, int pTargetSize, double pProbabilityOfSuccess) 
        {
            double biDis = 0.0;

            for (int i = 0; i <= pTargetSize; i++)
            {
                biDis += BinomialCoefficient(pSetSize, i) * Math.Pow(pProbabilityOfSuccess, i) * Math.Pow(1 - pProbabilityOfSuccess, pSetSize - i);
            }

            return biDis;
        }
    }
}
