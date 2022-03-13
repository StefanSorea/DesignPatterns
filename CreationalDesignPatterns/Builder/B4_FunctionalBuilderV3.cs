using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.FunctionalBuilderV3
{
    // Here we shall build a Functional Builder, by making use of the functional programming paradigm.
    // This version improves on the V2 by adding a layer of abstraction.
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

        public abstract class FunctionalBuilderTemplate<TSubject,TSelf>
        where TSubject: new()
        where TSelf: FunctionalBuilderTemplate<TSubject, TSelf>
        {
            private readonly List<Func<TSubject, TSubject>> Actions
              = new List<Func<TSubject, TSubject>>();

            private TSelf AddAction(Action<TSubject> action)
            {
                Actions.Add(p =>
                {
                    action(p);
                    return p;
                });

                return (TSelf) this;
            }

            public TSelf Do(Action<TSubject> action)
            {
                AddAction(action);
                return (TSelf) this;
            }


            public TSubject Build()
            {
                return Actions.Aggregate(new TSubject(), (t, f) => f(t));
            }

        }


        public sealed class PersonBuilder: FunctionalBuilderTemplate<Person,PersonBuilder>
        {
            public PersonBuilder Called(string name)
            {
                Do(p => p.Name = name);
                return this;
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
