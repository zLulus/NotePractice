using System.Linq.Expressions;
using System.Text;

namespace DotNet6.CodeLibrary.ExpressionTest
{
    public static class ExpressionTestDemo
    {
        public static void Run()
        {
            Action[] actions = new Action[]
            {
                BaseUse,
                LamdaToExpression,
                GetExpressionInfo,
                GetExpressionFullInfo,
            };
            foreach (var action in actions)
                action.Invoke();
        }

        public static void BaseUse()
        {
            Console.WriteLine("调用Math.Pow方法");
            Func<double, double, double> pow = Math.Pow;
            var result = pow.Invoke(1, 2);

            Console.WriteLine("最简单的表达式");

            //常量表达式
            Expression firstArg = Expression.Constant(2);
            Expression secondArg = Expression.Constant(4);

            //相加
            Expression addExpression = Expression.Add(firstArg, secondArg);
            Console.WriteLine(addExpression.ToString());

            Console.WriteLine("将表达式编译成委托");
            Expression<Func<int>> expression = Expression.Lambda<Func<int>>(addExpression);
            Console.WriteLine(expression);

            var func = expression.Compile();
            Console.WriteLine(func);
            Console.WriteLine("结果：" + func());


        }

        public static void LamdaToExpression()
        {
            Console.WriteLine("将Lamda表达式转换成表达式");

            Expression<Func<int>> expression = () => 5;

            Console.WriteLine(expression);
            Console.WriteLine(expression.Compile());
            Console.WriteLine(expression.Compile()());
        }

        public static void GetExpressionInfo()
        {
            Console.WriteLine("获得表达式基本信息");

            Expression<Func<int, int, double, double>> expression = (num1, num2, num3) => (num1 + 5) * num2 / num3;
            Console.WriteLine(expression.ToString());
            Console.WriteLine(expression.Compile());
            Console.WriteLine(expression.Compile()(1,2,3));

            if (expression.NodeType == ExpressionType.Lambda)
            {
                //LambdaExpression继承于Expression
                var lambdaExpression = expression as LambdaExpression;
                Console.WriteLine(lambdaExpression.Body);
                Console.WriteLine("返回值类型：" + lambdaExpression.ReturnType.ToString());

                IReadOnlyList<ParameterExpression> parameters = lambdaExpression.Parameters;

                foreach (var parameter in parameters)
                {
                    Console.WriteLine("形参名： " + parameter.Name);
                    Console.WriteLine("形参类型： " + parameter.Type.Name);
                }
            }
        }


        public static void GetExpressionFullInfo()
        {
            Console.WriteLine("表达式详细信息");
            Console.WriteLine();

            //基本信息
            Expression<Func<int, int>> sumExpression = num => num + 5;
            Console.WriteLine($"根节点的节点类型：{sumExpression.NodeType}");
            Console.WriteLine($"根节点的类型：{sumExpression.Type.Name}");
            Console.WriteLine($"根节点的名字：{sumExpression.Name}");
            Console.WriteLine($"根节点的代码：{sumExpression.ToString()}");
            Console.WriteLine();

            //形参
            Console.WriteLine($@"表达式的形参：{sumExpression.Parameters.Count}个,");
            foreach (var item in sumExpression.Parameters)
            {
                Console.WriteLine($"节点的节点类型：{sumExpression.Parameters[0].NodeType},");
                Console.WriteLine($"类型：{sumExpression.Parameters[0].Type.Name},");
                Console.WriteLine($"参数的名字：{sumExpression.Parameters[0]}");
            }
            Console.WriteLine();

            //Body
            var sumBody = sumExpression.Body as BinaryExpression;

            Console.WriteLine($"主体节点的节点类型：{sumBody.NodeType}");
            Console.WriteLine($"主体节点代码：{sumBody.ToString()}");
            Console.WriteLine();

            //左节点
            var leftNode = sumBody.Left as ParameterExpression;
            Console.WriteLine($"主体左节点：{leftNode.ToString()}");
            Console.WriteLine($"主体左节点的节点类型：{leftNode.NodeType}");
            Console.WriteLine($"主体左节点的类型：{leftNode.Type.Name}");
            Console.WriteLine($"主体左节点的名字：{leftNode.Name}");
            Console.WriteLine();

            //右节点
            var rightNode = sumBody.Right as ConstantExpression;
            Console.WriteLine($"主体右节点：{rightNode.ToString()}");
            Console.WriteLine($"主体右节点的节点类型：{rightNode.NodeType}");
            Console.WriteLine($"主体右节点的类型：{rightNode.Type.Name}");
        }
    }
}
