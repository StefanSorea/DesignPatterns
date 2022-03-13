using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.FunctionalBuilderV1
{
    // Here we shall build a Functional Builder, by making use of the functional programming paradigm.
    // This version is lightweight and does the job

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
            public readonly List<Action<Person>> Actions
              = new List<Action<Person>>();

            public PersonBuilder Called(string name)
            {
                Actions.Add(p => { p.Name = name; });
                return this;
            }

            public Person Build()
            {
                var p = new Person();
                Actions.ForEach(a => a(p));
                return p;
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
            builder.Actions.Add(p =>
            {
                p.Occupation = occupation;
            });
            return builder;
        }
    }


}
