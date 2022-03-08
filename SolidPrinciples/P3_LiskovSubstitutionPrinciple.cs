using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SolidPrinciples.LiskovSubstitutionPrinciple
{
    public class LiskovSubstitutionPrinciple
    {

        public class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle() { }

            public Rectangle(int w, int h)
            {
                this.Width = w;
                this.Height = h;
            }

            public override string ToString()
            {
                return $" The {this.GetType().Name} has the {nameof(Width)}: {Width} and the {nameof(Height)}: {Height}.";
            }
        }

        public class Areas
        {
            public static int CalculateRectangleArea(Rectangle r)
            {
                return r.Width * r.Height;
            }
        }

        public class Square : Rectangle
        {

            public Square() { }
            public Square(int l)
            {
                base.Width = base.Height = l;
            }

            public override int Width { set { base.Width = base.Height = value; } }
            public override int Height { set { base.Height = base.Width = value; } }
        }


        static void Main(string[] args)
        {
            Rectangle r1 = new Rectangle(4, 4);
            Console.WriteLine(r1.ToString() + $" And has area {Areas.CalculateRectangleArea(r1)}");

            // Will Work

            Square s1 = new Square(4);
            Console.WriteLine(s1.ToString() + $" And has area {Areas.CalculateRectangleArea(s1)}");

            Square s2 = new Square();
            s2.Width = 4;
            Console.WriteLine(s2.ToString() + $" And has area {Areas.CalculateRectangleArea(s2)}");


            // Will Work now

            Rectangle s3 = new Square();
            s3.Width = 4;
            Console.WriteLine(s3.ToString() + $" And has area {Areas.CalculateRectangleArea(s3)}");

            Console.ReadLine();

            


        }
    }
    
}
