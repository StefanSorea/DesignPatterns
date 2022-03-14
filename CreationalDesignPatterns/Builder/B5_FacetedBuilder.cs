using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.FacetedBuilder
{
    // The Faceted Builder Design Pattern exists to allow the user to build different logical parts of the object that is beeing built.
    // In this case, we will build Employees that are composed of (has fields) different Logical Parts.
    class FacetedBuilder
    {
        public class Employee
        {

            // Personal Info Logical Part

            public string FirstName, LastName;
            public bool Married;

            // Employement Information Logical Part
            public string CompanyName, Position;

            public int AnnualIncome;

            // Address Logical Part
            public string StreetAddress, Postcode, City;

       

            public override string ToString()
            {
                return $"{LastName} {FirstName} is {((Married == true)? "Married" : "not Married" )}," +
                    $" Works at: {CompanyName} as a/an {Position}, has an {nameof(AnnualIncome)} of {AnnualIncome} RON and" +
                    $" Lives  in {City} on {StreetAddress}, {nameof(Postcode)}: {Postcode}.";
                    
            }
        }

        public class EmployeeBuilder // facade 
        {
            // the object we're going to build
            protected Employee person = new Employee(); // this is a reference!

            public EmployeeAddressBuilder Lives => new EmployeeAddressBuilder(person);
            public EmployeeJobBuilder Works => new EmployeeJobBuilder(person);

            public EmployeeIdentityBuilder Identity => new EmployeeIdentityBuilder(person);

            public static implicit operator Employee(EmployeeBuilder pb)
            {
                return pb.person;
            }
        }

        public class EmployeeIdentityBuilder : EmployeeBuilder
        {
            public EmployeeIdentityBuilder(Employee person)
            {
                this.person = person;
            }

            public EmployeeIdentityBuilder Named(string lastName, string firstName)
            {
                person.LastName = lastName;
                person.FirstName = firstName;
                return this;
            }

            public EmployeeIdentityBuilder isMarried(bool married)
            {
                person.Married = married;
                return this;
            }



        }

        public class EmployeeJobBuilder : EmployeeBuilder
        {
            public EmployeeJobBuilder(Employee person)
            {
                this.person = person;
            }

            public EmployeeJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public EmployeeJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }

            public EmployeeJobBuilder Earning(int annualIncome)
            {
                person.AnnualIncome = annualIncome;
                return this;
            }
        }

        public class EmployeeAddressBuilder : EmployeeBuilder
        {
            // might not work with a value type!
            public EmployeeAddressBuilder(Employee person)
            {
                this.person = person;
            }

            public EmployeeAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public EmployeeAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public EmployeeAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }

        }

        static void Main(string[] args)
        {
            EmployeeBuilder eb = new EmployeeBuilder();
            Employee employee = eb
                .Identity
                    .Named("Penn", "Sean")
                    .isMarried(false)        
                .Works
                    .At("Microsoft")
                    .AsA("Floor Manager")
                    .Earning(2550)
                .Lives
                    .At("Street Hope, Sector 1")
                    .In("Bucharest")
                    .WithPostcode("ABCDE");
                

            Console.WriteLine(employee);
            Console.ReadLine();
        }





    }
}
