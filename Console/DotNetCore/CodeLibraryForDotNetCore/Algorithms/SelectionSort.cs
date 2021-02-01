using CodeLibraryForDotNetCore.Algorithms.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Algorithms
{
    public static class SelectionSort
    {
        public static int[] Sort(int[] data, SortMethodEnum sortMethod)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (sortMethod == SortMethodEnum.FromLargeToSmall)
                {
                    int maxDataIndex = i;
                    for (int j = i; j < data.Length; j++)
                    {
                        if (data[j] > data[maxDataIndex])
                        {
                            maxDataIndex = j;
                        }
                    }
                    var temp = data[maxDataIndex];
                    data[maxDataIndex] = data[i];
                    data[i]= temp;
                }
                else
                {
                    int minDataIndex = i;
                    for (int j = i; j < data.Length; j++)
                    {
                        if (data[j] < data[minDataIndex])
                        {
                            minDataIndex = j;
                        }
                    }
                    var temp = data[minDataIndex];
                    data[minDataIndex] = data[i];
                    data[i] = temp;
                }
                
            }
            return data;
        }
    }
}
