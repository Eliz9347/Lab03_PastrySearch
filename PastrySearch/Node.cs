using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace PastrySearch
{
    class Node
    {
        public string ComputeHash(string initialStr)
        {
            // Вычисление хеша
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;

            sSourceData = initialStr;
            //Create a byte array from source data.
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            //Compute hash based on source data.
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource); // 32 digits
            //tmpHash = new SHA1CryptoServiceProvider().ComputeHash(tmpSource); // 40 digits
            Console.WriteLine(ByteArrayToString(tmpHash));
            static string ByteArrayToString(byte[] arrInput)
            {
                int i;
                StringBuilder sOutput = new StringBuilder(arrInput.Length);
                for (i = 0; i < arrInput.Length; i++)
                {
                    sOutput.Append(arrInput[i].ToString("X2"));
                }
                return sOutput.ToString();
            }
            string hashStr = ByteArrayToString(tmpHash);
            return hashStr;
        }

        public bool CompareHashes(string hash1, string hash2)
        {
            // Сравнение хешей
            bool bEqual = false;
            if (hash1.Length == hash2.Length)
            {
                int i = 0;
                while ((i < hash2.Length) && (hash2[i] == hash1[i]))
                {
                    i += 1;
                }
                if (i == hash2.Length)
                {
                    bEqual = true;
                }
            }
            //if (tmpNewHash.Length == tmpHash.Length)
            //{
            //    int i = 0;
            //    while ((i < tmpNewHash.Length) && (tmpNewHash[i] == tmpHash[i]))
            //    {
            //        i += 1;
            //    }
            //    if (i == tmpNewHash.Length)
            //    {
            //        bEqual = true;
            //    }
            //}

            if (bEqual)
                Console.WriteLine("The two hash values are the same");
            else
                Console.WriteLine("The two hash values are not the same");

            return bEqual;
        }

        public int ComparePrefixes(string hash1, string hash2)
        {
            // Сравнение хешей
            int bEqual = 0;
            if (hash1.Length == hash2.Length)
            {
                int i = 0;
                while ((i < hash2.Length) && (hash2[i] == hash1[i]))
                {
                    i += 1;
                }
                bEqual = i;
            }

            //if (bEqual>0)
            //    Console.WriteLine("Common prefix > 0");
            //else
            //    Console.WriteLine("Keys don't have common prefix");

            return bEqual;
        }

        public Node(int ip)
        {
            this.ip = ip;
            string ipStr = ip.ToString();
            string hashId = this.ComputeHash(ipStr);
            ID = hashId.Substring(0, 4);

            string colhex = hashId.Substring(0, 3);
            int col = Int32.Parse(colhex, System.Globalization.NumberStyles.HexNumber);
            double t = (double)col / 4096;
            if ((1024 < col) && (col < 3072))
            {
                t = -t;
            }
            double temp = 200 * Math.Cos(t);
            this.x = 300 + (int)temp;
            temp = 200 * Math.Sin(t);
            this.y = 300 + (int)temp;

            // data.Add(title, datastr);

            int L = (int)Math.Pow(2, b); // число соседей, число листьев, столбцов в таблице маршрутизации

            for (int i = 0; i < R; i++)
            {
                RoutingTable[i] = new NodeEntry[L];
                for (int j = 0; j < L; j++)
                {
                    RoutingTable[i][j] = new NodeEntry();
                }
            }

        }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.ip = 0;
            this.ID = " ";
        }

        public Node(string id)
        {
            string colhex = id.ToString().Substring(0,3);
            int col = Int32.Parse(colhex, System.Globalization.NumberStyles.HexNumber);
            double t = (double)col / 4096;
            if ((1024 < col) && (col < 3072))
            {
                t = -t;
            }
            double temp = 200*Math.Cos(t);
            this.x = 300+(int)temp;
            temp = 200 * Math.Sin(t);
            this.y = 300+(int)temp;
            this.ip = 0;
            this.ID = " ";
        }

        public Node(int ip, Node prev)
        {
            this.ip = ip;
            string ipStr = ip.ToString();
            string hashId = this.ComputeHash(ipStr);
            ID = hashId.Substring(0, 4);

            //int coord = 
            this.x = 245;
            this.y = 161;

            // Добавление соседей от соседа при первом подключении
            NeighboorhoodSet = prev.NeighboorhoodSet;
            int L = (int)Math.Pow(2, b); // число соседей, число листьев, столбцов в таблице маршрутизации
            if (NeighboorhoodSet.Count < L - 1)
            {
                AddNeighboor(prev);
            }

            // Отправка первого сообщения до своего нового Id
            prev.Put(ID, ID);


            for (int i = 0; i < R; i++)
            {
                RoutingTable[i] = new NodeEntry[L];
                for (int j = 0; j < L; j++)
                {
                    RoutingTable[i][j] = new NodeEntry();
                }
            }

        }



        class NodeEntry
        {
            int ip;
            string ID;

            public int GetIp
            {
                get { return ip; }
                set { ip = value; }
            }

            public string GetId
            {
                get { return ID; }
                set { ID = value; }
            }

            public NodeEntry()
            {
                this.ip = -1;
                this.ID = " - ";
            }

            public NodeEntry(int ip, string ID)
            {
                this.ip = ip;
                this.ID = ID;
            }
        }

        public int x;
        public int y;
        // ID, ip, port?
        public int ip { get; }
        public string ID { get; }
        //string nodeHash;
        Dictionary<string, string> data = new Dictionary<string, string>();
        //string data = "some data ";


        // Neighboorhood set, Leaf set, Routing table
        HashSet<NodeEntry> NeighboorhoodSet = new HashSet<NodeEntry>();
        HashSet<NodeEntry> LeafSet = new HashSet<NodeEntry>();

        const int N = 1024; // Количество узлов/хешей
        const int b = 4; //битность цифры

        static int R = (int)Math.Ceiling((Math.Log2(N)) / b); // число строк в таблице маршрутизации
        int M = (int)Math.Pow(4, b); // число соседей, число листьев, столбцов в таблице маршрутизации
        NodeEntry[][] RoutingTable = new NodeEntry[R][]; // Таблица маршрутизации

        public void AddFile(string title, string datastr)
        {
            data.Add(title, datastr);
        }


        public void AddNeighboor(Node neighboor)
        {
            if (NeighboorhoodSet.Count < this.M)
            {
                NeighboorhoodSet.Add(new NodeEntry(neighboor.ip, neighboor.ID));
            }
        }

        public void WriteNeighboorhoodSet()
        {
            Console.WriteLine("\nNeighboorhood Set");
            foreach (NodeEntry neighboor in NeighboorhoodSet)
            {
                Console.WriteLine(neighboor.GetId);
            }
        }

        public void AddLeaf(Node leaf)
        {

            int right = ComparePrefixes(ID, leaf.ID);
            //Console.WriteLine("{0}, {1}, Right = {2}", ID, leaf.ID, right);
            if ((LeafSet.Count < this.M) && (right > 0))
            {
                LeafSet.Add(new NodeEntry(leaf.ip, leaf.ID));
            }
        }

        public void WriteLeafSet()
        {
            Console.WriteLine("\nLeaf Set");
            foreach (NodeEntry leaf in LeafSet)
            {
                Console.WriteLine(leaf.GetId);
            }
        }

        public int FindInLeafSet(string keyhash)
        {
            int oldCommonPref = ComparePrefixes(ID, keyhash);
            int newCommonPref = -1;
            int newIp = -1;
            foreach (NodeEntry leaf in LeafSet)
            {
                int pref = ComparePrefixes(leaf.GetId, keyhash);
                if (pref > newCommonPref)
                {
                    newCommonPref = pref;
                    newIp = leaf.GetIp;
                }
            }
            if (newCommonPref > oldCommonPref)
            {
                return newIp;
            }
            return -1;
        }


        public void AddRoute(Node peer)
        {
            // строка
            int right = ComparePrefixes(ID, peer.ID);
            // столбец
            string colhex = peer.ID[right].ToString();
            int col = Int32.Parse(colhex, System.Globalization.NumberStyles.HexNumber);
            // Console.WriteLine("\nrow {0} and col {1}", right, col);
            if (RoutingTable[right][col].GetId == " - ")
            {
                RoutingTable[right][col].GetId = peer.ID;
                RoutingTable[right][col].GetIp = peer.ip;
            }

        }

        public int FindInRoutingTable(string keyhash)
        {
            int row = ComparePrefixes(ID, keyhash);
            string colhex = keyhash[row].ToString();
            int col = Int32.Parse(colhex, System.Globalization.NumberStyles.HexNumber);

            if (RoutingTable[row][col].GetId != " - ")
            {
                // Можно вывести ID тут
                return RoutingTable[row][col].GetIp;
            }
            return -1;
        }


        public void WriteRoutingTable()
        {

            Console.WriteLine("\nRouting Table {0}X{1}", RoutingTable.Length, RoutingTable[0].Length);
            for (int i = 0; i < RoutingTable.Length; i++)
            {
                for (int j = 0; j < RoutingTable[i].Length; j++)
                {
                    Console.Write("{0} ", RoutingTable[i][j].GetId);
                }
                Console.WriteLine();
            }
        }

        public void Add(string title, string datastr)
        {
            data.Add(title, datastr);
        }

        // добавление, поиск, удаление
        public string Find(string k)
        {

            return ID + k;
        }

        public void Put(string k, string data)
        {

            k = ID;
        }

        public void Del(string k)
        {
        }

    }
}
