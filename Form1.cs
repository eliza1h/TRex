using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRex
{
    public partial class TRexMainClass : Form
    {
        bool jumping = false;
        bool isGameOver = false;
        Random random = new Random();
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        int position = 0;
        public TRexMainClass()
        {
            InitializeComponent();
            GameReset();
        }

        private void GameReset()
        {
            jumping = false;
            force = 12;
            score = 0;
            jumpSpeed = 0;
            obstacleSpeed = 10;
            txtScoreLabel.Text = "Score" + score;
            trex.Image = Properties.Resources.running;
            isGameOver = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + random.Next(500, 800) + (x.Width * 10);
                    x.Left = position;
                }
            }

            gameTimer.Start();

        }
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            trex.Top = trex.Top + jumpSpeed;
            txtScoreLabel.Text = "Score: " + score + " Jumping speed: " + jumpSpeed + " force: " + force + " Top: " + trex.Top + " Bottom: " + trex.Bottom;

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force--;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (trex.Top > 366 && jumping == false)
            {
                force = 12;
                trex.Top = 367;
                jumpSpeed = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + random.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtScoreLabel.Text += " Press R to restart the game! ";
                        isGameOver = true;
                    }
                }
            }

            if (score > 5)
            {
                obstacleSpeed = 15;
            }
        }

        private void KeyIsDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUpEvent(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
                GameReset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void floor_Click(object sender, EventArgs e)
        {

        }
    }
}
