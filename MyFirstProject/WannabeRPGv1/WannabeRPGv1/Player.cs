using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace WannabeRPGv1
{
    public class Player
    {
        int _level = 1;
        int _health = 20;
        int _xp = 0;
        int _minDmg = 1;
        int _maxDmg;
        int _attDmg;
        Image _img;
        ProgressBar _healthBar;
        protected Random rnd = new Random();

        public Image Img { get { return _img; } set { _img = value; } }
        public ProgressBar PlayerBar { get { return _healthBar; } set { _healthBar = value; } }
        public int PlayerDmg { get { return _attDmg; } set { _attDmg = value; } }
        public int Health { get { return _health; } set { _health = value; } }
        public int XP { get { return _xp; } set { _xp = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public int MinDmg { get { return _minDmg; } set { _minDmg = value; } }
        public int MaxDmg { get { return _maxDmg; } set { _maxDmg = value; } }

        public Player(Image image, ProgressBar playerHP, int gridRow, int gridColumn)
        {
            Grid.SetRow(image, gridRow);
            Grid.SetColumn(image, gridColumn);
            _img = image;
            _healthBar = playerHP;
            _healthBar.Width = _health * 20;

            if (_xp == 100)
            {
                _level++;
                _xp = 0;
                _health *= 3;
                _minDmg = _maxDmg / 3;
            }

            _maxDmg = _level * 3;
        }

        public virtual void NormalAttack(MediaElement sound)
        {
            _img.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharacterAtt.gif"));
            _img.Margin = new Thickness(0, 0, 0, -30);
            // sound.Play();
            _attDmg = rnd.Next(_minDmg, _maxDmg++);
        }
    }
}
