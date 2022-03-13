using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.FunctionalBuilderV2
{
    // Here we shall build a Functional Builder, by making use of the functional programming paradigm.
    // This version highlights the thought process behind developing the Functional Builder the best.
    public class FunctionalBuilder
    {
        public class Person
        {
            public string Name, Occupation;

            public override string ToString()
            {
                return $"{Name} works as an {Occupation}.";
            }
        }


        public sealed class PersonBuilder
        {
            public readonly List<Func<Person, Person>> Actions
              = new List<Func<Person, Person>>();

            private PersonBuilder AddAction(Action<Person> action)
            {
                Actions.Add(p =>
                {
                    action(p);
                    return p;
                });

                return this;
            }

            public PersonBuilder Do(Action<Person> action)
            {
                AddAction(action);
                return this;
            }

            public PersonBuilder Called(string name)
            {
                Do(p => p.Name = name);
                return this;
            }

            public Person Build()
            {     
                return Actions.Aggregate(new Person(), (p, f) => f(p));             
            }

        }

       

        public static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Stefan").WorksAsA("Dev").Build();
            Console.WriteLine(person.ToString());
            Console.ReadLine();

        }
    }

    public static class PersonBuilderExtensions
    {
        public static FunctionalBuilder.PersonBuilder WorksAsA(this FunctionalBuilder.PersonBuilder builder, string occupation)
        {
            builder.Do(p => p.Occupation = occupation);
            return builder;
        }
    }


}
