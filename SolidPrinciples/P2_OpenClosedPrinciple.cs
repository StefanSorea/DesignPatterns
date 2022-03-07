using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SolidPrinciples.OpenClosedPrinciple
{
    public class OpenClosedPrinciple
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

        public interface ISpecification<T>
        {
            bool isSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color someColor)
            {
                this.color = someColor;
            }
            public bool isSatisfied(Product t)
            {
                return t.productColor == color;
            }

        }
        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size someSize)
            {
                this.size = someSize;
            }
            public bool isSatisfied(Product t)
            {
                return t.productSize == size;
            }

        }

        public class AndSpecification : ISpecification<Product>
        {
            private ISpecification<Product>[] multipleSpecifications;

            public AndSpecification(params ISpecification<Product>[] multipleSpecifications)
            {
                this.multipleSpecifications = multipleSpecifications;
            }
            public bool isSatisfied(Product t)
            {
                foreach(ISpecification<Product> spec in multipleSpecifications)
                {
                    if (!spec.isSatisfied(t)) { return false; }
                }

                return true;
            }
        }

        public class ProductFilter: IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach(Product p in items)
                {
                    if (spec.isSatisfied(p))
                        yield return p;
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

            ProductFilter pf = new ProductFilter();

            foreach(Product p in pf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"{p.productName} is Medium sized.");
            }

            foreach (Product p in pf.Filter(products,  new AndSpecification(new ColorSpecification(Color.Green), new SizeSpecification(Size.Small) )))
            {
                Console.WriteLine($"{p.productName} is Small sized and has the color Red.");
            }



            Console.ReadLine();


        }
        
    }
}
