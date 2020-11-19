using System;

namespace PrimParallel
{
    class Program
    {
        const int V = 5;

        static int minKey(int[] key, bool[] mstSet)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; ++v)            
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }

        static void printMST(int[] parent, int[][] graph)
        {
            Console.WriteLine("Edge \tWeight");
            for (int i = 1; i < V; ++i)
                Console.WriteLine(parent[i] + " - " + i + "\t" + graph[i][parent[i]]);
        }

        static void primMST(int[][] graph)
        {
            int[] parent = new int[V];

            int[] key = new int[V];

            bool[] mstSet = new bool[V];            

            for (int i = 0; i < V; ++i)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < V - 1; ++count)
            {
                int u = minKey(key, mstSet);

                mstSet[u] = true;

                for (int v = 0; v < V; ++v)
                {
                    if (graph[u][v] != 0 && mstSet[v] == false
                        && graph[u][v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u][v];
                    }
                }
            }

            printMST(parent, graph);
        }

        public static int[][] MakeMatrix(int size, int? value = null)
        {
            var matrix = new int[size][];
            var rng = new Random();
            for (int i = 0; i < size; i++)
            {
                matrix[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        matrix[i][j] = value ?? rng.Next(10);
                    }
                    else
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
            return matrix;
        }

        public static void TestParallelism(int[][] graph, int start, int size, int numThreads)
        {
            var startTime = DateTime.Now;
            var result = ParallelPrim(graph, start, size, numThreads);
            //Console.WriteLine("result: ");
            //Print(result, size);
            var duration = DateTime.Now - startTime;
            Console.WriteLine($"Additional threads: {numThreads}, duration: {duration}");
        }

        static void Main(string[] args)
        {
            int[][] graph = new int[][]
                {
                    new int[] { 0, 2, 0, 6, 0 },
                    new int[] { 2, 0, 3, 8, 5 },
                    new int[] { 0, 3, 0, 0, 7 },
                    new int[] { 6, 8, 0, 0, 9 },
                    new int[] { 0, 5, 7, 9, 0 },
                };

            primMST(graph);
        }

        public static int[] ParallelPrim(int[][] graph, int iv, int size, int numThreads)
        { 
            
        }


    }
}
