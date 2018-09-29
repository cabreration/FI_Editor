using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_Editor.Gramatica
{
    class ErrorC
    {
        private int linea;
        private int columna;
        private String lexema;
        private String tipo;
        private String descripcion;

        public ErrorC(int linea, int columna, String lexema, String tipo, String descripcion) {

            this.linea = linea;
            this.columna = columna;
            this.lexema = lexema;
            this.tipo = tipo;
            this.descripcion = descripcion;
        }
    }
}
