using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SolidPrinciples.InterfaceSegregationPrinciple
{
    public class InterfaceSegregationPrinciple
    {

        public class Document
        {
        }

        public interface IMachine
        {
            void Print(Document d);
            void Fax(Document d);
            void Scan(Document d);
        }

        // ok if you need a multifunction machine
        public class MultiFunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
                //
            }

            public void Fax(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        // Here is the problem
        public class OldFashionedPrinter : IMachine
        {
            public void Print(Document d)
            {
                // yep
            }

            public void Fax(Document d)
            {
                // excess code
                throw new System.NotImplementedException();
            }

            public void Scan(Document d)
            {
                // excess code
                throw new System.NotImplementedException();
            }
        }

        // ^^^^^^^^^^^^^^^
        // THE PROBLEM 

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public class Printer : IPrinter
        {
            public void Print(Document d)
            {

            }
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                throw new System.NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new System.NotImplementedException();
            }
        }

        public interface IMultiFunctionDevice : IPrinter, IScanner //
        {

        }

        public struct MultiFunctionMachine : IMultiFunctionDevice
        {
            // compose this out of several modules
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                if (printer == null)
                {
                    throw new ArgumentNullException(paramName: nameof(printer));
                }
                if (scanner == null)
                {
                    throw new ArgumentNullException(paramName: nameof(scanner));
                }
                this.printer = printer;
                this.scanner = scanner;
            }

            public void Print(Document d)
            {
                printer.Print(d);
            }

            public void Scan(Document d)
            {
                scanner.Scan(d);
            }

            // Decorator pattern TO LEARN
        }

  

        static void Main(string[] args)
        {
           
        }
        
    }
}
