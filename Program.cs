using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public partial class Program
    {
        #region Vecchie dichiarazioni
        //static int nNodi = 0, posGenerale = 0;
        //Lista contenente Liste di interi, il tutto crea una matrice dinamica
        //static List<List<Nodo>> matrice = new List<List<Nodo>>();
        //static List<Nodo> PercorsoCosti = new List<Nodo>();
        #endregion

        static List<List<int>> matrice = new List<List<int>>();
        static List<Nodo> routers = new List<Nodo>();

        public static void Main(string[] args)
        {
            int nNodi, nIniziale, nFinale;
            Console.Write("Inserire il numero di nodi: ");
            nNodi = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Desidera inserire la matrice a mano o da codice? (0 Mano - 1 Codice)");
            int scelta = Convert.ToInt32(Console.ReadLine());
            switch (scelta)
            {
                case 0:
                    {
                        InizializzazioneMatrice(nNodi);
                        InserimentoArchi(nNodi);
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                default:
                    Console.Clear();
                    Main(args);
                    return;
                    break;
            }
            for (int i = 0; i < nNodi; i++) routers.Add(new Nodo());    //Inizializzazione della lista di nodi

            //Console.Write("\nInserire il nodo iniziale: ");
            //nIniziale = Convert.ToInt32(Console.ReadLine());
            //Console.Write("\nInserire il nodo finale: ");
            //nFinale = Convert.ToInt32(Console.ReadLine());


        }

        private static void InizializzazioneMatrice(int nNodi)
        {
            //Ciclo che inizializza le liste con nNodi elementi al loro interno e crea la diagonale di 0
            for (int i = 0; i < nNodi; i++)
            {
                matrice.Add(new List<int>());
                for (int k = 0; k < nNodi; k++)
                {
                    if (i == k) matrice[i].Add(0);
                    else matrice[i].Add(-1);
                }
            }
        }

        private static void InserimentoArchi(int nNodi)
        {
            //Funzione che inserisce i costi dei collegamenti tra i nodi all'interno della matrice
            Console.WriteLine("Se non vi e' collegamento lasciare vuoto (premere invio)");
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
                        string inserito = Console.ReadLine();
                        if (!string.IsNullOrEmpty(inserito))
                        {
                            int val = Convert.ToInt32(inserito);
                            matrice[y][x] = val;
                            matrice[x][y] = val;
                        }
                    }
                }
                indice++;
            }
        }

        private static void Stampa(int nNodi)
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

        private static void GeneraPercorso(int nIniziale, int nNodi)
        {
            int nodo_successivo = nIniziale;
            //imposto  il ciclo in modo che continui per ogni router della rete
            for (int i = 0; i < nNodi; i++)
            {

            }
        }
        private static int TrovaNodoSuccessivo(int pos, int nNodi, int nIniziale)
        {
            routers[pos].Costo = matrice[nIniziale][]
            for (int c = 0; c < nNodi; c++)
            {
                
            }
            return 0;
        }

    }
}

//Per fare l'"hop" semplicemente partendo dall'indice del nodo vedo a cosa è collegato (scorrendo l'indice x)
//e mettendo il valore della x del nodo con costo minore come indice y si fà per l'appunto il salto al prossimo nodo

//int[,] test = new int[5, 5]
//{
//    { 0, 7, 8, 10, 1 },
//    { 7, 0, 1, 3, 5 },
//    { 5, 1, 0, 2, 4 },
//    { 3, 3, 2, 0, 2 },
//    { 1, 5, 4, 2, 0 },
//};