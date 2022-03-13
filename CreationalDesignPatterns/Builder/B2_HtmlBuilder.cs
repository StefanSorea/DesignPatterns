using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.HtmlBuilder
{
    // This is the continuation of the String Builder example. Here we shall create a custom HTML Builder that will allow us to more easily output HTML code.

    class HtmlElement
    {
        public string TagName, InnerHtml;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string elementTagName, string innerHtmlText)
        {
            TagName = elementTagName;
            InnerHtml = innerHtmlText;
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new StringBuilder();
            string i = new string(' ', indentSize * indent);

            sb.Append($"{i}<{TagName}>\n");

            if (!string.IsNullOrWhiteSpace(InnerHtml))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(InnerHtml);
                sb.Append("\n");
            }

            foreach (var e in Elements)
                sb.Append(e.ToStringImpl(indent + 1));

            sb.Append($"{i}</{TagName}>\n");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }

    class HtmlBuilder
    {
        private readonly string rootTagName;

        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootTagName = rootName;
            root.TagName = rootName;
        }

        // Non Fluent Builder method
        public void AddChild(string childTagName, string childInnerHtml)
        {
            var e = new HtmlElement(childTagName, childInnerHtml);
            root.Elements.Add(e);
        }

        // Fluent Builder method

        public HtmlBuilder AddChildFluent(string childTagName, string childInnerHtml)
        {
            var e = new HtmlElement(childTagName, childInnerHtml);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void ClearInnerHtml()
        {
            root = new HtmlElement { TagName = rootTagName };
        }



        
    }

    class Demo
    {
        static void Main(string[] args)
        {
            // non-fluent builder
            HtmlBuilder htmlBuilder = new HtmlBuilder("ul");
            htmlBuilder.AddChild("li", "hello");
            htmlBuilder.AddChild("li", "world");

            Console.WriteLine(htmlBuilder.ToString());

            // fluent builder

            // disengage builder from the object it's building (but preserve it's root element => if you want to have a different root element, you need to create a new HtmlBuilder), then...
            htmlBuilder.ClearInnerHtml();

            htmlBuilder
                .AddChildFluent("li", "hello")
                .AddChildFluent("li", "world");

            Console.WriteLine(htmlBuilder);

            Console.ReadLine();
        }

    }
}
