using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public partial class Program
    {
        static int nNodi = 0;
        static List<List<int>> matrice = new List<List<int>>();
        public static void Main(string[] args)
        {
            List<int> temp = new List<int>();
            Console.Write("Inserire il numero di nodi: ");
            nNodi = Convert.ToInt32(Console.ReadLine());
            nNodi--;
            for (int i = 0; i < nNodi; i++)
            {
                matrice.Add(temp);
                matrice[i].Add(0);
            }
            for (int i = 0; i < nNodi; i++)
            {
                for (int k = 0; k < nNodi; k++)
                {
                    Console.Write(matrice[i][k].ToString());
                }
                Console.Write("\n");
            }
            InserimentoArchi();
            for (int i = 0; i < nNodi; i++)
            {
                for (int k = 0; k < nNodi; k++)
                {
                    Console.Write(matrice[i][k].ToString());
                }
                Console.Write("\n");
            }
        }

        public static void InserimentoArchi()
        {
            int indice = 1;
            for (int y = 0; y < nNodi; y++)
            {
                int tempX = 0;
                for (int x = 0; x < nNodi; x++)
                {
                    if (tempX < indice) tempX++;
                    else
                    {
                        Console.WriteLine("Arco " + y.ToString() + " -> " + x.ToString());
                        matrice[y][x] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                indice++;
            }
        }
    }
}
