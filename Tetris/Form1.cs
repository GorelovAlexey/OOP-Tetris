using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio;
using NAudio.Wave;

// нажимая вниз - фигура летит быстрей, но зажимая З или Х фигура слишком быстро крутится

    // Экранов нет, конец игры/ начало игры 
    // мен.

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
        string[] musicPathes = { @"music/BigCarTheft.mp3", @"music/Cycles.mp3", @"music/Marauder.mp3" };
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;
        bool musicPlaying;
        GameController input;
        GraphicsController output;    


        /// Фигура имеет цвет
        /// фигура перемещается вниз - в стакане
        /// фигура вращается
        public Form1()
        {
            InitializeComponent();
            StartGame();
            timer1.Start();
        }
        public void StartGame()
        {
            game = new Game(10, 20, 0); // Строки столбцы уровень сложности
            input = new GameController(game);
            game.GameOverEventHandler += game_GameOver;
            output = new GraphicsController();
            showScores();
            output.DrawBoardAndFigure(pictureBoxMain, game.cup, game.GetCurrentFigure(), game.GetFigurePos().Item1, game.GetFigurePos().Item2);
            output.DrawBoardAndFigure(pictureBoxNextFigure, new int[4, 4], game.GetNextFigure(), 0, 0);
        }
        /// Основной тик таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (game != null)
            {
                // Сообщаем игре сколько прошло времени
                // Перерисовываем графику, счет, уровень сложности
                // Если игра на паузе или кончилась выводим соответствующий элемент
                game.update(timer1.Interval);
                game.draw(pictureBoxMain, pictureBoxNextFigure);



                Form1.on


                labelScore.Text = game.score.ToString();
                labelDifficulty.Text = "level" + (1 + game.difficultyLevel).ToString();
                pauseLabel.Visible = game.paused;
                labelGameEnded.Visible = game.gameOver;
                if (game.gameOver) pauseLabel.Visible = false;
            }
        }

        /// Код для претягивания экрана за края 
        /// Перемещение за держа за саму форму
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            mousePickPos = new Point(e.X,e.Y); // позиция мыши относительно левого верхнего края формы
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point tmp = new Point(Location.X, Location.Y);
                tmp.X += e.X - mousePickPos.X;
                tmp.Y += e.Y - mousePickPos.Y;
                Location = tmp;
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        //Перемещение окна дергая за Панель1
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            mousePickPos = new Point(e.X, e.Y); // позиция мыши относительно левого верхнего края формы
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point tmp = new Point(Location.X, Location.Y);
                tmp.X += e.X - mousePickPos.X;
                tmp.Y += e.Y - mousePickPos.Y;
                Location = tmp;
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isDragging = false;
        }
        
        
        
        // считывание при нажатии кнопки
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Controller c1 = new Controller();
            c1.Init(); 
            if ((e.KeyCode == Keys.A) || (e.KeyCode == Keys.Left)) { c1.moveLeft = true; c1.captured = true; }
            if ((e.KeyCode == Keys.D) || (e.KeyCode == Keys.Right)) { c1.moveRight = true; c1.captured = true; }
            if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Space)) { c1.goDown = true; c1.captured = true; }
            if ((e.KeyCode == Keys.X)) { c1.turnRight = true; c1.captured = true; }
            if ((e.KeyCode == Keys.Z)) { c1.turnLeft = true; c1.captured = true; }
            if (e.KeyCode == Keys.Space) { c1.pause = true; c1.captured = true; }
            game.input(c1);
            pauseLabel.Visible = game.paused;
            if ((e.KeyCode == Keys.R) && (game.gameOver))
            {
                StartGame();
            }
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
            if (game != null) game.paused = true;

            string text;
            text = "                                Тетрис Клон\n";
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
            musicPlaying = !musicPlaying;
            if (musicPlaying)
            {
                audioFileReader = new AudioFileReader(musicPathes[new Random().Next(0, 3)]);
                waveOutDevice = new WaveOut();
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
                waveOutDevice.Volume = 0.5f;

                waveOutDevice.PlaybackStopped += new EventHandler<StoppedEventArgs>(onMusicStopped);

            }
            else
            {
               /// waveOutDevice.PlaybackStopped -= onMusicStopped;
                waveOutDevice.Stop();
            }
        }

        public void onMusicStopped(object sender, StoppedEventArgs e)
        {
            if (musicPlaying)
            {
                audioFileReader = new AudioFileReader(musicPathes[new Random().Next(0, musicPathes.Length)]);
                waveOutDevice = new WaveOut(); /// HZ CHTO
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
                waveOutDevice.Volume = 0.5f;      
                waveOutDevice.PlaybackStopped += new EventHandler<StoppedEventArgs>(onMusicStopped);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (waveOutDevice!=null)
            {
                waveOutDevice.Stop();
            }
        }
    }






    public struct Controller
    {
        public bool captured;
        public bool moveLeft;
        public bool moveRight;
        public bool goDown;
        public bool turnRight;
        public bool turnLeft;
        public bool pause;
        public void Init()
        {
            captured = false;
            moveLeft = false;
            moveRight = false;
            goDown = false;
            turnRight = false;
            turnLeft = false;
            pause = false;
        }
    }

    


    // Игровое поле - стакан, в котором размещаются фигуры
    class Stakan
    {
        // размеры
        public int rows { get; private set; }
        public int columns { get; private set; }
        private bool[,] maskPlaced;  // массив состояний клеток - пустая или заполненная
        private int[,] maskColor; // цветовой код каждой клетки разные коды разные цвета
        public Stakan(int _col, int _rows)  
        {
            columns = _col;
            rows = _rows;
            maskPlaced = new bool[_col, _rows]; // false - пусто
            maskColor = new int[_col, _rows];  // 0 - стандартный цвет
        }
        public void place(int x, int y, int color) // Поместить клетку в стакан 
        {
            maskPlaced[x, y] = true;
            maskColor[x, y] = color;
        }
        public bool check(int x, int y) // Проверить занята ли клетка 
        {
            return maskPlaced[x, y];
        }
        public int color(int x, int y) // Вернуть номер цвета клетки 
        {
            return maskColor[x, y];
        }
        public int clearRows() // очищаем заполненные ряды, переносим, возвращаем число очищенных рядов для подсчета очков
        {
            int count = 0;
            int r = rows - 1;

            while (r - count >= 0) // идем по рядам cнизу вверх, очищаем заполненные ряды, 
            {
                bool full = true;
                for (int i = 0; i < columns; i++)
                {
                    if (!check(i, r)) full = false; // в ряду есть пустая строчка - чистить ряд  не нужно
                }
                if (full) // ряд нужно очистить
                {
                    for (int x = 0; x < columns; x++) // чистим заполненный ряд
                    {
                        maskPlaced[x, r] = false;
                        maskColor[x, r] = 0;
                    }
                    count++;
                    for (int y = r; y > 0; y--) // перенос всех рядов на 1 вниз из-за очистки
                    {
                        for (int x = 0; x < columns; x++)
                        {
                            maskPlaced[x, y] = maskPlaced[x, y - 1];
                            maskColor[x, y] = maskColor[x, y - 1];
                        }
                    }
                    for (int x = 0; x < columns; x++) // 0вой ряд очищается 
                    {
                        maskPlaced[x, 0] = false;
                        maskColor[x, 0] = 0;
                    }
                }
                else r--; // если ряд не чистили идем на 1 вверх
            }
            return count;
        }
    }
    // Класс игры
    class GameN
    {
        // Состояние игры
        public bool paused;
        public bool gameOver { get; private set; }
        // Таймеры 
        int timeForTurn;
        int timerCurrent;
        int timeForTurnMax;
        int timerTotal;
        //
        public int score { get; private set; }
        int scorePerRow = 10;
        // inputs
        Controller controller;
        private int[] difficultyLevels = new int[] { 550, 500, 450, 400, 350, 300, 250, 200, 150, 100 };
        public int difficultyLevel { get; private set; } = 0;
        int difficultyRowsTimer = 0;
        int difficultyRowsToLvlUp = 4;
        int difficultyIncreceNextLvlUp = 1;


        // поле игры
        public Stakan stakan { get; private set; }
        private Random RND;
        // фигура которая падает сейчас и следующая за ней
        public FallingFigure currentFigure { get; private set; }
        public FallingFigure nextFigure { get; private set; }

        public GameN(int columns, int rows, int diff)
        {
            if (rows < 6) rows = 5;
            if (rows > 100) rows = 100;
            if (columns < 6) columns = 5;
            if (columns > 100) columns = 100;

            difficultyLevel = diff;
            timeForTurn = difficultyLevels[difficultyLevel];
            timeForTurnMax = timeForTurn * 2 + 200;

            stakan = new Stakan(columns, rows);
            RND = new Random(DateTime.Now.Millisecond);
            paused = true;
        }

        // Основной цикл игры 
        public void update(int ms) // ms - время с предидущего обновления
        {
            if (currentFigure == null) // если начало игры или предидущая фигура была поставлена
            {
                if (!gameOver)
                {
                    setFigures();
                    gameOver = !currentFigure.spawn();
                    if (gameOver) OnGameOverReached(EventArgs.Empty); // событие что игра закончилась
                }                
            }

            /// основной цикл игры - если игра не на паузе и не закончилась
            if (!paused && !gameOver)
            {
                // считывание управления и выполнение поворотов или движения в бок, при этом дается дополнительное время 
                if (controller.captured)
                {
                    if (controller.moveRight) { currentFigure.moveRight(); timerCurrent = -100; }
                    if (controller.moveLeft) { currentFigure.moveLeft(); timerCurrent = -100; }
                    if (controller.goDown) { timerCurrent = timeForTurn; } // пользователь хочет ускорить передвижение фигуры
                    if (controller.turnLeft) { currentFigure.rotateLeft(); timerCurrent = -100; }
                    if (controller.turnRight) { currentFigure.rotateRight(); timerCurrent = -100; }
                    controller.Init();
                }
                timerCurrent += ms;
                timerTotal += ms;                
                if (timerCurrent >= timeForTurn || timerTotal >= timeForTurnMax) /// Окончание 1 хода игры
                {
                    currentFigure.moveDown(); 
                    bool placed = !currentFigure.check();
                    if (placed) /// Фигура не может двигаться и записана в стакан, добавляем очки
                    {
                        float scoreMult = 1 + (difficultyLevels[0] - difficultyLevels[difficultyLevel])/100;
                        int rowsCleared = stakan.clearRows();
                        if (rowsCleared > 0)
                        {
                            score += (int)(scorePerRow * rowsCleared * scoreMult);
                            difficultyRowsTimer += rowsCleared;
                            if ((difficultyRowsTimer >= difficultyRowsToLvlUp) && (difficultyLevel != difficultyLevels.Length - 1 )) 
                            {   /// Меняем уровень сложности если он не последний 
                                difficultyLevel++;
                                timeForTurn = difficultyLevels[difficultyLevel];  // Меняем время на ход в зависимости от уровня сложности
                                timeForTurnMax = timeForTurn * 2 + 200;
                                difficultyRowsTimer -= difficultyRowsToLvlUp;
                                difficultyRowsToLvlUp += difficultyIncreceNextLvlUp;
                            }
                        }
                        currentFigure = null;
                    }
                    timerCurrent = 0;
                    timerTotal = 0;
                }
            }
        }
        public void input(Controller a) // ввод
        {
            controller = a;
            if (a.pause) { paused = !paused; }
        }
        public void setFigures() // Смена фигур 
        {
            if (nextFigure != null)
            {
                currentFigure = nextFigure;
            }
            else
            {
                switch (RND.Next(7))
                {
                    case 0:
                        currentFigure = new FigureGE(stakan);
                        break;
                    case 1:
                        currentFigure = new FigureGErev(stakan);
                        break;
                    case 2:
                        currentFigure = new FigureZIG(stakan);
                        break;
                    case 3:
                        currentFigure = new FigureZIGrev(stakan);
                        break;
                    case 4:
                        currentFigure = new FigureSQ(stakan);
                        break;
                    case 5:
                        currentFigure = new FigureLINE(stakan);
                        break;
                    case 6:
                        currentFigure = new FigureT(stakan);
                        break;
                }
            }
            switch (RND.Next(8))
            {
                case 0:
                    nextFigure = new FigureGE(stakan);
                    break;
                case 1:
                    nextFigure = new FigureGErev(stakan);
                    break;
                case 2:
                    nextFigure = new FigureZIG(stakan);
                    break;
                case 3:
                    nextFigure = new FigureZIGrev(stakan);
                    break;
                case 4:
                    nextFigure = new FigureSQ(stakan);
                    break;
                case 5:
                    nextFigure = new FigureLINE(stakan);
                    break;
                case 6:
                    nextFigure = new FigureT(stakan);
                    break;
                case 7:
                    nextFigure = new Bonus(stakan);
                    break;
            }
        }

        public void draw(PictureBox pictureBoxMain, PictureBox pictureBoxNextFigure) // процедура вывода на заданные элементы 
        {
            Color[] colors = new Color[9] {
                Color.Black,        // Поле
                Color.OrangeRed,    // Г обратная фигура
                Color.Cyan,         // Г фигура
                Color.Red,          // z фигура
                Color.Purple,       // z обратная
                Color.Yellow,       // Квадрат
                Color.Lime,         // Палка
                Color.Blue,         // Т фигура
                Color.White         // Бонус
            };
            SolidBrush brush;
            RectangleF rect;

            float cellWidth = pictureBoxMain.Width / stakan.columns;
            float cellHeight = pictureBoxMain.Height / stakan.rows;
            float cellWidthNext = pictureBoxNextFigure.Width / 4;
            float cellHeightNext = pictureBoxNextFigure.Height / 4;

            /// Рисуем главное поле и фигуру на нем 
            Bitmap bmp = new Bitmap(pictureBoxMain.Width, pictureBoxMain.Height);
            Graphics draw = Graphics.FromImage(bmp);

            for (int x = 0; x < stakan.columns; x++) // рисуем то что есть в стакане
            {
                for (int y = 0; y < stakan.rows; y++)
                {
                    brush = new SolidBrush(colors[stakan.color(x, y)]);
                    rect = new RectangleF(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    draw.FillRectangle(brush, rect);
                }
            }

            if (currentFigure != null) // если есть падающая фигруа - рисуем ее 
            {
                brush = new SolidBrush(colors[currentFigure.color]);
                if (currentFigure.GetType() == typeof(Bonus)) // рисуем одну клетку
                {
                    rect = new RectangleF(currentFigure.posX * cellWidth, currentFigure.posY * cellHeight, cellWidth, cellHeight);
                    draw.FillRectangle(brush, rect);
                }
                else
                {
                    FigureForm form = currentFigure.currentForm();

                    for (int x = 0; x < form.maxX; x++)
                    {
                        for (int y = 0; y < form.maxY; y++)
                        {
                            if (form.get(x, y))
                            {
                                rect = new RectangleF((currentFigure.posX + x) * cellWidth, (currentFigure.posY + y) * cellHeight, cellWidth, cellHeight);
                                draw.FillRectangle(brush, rect);
                            }
                        }
                    }
                }
            }
            pictureBoxMain.Image = bmp;

            // рисуем следующую фигуру в своем окне
            bmp = new Bitmap(pictureBoxNextFigure.Width, pictureBoxNextFigure.Height);
            draw = Graphics.FromImage(bmp);
            if (nextFigure != null)
            {
                brush = new SolidBrush(colors[nextFigure.color]);
                if (nextFigure.GetType() == typeof(Bonus)) // рисуем одну клетку
                {
                    rect = new RectangleF(cellWidth, cellHeight, cellWidth, cellHeight);
                    draw.FillRectangle(brush, rect);
                }
                else
                {
                    FigureForm form = nextFigure.currentForm();

                    for (int x = 0; x < form.maxX; x++)
                    {
                        for (int y = 0; y < form.maxY; y++)
                        {
                            if (form.get(x, y))
                            {
                                rect = new RectangleF(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                                draw.FillRectangle(brush, rect);
                            }
                        }
                    }
                }
            }
            pictureBoxNextFigure.Image = bmp;
        }


        protected virtual void OnGameOverReached(EventArgs e) // событие которое просходит когда игра завершается
        {
            EventHandler handler = GameOverEventHandler; 
            if (handler != null)
            {
                handler(this, e);
            }            
        }

        public event EventHandler GameOverEventHandler;

    }
    // Структура содержащая форму фигуры
    public struct FigureForm
    {
        public bool[] mask // двумерный массив в одномерном стороны rasmX и mask/rasmX
        { get; private set; }
        public int maxX
        { get; private set; }
        public int maxY
        { get; private set; }

        public FigureForm(bool[] a, int b)
        {
            if (a.Length % b != 0)
            {
                MessageBox.Show("FigureForm init error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            mask = a;
            maxX = b;
            maxY = a.Length / b;
        }
        public FigureForm(int[] a, int b)
        {
            if (a.Length % b != 0)
            {
                MessageBox.Show("FigureForm init error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            mask = new bool[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == 0)
                {
                    mask[i] = false;
                }
                else
                {
                    mask[i] = true;
                }
            }
            maxX = b;
            maxY = a.Length / b;
        }

        public bool get(int x, int y)
        {
            return mask[x + y * maxX];
        }
    }

    abstract class FallingFigure
    {
        public int color { get; protected set; } // Код для цвета 0 или любой другой стандартный, другие задаются
        public int posX { get; protected set; }
        public int posY { get; protected set; }
        protected Stakan stakan;

        public FallingFigure(Stakan st)
        {
            stakan = st;
        }

        public abstract bool check(); // проверка свободна ли текущая позиция в стакане
        public abstract bool spawn(); // true - фигура поставилась игра продолжается, false - некуда ставить игра окончена
        public abstract void place();
        public abstract void rotateRight();
        public abstract void rotateLeft();
        public abstract FigureForm currentForm();
        public void moveRight()
        {
            posX += 1;
            if (!check())
            {
                posX -= 1;
            }
        }
        public void moveLeft()
        {
            posX -= 1;
            if (!check())
            {
                posX += 1;
            }
        }
        public void moveDown()
        {
            posY += 1;
            if (!check())
            {
                posY -= 1;
                place();
            }
        }

    } // класс для фигур которые падают в стакане

    abstract class FallingFigureStandart : FallingFigure
    {
        protected FigureForm[] forms;
        protected int state;
        protected int states;

        public FallingFigureStandart(Stakan st) : base(st) { }

        public override bool check()
        {
            if (((posX + forms[state].maxX) > stakan.columns) ||
                    ((posY + forms[state].maxY) > stakan.rows)) return false;
            if (posX < 0 || posY < 0) return false;

            for (int x = 0; x < forms[state].maxX; x++)
            {
                for (int y = 0; y < forms[state].maxY; y++)
                {
                    if (forms[state].get(x, y) && stakan.check(posX + x, posY + y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override bool spawn()
        {
            posX = (stakan.columns - forms[state].maxX) / 2;
            posY = 0;

            if (!check()) return false;
            return true;
        }
        public override void rotateLeft()
        {
            int initState = state;
            state -= 1;
            if (state < 0) state = states - 1;

            if (!check())
            {
                int place = posX;

                for (int i = 0; i < 3; i++)
                {
                    posX--;
                    if (check()) break;
                }
                if (!check())
                {
                    posX = place;
                    state = initState;
                }
            }
        }
        public override void rotateRight()
        {
            int initState = state;
            state += 1;
            if (state >= states) state = 0;

            if (!check())
            {
                int place = posX;

                for (int i = 0; i < 3; i++)
                {
                    posX--;
                    if (check()) break;
                }
                if (!check())
                {
                    posX = place;
                    state = initState;
                }
            }
        }
        public override void place()
        {
            for (int x = 0; x < forms[state].maxX; x++)
            {
                for (int y = 0; y < forms[state].maxY; y++)
                {
                    if (forms[state].get(x, y))
                    {
                        stakan.place(posX + x, posY + y, color);
                    }
                }
            }
        }
        public override FigureForm currentForm()
        {
            return forms[state];
        }
    } // класические фигуры, которые могут вращаться 

    class FigureGE : FallingFigureStandart
    {
        public FigureGE(Stakan st) : base(st)
        {
            states = 4;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] {   1, 1, 1,
                                                        0, 0, 1 }, 3);
            forms[1] = new FigureForm(new int[] {   0, 1,
                                                        0, 1,
                                                        1, 1}, 2);
            forms[2] = new FigureForm(new int[] {   1, 0, 0,
                                                        1, 1, 1 }, 3);
            forms[3] = new FigureForm(new int[] {   1, 1,
                                                        1, 0,
                                                        1, 0}, 2);
            color = 1;
            
        }
    }
    class FigureGErev : FallingFigureStandart
    {
        public FigureGErev(Stakan st) : base(st)
        {
            states = 4;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] {   1, 1, 1,
                                                        1, 0, 0 }, 3);
            forms[1] = new FigureForm(new int[] {   1, 1,
                                                        0, 1,
                                                        0, 1}, 2);
            forms[2] = new FigureForm(new int[] {   0, 0, 1,
                                                        1, 1, 1 }, 3);
            forms[3] = new FigureForm(new int[] {   1, 0,
                                                        1, 0,
                                                        1, 1}, 2);
            color = 2;
        }
    }
    class FigureZIG : FallingFigureStandart
    {
        public FigureZIG(Stakan st) : base(st)
        {
            states = 2;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] {   0, 1, 1,
                                                        1, 1, 0 }, 3);
            forms[1] = new FigureForm(new int[] {   1, 0,
                                                        1, 1,
                                                        0, 1}, 2);
            color = 3;
        }
    }
    class FigureZIGrev : FallingFigureStandart
    {
        public FigureZIGrev(Stakan st) : base(st)
        {
            states = 2;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] {   1, 1, 0,
                                                        0, 1, 1 }, 3);
            forms[1] = new FigureForm(new int[] {   0, 1,
                                                        1, 1,
                                                        1, 0}, 2);
            color = 4;
        }
    }
    class FigureSQ : FallingFigureStandart
    {
        public FigureSQ(Stakan st) : base(st)
        {
            states = 1;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] {   1, 1,
                                                        1, 1 }, 2);
            color = 5;
        }
    }
    class FigureLINE : FallingFigureStandart
    {
        public FigureLINE(Stakan st) : base(st)
        {
            states = 2;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] { 1, 1, 1, 1 }, 4);
            forms[1] = new FigureForm(new int[] { 1, 1, 1, 1 }, 1);
            color = 6;
        }
    }
    class FigureT : FallingFigureStandart
    {
        public FigureT(Stakan st) : base(st)
        {
            states = 4;
            forms = new FigureForm[states];
            forms[0] = new FigureForm(new int[] {   1, 1, 1,
                                                        0, 1, 0 }, 3);
            forms[1] = new FigureForm(new int[] {   0, 1,
                                                        1, 1,
                                                        0, 1}, 2);
            forms[2] = new FigureForm(new int[] {   0, 1, 0,
                                                        1, 1, 1 }, 3);
            forms[3] = new FigureForm(new int[] {   1, 0,
                                                        1, 1,
                                                        1, 0}, 2);
            color = 7;
        }
    }
    class Bonus : FallingFigure
    {
        bool placed = false;
        public Bonus(Stakan st) : base(st)
        {
            color = 8;
        }
        public override bool check()
        {
            if (posX >= stakan.columns || posY >= stakan.rows) return false;
            if (posX < 0 || posY < 0) return false;

            if (stakan.check(posX, posY)) return false;
            if (placed) return false;
            return true;
        }
        public override void place()
        {
            for (int y = posY + 1; y < stakan.rows; y++)
            {
                if (!stakan.check(posX, y) && !placed)
                {
                    placed = true;
                    stakan.place(posX, y, color);
                }
            }
            if (!placed) stakan.place(posX, posY, color);
        }
        public override void rotateLeft()
        {
            //
        }
        public override void rotateRight()
        {
            // 
        }
        public override bool spawn()
        {
            posX = stakan.columns / 2;
            posY = 0;

            if (!check()) return false;
            return true;
        }
        public override FigureForm currentForm()
        {
            return new FigureForm(new int[] { 1 }, 1);
        }
    } // точка которая занимает вторую свободную позицию в стакане выбранной колонне или первую если нет второй




}

