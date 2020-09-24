using CSharpVerbalExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.GenerateRegularExpressionByCode.UseCSharpVerbalExpressions
{
    public class UseCSharpVerbalExpressionsDemo
    {
        //https://github.com/VerbalExpressions/CSharpVerbalExpressions
        public static void Run()
        {
            // Create an example of how to test for correctly formed URLs
            var verbEx = new VerbalExpressions()
                        .StartOfLine()
                        .Then("http")
                        .Maybe("s")
                        .Then("://")
                        .Maybe("www.")
                        .AnythingBut(" ")
                        .EndOfLine();
            var testMe = "";
            while (true)
            {
                Console.WriteLine("Input your url:");
                testMe = Console.ReadLine();
                if (verbEx.Test(testMe))
                {
                    Console.WriteLine("We have a correct URL ");
                }
                else
                {
                    Console.WriteLine("The URL is incorrect");
                }
            }
            
        }
    }
}
