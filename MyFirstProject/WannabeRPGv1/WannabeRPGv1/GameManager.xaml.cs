using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace WannabeRPGv1
{
    public sealed partial class GameManager : Page
    {
        DispatcherTimer _timer;
        DispatcherTimer _levelUpTimer;

        DispatcherTimer _tpTimer;
        DispatcherTimer _attTimer;
        DispatcherTimer _playerAttTimer;

        DispatcherTimer _enemyTimer;
        DispatcherTimer _slimeMoveTimer;
        DispatcherTimer _slimeFlinchTimer;
        DispatcherTimer _slimeDeathTimer;
        DispatcherTimer _slimeAttTimer;
        DispatcherTimer _bossTimer;
        DispatcherTimer _bossMoveTimer;
        DispatcherTimer _bossFlinchTimer;
        DispatcherTimer _bossAttTimer;
        DispatcherTimer _bossDeathTimer;
        DispatcherTimer _bossSpMoveTimer;

        Player _player;
        Enemy _slime1;
        Enemy _slime2;
        Enemy _boss;
        Enemy _temp;

        List<Image> _pBlocks;
        List<Image> _eBlocks;

        bool _isGamePaused;
        bool _isPressed;
        bool _gameOver;
        bool _mapClear;
        bool _changedMap;

        string _key;
        int _movingAllowedCtr;
        int _enemyAttCtr = 0;
        int _spAttCtr = 0;
        double _enemyBarWidth; 
        double _playerBarWidth;
        double _pMaxHealth;
        double _sMaxHealth;

        public GameManager()
        {
            this.InitializeComponent();
            _player = new Player(player, playerHP, 0, 0);
            _slime1 = new Enemy(slime1, monsterHP1, 1, 5, "slime");
            _slime2 = new Enemy(slime2, monsterHP2, 2, 5, "slime");

            #region Timers

            // General game timers
            _timer = new DispatcherTimer();
            _levelUpTimer = new DispatcherTimer();

            // Player mechanics & animation related timers
            _tpTimer = new DispatcherTimer();
            _attTimer = new DispatcherTimer();
            _playerAttTimer = new DispatcherTimer();

            // Enemy mechanics & animation related timers
            _enemyTimer = new DispatcherTimer();
            _slimeMoveTimer = new DispatcherTimer();
            _slimeFlinchTimer = new DispatcherTimer();
            _slimeDeathTimer = new DispatcherTimer();
            _slimeAttTimer = new DispatcherTimer();
            _bossTimer = new DispatcherTimer();
            _bossFlinchTimer = new DispatcherTimer();
            _bossAttTimer = new DispatcherTimer();
            _bossSpMoveTimer = new DispatcherTimer();
            _bossDeathTimer = new DispatcherTimer();
            _bossMoveTimer = new DispatcherTimer();
            _pBlocks = new List<Image>();
            _eBlocks = new List<Image>();

            _pBlocks.Add(pBlock1);
            _pBlocks.Add(pBlock2);
            _pBlocks.Add(pBlock3);
            _pBlocks.Add(pBlock4);
            _pBlocks.Add(pBlock5);
            _pBlocks.Add(pBlock6);
            _pBlocks.Add(pBlock7);
            _pBlocks.Add(pBlock8);
            _pBlocks.Add(pBlock9);

            _eBlocks.Add(eBlock1);
            _eBlocks.Add(eBlock2);
            _eBlocks.Add(eBlock3);
            _eBlocks.Add(eBlock4);
            _eBlocks.Add(eBlock5);
            _eBlocks.Add(eBlock6);
            _eBlocks.Add(eBlock7);
            _eBlocks.Add(eBlock8);
            _eBlocks.Add(eBlock9);

            Window.Current.CoreWindow.KeyDown += User_KeyDown;
            Window.Current.CoreWindow.KeyUp += User_KeyUp;

            _timer.Tick += GameLoop;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timer.Start();

            _tpTimer.Tick += TpTimer;
            _tpTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);

            _attTimer.Tick += AttTimer;
            _attTimer.Interval = new TimeSpan(0, 0, 0, 1, 380);

            _playerAttTimer.Tick += PlayerAtt_Timer;
            _playerAttTimer.Interval = new TimeSpan(0, 0, 0, 0, 80);

            _enemyTimer.Tick += EnemyTimer;
            _enemyTimer.Interval = new TimeSpan(0, 0, 0, 0, 800);

            _slimeMoveTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            _slimeMoveTimer.Tick += SlimeMove_Timer;

            _slimeFlinchTimer.Tick += SlimeFlinch_Timer;
            _slimeFlinchTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);

            _slimeDeathTimer.Tick += SlimeDeath_Timer;
            _slimeDeathTimer.Interval = new TimeSpan(0, 0, 0, 0, 350);

            _slimeAttTimer.Tick += SlimeAtt_Timer;
            _slimeAttTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);

            _bossAttTimer.Tick += BossAtt_Timer;
            _bossAttTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

            _bossFlinchTimer.Tick += BossFlinch_Timer;
            _bossFlinchTimer.Interval = new TimeSpan(0, 0, 0, 0, 600);

            _bossDeathTimer.Tick += BossDeath_Timer;
            _bossDeathTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

            _bossSpMoveTimer.Tick += BossSpecialMove_Timer;
            _bossSpMoveTimer.Interval = new TimeSpan(0, 0, 0, 0, 900);

            _bossMoveTimer.Tick += BossMove_Timer;
            _bossMoveTimer.Interval = new TimeSpan(0, 0, 0, 0, 600);

            _levelUpTimer.Tick += LevelUp_Timer;
            _levelUpTimer.Interval = new TimeSpan(0, 0, 0, 2, 0);
        }

        private void BossMove_Timer(object sender, object e)
        {
            foreach (Enemy enemy in Enemy.Enemies)
            {
                if (enemy.EnemyName == "minotaur")
                {
                    enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossIdle.gif"));
                }
            }
            _bossMoveTimer.Stop();
        }

        private void SlimeMove_Timer(object sender, object e)
        {
            foreach (Enemy enemy in Enemy.Enemies)
            {
                if (enemy.EnemyName == "slime")
                {
                    enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeIdle.gif"));
                }
            }
            _slimeMoveTimer.Stop();
        }

        private void LevelUp_Timer(object sender, object e)
        {
            LevelUp.Visibility = Visibility.Collapsed;
            _levelUpTimer.Stop();
        }

        private void EnemyTimer(object sender, object e)
        {
            EnemyMechanics(Enemy.Enemies);
        }


        #region Enemy Mechanics
        private void EnemyMechanics(List<Enemy> list)
        {
            _enemyAttCtr++;

            if (!_gameOver && !_isGamePaused && !_playerAttTimer.IsEnabled)
            {
                if(_enemyAttCtr % 2 == 0)
                {
                    damage.Text = "";
                }

                foreach (Enemy enemy in list)
                {
                    if (Grid.GetRow(_player.Img) == Grid.GetRow(enemy.EnImg))
                    {
                        if (Grid.GetColumn(enemy.EnImg) <= Grid.GetColumn(_player.Img) + 2)
                        {
                                switch (enemy.EnemyName)
                                {
                                    case "slime":
                                        enemy.NormalAttack(enemy.AttSound);
                                        _slimeAttTimer.Start();
                                        break;

                                    case "minotaur":
                                    if (_enemyAttCtr % 2 == 0)
                                    {
                                        enemy.NormalAttack(enemy.AttSound);
                                        _bossAttTimer.Start();
                                    }
                                        break;
                                }
                            return;
                        }

                        if (Grid.GetColumn(enemy.EnImg) != 2)
                        {
                            Grid.SetColumn(enemy.EnImg, Grid.GetColumn(enemy.EnImg) - 1);

                            if(enemy.EnemyName == "slime")
                            {
                                enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeMove.gif"));
                                _slimeMoveTimer.Start();
                            }

                            else
                            {
                                enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossMove.gif"));
                                _bossMoveTimer.Start();
                            }
                                Grid.SetRow(damage, Grid.GetRow(enemy.EnImg));
                                Grid.SetColumn(damage, Grid.GetColumn(enemy.EnImg));
                            return;
                        }
                    }

                    else
                    {
                        _spAttCtr++;

                        if (enemy.EnemyName == "minotaur")
                        {
                            if (Grid.GetRow(enemy.EnImg) == Grid.GetRow(_player.Img) - 1 || Grid.GetRow(enemy.EnImg) == Grid.GetRow(_player.Img) + 1)
                            {
                                if (Grid.GetColumn(enemy.EnImg) - Grid.GetColumn(_player.Img) == 1)
                                {
                                    if (_spAttCtr % 2 == 0)
                                    {
                                        _bossSpMoveTimer.Start();
                                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpecialMove.gif"));

                                        Grid.SetRow(damage, Grid.GetRow(enemy.EnImg));
                                        Grid.SetColumn(damage, Grid.GetColumn(enemy.EnImg));
                                        return;
                                    }
                                }
                            }

                            if (Grid.GetRow(_player.Img) == 0)
                            {
                                Grid.SetRow(enemy.EnImg, Grid.GetRow(enemy.EnImg) - 1);
                                Grid.SetRow(damage, Grid.GetRow(enemy.EnImg));
                                Grid.SetColumn(damage, Grid.GetColumn(enemy.EnImg));
                                return;
                            }

                            if (Grid.GetRow(_player.Img) == 1)
                            {
                                Grid.SetRow(enemy.EnImg, 1);
                                Grid.SetRow(damage, 1);
                                Grid.SetColumn(damage, 1);
                                return;
                            }

                            if (Grid.GetRow(_player.Img) == 2)
                            {
                                Grid.SetRow(enemy.EnImg, Grid.GetRow(enemy.EnImg) + 1);
                                Grid.SetRow(damage, Grid.GetRow(enemy.EnImg));
                                Grid.SetColumn(damage, Grid.GetColumn(enemy.EnImg));
                                return;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        private void BossSpecialMove_Timer(object sender, object e)
        {
            if (!_isGamePaused && !_gameOver)
            {
                foreach (Enemy enemy in Enemy.Enemies)
                {
                    if (_player.Health > 0 && enemy.EnemyHealth > 0)
                    {
                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossIdle.gif"));

                        _playerBarWidth = _player.PlayerBar.Width;
                        _playerBarWidth -= ((enemy.EnemyDmg / _pMaxHealth) * _player.PlayerBar.Width);

                        if(_playerBarWidth >= 0)
                        {
                            _player.PlayerBar.Width = _playerBarWidth;
                        }

                        else
                        {
                            _player.PlayerBar.Visibility = Visibility.Collapsed;
                        }

                        _player.Health -= enemy.EnemyDmg;
                        _bossSpMoveTimer.Stop();
                        break;
                    }

                    if (_player.Health <= 0)
                    {
                        _player.PlayerBar.Visibility = Visibility.Collapsed;
                        _gameOver = true;
                        return;
                    }
                }
            }
        }

        private void BossAtt_Timer(object sender, object e)
        {
            if (!_isGamePaused && !_gameOver)
            {
                foreach (Enemy enemy in Enemy.Enemies)
                {
                    if (_player.Health > 0 && enemy.EnemyHealth > 0)
                    {
                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossIdle.gif"));

                        _playerBarWidth = _player.PlayerBar.Width;
                        _playerBarWidth -= ((enemy.EnemyDmg / _pMaxHealth) * _player.PlayerBar.Width);

                        if (_playerBarWidth >= 0)
                        {
                            _player.PlayerBar.Width = _playerBarWidth;
                        }

                        else
                        {
                            _player.PlayerBar.Visibility = Visibility.Collapsed;
                        }

                        _player.Health -= enemy.EnemyDmg;
                        _bossAttTimer.Stop();
                    }

                    if (_player.Health <= 0)
                    {
                        _player.PlayerBar.Visibility = Visibility.Collapsed;
                        _gameOver = true;
                        return;
                    }
                }
            }
        }

        private void BossFlinch_Timer(object sender, object e)
        {
            foreach (Enemy enemy in Enemy.Enemies)
            {
                if (enemy.EnemyName == "minotaur")
                {
                    enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossIdle.gif"));
                }
            }
            _bossFlinchTimer.Stop();
        }

        private void BossDeath_Timer(object sender, object e)
        {
            foreach (Enemy enemy in Enemy.Enemies)
            {
                if (enemy.EnemyHealth <= 0)
                {
                   enemy.EnemyBar.Visibility = Visibility.Collapsed;
                    damage.Visibility = Visibility.Collapsed;
                    enemy.EnImg.Opacity = 0;
                    _player.XP += 50;
                }

                if (enemy.EnImg.Opacity == 0)
                {
                    Enemy.Enemies.Remove(enemy);
                    _bossDeathTimer.Stop();
                    YouWin.Visibility = Visibility.Visible;
                    _timer.Stop();
                    tryAgain.Content = "Back to menu";
                    tryAgain.IsEnabled = true;
                    tryAgain.Visibility = Visibility.Visible;
                    break;
                }
            }
        }

        private void SlimeAtt_Timer(object sender, object e)
        {
            _pMaxHealth = _player.Health;

            if (!_isGamePaused && !_gameOver)
            {
                foreach (Enemy enemy in Enemy.Enemies)
                {
                    if (_player.Health > 0 && enemy.EnemyHealth > 0 && enemy.EnemyName == "slime")
                    {
                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeIdle.gif"));

                        _playerBarWidth = _player.PlayerBar.Width;
                        _playerBarWidth -= ((enemy.EnemyDmg / _pMaxHealth) * _player.PlayerBar.Width);

                        if (_playerBarWidth >= 0)
                        {
                            _player.PlayerBar.Width = _playerBarWidth;
                        }

                        else
                        {
                            _player.PlayerBar.Visibility = Visibility.Collapsed;
                        }

                        _player.Health -= enemy.EnemyDmg;
                        _slimeAttTimer.Stop();
                    }

                    if (_player.Health <= 0)
                    {
                        _player.PlayerBar.Visibility = Visibility.Collapsed;
                        _gameOver = true;
                        return;
                    }
                }
            }
        }

        private void SlimeFlinch_Timer(object sender, object e)
        {
            _temp.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeIdle.gif"));
            _slimeFlinchTimer.Stop();
        }

        private void SlimeDeath_Timer(object sender, object e)
        {
            foreach (Enemy slime in Enemy.Enemies)
            {
                if (slime.EnemyHealth <= 0)
                {
                    slime.EnemyBar.Visibility = Visibility.Collapsed;
                    damage.Visibility = Visibility.Collapsed;
                    slime.EnImg.Opacity = 0;
                    _player.XP += 50;
                }

                if (slime.EnImg.Opacity == 0)
                {
                    damage.Visibility = Visibility.Collapsed;
                    Enemy.Enemies.Remove(slime);
                    slime.EnemyName = "";
                    _slimeDeathTimer.Stop();
                    break;
                }
            }
        }

        private void PlayerAtt_Timer(object sender, object e)
        {
            foreach (Enemy enemy in Enemy.Enemies)
            {
                _temp = enemy;
                _sMaxHealth = enemy.EnemyHealth;

                if (arrow.Visibility == Visibility.Collapsed)
                {
                    _isPressed = false;
                    Grid.SetColumn(arrow, Grid.GetColumn(_player.Img) + 1);
                    Grid.SetRow(arrow, Grid.GetRow(_player.Img));
                    arrow.Visibility = Visibility.Visible;
                    break;
                }

                if (arrow.Visibility == Visibility.Visible)
                {
                    if (Grid.GetRow(arrow) == Grid.GetRow(enemy.EnImg))
                    {
                        if (Grid.GetColumn(arrow) == Grid.GetColumn(enemy.EnImg))
                        {
                            switch (enemy.EnemyName)
                            {
                                case "slime":
                                    enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeFlinch.gif"));
                                    _slimeFlinchTimer.Start();
                                    break;

                                case "minotaur":
                                    enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossFlinch.gif"));
                                    _bossFlinchTimer.Start();
                                    break;
                            }

                            enemy.EnemyHealth -= _player.PlayerDmg;

                            if (enemy.EnemyHealth > 0)
                            {
                                _enemyBarWidth = enemy.EnemyBar.Width;
                                _enemyBarWidth -= (_player.PlayerDmg / _sMaxHealth) * enemy.EnemyBar.Width;

                                if(_enemyBarWidth >= 0)
                                {
                                    enemy.EnemyBar.Width = _enemyBarWidth;
                                }

                                else
                                {
                                    enemy.EnemyBar.Visibility = Visibility.Collapsed;
                                }

                            }
                        
                            else
                            {
                                switch (enemy.EnemyName)
                                {
                                    case "slime":
                                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeDead.gif"));
                                        _slimeDeathTimer.Start();
                                        break;

                                    case "minotaur":
                                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossDead.gif"));
                                        _bossDeathTimer.Start();
                                        break;
                                }
                            }

                            damage.Text = "-" + _player.PlayerDmg.ToString();
                            damage.Visibility = Visibility.Visible;
                            arrow.Visibility = Visibility.Collapsed;
                            Grid.SetColumn(arrow, Grid.GetColumn(_player.Img) + 1);
                            Grid.SetRow(arrow, Grid.GetRow(_player.Img));
                            _playerAttTimer.Stop();
                            break;
                        }

                        Grid.SetColumn(arrow, Grid.GetColumn(arrow) + 1);
                        break;
                    }

                    else
                    {
                        if (Enemy.Enemies.Last() != enemy)
                        {
                            continue;
                        }

                        if (Grid.GetColumn(arrow) == 5)
                        {
                            Grid.SetColumn(arrow, Grid.GetColumn(_player.Img) + 1);
                            arrow.Visibility = Visibility.Collapsed;
                            _playerAttTimer.Stop();
                            break;
                        }
                        Grid.SetColumn(arrow, Grid.GetColumn(arrow) + 1);
                    }
                }
            }
        }

        private void AttTimer(object sender, object e)
        {
            player.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharacterIdle.gif"));
            _playerAttTimer.Start();
           // arrowSound.Stop();
            _attTimer.Stop();
            player.Margin = new Thickness(0, 0, 0, 0);
        }

        private void TpTimer(object sender, object e)
        {
            playerTP.Source = null;
           // SmokeSound.Stop();
            _tpTimer.Stop();
            _isPressed = false;
        }

        private void GameLoop(object sender, object e)
        {

            if (_gameOver)
            {
                int rndNum;
                Random rnd = new Random();

                _player.XP = 0;
                _player.Level = 1;
                _player.MinDmg = 1;
                _player.Health = 20;

                _player.Img.Height = 100;
                _player.Img.Width = 100;
                _player.Img.Margin = new Thickness(0, 50, 0, 0);
                _player.Img.Source = new BitmapImage(new Uri("ms-appx:///Assets/PlayerDeath.png"));
                _timer.Stop();
                tryAgain.IsEnabled = true;
                tryAgain.Visibility = Visibility.Visible;

                rndNum = rnd.Next(0, 3);
   
                switch(rndNum)
                {

                    case 2:
                        tip.Source = new BitmapImage(new Uri("ms-appx:///Assets/Tip3.png"));
                        break;

                    case 1:
                        tip.Source = new BitmapImage(new Uri("ms-appx:///Assets/Tip2.png"));
                        break;

                    case 0:
                        tip.Source = new BitmapImage(new Uri("ms-appx:///Assets/Tip1.png"));
                        break;
                }

                Grid.SetColumn(tip, Grid.GetColumn(_player.Img));
                Grid.SetRow(tip, Grid.GetRow(_player.Img));
                tip.Visibility = Visibility.Visible;

                foreach (Enemy enemy in Enemy.Enemies)
                {
                    if (enemy.EnemyName == "slime")
                    {
                        enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/SlimeIdle.gif"));
                        return;
                    }
                    enemy.EnImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/BossIdle.gif"));
                    return;
                }
            }

            if (_mapClear && !_changedMap)
            {
                Portal.Visibility = Visibility.Visible;
                Spacebar.Visibility = Visibility.Visible;
            }

            if (_player.XP >= 100)
            {
                _player.Level++;
                _player.XP = 0;
                _player.Health += 5;
                _player.MinDmg = _player.MaxDmg / 3;

                Grid.SetRow(LevelUp, Grid.GetRow(_player.Img));
                Grid.SetColumn(LevelUp, Grid.GetColumn(_player.Img));
                LevelUp.Visibility = Visibility.Visible;
                _levelUpTimer.Start();
            }

            if (_player.Img.Opacity < 1)
            {
                _player.Img.Opacity += 0.02;

            }

            foreach (Enemy enemy in Enemy.Enemies)
            {
                if (enemy.Img.Opacity < 1)
                {
                    enemy.EnImg.Opacity += 0.02;
                }

                if (enemy.EnImg.Opacity >= 1)
                {
                    Grid.SetRow(enemy.EnemyBar, Grid.GetRow(enemy.EnImg));
                    Grid.SetColumn(enemy.EnemyBar, Grid.GetColumn(enemy.EnImg));
                    enemy.EnemyBar.Visibility = Visibility.Visible;
                    if (enemy == Enemy.Enemies.Last() && enemy.ObjectSpawned == false)
                    {
                        enemy.ObjectSpawned = true;
                        _enemyTimer.Start();
                    }
                }
            }

            if (_player.Img.Opacity >= 1)
            {
                Grid.SetRow(_player.PlayerBar, Grid.GetRow(_player.Img));
                Grid.SetColumn(_player.PlayerBar, Grid.GetColumn(_player.Img));
                _player.PlayerBar.Visibility = Visibility.Visible;
                HandlePlayerMovement(Enemy.Enemies);
            }

        }
        #endregion

        #region Player's Movement

        private void User_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            _key = "";
        }

        private void User_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            _key = args.VirtualKey.ToString();
        }

        private void HandlePlayerMovement(List<Enemy> list)
        {
            if (Enemy.Enemies.Count == 0 && !_mapClear && !_gameOver)
            {
                _mapClear = true;
                _isPressed = false;
            }

            if (!_isPressed && !_isGamePaused)
            {
                if (_key == "Right" || _key == "D")
                {
                    _movingAllowedCtr = 0;

                    if (Grid.GetColumn(player) != 2)
                    {
                        foreach (Enemy enemy in Enemy.Enemies)
                        {
                            if (Grid.GetRow(_player.Img) != Grid.GetRow(enemy.EnImg) || Grid.GetColumn(_player.Img) != Grid.GetColumn(enemy.EnImg) - 1)
                            {
                                _movingAllowedCtr++;
                            }
                        }

                        if (_movingAllowedCtr == Enemy.Enemies.Count)
                        {
                            _movingAllowedCtr = 0;
                            playerTP.Source = new BitmapImage(new Uri("ms-appx:///Assets/playerTP.gif"));
                            _tpTimer.Start();
                            _isPressed = true;

                            Grid.SetColumn(player, Grid.GetColumn(player) + 1);
                            Grid.SetColumn(playerTP, Grid.GetColumn(player));
                            Grid.SetRow(playerTP, Grid.GetRow(player));
                            Grid.SetRow(LevelUp, Grid.GetRow(_player.Img));
                            Grid.SetColumn(LevelUp, Grid.GetColumn(_player.Img));
                           // SmokeSound.Play();
                        }
                    }
                }

                if (_key == "Left" || _key == "A")
                {

                    if (Grid.GetColumn(player) != 0)
                    {
                        playerTP.Source = new BitmapImage(new Uri("ms-appx:///Assets/playerTP.gif"));
                        _isPressed = true;
                        _tpTimer.Start();

                        Grid.SetColumn(player, Grid.GetColumn(player) - 1);
                        Grid.SetColumn(playerTP, Grid.GetColumn(player));
                        Grid.SetRow(playerTP, Grid.GetRow(player));
                        Grid.SetRow(LevelUp, Grid.GetRow(_player.Img));
                        Grid.SetColumn(LevelUp, Grid.GetColumn(_player.Img));
                       // SmokeSound.Play();
                    }
                }

                if (_key == "Up" || _key == "W")
                {
                    _movingAllowedCtr = 0;

                    if (Grid.GetRow(player) != 0)
                    {
                        foreach (Enemy enemy in Enemy.Enemies)
                        {
                            if (Grid.GetColumn(_player.Img) != Grid.GetColumn(enemy.EnImg) || Grid.GetRow(_player.Img) != Grid.GetRow(enemy.EnImg) + 1)
                            {
                                _movingAllowedCtr++;
                            }
                        }

                        if (_movingAllowedCtr == Enemy.Enemies.Count)
                        {
                            _movingAllowedCtr = 0;
                            playerTP.Source = new BitmapImage(new Uri("ms-appx:///Assets/playerTP.gif"));
                            _isPressed = true;
                            _tpTimer.Start();

                            Grid.SetRow(player, Grid.GetRow(player) - 1);
                            Grid.SetColumn(playerTP, Grid.GetColumn(player));
                            Grid.SetRow(playerTP, Grid.GetRow(player));
                            Grid.SetRow(LevelUp, Grid.GetRow(_player.Img));
                            Grid.SetColumn(LevelUp, Grid.GetColumn(_player.Img));
                          // SmokeSound.Play();
                        }
                    }
                }

                if (_key == "Down" || _key == "S")
                {
                    _movingAllowedCtr = 0;

                    if (Grid.GetRow(player) != 2)
                    {
                        foreach (Enemy enemy in Enemy.Enemies)
                        {
                            if (Grid.GetColumn(_player.Img) != Grid.GetColumn(enemy.EnImg) || Grid.GetRow(_player.Img) != Grid.GetRow(enemy.EnImg) - 1)
                            {
                                _movingAllowedCtr++;
                            }
                        }

                        if (_movingAllowedCtr == Enemy.Enemies.Count)
                        {
                            _movingAllowedCtr = 0;
                            playerTP.Source = new BitmapImage(new Uri("ms-appx:///Assets/playerTP.gif"));
                            _isPressed = true;
                            _tpTimer.Start();

                            Grid.SetRow(player, Grid.GetRow(player) + 1);
                            Grid.SetColumn(playerTP, Grid.GetColumn(player));
                            Grid.SetRow(playerTP, Grid.GetRow(player));
                            Grid.SetRow(LevelUp, Grid.GetRow(_player.Img));
                            Grid.SetColumn(LevelUp, Grid.GetColumn(_player.Img));
                           // SmokeSound.Play();
                        }
                    }
                }

                if (_key == "Control")
                {
                    if (!_mapClear)
                    {
                        _isPressed = true;
                        _player.NormalAttack(arrowSound);
                        _attTimer.Start();
                    }
                }

                if (_key == "Space")
                {
                    if (Grid.GetColumn(player) == 1 && Grid.GetRow(player) == 1)
                    {
                        if (Portal.Visibility == Visibility.Visible)
                        {
                            _mapClear = false;
                            _changedMap = true;
                            Background.Source = new BitmapImage(new Uri("ms-appx:///Assets/FinalBoss.gif"));
                            Portal.Visibility = Visibility.Collapsed;
                            Spacebar.Visibility = Visibility.Collapsed;
                            _playerAttTimer.Stop();
                            Grid.SetColumn(arrow, Grid.GetColumn(_player.Img) + 1);
                            Grid.SetRow(arrow, Grid.GetRow(_player.Img));
                            arrow.Visibility = Visibility.Collapsed;

                            foreach (Image pBlock in _pBlocks)
                            {
                                pBlock.Source = new BitmapImage(new Uri("ms-appx:///Assets/RockPlatform.png"));
                                pBlock.VerticalAlignment = VerticalAlignment.Center;
                                pBlock.Margin = new Thickness(0, 0, 0, -150);
                            }

                            foreach (Image eBlock in _eBlocks)
                            {
                                eBlock.Source = new BitmapImage(new Uri("ms-appx:///Assets/RockPlatform2.png"));
                                eBlock.Margin = new Thickness(0, 190, 0, 0);
                            }
                            _boss = new Enemy(boss, monsterHP3, 1, 5, "minotaur");
                        }
                    }
                }
            }
        }
        #endregion

        #region Settings
        private void ChangeMediaVolume(object sender, RangeBaseValueChangedEventArgs e)
        {
            // stage1.Volume = e.NewValue * 0.01;
        }

        private void settings_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (!_gameOver && !_isGamePaused)
            {
                _timer.Stop();
                _isGamePaused = true;
                VolumeSlider.Visibility = Visibility.Visible;
                return;
            }
            _timer.Start();
            // stage1.Play();
            _isGamePaused = false;
            VolumeSlider.Visibility = Visibility.Collapsed;
        }
        #endregion

        private void tryAgain_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.RequestRestartAsync(string.Empty);
        }
    }
}