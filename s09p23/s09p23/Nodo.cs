using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace s09p23
{
    public  class Nodo
    {
        public string dato { get; set; }
        public Nodo hijo { get; set; }
        public Nodo hermano { get; set; }

        public Nodo()
        {
            dato = "";
            hijo = null;
            hermano = null;
        }

    }
}
