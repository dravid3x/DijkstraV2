using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public partial class Program
    {

        static List<List<int>> matrice = new List<List<int>>();
        static List<Nodo> routers = new List<Nodo>();
        static List<int> percorso_router = new List<int>();

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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
                matrice[0][1] = 1; matrice[1][0] = 1;
                matrice[0][2] = 6; matrice[2][0] = 6;
                matrice[0][3] = 5; matrice[3][0] = 5;
                matrice[1][4] = 2; matrice[4][1] = 2;
                matrice[1][5] = 5; matrice[5][1] = 5;
                matrice[2][5] = 2; matrice[5][2] = 2;
                matrice[3][5] = 4; matrice[5][3] = 4;
                matrice[3][6] = 2; matrice[6][3] = 2;
                matrice[4][7] = 3; matrice[7][4] = 3;
                matrice[5][7] = 4; matrice[7][5] = 4;
                matrice[5][8] = 3; matrice[8][5] = 3;
                matrice[6][8] = 6; matrice[8][6] = 6;
                matrice[7][9] = 6; matrice[9][7] = 6;
                matrice[8][9] = 5; matrice[9][8] = 5;
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
            Console.WriteLine("Per arrivare al router " + Convert.ToString(nFinale) + " dal router " + Convert.ToString(nIniziale) + " bisogna seguire il percorso a ritroso\n");
            Salva_Percorso(nFinale);
            Stampa_Percorso(nFinale);
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
        private static void Salva_Percorso(int nFinale)
        {
            int precedente = nFinale;
            while (precedente != -1)
            {
                percorso_router.Add(precedente);
                precedente = routers[precedente].Precedente;
            }
        }
        private static void Stampa_Percorso(int nFinale)
        {
            int dim = percorso_router.Count - 1;
            for(int i = dim; i > 0; i--)
            {
                Console.WriteLine(percorso_router[i]);
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

                Console.WriteLine("Iterazione: " + i);
                Stampa_Routers();

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
        private static void Stampa_Routers()
        {
            int dim = routers.Count;
            for(int i = 0; i < dim; i++)
            {
                Console.WriteLine("Nodo: " + i + "\tCosto: " + (routers[i].Costo == int.MaxValue ? "∞" : routers[i].Costo)  + "\tPrecedente: " + routers[i].Precedente + "\t" + (routers[i].Visitato ? "Definitivo" : "Non Definitivo"));
            }
        }

    }
}

//Per fare l'"hop" semplicemente partendo dall'indice del nodo vedo a cosa è collegato (scorrendo l'indice x)
//e mettendo il valore della x del nodo con costo minore come indice y si fà per l'appunto il salto al prossimo nodo
