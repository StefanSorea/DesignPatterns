using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SolidPrinciples.DependencyInversionPrinciple
{
    public class DependencyInversionPrinciple
    {
        public enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        public class Person
        {
            public string Name;
            public DateTime DateOfBirth;
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        // LOW-LEVEL PART OF THE SYSTEM
        public class Relationships : IRelationshipBrowser 
        {
            private List<(Person, Relationship, Person)> relations
              = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }
  
            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations
                  .Where(x => x.Item1.Name == name
                              && x.Item2 == Relationship.Parent).Select(r => r.Item3);
            }
        }

        // HIGH-LEVEL PART OF THE SYSTEM: Find all of John's children

        public class Research
        { 
            public Research(IRelationshipBrowser browser)
            {
                foreach (var p in browser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {p.Name}");
                }
            }
        }


        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Matt" };

            // low-level module
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);

            Console.ReadLine();

        }

    }
}
