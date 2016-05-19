using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ElementsOfProgrammerInterviewsCSharp
{
    public class Chapter06
    {
        #region 01 Dutch National Flag

        public static void _01_DutchNationalFlag(int[] arr, int pivotIdx)
        {
            int pivot = arr[pivotIdx];
            int lt = 0;
            int gt = arr.Length - 1;
            int current = 0;
            while (current <= gt)
            {
                if (arr[current] < pivot)
                {
                    _01_DutchNationalFlag_Swap(arr, lt, current);
                    lt++;
                        // advance both lt and current. this works because current >= lt, which order is retained after swap
                    current++;
                }
                else if (arr[current] > pivot)
                {
                    _01_DutchNationalFlag_Swap(arr, current, gt);
                    gt--;
                }
                else
                {
                    current++;
                }
            }
        }

        private static void _01_DutchNationalFlag_Swap(int[] arr, int idx1, int idx2)
        {
            int temp = arr[idx2];
            arr[idx2] = arr[idx1];
            arr[idx1] = temp;

        }

        #endregion

        #region _07_MinMissingPositiveInt

        public static int _07_FindMinMissingPositiveInt(int[] arr)
        {
            int start = 0;
            // move any -tive ints to the start of the array
            for (int i = start; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    _07_FindMinMissingPositiveInt_Swap(arr, i, start++);
                }
            }

            // flag an int as found by switching the number at the 
            // found index fron +tive to -tive
            for (int i = start; i < arr.Length; i++)
            {
                int idx = (Math.Abs(arr[i]) - 1) + start;
                if (idx < arr.Length)
                {
                    arr[idx] = -arr[idx];
                }
            }

            int missing = arr.Length - start;
            for (int i = start; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    missing = (i + 1) - start;
                    break;
                }
            }

            return missing;
        }

        private static void _07_FindMinMissingPositiveInt_Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        #endregion

        #region _12_EnumerateAllPrimesToN

        public static List<int> _12_EnumerateAllPrimesToN(int n)
        {
            int[] arr = Enumerable.Range(2, n).ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    for (int j = i + arr[i]; j < arr.Length; j += arr[i])
                    {
                        arr[j] = 0;
                    }
                }
            }
            return arr.Where(i => i != 0).ToList();
        }

        #endregion
    }
}