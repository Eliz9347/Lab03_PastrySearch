using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastrySearch
{
    class Graph
    {
        public Graph()
        {
            var n0 = new Node(300, 300);
            var n1 = new Node("FFFF");
            var n2 = new Node("7834");
            var n3 = new Node("2834");

            //var n0 = new Node(124);
            //Console.WriteLine("An element ID: {0}", n0.ID);
            //var n1 = new Node(254);
            //Console.WriteLine("An element ID: {0}", n1.ID);
            //var n2 = new Node(172);
            //Console.WriteLine("An element ID: {0}", n2.ID);
            //var n3 = new Node(94);
            //Console.WriteLine("An element ID: {0}", n3.ID);
            //var n4 = new Node(47);
            //Console.WriteLine("An element ID: {0}", n4.ID);
            ////var n5 = new Node(49);
            ////Console.WriteLine("An element ID: {0}", n5.ID);
            //var n5 = new Node(2);
            //Console.WriteLine("An element ID: {0}", n5.ID);
            VertexList.Add(n0);
            VertexList.Add(n1);
            VertexList.Add(n2);
            VertexList.Add(n3);
            //VertexList.Add(n4);
            //VertexList.Add(n5);

            //Console.WriteLine();
            for (int i = 1; i < 51; i++)
            {
                var nn = new Node(i * 6 - 4);
                Console.WriteLine("An element ID: {0}", nn.ID);
                VertexList.Add(nn);
            }
            //Console.WriteLine();

            //Generate(VertexList.Count, 2);
            //AddEdge(0, 3);
            //AddEdge(1, 3);
            //AddEdge(2, 3);
            //AddEdge(3, 4);
            //AddEdge(4, 2);

            //AddEdge(0, 1);
            //AddEdge(0, 5);
            for (int i = 0; i < EdgeList.Count; i++)
            {
                AdjacencyList.Add(EdgeList[i].v1);
                HL.Add(-1);
            }


            for (int i = EdgeList.Count - 1; i >= 0; i--)
            {
                AdjacencyList.Add(EdgeList[i].v2);

            }
            for (int k = 0; k < AdjacencyList.Count; k++)
            {
                int i = AdjacencyList[k];
                LL.Add(HL[i]);
                HL[i] = k;
            }
            this.GraphWrite();

            //Console.WriteLine("Обход списка рёбер вершины 2");
            //int v = 2;
            //for(int k = HL[v]; k!=-1; k = LL[k])
            //{
            //    int j = AdjacencyList[AdjacencyList.Count - 1-k];
            //    Console.WriteLine("{0} - {1}", v, j);
            //}



            //foreach (Node v in VertexList)
            //{
            //    Console.WriteLine("\n\n\nNode IP: {0}, Node ID: {1}", v.ip, v.ID);
            //    v.WriteNeighboorhoodSet();
            //    int pref = v.ComparePrefixes(v.ID, VertexList[0].ID);
            //    // Console.WriteLine("\nPrefixes ID1: {0}, ID2: {1}, {2}", v.ID, VertexList[0].ID, pref);
            //    v.WriteLeafSet();
            //    v.WriteRoutingTable();
            //}

        }

        // Список вершин
        public List<Node> VertexList = new List<Node>();

        // Список дуг
        public List<Edge> EdgeList = new List<Edge>();
        List<int> AdjacencyList = new List<int>();
        List<int> HL = new List<int>();
        List<int> LL = new List<int>();
        //Adjacency = new List<int>();

        public class Edge
        {
            //public class Vertex
            //{
            //    int v1;
            //    int ip1;
            //}
            public int v1, v2;

            public Edge(int v1, int v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }

        }

        public void AddEdge(int v1, int v2)
        {
            EdgeList.Add(new Edge(v1, v2));
            VertexList[v1].AddNeighboor(VertexList[v2]);
            VertexList[v2].AddNeighboor(VertexList[v1]);

            VertexList[v1].AddLeaf(VertexList[v2]);
            VertexList[v2].AddLeaf(VertexList[v1]);

            VertexList[v1].AddRoute(VertexList[v2]);
            VertexList[v2].AddRoute(VertexList[v1]);
        }

        public void BFS(int v1, int v2)
        {
            Queue<int> Q = new Queue<int>();
            int[] Dist = new int[VertexList.Count];
            List<int> Parent = new List<int>();

            for (int i = 0; i < VertexList.Count; i++)
            {
                Dist[i] = -1;
                Parent.Add(-1);
            }
            Dist[v1] = 0;
            Q.Enqueue(v1);
            while (Q.Count > 0)
            {
                int u = Q.Dequeue();

                Console.WriteLine("\nОбход списка рёбер вершины");
                for (int k = HL[u]; k != -1; k = LL[k])
                {
                    int j = AdjacencyList[AdjacencyList.Count - 1 - k];
                    Console.WriteLine("{0} - {1}", u, j);
                    if (Dist[j] == -1)
                    {
                        Q.Enqueue(j);
                        Dist[j] = Dist[u] + 1;
                        Parent[j] = u;
                    }
                }

            }
            Console.WriteLine("\nDist");
            foreach (int d in Dist)
            {
                Console.WriteLine(d);
            }
            Console.WriteLine("\nParent");
            foreach (int p in Parent)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("\nPath");
            int l = 0;
            for (l = v2; l != -1; l = Parent[l])
            {
                Console.WriteLine(l);
            }
            //Console.WriteLine(l);


        }

        public void Pastry(int v1, string key, string message)
        {
            //string keyStr = key.ToString();
            //string keyhash = VertexList[v1].ComputeHash(keyStr);
            string keyhash = key;
            int commonPref = VertexList[v1].ComparePrefixes(VertexList[v1].ID, keyhash);
            Console.WriteLine("Common Prefix {0}", commonPref);
            if (commonPref >= 3) //3 - константа
            {
                VertexList[v1].AddFile(keyhash, message);
            }
            else
            {
                int vnext = -1;
                vnext = VertexList[v1].FindInLeafSet(keyhash);
                Console.WriteLine("vnext (after search in LeafSet) = {0}", vnext);
                if (vnext != -1)
                {

                    //BFS(v1, vnext);
                    //VertexList.FindLastIndex(Node v.Ip == vnext)
                    for (int i = 0; i < VertexList.Count; i++)
                    {
                        if (VertexList[i].ip == vnext)
                        {
                            Console.WriteLine("Ready to continue {0}", i);
                            Pastry(i, key, message);
                        }
                    }

                }
                else
                {
                    vnext = VertexList[v1].FindInRoutingTable(keyhash);
                    Console.WriteLine("vnext (after search in RoutingTable) = {0}", vnext);
                    if (vnext != -1)
                    {
                        Console.WriteLine("Ready to continue");
                        //BFS(v1, vnext);
                        //Pastry(vnext, key, message);
                    }
                    else
                    {
                        Console.WriteLine("This node doesn't exist");
                    }
                }
            }

        }

        public void GraphWrite()
        {
            foreach (Edge e in EdgeList)
            {
                Console.WriteLine("{0} - {1}", e.v1, e.v2);
            }

            Console.WriteLine("\nIJ");
            foreach (int e in AdjacencyList)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("\nH");
            foreach (int e in HL)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("\nL");
            foreach (int e in LL)
            {
                Console.WriteLine(e);
            }

        }




        // Функция генерации графа
        public int Generate(int vertex_amount, int vertex_min_degree)
        {
            Random rnd = new Random();
            int randnum = 0;
            for (int i = 0; i < vertex_amount - 1; i++)
            {
                randnum = rnd.Next(i + 1, vertex_amount - 1); //Получить случайное число (в диапазоне от 0 до vertex_amount);
                if (i != randnum)
                {
                    this.AddEdge(i, randnum);
                }
                else
                {
                    this.AddEdge(i, randnum + 1);
                }
            }
            randnum = rnd.Next(0, vertex_amount - 2); //Получить случайное число (в диапазоне от 0 до vertex_amount);
            this.AddEdge(vertex_amount - 1, randnum);
            //this.GraphWrite();
            return 0;
        }
    }
}
