using System.Windows.Forms;
using System.Drawing;

namespace Tetris
{    
    //Рисует поле с фигурами 
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

        public void DrawBoardAndFigure(PictureBox P, int [,] cup, int[,] figure, int xPos, int yPos)
        {
            int columns = cup.GetLength(0);
            int rows = cup.GetLength(1);
            int width = figure.GetLength(0);
            int height = figure.GetLength(1);
            
            float cellWidth = P.Width / cup.GetLength(0);
            float cellHeight = P.Height / cup.GetLength(1);
            
            // Рисуем главное поле и фигуру на нем 
            Bitmap bmp = new Bitmap(P.Width, P.Height);
            Graphics draw = Graphics.FromImage(bmp);

            for (int x = 0; x < columns; x++) // рисуем то что есть в стакане
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x >= xPos && x < xPos + width && y >= yPos && y < yPos + height)
                    {
                        if (figure[x - xPos, y - yPos] != 0) drawRect(draw, x, y, cellWidth, cellHeight, figure[x - xPos, y - yPos]);
                        else drawRect(draw, x, y, cellWidth, cellHeight, cup[x, y]);
                    }
                    else drawRect(draw, x, y, cellWidth, cellHeight, cup[x, y]);
                }
            }

            P.Image = bmp;
        }        
        void drawRect(Graphics graphics, float x0, float y0, float w, float h, int color)
        {
            SolidBrush brush = new SolidBrush(colors[color]);
            RectangleF rect = new RectangleF(x0*w, y0*h, w, h);
            graphics.FillRectangle(brush, rect);
        }
                
    }
}