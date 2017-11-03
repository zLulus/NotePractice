using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public class ReturnsMultipleValuesTest
    {
        public void Test()
        {
            //ref
            int number = 1;
            int newNumber=CaculateByRef(ref number);

            //out
            newNumber = CaculateByOut(out number);

            //元组
            var returnValues = CaculateByTuple(number);
            number = returnValues.number;
            newNumber = returnValues.newNumber;


            var returnValues2 = CaculateByDynamic(number);
            number = returnValues2.number;
            newNumber = returnValues2.newNumber;


            //元组2
            List<int> numbers = new List<int>();
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
            var returnValues3 = CaculateByTuple(numbers);
        }

        private int CaculateByRef(ref int number)
        {
            //对于ref
            //在进入方法CaculateByRef之前，number必须赋值
            //在方法CaculateByRef里面，number可以不进行修改
            number = 2;
            return number * number;
        }

        private int CaculateByOut(out int number)
        {
            //对于ref
            //在进入方法CaculateByOut之前，number不必赋值
            //在方法CaculateByOut里面，number必须进行赋值
            number = 3;
            return number * number;
        }

        private (int number,int newNumber) CaculateByTuple(int number)
        {
            return (number: number,
                newNumber: number * number);
        }

        private dynamic CaculateByDynamic(int number)
        {
            return new
            {
                number = number,
                newNumber = number * number
            };
        }

        private List<(int number, int newNumber)> CaculateByTuple(List<int> numbers)
        {
            return numbers.AsEnumerable().Select(
                x =>
                {
                    return (number: x,
                    newNumber: x * x);
                })
                .ToList();
        }
    }
}
