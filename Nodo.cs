using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    public class Nodo
    {
        private int costo, precedente;
        private bool visitato;

        public Nodo()
        {
            visitato = false;
            costo = int.MaxValue;
            precedente = -1;
        }

        //Funzioni che consentono la modifica dei valori
        public int Costo { get { return costo; } set { costo = value; } }
        public int Precedente { get { return precedente; } set { precedente = value; } }
        public bool Visitato { get { return visitato; } set { visitato = value; } }
    }
}
