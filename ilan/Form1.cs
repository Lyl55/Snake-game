using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ilan
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        int maxWidth;
        int maxHeight;
        int score;
        private bool gameover;

        Random rand = new Random();
        bool goLeft, goRight, goDown, goUp;
        public Form1()
        {
            InitializeComponent();
            new Settings();
        }

        private void Keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                goUp = true;
            }

            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                goDown = true;
            }
        }

        private void Keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            gameover = false;
            RestartGame();
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Start();
        }
        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (gameover)
            {
                return;
            }
            if (goLeft)
            {
                Settings.directions = "left";
            }

            if (goRight)
            {
                Settings.directions = "right";
            }

            if (goDown)
            {
                Settings.directions = "down";
            }

            if (goUp)
            {
                Settings.directions = "up";
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }

                    int Xmax = picCanvas.Width / Settings.Width;
                    int Ymax = picCanvas.Height / Settings.Height;
                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > Xmax || Snake[i].Y > Ymax)

                    {
                        Die();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
            picCanvas.Invalidate();
        }

        private void Die()
        {
            gameover = true;
            startbutton.Enabled = true;
            gameTimer.Stop();
        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (gameover)
            {
                MessageBox.Show("Game Over");
            }
            else
            {
                Brush snakeColour;
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        snakeColour = Brushes.RosyBrown;
                    }
                    else
                    {
                        snakeColour = Brushes.DarkRed;
                    }

                    canvas.FillEllipse(snakeColour, new Rectangle
                    (
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }

                canvas.FillEllipse(Brushes.DarkOrange, new Rectangle
                (
                    food.X * Settings.Width,
                    food.Y * Settings.Height,
                    Settings.Width, Settings.Height
                ));
            }
        }

        private void RestartGame()
        {

            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;
            Snake.Clear();
            score = 0;
            txtScore.Text = "Score:" + score;
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);
            //for (int i = 0; i < 10; i++)
            //{
            //    Circle body = new Circle();
            //    Snake.Add(body);
            //}

            startbutton.Enabled = false;

        }

        private void EatFood()
        {
            score += 10;
            txtScore.Text = "Score:" + score;
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);
            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

        }

    }
}
