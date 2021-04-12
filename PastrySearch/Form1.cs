using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PastrySearch
{
    public partial class Form1 : Form
    {
        Graph graph = new Graph();
        DrawGraph G;
        //List<Vertex> V;
        //public List<Edge> E;
        //int[,] AMatrix; //матрица смежности 

        int selected1; //выбранные вершины, для соединения линиями
                       //int selected2;
                       //int fl = 0;
                       //List<int> Sol;

        public Form1()
        {
            InitializeComponent();
            G = new DrawGraph(canvas.Width, canvas.Height);
            canvas.Image = G.GetBitmap();

            //var nn = new Node(245, 161);
            //G.drawVertex(nn.x, nn.y, "1");


            G.drawALLGraph(graph.VertexList, graph.EdgeList);
            canvas.Image = G.GetBitmap();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var graph = new Graph();
            //graph.Pastry(1, "C815", "First Message");
            //graph.Pastry(1, "FF85", "First Message");
            //graph.BFS(1, 2);


            listBox.Items.Add("Something");
        }
    }
}
