using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace VendingMachine
{
    class Manager
    {
        StringBuilder _sb;
        TextBlock _tb;
        VendingMachine _vm;
        List<Beverage> _beverages = new List<Beverage>();

        public Manager(TextBlock hotVanilla, TextBlock irishCoffee, TextBlock espresso, TextBlock latte, TextBlock tea, TextBlock hotChocolate, TextBlock tb, StringBuilder sb, params Image[] images)
        {
            _sb = sb;
            _tb = tb;
            Beverage _espresso = new Espresso("Espresso", 8, _tb, _sb, "ground coffee");
            Beverage _latteCoffee = new LatteCoffee("Latte", 12, _tb, _sb, "milk", "hot dark roast espresso coffee");
            Beverage _tea = new Tea("Tea", 12, _tb, _sb, "ginger", "tea leaves", "Lemon juice", "honey");
            Beverage _hotChocolate = new HotChocolate("HotChocolate", 15, _tb, _sb, "cocoa powder", "whole milk", "semisweet chocolate");

            espresso.Text = _espresso.ToString();
            latte.Text = _latteCoffee.ToString();
            tea.Text = _tea.ToString();
            hotChocolate.Text = _hotChocolate.ToString();

            _beverages.Add(_espresso);
            _beverages.Add(_latteCoffee);
            _beverages.Add(_tea);
            _beverages.Add(_hotChocolate);

            _vm = new VendingMachine(_beverages, images[0], images[1], images[2], images[3]);
        }

        public void AddBeverage(TextBlock tb, Beverage beverage, Image image)
        {
            image.PointerPressed += _vm.ChoosingDrink;
            tb.Text = beverage.ToString();
            _beverages.Add(beverage);
        }

        public void RemoveBeverage(TextBlock tb, Beverage beverage, Image image)
        {
            image.PointerPressed -= _vm.ChoosingDrink;
            tb.Text = "";
            _beverages.Remove(beverage);
            image.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
