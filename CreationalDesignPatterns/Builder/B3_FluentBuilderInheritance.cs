using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.FluentBuilderWithInheritance
{
    // In this exercice we will highlight how Fluent Builders can inherit from each other. The solution? Recursive generics.
    class FluentBuilderWithInheritance
    {
        // Person Class
        public class Person
        {
            public string Name;

            public string Position;

            public DateTime DateOfBirth;

            public class Builder : PersonBirthDateBuilder<Builder>
            {
                // Is there value here ?
                //internal Builder() { }
            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}, Birthday: {DateOfBirth.AddYears(-28).ToString("MM/dd/yyyy")}";
            }

        }

        // PersonBuilder Template

        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }
        }

        // Different Builders that inherit from each other

        public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
        {
            public SELF Called(string name)
            {
                person.Name = name;
                return (SELF)this;
            }
        }

        public class PersonJobBuilder<SELF>: PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorksAsA(string position)
            {
                person.Position = position;
                return (SELF)this;
            }
        }

        public class PersonBirthDateBuilder<SELF>: PersonJobBuilder<PersonBirthDateBuilder<SELF>>
        where SELF : PersonBirthDateBuilder<SELF>
        {
            public SELF Born(DateTime dateOfBirth)
            {
                person.DateOfBirth = dateOfBirth;
                return (SELF)this;
            }
        }

        public static void Main(string[] args)
        {
            Person p = Person.New
              .Called("Stefan")
              .WorksAsA("Dev")
              .Born(DateTime.UtcNow)
              .Build();
            Console.WriteLine(p);

            // This has been added for debug purposes to better understand the mechanism, HOVER over var:
            Console.WriteLine();

            Person.Builder p0 = Person.New;
            Console.WriteLine($"{nameof(p0)}:{p0.GetType().Name}");

            var p1 = p0.Called("Debugger");
            Console.WriteLine($"{nameof(p1)}:{p1.GetType().Name}");

            var p2 = p1.WorksAsA("Magic");
            Console.WriteLine($"{nameof(p2)}:{p2.GetType().Name}");

            var p3 = p2.Born(DateTime.UtcNow);
            Console.WriteLine($"{nameof(p3)}:{p3.GetType().Name}");

            var p4 = p3.Build();
            Console.WriteLine($"{nameof(p4)}:{p4.GetType().Name}");


            Console.WriteLine();
            Console.WriteLine(p4);
            Console.ReadLine();
        }


    }
}
