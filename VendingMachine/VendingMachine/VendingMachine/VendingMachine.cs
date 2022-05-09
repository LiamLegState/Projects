using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VendingMachine
{
    class VendingMachine
    {
        int _beveragesAmount;
        Beverage _beverage;
        List<Beverage> _beverageKinds;

        public VendingMachine(List<Beverage> beverages, params Image[] images)
        {
            _beverageKinds = beverages;
            _beveragesAmount = _beverageKinds.Count;

            foreach (Image item in images)
            {
                item.PointerPressed += ChoosingDrink;
            }
        }

        public void ChoosingDrink(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Image temp;
            foreach (Beverage item in _beverageKinds)
            {
                if(sender is Image)
                {
                    temp = (Image)sender;

                    if (temp.Name == item.Name)
                    {
                        _beverage = item;
                        break;
                    }
                }   
            }
            _beverage.Prepare();
        }
    }
}
