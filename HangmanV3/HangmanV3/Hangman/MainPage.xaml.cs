using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hangman
{
    public sealed partial class MainPage : Page
    {
        List<Button> _keyboard = new List<Button>();

        public MainPage()

        {
            this.InitializeComponent();

            _keyboard.Add(_buttonA);
            _keyboard.Add(_buttonB);
            _keyboard.Add(_buttonC);
            _keyboard.Add(_buttonD);
            _keyboard.Add(_buttonE);
            _keyboard.Add(_buttonF);
            _keyboard.Add(_buttonG);
            _keyboard.Add(_buttonH);
            _keyboard.Add(_buttonI);
            _keyboard.Add(_buttonJ);
            _keyboard.Add(_buttonK);
            _keyboard.Add(_buttonL);
            _keyboard.Add(_buttonM);
            _keyboard.Add(_buttonN);
            _keyboard.Add(_buttonO);
            _keyboard.Add(_buttonP);
            _keyboard.Add(_buttonQ);
            _keyboard.Add(_buttonR);
            _keyboard.Add(_buttonS);
            _keyboard.Add(_buttonT);
            _keyboard.Add(_buttonU);
            _keyboard.Add(_buttonV);
            _keyboard.Add(_buttonW);
            _keyboard.Add(_buttonX);
            _keyboard.Add(_buttonY);
            _keyboard.Add(_buttonZ);

            GameManager gm = new GameManager(_keyboard, _hangman, _gameOver, _gameWon, _newGame, _diffToggle, _textBl);
            Menu m = new Menu(gm);
        }
    }
}
