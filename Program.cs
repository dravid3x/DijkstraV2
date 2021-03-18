using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> matrice = new List<List<int>>();

            List<int> temp = new List<int>();

            Console.Write("Inserire il numero di nodi: ");
            int nNodi = Convert.ToInt32(Console.ReadLine());
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
        }//GIULIA BORTOLI
    }
}
