using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Hangman
{
    class Menu
    {
        Random rnd = new Random();
        GameManager gm;

        public Menu(GameManager gManager)
        {
            gm = gManager;
            gm.NewGame.Click += NewGame_Click;
            gm.DiffToggle.Checked += DiffToggle_On;
            gm.DiffToggle.Unchecked += DiffToggle_Off;
        }

        // Start new game
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in gm.Keyboard)
            {
                button.IsEnabled = true;
            }

            gm.GameStarted = true;
            gm.Hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr0.png"));
            gm.HiddenWord = String.Empty;
            gm.EditedWord = String.Empty;
            gm.EasyModeCtr = 0;
            gm.HardModeCtr = 0;
            gm.DiffToggle.IsEnabled = false;
            gm.GameOver.Visibility = Visibility.Collapsed;
            gm.GameWon.Visibility = Visibility.Collapsed;
            gm.CurrentIndex = rnd.Next(0, 7);
            gm.CurrentWord = gm.WordArr[gm.CurrentIndex];

            foreach (char c in gm.CurrentWord)
            {
                gm.HiddenWord += "_" + " ";
                gm.EditedWord += c + " ";
            }
            gm.TextBl.Text = gm.HiddenWord;
        }

        //Change to hard difficulty
        private void DiffToggle_On(object sender, RoutedEventArgs e)
        {
            BitmapIcon Hard = new BitmapIcon();
            Hard.UriSource = new Uri("ms-appx:///Assets/Hard.png");
            gm.DiffToggle.Icon = Hard;
            gm.HardMode = true;
        }

        //Change to easy difficulty
        private void DiffToggle_Off(object sender, RoutedEventArgs e)
        {
            BitmapIcon Easy = new BitmapIcon();
            Easy.UriSource = new Uri("ms-appx:///Assets/Easy.png");
            gm.DiffToggle.Icon = Easy;
            gm.HardMode = false;
        }
    }
}
