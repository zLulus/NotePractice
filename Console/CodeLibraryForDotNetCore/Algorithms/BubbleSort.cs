using CodeLibraryForDotNetCore.Algorithms.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Algorithms
{
    public static class BubbleSort
    {
        public static int[] Sort(int[] data, SortMethodEnum sortMethod)
        {
            for(int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    if (sortMethod == SortMethodEnum.FromLargeToSmall)
                    {
                        if (data[i] > data[j])
                        {
                            var temp = data[i];
                            data[i] = data[j];
                            data[j] = temp;
                        }
                    }
                    else
                    {
                        if (data[i] < data[j])
                        {
                            var temp = data[i];
                            data[i] = data[j];
                            data[j] = temp;
                        }
                    }
                }
            }
            return data;
        }
    }
}
