using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace s09p23
{
    public class Arbol
    {

        public Nodo raiz;
        public Nodo trabajo;

        public int i = 0;

        public Arbol()
        {
            raiz = new Nodo();
        }

        public Nodo Insertar(string dato, Nodo nodo)
        {

            if (nodo == null)
            {
                raiz = new Nodo();
                raiz.dato = dato;

                raiz.hijo = null;
                raiz.hermano = null;

                return raiz;
            }


            if (nodo.hijo == null)
            {
                Nodo temp = new Nodo();
                temp.dato = dato;

                nodo.hijo = temp;
                return temp;
            }
            else
            {
                trabajo = nodo.hijo;

                while (trabajo.hermano != null)
                {
                    trabajo = trabajo.hermano;
                }

                Nodo temp = new Nodo();
                temp.dato = dato;
                trabajo.hermano = temp;
                return temp;
            }

        }


        public void TransversaPreO(Nodo nodo)
        {

            if (nodo == null)
                return;

            for (int n = 0; n < i; n++)
                Console.Write("\t");
                

            Console.WriteLine(nodo.dato);

            if (nodo.hijo != null)
            {
                i++;
                TransversaPreO(nodo.hijo);
                i--;
            }

            if (nodo.hermano != null)
                TransversaPreO(nodo.hermano);

        }


        public void TransversaPostO(Nodo nodo)
        {
            if (nodo == null)
                return;

            if (nodo.hijo != null)
            {
                i++;
                TransversaPostO(nodo.hijo);
                i--;
            }
            if (nodo.hermano != null)
                TransversaPostO(nodo.hermano);

            for (int n = 0; n < i; n++)
                Console.Write("\t");

            Console.WriteLine(nodo.dato);

        }


        public Nodo Buscar(string dato, Nodo nodo)
        {
            Nodo encontrado = null;

            if (nodo == null)
                return encontrado;

            if (nodo.dato.CompareTo(dato) == 0)
            {
                encontrado = nodo;
                return encontrado;
            }

            if (nodo.hijo != null)
            {
                encontrado = Buscar(dato, nodo.hijo);

                if (encontrado != null)
                    return encontrado;
            }

            if (nodo.hermano != null)
            {
                encontrado = Buscar(dato, nodo.hermano);

                if (encontrado != null)
                    return encontrado;
            }

            return encontrado;
        }

    }
}
