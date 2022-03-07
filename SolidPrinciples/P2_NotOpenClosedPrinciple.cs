using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SolidPrinciples.OpenClosedPrinciple
{
    public class NotOpenClosedPrinciple
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large
        }

        public class Product
        {
            public string productName;
            public Color productColor;
            public Size productSize;

            public Product(string name, Color color, Size size)
            {
                this.productName = name;
                this.productColor = color;
                this.productSize = size;

            }      
        }

        public class ProductFilter
        {
            public static IEnumerable<Product> FilterBySize(IEnumerable<Product>products, Size size)
            {
                foreach(Product p in products)
                {
                    if(p.productSize == size)
                    {
                        yield return p;
                    }
                }
            }

            public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (Product p in products)
                {
                    if (p.productColor == color)
                    {
                        yield return p;
                    }
                }
            }

            public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
            {
                foreach (Product p in products)
                {
                    if (p.productSize == size && p.productColor == color)
                    {
                        yield return p;
                    }
                }
            }

        }

        static void Main(string[] args)
        {
            Product p1 = new Product("p1", Color.Blue, Size.Large);
            Product p2 = new Product("p2", Color.Red, Size.Small);
            Product p3 = new Product("p3", Color.Green, Size.Small);
            Product p4 = new Product("p4", Color.Green, Size.Medium);
            Product p5= new Product("p5", Color.Green, Size.Medium);

            Product[] products = { p1, p2, p3, p4, p5 };

            foreach(Product p in ProductFilter.FilterBySize(products, Size.Medium))
            {
                Console.WriteLine($"{p.productName} is Medium sized.");
            }

            foreach (Product p in ProductFilter.FilterBySizeAndColor(products, Size.Small, Color.Red))
            {
                Console.WriteLine($"{p.productName} is Small sized and has the color Red.");
            }



            Console.ReadLine();


        }
        
    }
}
