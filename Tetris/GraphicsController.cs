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

        public void DrawBoardAndFigure(int [,] cup, int[,] figure, int xPos, int yPos, int color)
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

            float cellWidth = picBoxMain.Width / cup.GetLength(0);
            float cellHeight = picBoxMain.Height / cup.GetLength(1);
            float cellWidthNext = picBoxNextFigure.Width / 4;
            float cellHeightNext = picBoxNextFigure.Height / 4;

            int columns = cup.GetLength(0);
            int rows = cup.GetLength(1);
            int width = figure.GetLength(0);
            int height = figure.GetLength(1);



            // Рисуем главное поле и фигуру на нем 
            Bitmap bmp = new Bitmap(picBoxMain.Width, picBoxMain.Height);
            Graphics draw = Graphics.FromImage(bmp);


            for (int x = 0; x < cup.GetLength(0); x++) // рисуем то что есть в стакане
            {
                for (int y = 0; y < cup.GetLength(1); y++)
                {
                    if (x >= xPos && x < xPos + width && y >= yPos && y < yPos + height)
                    {
                        if (figure[x - xPos, y - yPos] != 0) drawRect(draw, x, y, cellWidth, cellHeight, color);
                        else drawRect(draw, x, y, cellWidth, cellHeight, cup[x, y]);
                    }
                    else drawRect(draw, x, y, cellWidth, cellHeight, cup[x, y]);



                }
            }
        }

        void drawRect(Graphics graphics, float x0, float y0, float w, float h, int color)
        {
            SolidBrush brush = new SolidBrush(colors[color]);
            RectangleF rect = new RectangleF(x0, y0, w, h);
            graphics.FillRectangle(brush, rect);
        }


        public void DrawFigure()
        {

        }



        public void draw(PictureBox pictureBoxMain, PictureBox pictureBoxNextFigure) // процедура вывода на заданные элементы 
        {


            


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



