using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;

namespace Tetris
{

    public partial class Form1 : Form
    {
        // пременные для перетаскивания экрана, код поглядел
        bool isDragging = false;
        Point mousePickPos;
        //
        Game game;
        List<Score> scoreBoard;
        string path = @"score"; // Путь и имя файла с таблицей рекордов
        // Музыка 
        string[] musicPathes = { @"Resourses/Music/BigCarTheft.mp3",
            @"Resourses/Music/Cycles.mp3",
            @"Resourses/Music/Marauder.mp3" };
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;
        bool musicPlaying;
        GameController input;
        GraphicsController output;
        Timer timer1= new Timer(); 


        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }
        public void InitializeGame()
        {
            game = new Game(10, 20); // Строки столбцы уровень сложности
            game.GameOverEventHandler += game_GameOver;
            game.CupChanged += Game_CupChanged;
            game.NextFigureChanged += Game_NextFigureChanged;

            input = new GameController(game);
            input.GamePausedHandler += Input_GamePausedHandler;
            input.LevelChangedHandler += Level_Changed;

            output = new GraphicsController();

            game.SetFigures();
            showScores();
        }

        private void Input_GamePausedHandler(object sender, EventArgs e)
        {
            pauseLabel.Visible = input.paused;
        }
        void UpdateNextFigure()
        {
            output.DrawBoardAndFigure(pictureBoxNextFigure, new int[4, 4], game.GetNextFigure(), 0, 0);
        }
        private void Game_NextFigureChanged(object sender, EventArgs e)
        {
            UpdateNextFigure();
        }
        void UpdateCup()
        {
            output.DrawBoardAndFigure(pictureBoxMain, game.cup, game.GetCurrentFigure(), game.GetFigurePos().Item1, game.GetFigurePos().Item2);
        }
        void Level_Changed(object sender, EventArgs e)
        {
            labelDifficulty.Text = $"level {input.level.ToString()}";
        }

        private void Game_CupChanged(object sender, EventArgs e)
        {
            UpdateCup();
        }

        /// Основной тик таймера
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (game != null)
            {
                labelScore.Text = game.score.ToString();
                labelDifficulty.Text = input.level.ToString();
                labelGameEnded.Visible = game.gameOver;
                if (game.gameOver) pauseLabel.Visible = false;
            }
        }

        // Код для претягивания экрана за края 
        private void StartDragging(object sender, MouseEventArgs e)
        {
            isDragging = true;
            mousePickPos = new Point(e.X, e.Y); // позиция мыши относительно левого верхнего края формы
        }
        private void Dragging(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point tmp = new Point(Location.X, Location.Y);
                tmp.X += e.X - mousePickPos.X;
                tmp.Y += e.Y - mousePickPos.Y;
                Location = tmp;
            }
        }
        private void StopDragging(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }


        // считывание при нажатии кнопки
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            input.Input(e);
        }
        
        public void loadScores() /// считываем данные из файла и записываем в список scoreBoard
        {
            String path = @"score";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            var newScore = new List<Score>();
            byte[] file = File.ReadAllBytes(path);

            int bytesToRead = file.Length;
            int sizeOfItem = sizeof(long) + sizeof(byte) * 3;
            int itemsToRead = bytesToRead / sizeOfItem;

            for (int i = 0; i < itemsToRead; i++)
            {
                Score sc = new Score();
                sc.value = BitConverter.ToInt64(file, i * sizeOfItem);
                sc.d = file[i * sizeOfItem + 4];
                sc.m = file[i * sizeOfItem + 5];
                sc.y = file[i * sizeOfItem + 6];
                newScore.Add(sc);
            }
            scoreBoard = newScore;
        }
        public void saveScore() /// Вставляем в отсортированый список результат игры и сохраняем в файл
        {
            /// Создаем новую запись
            var sc = new Score();
            sc.value = game.score;
            var dt = DateTime.Now;
            sc.d = Convert.ToByte(dt.Day);
            sc.m = Convert.ToByte(dt.Month);
            sc.y = Convert.ToByte(dt.Year-2000);
            /// Ищем куда вставить запись со счетом, пропускаем все записи которые больше текущего счета
            int i = 0;
            while ( i<scoreBoard.Count )
            {
                if (game.score > scoreBoard[i].value) break;
                i++;
            } 
            if ( i == scoreBoard.Count) /// не нашли элемента с меньшим счетом чем текущий
            {
                scoreBoard.Add(sc);
            }
            else /// нашли, вставляем в найденную позицию
            {
                scoreBoard.Insert(i, sc);
            }
            /// Записываем в файл
            /// Превращаем таблицу очков в список байтов и записываем в файл
            var file = new List<byte>(); 
            for (int j = 0; j < scoreBoard.Count; j++)
            {
                file.AddRange(  BitConverter.GetBytes(scoreBoard[j].value)  );
                file.Add(scoreBoard[j].d);
                file.Add(scoreBoard[j].m);
                file.Add(scoreBoard[j].y);
            }
            File.WriteAllBytes(path, file.ToArray());
        }
        public void showScores() /// считываем из файла и выводим на экран
        {
            loadScores();
            labelScoreBoard.Text = "Top scores:\n";
            int i = 0;
            if (scoreBoard != null)
            {
                while (i < 10 && i < scoreBoard.Count)
                {
                    labelScoreBoard.Text += scoreBoard[i].value.ToString() + "\n";
                    i++;
                }
            }
        }
        public void game_GameOver(object sender, EventArgs e) /// событие по окончанию игры
        {
            saveScore();
            showScores();
        }
        public struct Score // струтктура счета, value - кол-во очков,   d,m,y - день месяц и год  от 2000 до 2255
        {
            public long value;
            public byte d;
            public byte m;
            public byte y;   
        }
        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void labelHowToPlay_Click(object sender, EventArgs e)
        {
            if (input != null)
                if (!input.paused) input.Input(new KeyEventArgs(Keys.Space));
            string text;
            text = "       Тетрис Клон\n";
            text += "Управление:\n";
            text += "   Переместить фигру вправо/ влево/ вниз:  D / A / S или стрелочки\n";
            text += "   Повернуть фигуру вправо/ влево:         Z / X \n";
            text += "   Пауза / продолжить: пробел \n";
            text += "Всего 10 уровней сложности, которые меняются автоматически\n";
            text += "В игре есть бонусная фигура - точка, которая перемещается сквозь фигуры и занимает первый пробел\n";
            text += "Игра хранит таблицу рекордов и отображает первые 10\n";
            text += "Музыка по лицензии СС 3.0 с веб-сайта: http://audionautix.com\n";
            text += "Код: Горелов Алексей";
            var caption = "Справка";
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void labelMusic_Click(object sender, EventArgs e)
        {
            try
            {
                musicPlaying = !musicPlaying;
                if (musicPlaying)
                {
                    try
                    {
                        audioFileReader = new AudioFileReader(musicPathes[new Random().Next(0, 3)]);
                        waveOutDevice = new WaveOut();
                        waveOutDevice.Init(audioFileReader);
                        waveOutDevice.Play();
                        waveOutDevice.Volume = 0.5f;
                        waveOutDevice.PlaybackStopped += new EventHandler<StoppedEventArgs>(onMusicStopped);
                    }
                    catch (Exception ex)
                    {
                        musicPlaying = false;
                    }
                }
                else
                {
                    //waveOutDevice.PlaybackStopped = null;
                    waveOutDevice.Stop();
                }
            }
            catch (Exception ex) { }
       
        }

        public void onMusicStopped(object sender, StoppedEventArgs e)
        {
            if (musicPlaying)
            {
                audioFileReader = new AudioFileReader(musicPathes[new Random().Next(0, musicPathes.Length)]);
                waveOutDevice = new WaveOut(); 
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
                waveOutDevice.Volume = 0.5f;      
                waveOutDevice.PlaybackStopped += new EventHandler<StoppedEventArgs>(onMusicStopped);
            }
        }
    }  
}

