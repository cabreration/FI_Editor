using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_Editor.Gramatica
{
    class ErrorC
    {
        public int linea;
        public int columna;
        public String lexema;
        public String tipo;
        public String descripcion;

        public ErrorC(int linea, int columna, String lexema, String tipo, String descripcion) {

            this.linea = linea;
            this.columna = columna;
            this.lexema = lexema;
            this.tipo = tipo;
            this.descripcion = descripcion;
        }
    }
}
