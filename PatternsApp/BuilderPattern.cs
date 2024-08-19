using Dynamitey;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    #region singleBuilder
    public class HtmlElement
    {
        public string Name = string.Empty, Text = string.Empty;

        public List<HtmlElement> Elements = new List<HtmlElement>();

        private const int indentSize = 2;

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indent * indentSize);
            sb.Append(i + "<" + Name + ">\n");
            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 11)));
                sb.Append(Text);
                sb.Append("\n");
            }

            foreach (var element in Elements)
            {
                sb.Append(element.ToStringImpl(indent + 1));
            }

            sb.Append(i + "</" + Name + ">\n");
            return sb.ToString();
        }

        public static HtmlBuilder Create(string name)
        {
            return new HtmlBuilder(name);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        protected HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement() { Name = rootName };
        }

        public HtmlElement Build() => root;

        public static implicit operator HtmlElement(HtmlBuilder builder)
        {
            return builder.root;
        }
    }
    #endregion

    #region ManyBuilders

    public class Person
    {
        public string StreetAdress = string.Empty, Postcode = string.Empty, City = string.Empty;
        public string CompanyName = string.Empty, Position = string.Empty;
        public int AnnualIncome;

        public override string ToString()
        {
            return nameof(StreetAdress) + " " + StreetAdress + " " + nameof(Postcode)
                + " " + Postcode + " " + nameof(City) + " " + City + " " + nameof(CompanyName) + " " + CompanyName
                + " " + nameof(Position) + " " + Position + " " + nameof(AnnualIncome) + " " + AnnualIncome;
        }
    }

    public class PersonBuilder
    {
        protected Person person;

        public PersonBuilder()
        {
            person = new Person();
        }
        public PersonBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAdressBuilder Lives() { return new PersonAdressBuilder(person); }

        public PersonJobBuilder Works() { return new PersonJobBuilder(person); }

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAdressBuilder : PersonBuilder
    {
        public PersonAdressBuilder(Person person) : base(person)
        {
        }

        public PersonAdressBuilder At(string streetAdress)
        {
            person.StreetAdress = streetAdress;
            return this;
        }

        public PersonAdressBuilder WithPostalCode(string postalCode)
        {
            person.Postcode = postalCode;
            return this;
        }

        public PersonAdressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person) : base(person)
        {
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Erning(int anualIncome)
        {
            person.AnnualIncome = anualIncome;
            return this;
        }
    }
    #endregion

    #region singleBuilderForTask

    public class CodeElement
    {
        public string ClassName;

        public List<(string, string)> Elements = new List<(string, string)>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("public class " + ClassName + Environment.NewLine + "{" + Environment.NewLine);

            if (Elements != null)
            {
                foreach (var element in Elements)
                {
                    sb.Append("  public " + element.Item2 + " " + element.Item1 + ";" + Environment.NewLine);
                }
            }

            sb.Append("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder
    {
        protected CodeElement root = new CodeElement();

        public CodeBuilder(string className)
        {
            this.root.ClassName = className;
        }

        public CodeBuilder AddField(string childValue, string childType)
        {
            root.Elements.Add(new(childValue, childType));
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }
    }

    #endregion

    #region RecursiveGenericsInheritanceForBuilders
    public class PersonG
    {
        public string Name;
        public string Position;

        public class BuilderLastClass : PersonGJobBuilder<BuilderLastClass>
        {

        }

        public static BuilderLastClass New()
        {
            return new BuilderLastClass();
        }
    }

    public abstract class PersonGBuilder
    {
        public PersonG personG = new PersonG();

        public PersonG Build()
        {
            return personG;
        }
    }

    public class PersonGInfoBuilder<SELF> : PersonGBuilder where SELF : PersonGInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            personG.Name = name;
            return (SELF)this;
        }
    }

    public class PersonGJobBuilder<SELF> : PersonGInfoBuilder<SELF> where SELF : PersonGJobBuilder<SELF>
    {
        public SELF WorkAs(string position)
        {
            personG.Position = position;
            return (SELF)this;
        }
    }

    // var personMe = PersonG.New().Called("Name").WorkAs("Admin").Build();


    #endregion

    #region FunctionalBuilderForOpenClosePrincipal

    public class PersonF
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf> where TSelf : FunctionalBuilder<TSubject, TSelf> where TSubject : new()
    {
        private readonly List<Func<TSubject, TSubject>> actions
       = new List<Func<TSubject, TSubject>>();

        public TSelf Do(Action<TSubject> action)
        {
            return AddAction(action);
        }

        private TSelf AddAction(Action<TSubject> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });

            return (TSelf)this;
        }

        public TSubject Build()
        {
            return actions.Aggregate(new TSubject(), (p, f) => f(p));
        }
    }

    public sealed class PersonFBuilder : FunctionalBuilder<PersonF, PersonFBuilder>
    {
        public PersonFBuilder Called(string name)
        {
            return Do(p => p.Name = name);
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonFBuilder WorksAsA
          (this PersonFBuilder builder, string position)
        {
            builder.Do(p => p.Position = position);
            return builder;
        }
    }


    #endregion


}


