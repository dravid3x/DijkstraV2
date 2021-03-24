using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public partial class Program
    {
        struct s_item { public int indice; public int costo; }
        static int nNodi = 0, posGenerale = 0;
        static List<List<int>> matrice = new List<List<int>>(); //Lista contenente Liste di interi, il tutto crea una matrice dinamica
        static List<s_item> PercorsoCosti = new List<s_item>();
        public static void Main(string[] args)
        {
            Console.Write("Inserire il numero di nodi: ");
            nNodi = Convert.ToInt32(Console.ReadLine());
            //Ciclo che inizializza le liste con nNodi elementi al loro interno
            for (int i = 0; i < nNodi; i++)
            {
                matrice.Add(new List<int>());
                PercorsoCosti.Add(new s_item());
                for(int k = 0; k < nNodi; k++) matrice[i].Add(0);
            }
            Stampa();
            InserimentoArchi();
            Stampa();

            PercorsoCosti[posGenerale] = CalcolaArcoMinimo(0);
            Console.WriteLine("Index: " + PercorsoCosti[posGenerale].indice + " Costo: " + PercorsoCosti[posGenerale].costo);
            posGenerale++;
        }

        public static void InserimentoArchi()
        {
            //Funzione che inserisce i costi dei collegamenti dei nodi all'interno della matrice
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
                        int val = Convert.ToInt32(Console.ReadLine());
                        matrice[y][x] = val;
                        matrice[x][y] = val;    //Evita di dover inserire collegamenti duplicati (A con B è uguale a B con A)
                        //Stampa();
                    }
                }
                indice++;
            }
        }

        private static void Stampa()
        {
            //Funzone di stampa della matrice
            for (int i = 0; i < nNodi; i++)
            {
                for (int k = 0; k < nNodi; k++)
                {
                    Console.Write(matrice[i][k].ToString());
                }
                Console.Write("\n");
            }
        }
        private static s_item CalcolaArcoMinimo(int indice)
        {
            s_item minore;
            minore.costo = 9999999;
            minore.indice = 0;
            for(int i = indice + 1; i < nNodi; i++)
            {
                if (matrice[indice][i] < minore.costo)
                {
                    minore.costo = matrice[indice][i];
                    minore.indice = i;
                }
            }
            return minore;
        }
    }
}

//Per fare l'"hop" semplicemente partendo dall'indice del nodo vedo a cosa è collegato (scorrendo l'indice x)
//e mettendo il valore della x del nodo con costo minore come indice y si fà per l'appunto il salto al prossimo nodo