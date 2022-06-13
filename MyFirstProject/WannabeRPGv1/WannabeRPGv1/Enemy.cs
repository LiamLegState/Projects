using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace WannabeRPGv1
{
    class Enemy : Player
    {
        int _health = 1;
        int _minDmg;
        int _maxDmg;
        int _attDmg;
        bool _objectSpawned;
        bool _specialMove;
        string _enemyName;
        MediaElement _attSound;
        Image _img;
        ProgressBar _healthBar;
        static List<Enemy> _slimes = new List<Enemy>();

        public Image EnImg { get { return _img; } set { _img = value; } }
        public static List<Enemy> Enemies { get { return _slimes; } set { _slimes = value; } }
        public bool ObjectSpawned { get { return _objectSpawned; } set { _objectSpawned = value; } }
        public int EnemyHealth { get { return _health; } set { _health = value; } }
        public int EnemyDmg { get { return _attDmg; } set { _attDmg = value; } }
        public ProgressBar EnemyBar { get { return _healthBar; } set { _healthBar = value; } }
        public string EnemyName { get { return _enemyName; } set { _enemyName = value; } }
        public MediaElement AttSound { get { return _attSound; } set { _attSound = value; } }


        public Enemy(Image image, ProgressBar monsterHP, int gridRow, int gridColumn, string enemy) : base(image, monsterHP, gridRow, gridColumn)
        {
            _enemyName = enemy;
            Grid.SetRow(image, gridRow);
            Grid.SetColumn(image, gridColumn);
            _img = image;
            _healthBar = monsterHP;

            switch (enemy)
            {
                case "slime":
                    _health = 7;
                    _minDmg = 2;
                    _healthBar.Width = _health * 20;
                    _img.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeIdle.gif"));
                    Enemies.Add(this);
                    break;

                case "minotaur":
                    _health = 80;
                    _minDmg = 4;
                    _healthBar.Width = _health * 5;
                    _img.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossIdle.gif"));
                    Enemies.Add(this);
                    break;
            }
            _maxDmg = _minDmg * 2;
        }

        public override void NormalAttack(MediaElement sound)
        {
            if (this._enemyName == "slime")
            {
                _img.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeAtt.gif"));
            }

            if (this._enemyName == "minotaur")
            {
                _img.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossAtt.gif"));
            }

            _attDmg = rnd.Next(_minDmg, _maxDmg++);
        }
    }
}
