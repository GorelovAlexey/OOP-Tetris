using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    // Класс игры
    class Game
    {   
        // поле игры
        public int[,] cup { get; private set; }
        public bool gameOver { get; private set; }        
        public int score { get; private set; }


        Random RND;
        // фигура которая падает сейчас и следующая за ней
        FallingFigure currentFigure;
        FallingFigure nextFigure;

        public event EventHandler GameOverEventHandler;
        public event EventHandler NextFigureChanged;
        public event EventHandler CupChanged;



        public Game(int columns, int rows, int diff)
        {
            if (rows < 6) rows = 5;
            if (rows > 100) rows = 100;
            if (columns < 6) columns = 5;
            if (columns > 100) columns = 100;

            cup = new int[rows, columns];
            RND = new Random(DateTime.Now.Millisecond);

            SetFigures();
        }

        // Действия которые может совершать игрок
        public void Left()
        {
            var x = currentFigure.posX;
            currentFigure.moveLeft(cup);
            if (x != currentFigure.posX) OnCupChanged(new EventArgs());            
        }
        public void Right()
        {
            var x = currentFigure.posX;
            currentFigure.MoveRight(cup);
            if (x != currentFigure.posX) OnCupChanged(new EventArgs());
        }
        public void Down()
        {
            var canDo = currentFigure.moveDown(cup);
            if (!canDo)
            {
                cup = currentFigure.Put(cup);
                SetFigures();
                if (!currentFigure.Check(cup)) GameOver();
                OnNextFigureChanged(new EventArgs());
            }
            OnCupChanged(new EventArgs());
        }
        public void RotateL()
        {
            var st = currentFigure.state;
            currentFigure.RotateLeft(cup);
            if (st != currentFigure.state) OnCupChanged(new EventArgs());
        }
        public void RotateR()
        {
            var st = currentFigure.state;
            currentFigure.RotateRight(cup);
            if (st != currentFigure.state) OnCupChanged(new EventArgs());
        }

        public int[,] GetCurrentFigure ()
        {
            return currentFigure.GetForm();
        }
        public int[,] GetNextFigure ()
        {
            return nextFigure.GetForm();
        }
        public Tuple<int, int> GetFigurePos ()
        {
            return new Tuple<int, int>(currentFigure.posX, currentFigure.posY);
        }

        void GameOver()
        {
            gameOver = true;
            OnGameOverReached(new EventArgs());
        }
        public void SetFigures() // Смена фигур 
        {
            if (nextFigure != null)
            {
                currentFigure = nextFigure;
            }
            else
            {
                currentFigure = ChooseRandomFig(false);
            }
            nextFigure = ChooseRandomFig();
        }
        FallingFigure ChooseRandomFig(bool bonus = true) // Выбор случайной формы фигры - если бонус истина то может выпасть бонусная форма (точка)
        {
            int options = bonus ? 8 : 7;
            switch (RND.Next(options))
            {
                case 0:
                    return new FigureGE(cup);
                case 1:
                    return new FigureGErev(cup);
                case 2:
                    return new FigureZIG(cup);
                case 3:
                    return new FigureZIGrev(cup);
                case 4:
                    return new FigureSQ(cup);
                case 5:
                    return new FigureLINE(cup);
                case 6:
                    return new FigureT(cup);
                case 7:
                    return new Bonus(cup);
                default:
                    return new Bonus(cup); // Не должно достигаться, но вдруг
            }

        }
        
        void OnGameOverReached(EventArgs e) // событие которое просходит когда игра завершается
        {
            EventHandler handler = GameOverEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        void OnNextFigureChanged(EventArgs e)
        {
            EventHandler handler = NextFigureChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        void OnCupChanged(EventArgs e)
        {
            EventHandler handler = CupChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }



        abstract class FallingFigure
        {
            protected List<bool[,]> forms;
            public int state { get; protected set; }
            const int emptyColor = 0; 
            public int color { get; protected set; } // Код для цвета 0 или любой другой стандартный, другие задаются
            public int posX { get; protected set; }
            public int posY { get; protected set; }
            public FallingFigure(int[,] cup)
            {
                
            }

            protected void SetupFigure (int [,] cup)
            {
                posY = 0;
                posX = 0;
                if (forms != null)
                    if (forms[0] != null)
                    {
                        int figureLength = forms[0].GetLength(0);
                        int cupLength = cup.GetLength(0);
                        posX = cupLength / 2 - figureLength / 2;
                        if (posX < 0 || posX >= figureLength - cupLength) posX = 0;
                    }
            }


            public abstract bool Check(int[,] cup); // проверка свободна ли текущая позиция в стакане
            public abstract int[,] Put(int [,] cup); // устанавливаем фигуру в стакан 
            public abstract void RotateRight(int[,] cup); // Вращение 
            public abstract void RotateLeft(int[,] cup);
            public abstract int[,] GetForm();

            public void MoveRight(int [,] cup)
            {
                posX += 1;
                if (!Check(cup))
                {
                    posX -= 1;
                }
            }
            public void moveLeft(int[,] cup)
            {
                posX -= 1;
                if (!Check(cup))
                {
                    posX += 1;
                }
            }
            public bool moveDown(int[,] cup)
            {
                posY += 1;
                if (!Check(cup))
                {
                    posY -= 1;
                    return false;
                }
                return true;
            }

        } // класс для фигур которые падают в стакане

        abstract class FallingFigureStandart : FallingFigure
        {
            
            public FallingFigureStandart(int[,] cup) : base(cup) { }

            public override bool Check(int[,] cup)
            {
                int maxX = forms[state].GetLength(0);
                int maxY = forms[state].GetLength(1);
                int columns = cup.GetLength(0);
                int rows = cup.GetLength(1);


                if ((posX + maxX) > columns || (posY + maxY) > rows) return false;
                if (posX < 0 || posY < 0) return false;

                for (int x = maxX - 1; x >= 0 ; x--)
                {
                    for (int y = maxY - 1; y >= 0; y--)
                    {
                        if (forms[state][x,y] && cup[posX + x, posY + y] != 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            public override void RotateLeft(int[,] cup)
            {
                int initialState = state;
                state -= 1;
                if (state < 0) state = forms.Count - 1;

                if (!Check(cup))
                {
                    int place = posX;

                    for (int i = 0; i < 3; i++)
                    {
                        posX--;
                        if (Check(cup)) break;
                    }
                    if (!Check(cup))
                    {
                        posX = place;
                        state = initialState;
                    }
                }
            }
            public override void RotateRight(int [,] cup)
            {
                int initState = state;
                state += 1;
                if (state >= forms.Count) state = 0;

                if (!Check(cup))
                {
                    int place = posX;

                    for (int i = 0; i < 3; i++)
                    {
                        posX--;
                        if (Check(cup)) break;
                    }
                    if (!Check(cup))
                    {
                        posX = place;
                        state = initState;
                    }
                }
            }
            public override int[,] Put(int[,] cup)
            {
                int[,] cloneCup = cup.Clone() as int[,];

                for (int x = 0; x < forms[state].GetLength(0); x++)
                {
                    for (int y = 0; y < forms[state].GetLength(1); y++)
                    {
                        if (forms[state][x, y]) cloneCup[x, y] = color;
                    }
                }
                return cloneCup;
            }
            public override int[,] GetForm()
            {
                int[,] form = new int[forms[state].GetLength(0), forms[state].GetLength(1)];
                for (int x = 0; x < form.GetLength(0); x++)
                {
                    for (int y = 0; y < form.GetLength(1); y++)
                    {
                        if (forms[state][x, y]) form[x, y] = color;
                    }
                }
                return form;                
            }
        } // класические фигуры, которые могут вращаться 

        class FigureGE : FallingFigureStandart
        { 
            public FigureGE(int[,] cup) : base(cup)
            {
                color = 1;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { true, false}, 
                                        { true, false},
                                        { true, true } });

                forms.Add(new bool[,] { { true, true, true},
                                        { false, false, true}});

                forms.Add(new bool[,] { { true, true},
                                        { false, true},
                                        { false, true} });

                forms.Add(new bool[,] { { true, false, false},
                                        { true, true, true} });                
                SetupFigure(cup);
            }
        }
        class FigureGErev : FallingFigureStandart
        {
            public FigureGErev(int[,] cup) : base(cup)
            {
                color = 2;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { false, true},
                                        { false, true},
                                        { true, true } });

                forms.Add(new bool[,] { { false, false, true },
                                        { true, true, true} });

                forms.Add(new bool[,] { { true, true},
                                        { true, false},
                                        { true, false} });

                forms.Add(new bool[,] { { true, true, true },
                                        { true, false, false} });
                SetupFigure(cup);
            }
        }
        class FigureZIG : FallingFigureStandart
        {
            public FigureZIG(int[,] cup) : base(cup)
            {
                color = 3;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { false, true},
                                        { true, true},
                                        { true, false } });

                forms.Add(new bool[,] { { false, true, true },
                                        { true, true, false} });
                SetupFigure(cup);
            }
        }
        class FigureZIGrev : FallingFigureStandart
        {
            public FigureZIGrev(int[,] cup) : base(cup)
            {
                color = 4;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { true, false},
                                        { true, true},
                                        { false, true } });

                forms.Add(new bool[,] { { true, true, false },
                                        { false, true, true} });
                SetupFigure(cup);
            }
        }
        class FigureSQ : FallingFigureStandart
        {
            public FigureSQ(int[,] cup) : base(cup)
            {
                color = 5;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { true, true},
                                        { true, true}});
                SetupFigure(cup);
            }
        }
        class FigureLINE : FallingFigureStandart
        {
            public FigureLINE(int[,] cup) : base(cup)
            {
                color = 6;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { true }, { true }, { true }, { true } });
                forms.Add(new bool[,] { { true, true, true, true } });
                SetupFigure(cup);
            }
        }
        class FigureT : FallingFigureStandart
        {
            public FigureT(int[,] cup) : base(cup)
            {
                color = 7;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { true, false},
                                        { true, true},
                                        { true, false}});
                forms.Add(new bool[,] { { false, true, false},
                                        { true, true, true}});
                forms.Add(new bool[,] { { false, true},
                                        { true, true},
                                        { false, true}});
                forms.Add(new bool[,] { { true, true, true},
                                        { false, true, false}});
                SetupFigure(cup);

            }
        }
        class Bonus : FallingFigure
        {
            bool placed = false;
            public Bonus(int[,] cup) : base(cup)
            {
                color = 8;
                forms = new List<bool[,]>();
                forms.Add(new bool[,] { { true } });
                SetupFigure(cup);
            }
            public override bool Check(int[,] cup)
            {
                if (posX >= cup.GetLength(0) || posY >= cup.GetLength(1)) return false;
                if (posX < 0 || posY < 0) return false;
                if (placed) return false;
                if (cup[posX, posY] != 0) return false;
                throw new NotImplementedException();
            }
            public override int[,] GetForm()
            {
                return new int[,] { { color } };
            }
            public override int[,] Put(int[,] cup)
            {
                int[,] cloneCup = cup.Clone() as int[,];


                for (int y = posY + 1; y < cloneCup.GetLength(1); y++)
                {
                    if (cloneCup[posX, y] == 0 && !placed)
                    {
                        placed = true;
                        cloneCup[posX, y] = color;
                    }
                }
                if (!placed) cloneCup[posX, posY] = color;
                return cloneCup;
            }
            public override void RotateLeft(int[,] cup)
            {
            }
            public override void RotateRight(int[,] cup)
            {
            }

        } // точка которая занимает вторую свободную позицию в стакане выбранной колонне или первую если нет второй


    }







}
