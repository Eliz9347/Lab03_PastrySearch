using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace PastrySearch
{
    class DrawGraph
    {
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGrayPen;
        Pen greenPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 10; //радиус окружности вершины

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGrayPen = new Pen(Color.DimGray);
            darkGrayPen.Width = 2;
            greenPen = new Pen(Color.GreenYellow);
            greenPen.Width = 3;
            fo = new Font("Arial", 10);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.Gold, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }

        //public void drawSelectedVertex(int x, int y)
        //{
        //    gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        //}

        //public void drawSelectedEdge(Node V1, Node V2, Graph.Edge E)
        //{
        //    redPen.Width = 6;
        //    if (E.v1 == E.v2)
        //    {
        //        gr.DrawArc(redPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
        //        //point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
        //        //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
        //        drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
        //    }
        //    else
        //    {
        //        gr.DrawLine(redPen, V1.x, V1.y, V2.x, V2.y);
        //        //point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
        //        //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
        //        drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
        //        drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
        //    }
        //    redPen.Width = 2;
        //}

        public void drawEdge(Node V1, Node V2, Graph.Edge E, int numberE)
        {
            //darkGrayPen.Width = E.width;
            gr.DrawLine(darkGrayPen, V1.x, V1.y, V2.x, V2.y);
            point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
            //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
            drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
        }

        public void drawALLGraph(List<Node> V, List<Graph.Edge> E)
        {
            //Сглаживание линий
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                    gr.DrawLine(darkGrayPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x)/2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    //буквенное обозначение
                    //gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }
    }
}
