using CodeLibraryForDotNetCore.UseYield.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseYield
{
    public class UseYieldDemo
    {
        public static void Run()
        {
            Console.WriteLine("First Test:");
            Run1();
            Console.WriteLine("");
            Console.WriteLine("Second Test:");
            Run2();
            Console.WriteLine("");
            Console.WriteLine("Similar Effect:");
            SimilarEffect();
        }

        public static void Run1()
        {
            Person person = new Person()
            {
                Name = "Bernadette",
                Age = 10
            };

            var enumerator = Test(person);
            Console.WriteLine($"{person.Name},{person.Age}");

            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
        }

        public static void Run2()
        {
            Person person = new Person()
            {
                Name = "Bernadette",
                Age = 10
            };

            var enumerator = Test(person);
            Console.WriteLine($"{person.Name},{person.Age}");

            enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            //enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
            //enumerator.MoveNext();
            Console.WriteLine($"{person.Name},{person.Age}");
        }

        private static IEnumerator Test(Person person)
        {
            person.Name = "Sheldon";
            person.Age += 1;
            yield return null;

            person.Name = "Penny";
            person.Age += 2;
            yield return null;

            person.Name = "Leonard";
            person.Age += 1;
            yield return null;
        }

        public static void SimilarEffect()
        {
            Person person = new Person()
            {
                Name = "Bernadette",
                Age = 10
            };
            List<Action> actions = new List<Action>();
            actions.Add(new Action(() =>
            {
                person.Name = "Sheldon";
                person.Age += 1;
            }));
            actions.Add(new Action(() =>
            {
                person.Name = "Penny";
                person.Age += 2;
            }));
            actions.Add(new Action(() =>
            {
                person.Name = "Leonard";
                person.Age += 1;
            }));
            Console.WriteLine($"{person.Name},{person.Age}");

            actions[0].Invoke();
            Console.WriteLine($"{person.Name},{person.Age}");
            actions[1].Invoke();
            Console.WriteLine($"{person.Name},{person.Age}");
            actions[2].Invoke();
            Console.WriteLine($"{person.Name},{person.Age}");
        }
    }
}
