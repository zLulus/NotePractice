using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RuleEngineFrameworkDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    await RulesEngineFromJson();
                    //await RulesEngineFromWorkflow();
                    //await RulesEngineWithReturn();
                    //await RulesEngineWithCustomActionAndReturn();
                }
                catch (Exception ex)
                {

                }

            });

            Console.ReadLine();
        }

        private static async Task RulesEngineWithCustomActionAndReturn()
        {
            //todo 
        }

        private static async Task RulesEngineWithReturn()
        {
            string ruleJson = File.ReadAllText($"{System.Environment.CurrentDirectory}\\rule2.json");
            string[] workflowRules = new string[1] { ruleJson }; //Get list of workflow rules declared in the json

            var re = new RulesEngine.RulesEngine(workflowRules, null);

            //test 1
            var input1 = new
            {
                couy = "india",
                loyalityFactor = 2,
                totalPurchasesToDate = 5000,
                TotalBilled = 120
            };
            var input2 = new
            {
                totalOrders = 10,
                noOfVisitsPerMonth = 3
            };

            var rp1 = new RuleParameter("input1", input1);
            var rp2 = new RuleParameter("input2", input2);

            //run GiveDiscount20Percent (flase) -> OnFailure -> run GiveDiscount10Percent
            var result = await re.ExecuteActionWorkflowAsync("inputWorkflow", "GiveDiscount20Percent", new RuleParameter[] { rp1, rp2 });
            Console.WriteLine(result.Output);

            result = await re.ExecuteActionWorkflowAsync("inputWorkflow", "GiveDiscount10Percent", new RuleParameter[] { rp1, rp2 });
            Console.WriteLine(result.Output);


            //test 2
            var input3 = new
            {
                couy = "india",
                loyalityFactor = 2,
                totalPurchasesToDate = 5000,
                TotalBilled = 120
            };
            var input4 = new
            {
                totalOrders = 1,
                noOfVisitsPerMonth = 3
            };

            var rp3 = new RuleParameter("input1", input3);
            var rp4 = new RuleParameter("input2", input4);

            //run GiveDiscount20Percent (flase) -> OnFailure -> run GiveDiscount10Percent (false) -> OnFailure
            var result2 = await re.ExecuteActionWorkflowAsync("inputWorkflow", "GiveDiscount20Percent", new RuleParameter[] { rp3, rp4 });
            Console.WriteLine(result2.Output);

            result2 = await re.ExecuteActionWorkflowAsync("inputWorkflow", "GiveDiscount10Percent", new RuleParameter[] { rp3, rp4 });
            Console.WriteLine(result2.Output);
        }

        private static async Task RulesEngineFromWorkflow()
        {
            List<Rule> rules = new List<Rule>();

            Rule rule = new Rule();
            rule.RuleName = "Test Rule";
            rule.SuccessEvent = "Count is within tolerance.";
            rule.ErrorMessage = "Over expected.";
            rule.Expression = "count < 3";
            rule.RuleExpressionType = RuleExpressionType.LambdaExpression;
            rules.Add(rule);

            var workflows = new List<Workflow>();

            Workflow exampleWorkflow = new Workflow();
            exampleWorkflow.WorkflowName = "Example Workflow";
            exampleWorkflow.Rules = rules;

            workflows.Add(exampleWorkflow);

            var bre = new RulesEngine.RulesEngine(workflows.ToArray(), null);
            var rp1 = new RuleParameter("count", 1);

            var resultList = await bre.ExecuteAllRulesAsync(exampleWorkflow.WorkflowName, rp1);

            //Check success for rule
            foreach (var result in resultList)
            {
                Console.WriteLine($"【Test 1】Rule - {result.Rule.RuleName}, IsSuccess - {result.IsSuccess}");
            }

            var rp2 = new RuleParameter("count", 10);

            var resultList2 = await bre.ExecuteAllRulesAsync(exampleWorkflow.WorkflowName, rp2);

            //Check success for rule
            foreach (var result in resultList2)
            {
                Console.WriteLine($"【Test 2】Rule - {result.Rule.RuleName}, IsSuccess - {result.IsSuccess}");
            }
        }

        private static async Task RulesEngineFromJson()
        {
            string ruleJson = File.ReadAllText($"{System.Environment.CurrentDirectory}\\rule1.json");
            string[] workflowRules = new string[1] { ruleJson }; //Get list of workflow rules declared in the json

            var re = new RulesEngine.RulesEngine(workflowRules, null);

            // Declare input1,input2,input3 
            var input1 = new
            {
                country = "india",
                loyalityFactor = 2,
                totalPurchasesToDate = 8000,
            };
            var input2 = new
            {
                totalOrders = 3,
            };
            var input3 = new
            {
                noOfVisitsPerMonth = 3
            };

            var rp1 = new RuleParameter("basicInfo", input1);
            var rp2 = new RuleParameter("orderInfo", input2);
            var rp3 = new RuleParameter("telemetryInfo", input3);

            var resultList = await re.ExecuteAllRulesAsync("DiscountWithCustomInputNames", rp1, rp2, rp3);

            //Check success for rule
            foreach (var result in resultList)
            {
                Console.WriteLine($"Rule - {result.Rule.RuleName}, IsSuccess - {result.IsSuccess}");
            }
        }
    }
}
