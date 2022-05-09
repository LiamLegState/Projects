using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VendingMachine
{
    class Beverage
    {
        string _name;
        int _price;

        public string Name { get { return _name; } }

        TextBlock _tb;
        StringBuilder _sb;
        List<string> _ingredients = new List<string>();

        public Beverage(string beverageName, int beveragePrice, TextBlock tb, StringBuilder sb, params string[] ingredients)
        {
            _name = beverageName;
            _price = beveragePrice;
            _sb = sb;
            _tb = tb;

            foreach (string item in ingredients)
            {
                _ingredients.Add(item);
            }
        }

        public void Prepare()
        {
            _sb.Clear();
            AddIngredients(_sb);
            AddWater(_sb);
            Stirring(_sb);
            _tb.Text = _sb.ToString();
        }

        public virtual void AddIngredients(StringBuilder sb)
        {
            sb.AppendLine("Adding ingredients...");
            sb.AppendLine("");

            foreach (string item in _ingredients)
            {
                sb.AppendLine(item);
            }
            sb.AppendLine("");
        }

        public virtual void AddWater(StringBuilder sb)
        {
            sb.AppendLine("Adding hot water...");
        }

        public virtual void Stirring(StringBuilder sb)
        {
            sb.AppendLine("Stirring...");
        }

        public override bool Equals(object obj)
        {
            Beverage temp;

            if(obj is Beverage)
            {
                temp = (Beverage)obj;
                if(temp._price == this._price)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"{_name} - {_price}";
        }
    }
}
