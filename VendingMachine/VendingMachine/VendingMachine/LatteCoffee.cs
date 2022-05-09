using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VendingMachine
{
    class LatteCoffee : Beverage
    {
        List<string> _ingredients = new List<string>();

        public LatteCoffee(string beverageName, int beveragePrice, TextBlock tb, StringBuilder sb, params string[] ingredients) : base(beverageName, beveragePrice, tb, sb)
        {
            foreach (string item in ingredients)
            {
                _ingredients.Add(item);
            }
        }

        public override void AddIngredients(StringBuilder sb)
        {
            sb.AppendLine("Adding latte coffee ingredients...");
            sb.AppendLine("");

            foreach (string item in _ingredients)
            {
                sb.AppendLine(item);
            }
            sb.AppendLine("");
        }

        public override void AddWater(StringBuilder sb)
        {
            sb.AppendLine("Adding hot water...");
        }

        public override void Stirring(StringBuilder sb)
        {
            sb.AppendLine("Stirring...");
        }
    }
}
