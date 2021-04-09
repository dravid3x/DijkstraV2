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
        static List<int> percorso_router = new List<int>();

        public static void Main(string[] args)
        {
            int nNodi, nIniziale, nFinale;
            Console.Write("Inserire il numero di nodi: ");
            nNodi = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Desidera inserire la matrice a mano o da codice? (0 Mano - 1 Codice)");
            int scelta = Convert.ToInt32(Console.ReadLine());

            InizializzazioneMatrice(nNodi);

            if (scelta == 0)
            {
                InserimentoArchi(nNodi);
            } else if(scelta == 1)
            {
                matrice[0][1] = 3; matrice[1][0] = 3;
                matrice[1][2] = 5; matrice[2][1] = 5;
                matrice[2][3] = 7; matrice[3][2] = 7;
                matrice[0][3] = 6; matrice[3][0] = 6;
            }
            else
            {
                Console.Clear();
                Main(args);
            }
            for (int i = 0; i < nNodi; i++) routers.Add(new Nodo());    //Inizializzazione della lista di nodi
            Console.Write("\nInserire il nodo iniziale: ");
            nIniziale = Convert.ToInt32(Console.ReadLine());
            routers[nIniziale].Costo = 0;
            Console.Write("\nInserire il nodo finale: ");
            nFinale = Convert.ToInt32(Console.ReadLine());
            GeneraPercorso(nIniziale, nNodi);
            TrovaPercorso(nFinale, nIniziale);
            Console.WriteLine("Per arrivare al router " + Convert.ToString(nFinale) + " dal router " + Convert.ToString(nIniziale) + " bisogna seguire il percorso a ritroso\n");
            Stampa_percorso(nFinale);
            Console.WriteLine("\ncosto : " + Convert.ToString(routers[nFinale].Costo) + "\n");
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
        private static void TrovaPercorso(int nFinale, int nIniziale)
        {
            percorso_router.Clear();
            int precedente = nFinale;
            do
            {
                precedente = routers[precedente].Precedente;
                //percorso_router.Add(precedente);
            } while (precedente != nIniziale);
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

        private static void Stampa_percorso(int nFinale)
        {
            int precedente = nFinale;
            while (precedente != -1)
            {
                Console.WriteLine(precedente);
                precedente = routers[precedente].Precedente;
            }
        }

        private static void GeneraPercorso(int nIniziale, int nNodi)
        {
            int nodo_successivo = nIniziale;
            //imposto  il ciclo in modo che continui per ogni router della rete
            for (int i = 0; i < nNodi; i++)
            {
                //metto definitivo il nodo che passo alla funzione e aggiorno i costi nei nodi collegati a quest'ultimo
                TrovaNodoSuccessivo(nodo_successivo, nNodi);
                //imposto come nodo successivo il nodo con il costo minimo che conosco
                nodo_successivo = TrovaMin(nNodi);
            }
        }
        private static void TrovaNodoSuccessivo(int pos, int nNodi)
        {
            //Scorro per tutti i router per vedere a quali sono collegato
            for (int c = 0; c < nNodi; c++)
            {
                //entro se ne trovo uno a cui sono collegato
                if (matrice[pos][c] > 0 && !routers[c].Visitato)
                {
                    //Se il percorso è migliorativo rispetto a quello precedente sovrascrivo il costo
                    if (routers[c].Costo > routers[pos].Costo + matrice[pos][c])
                    {
                        routers[c].Costo = routers[pos].Costo + matrice[pos][c];
                        //inserisco la provenienza del router che mi porta a questo percorso
                        routers[c].Precedente = pos;
                    }
                }
            }
            routers[TrovaMin(nNodi)].Visitato = true;
        }
        private static int TrovaMin(int nNodi)
        {
            int max = int.MaxValue, pos = 0;
            //Scorro per tutti i nodi e trovo quello non visitato che ha costo minore
            for (int c = 0; c < nNodi; c++)
            {
                //Se trovo un valore più piccolo di max che non è già stato visitato mi salvo la posizione e aggioro il valore di max
                if (routers[c].Costo < max && routers[c].Visitato == false)
                {
                    pos = c;
                    max = routers[c].Costo;
                }
            }
            return pos;
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