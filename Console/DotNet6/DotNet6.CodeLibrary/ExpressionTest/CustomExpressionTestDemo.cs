using DynamicExpresso;
using System.Linq.Expressions;

namespace DotNet6.CodeLibrary.ExpressionTest
{
    public class CustomExpressionTestDemo
    {
        public static void Run()
        {
            ////from stackoverflow
            //CustomDemo2();
            ////from stackoverflow
            //CustomDemo1();

            ////https://github.com/dynamicexpresso/DynamicExpresso
            DynamicExpressoDemo1();
            DynamicExpressoDemo2();
        }

        private static void DynamicExpressoDemo2()
        {
            List<CustomCondition> customConditions = new List<CustomCondition>();
            //customConditions.Add(new CustomCondition() { MethodType = MethodTypeEnum.Method1, ConnectionType = ConnectionTypeEnum.Add, Value = "not null" });
            customConditions.Add(new CustomCondition() { MethodType = MethodTypeEnum.Method2, ConnectionType = ConnectionTypeEnum.Or, Value = 0 });
            customConditions.Add(new CustomCondition() { MethodType = MethodTypeEnum.Method1, ConnectionType = ConnectionTypeEnum.Or, Value = "not null" });
            //customConditions.Add(new CustomCondition() { MethodType = MethodTypeEnum.Method2, ConnectionType = ConnectionTypeEnum.Add, Value = 1 });

            bool result = false;
            string expressionText = "";
            List<Parameter> parameters = new List<Parameter>();
            int index = 0;
            foreach (CustomCondition condition in customConditions)
            {
                var parameterName = $"parameter{index}";
                switch (condition.MethodType)
                {
                    case MethodTypeEnum.Method1:
                        //expressionText += $"{nameof(Method1)}(\"{(string)condition.Value}\") {GetConnectionType(condition.ConnectionType)} ";
                        expressionText += $"{nameof(Method1)}({parameterName}) {GetConnectionType(condition.ConnectionType)} ";
                        parameters.Add(new Parameter(parameterName, typeof(string), (string)condition.Value));
                        break;
                    case MethodTypeEnum.Method2:
                        //expressionText += $"{nameof(Method2)}({(int)condition.Value}) {GetConnectionType(condition.ConnectionType)} ";
                        expressionText += $"{nameof(Method2)}({parameterName}) {GetConnectionType(condition.ConnectionType)} ";
                        parameters.Add(new Parameter(parameterName, typeof(int), (int)condition.Value));
                        break;
                    default:
                        throw new Exception("错误的MethodType");
                }
                index++;
            }
            expressionText = expressionText.Remove(expressionText.Length - 3);

            var target = new Interpreter().SetFunction(nameof(Method1), Method1).SetFunction(nameof(Method2), Method2);

            var r3 = target.Eval<bool>(expressionText, parameters.ToArray());
        }

        private static object GetConnectionType(ConnectionTypeEnum connectionType)
        {
            return connectionType == ConnectionTypeEnum.Or ? "||" : "&&";
        }

        private static void DynamicExpressoDemo1()
        {
            var target = new Interpreter().SetFunction("method1", Method1).SetFunction("method2", Method2);

            //var r1 = target.Eval<bool>("method1(s)", new Parameter("s", typeof(string), ""));
            //var r2 = target.Eval<bool>("method1(s)", new Parameter("s", typeof(string), "not null"));

            var r3 = target.Eval<bool>("method1(s) ||  method2(n)",
                new Parameter("s", typeof(string), ""),
                new Parameter("n", typeof(int), 1));
        }

        static bool Method1(string s)
        {
            return string.IsNullOrEmpty(s);
        }

        static bool Method2(int num)
        {
            return num == 0;
        }

        private static void CustomDemo1()
        {
            var inputType = typeof(int);

            //var result = left + (rightLeft * rightRight);
            var left = Expression.Parameter(inputType, "left");
            var rightLeft = Expression.Parameter(inputType, "rightLeft");
            var rightRight = Expression.Parameter(inputType, "rightRight");

            var multiply = Expression.Multiply(rightLeft, rightRight);
            var add = Expression.Add(left, multiply);

            var lambda = Expression.Lambda<Func<int, int, int, int>>(add, left, rightLeft, rightRight);
            Console.WriteLine(lambda.ToString()); // will print "(left, rightLeft, rightRight) => (left + (rightLeft * rightRight))"

            var result = lambda.Compile().Invoke(1, 2, 3);

            Console.WriteLine(result); // will print "7"
        }

        #region CustomDemo2

        private static void CustomDemo2()
        {
            var predicate = CreatePredicate();

            var levelDetail = new LevelDetail { LevelDate = new DateTime(2017, 08, 19) };
            var level = new Level { LevelDetails = new List<LevelDetail> { levelDetail } };
            var test = new Test { TestDate = new DateTime(2027, 08, 19), Levels = new List<Level> { level } };

            var result = predicate.Compile()(test);
        }

        internal class LevelDetail
        {
            internal DateTime LevelDate { get; set; }
        }

        internal class Level
        {
            internal List<LevelDetail> LevelDetails { get; set; }
        }

        internal class Test
        {
            internal DateTime TestDate { get; set; }
            internal List<Level> Levels { get; set; }
        }

        private class SwapVisitor : ExpressionVisitor
        {
            public readonly Expression _from;
            public readonly Expression _to;

            public SwapVisitor(Expression from, Expression to)
            {
                _from = from;
                _to = to;
            }

            public override Expression Visit(Expression node) => node == _from ? _to : base.Visit(node);
        }

        private static Expression<Func<Test, bool>> CreatePredicate()
        {
            Expression<Func<LevelDetail, DateTime?>> left = ld => ld.LevelDate;
            // I didn't include EF, so I did test it just use directly Test.TestDate
            Expression<Func<Test, DateTime?>> right = t => t.TestDate;
            //Expression<Func<Test, DateTime?>> right = t => DbFunctions.AddDays(t.TestDate, 1);

            var testParam = Expression.Parameter(typeof(Test), "test_par");
            var levelParam = Expression.Parameter(typeof(Level), "level_par");
            var detailParam = Expression.Parameter(typeof(LevelDetail), "detail_par");

            // Swap parameters for right and left operands to the correct parameters
            var swapRight = new SwapVisitor(right.Parameters[0], testParam);
            right = swapRight.Visit(right) as Expression<Func<Test, DateTime?>>;

            var swapLeft = new SwapVisitor(left.Parameters[0], detailParam);
            left = swapLeft.Visit(left) as Expression<Func<LevelDetail, DateTime?>>;

            BinaryExpression comparer = Expression.GreaterThan(left.Body, right.Body);
            var lambdaComparer = Expression.Lambda<Func<LevelDetail, bool>>(comparer, detailParam);

            // Well, we created here the lambda for ld => ld.LevelDate > DbFunctions.AddDays(t.TestDate, 1)

            var anyInfo = typeof(Enumerable).GetMethods().Where(info => info.Name == "Any" && info.GetParameters().Length == 2).Single();

            // Will create **l.LevelDetails.Any(...)** in the code below

            var anyInfoDetail = anyInfo.MakeGenericMethod(typeof(LevelDetail));
            var anyDetailExp = Expression.Call(anyInfoDetail, Expression.Property(levelParam, "LevelDetails"), lambdaComparer);
            var lambdaAnyDetail = Expression.Lambda<Func<Level, bool>>(anyDetailExp, levelParam);

            // Will create **t.Levels.Any(...)** in the code below and will return the finished lambda

            var anyInfoLevel = anyInfo.MakeGenericMethod(typeof(Level));
            var anyLevelExp = Expression.Call(anyInfoLevel, Expression.Property(testParam, "Levels"), lambdaAnyDetail);
            var lambdaAnyLevel = Expression.Lambda<Func<Test, bool>>(anyLevelExp, testParam);

            return lambdaAnyLevel;
        }
        #endregion
    }


}
