using Newtonsoft.Json;
using RulesEngine.Actions;
using RulesEngine.Models;
using System;
using System.Threading.Tasks;

namespace RuleEngineFrameworkDemo
{
    internal class MyCustomAction : ActionBase
    {
        MyCustomActionInput input { get; set; }
        public MyCustomAction(MyCustomActionInput _input)
        {
            input = _input;
        }


        public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            foreach (var ruleParameter in ruleParameters)
            {
                Console.WriteLine(ruleParameter.Name);
                Console.WriteLine(ruleParameter.Type.ToString());
                Console.WriteLine(JsonConvert.SerializeObject(ruleParameter.Value));
                //Console.WriteLine(ruleParameter.ParameterExpression.ToString());
            }
            var customInput = context.GetContext<string>("customContextInput");
            var ruleResultTree = context.GetParentRuleResult();
            Console.WriteLine(ruleResultTree.Rule.RuleName);
            Console.WriteLine(ruleResultTree.Rule.Expression);
            Console.WriteLine(ruleResultTree.Rule.RuleExpressionType);

            Console.WriteLine(ruleResultTree.Rule.ErrorMessage);

            Console.WriteLine(ruleResultTree.IsSuccess);
            return new ValueTask<object>(1);
        }
    }
}
