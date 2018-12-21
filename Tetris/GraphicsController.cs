using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris
{
    class GraphicsController
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
        PictureBox picBoxMain;
        PictureBox picBoxNextFigure;

        public GraphicsController(PictureBox _main, PictureBox _next)
        {
            picBoxMain = _main;
            picBoxNextFigure = _next;
        }

        public void DrawBoard(int [,] cup, int[,] figure, int x, int y, int color)
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

            float cellWidth = picBoxMain.Width / cup.GetLength(0);
            float cellHeight = picBoxMain.Height / cup.GetLength(1);
            float cellWidthNext = picBoxNextFigure.Width / 4;
            float cellHeightNext = picBoxNextFigure.Height / 4;

        }



        public void draw(PictureBox pictureBoxMain, PictureBox pictureBoxNextFigure) // процедура вывода на заданные элементы 
        {


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
    }
}



