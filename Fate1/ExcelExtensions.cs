using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;

namespace Fate1
{
    public static class ExcelExtensions
    {
        public static string NextCell(this WorkSheet pWorksheet, string pThisCell)
        {
            char lastChar = pThisCell.Last();
            if (lastChar == 'Z')
            {

                string newColumn = "";
                for (int j = pThisCell.Length - 1; j > -1; j--)
                {
                    if (pThisCell[j] == 'Z')
                    {
                        newColumn = pThisCell.Substring(0, j) + 'A';
                        if (j == 0)
                        {
                            newColumn = "A" + newColumn;
                            pThisCell = newColumn;
                            break;
                        }
                    }
                    else
                    {
                        char nextChar = (char)(((int)pThisCell[j]) + 1);
                        newColumn = pThisCell.Substring(0, j) + nextChar.ToString() + pThisCell.Substring(j + 1, pThisCell.Length - 1);
                        break;
                    }
                    pThisCell = newColumn;
                }
            }
            else
            {
                char nextChar = (char)(((int)lastChar) + 1);
                pThisCell = pThisCell.Substring(0, pThisCell.Length - 1) + nextChar.ToString();
            }

            return pThisCell;
        }
    }
}
