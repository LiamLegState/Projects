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
    class GameManager
    {
        int _easyModeCtr;
        int _hardModeCtr;
        int _currentIndex;
        bool _gameStarted;
        bool _hardMode = false;
        string _currentWord;
        string _editedWord;
        string _hiddenWord;
        string[] _wordArr = { "ant", "bee", "spider", "beetle", "cockroach", "worm", "mosquito" };

        Image _hangman;
        Image _gameOver;
        Image _gameWon;
        AppBarButton _newGame;
        AppBarToggleButton _diffToggle;
        TextBlock _textBl;
        List<Button> _keyboard;

        public int EasyModeCtr { get { return _easyModeCtr; } set { _easyModeCtr = value; } }
        public int HardModeCtr { get { return _hardModeCtr; } set { _hardModeCtr = value; } }
        public int CurrentIndex { get { return _currentIndex; } set { _currentIndex = value; } }
        public bool GameStarted { get { return _gameStarted; } set { _gameStarted = value; } }
        public bool HardMode { get { return _hardMode; } set { _hardMode = value; } }
        public string CurrentWord { get { return _currentWord; } set { _currentWord = value; } }
        public string EditedWord { get { return _editedWord; } set { _editedWord = value; } }
        public string HiddenWord { get { return _hiddenWord; } set { _hiddenWord = value; } }
        public string[] WordArr { get { return _wordArr; } set { _wordArr = value; } }
        public Image Hangman { get { return _hangman; } set { _hangman = value; } }
        public Image GameOver { get { return _gameOver; } set { _gameOver = value; } }
        public Image GameWon { get { return _gameWon; } set { _gameWon = value; } }
        public AppBarButton NewGame { get { return _newGame; } set { _newGame = value; } }
        public AppBarToggleButton DiffToggle { get { return _diffToggle; } set { _diffToggle = value; } }
        public TextBlock TextBl { get { return _textBl; } set { _textBl = value; } }
        public List<Button> Keyboard { get { return _keyboard; } set { _keyboard = value; } }


        public GameManager()
        {

        }

        public GameManager(List<Button> keyboard, Image hangman, Image gameOver, Image gameWon, AppBarButton newGame, AppBarToggleButton difficulty, TextBlock textBl) : this()
        {
            _keyboard = keyboard;
            _hangman = hangman;
            _gameOver = gameOver;
            _gameWon = gameWon;
            _newGame = newGame;
            _diffToggle = difficulty;
            _textBl = textBl;

            foreach (Button button in keyboard)
            {
                button.Click += Button_Click;
            }
        }

        #region Clicking 'letter' button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_gameStarted == false)
            {
                return;
            }

            Button btn = sender as Button;
            string tag = btn.Tag.ToString();
            char tagChar = char.Parse(tag);
            StringBuilder sb = new StringBuilder(_hiddenWord);

            for (int i = 0; i < _hiddenWord.Length; i++)
            {
                if (_editedWord[i] == tagChar)
                {
                    sb[i] = tagChar;
                    btn.IsEnabled = false;
                }
            }

            if (_editedWord.IndexOf(tagChar) == -1)
            {
                btn.IsEnabled = false;
                if (_hardMode)
                {
                    _hardModeCtr++;
                }
                _easyModeCtr++;
            }

            if (_hardMode)
            {
                // Hard difficulty
                switch (_hardModeCtr)
                {
                    case 1:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr1.png"));
                        break;

                    case 2:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr4.png"));
                        break;

                    case 3:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr6.png"));
                        _gameOver.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameOver.gif"));
                        _gameOver.Visibility = Visibility.Visible;
                        _gameStarted = false;
                        _hardModeCtr = 0;
                        _diffToggle.IsEnabled = true;
                        break;
                }
            }

            else
            {
                // Easy difficulty
                switch (_easyModeCtr)
                {
                    case 1:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr1.png"));
                        break;
                    case 2:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr2.png"));
                        break;
                    case 3:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr3.png"));
                        break;
                    case 4:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr4.png"));
                        break;
                    case 5:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr5.png"));
                        break;
                    case 6:
                        _hangman.Source = new BitmapImage(new Uri("ms-appx:///Assets/ctr6.png"));
                        _gameOver.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameOver.gif"));
                        _gameOver.Visibility = Visibility.Visible;
                        _gameStarted = false;
                        _easyModeCtr = 0;
                        _diffToggle.IsEnabled = true;
                        break;
                }
            }

            _hiddenWord = sb.ToString();
            _textBl.Text = _hiddenWord;

            if (_hiddenWord.IndexOf('_') == -1)
            {
                _gameWon.Visibility = Visibility.Visible;
                _gameStarted = false;
            }
        }
        #endregion
    }
}
