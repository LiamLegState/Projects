using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VendingMachine
{
    public sealed partial class MainPage : Page
    {
        StringBuilder _sb;
        Manager _mg;

        public MainPage()
        {
            this.InitializeComponent();

            _sb = new StringBuilder();
            _mg = new Manager(HotVanilla_ToString, IrishCoffee_ToString, Espresso_ToString, Latte_ToString, Tea_ToString, HotChocolate_ToString, TBlock, _sb, Espresso, Latte, Tea, HotChocolate, HotVanilla, IrishCoffee);
            Beverage _hotVanilla = new HotVanilla("HotVanilla", 15, TBlock, _sb, "milk", "white sugar", "vanilla extract", "ground cinnamon");
            Beverage _irishCoffee = new IrishCoffee("IrishCoffee", 15, TBlock, _sb, "brewed hot coffee", "brown sugar", "jigger Irish whiskey", "heavy cream");

            _mg.AddBeverage(IrishCoffee_ToString, _irishCoffee, IrishCoffee);
            _mg.AddBeverage(HotVanilla_ToString, _hotVanilla, HotVanilla);
        }
    }
}
