﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class GameController
    {
        List<Keys> moveR = new List<Keys>();
        List<Keys> moveL = new List<Keys>();
        List<Keys> rotateR = new List<Keys>();
        List<Keys> rotateL = new List<Keys>();
        List<Keys> moveD = new List<Keys>();
        List<Keys> pause = new List<Keys>();
        List<Keys> restart = new List<Keys>();

        Timer timer;

        Game G;

        int defaultTimeForTurn = 1000;
        int timeForTurn = 1000;
        int timePassed = 0;
        int scoreGain = 0;
        int prevScore;
        int scoreToLvlup = 100;

        bool paused = true;
        public GameController(Game _G)
        {
            G = _G;

            moveR.Add(Keys.D);
            moveR.Add(Keys.Right);
            moveL.Add(Keys.A);
            moveL.Add(Keys.Left);
            rotateR.Add(Keys.X);
            rotateL.Add(Keys.Z);
            pause.Add(Keys.Space);
            restart.Add(Keys.R);

            timer = new Timer();
            timer.Tick += GameTick;
            timer.Interval = 25;
            timer.Start();

            prevScore = G.score;
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (!paused && !G.gameOver)
            {
                timePassed += timer.Interval;
                if (timePassed == timeForTurn)
                {
                    G.Down();
                    timePassed = 0;
                    if (G.score != prevScore)
                    {
                        if (prevScore < G.score) scoreGain = G.score - prevScore;
                    }
                }
            }                       
        }

        public void Input(KeyEventArgs e)
        {
            var k = e.KeyCode;
            if (!paused && !G.gameOver)
            {
                if (moveR.Contains(k)) G.Right();
                if (moveL.Contains(k)) G.Left();
                if (moveD.Contains(k)) G.Down();
                if (rotateR.Contains(k)) G.RotateR();
                if (rotateL.Contains(k)) G.RotateL();
            }
            if (pause.Contains(k)) paused = !paused;
        }


    }
}
