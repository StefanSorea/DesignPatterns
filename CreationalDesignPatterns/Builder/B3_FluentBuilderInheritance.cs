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
            var p = Person.New
              .Called("Stefan")
              .WorksAsA("Dev")
              .Born(DateTime.UtcNow)
              .Build();
            Console.WriteLine(p);
            Console.ReadLine();
        }


    }
}
