using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_Editor.Logica
{
    class Simbolo
    {
        public String tipo;
        public Object valor;
        public String identificador;

        public Simbolo() {
            this.tipo = null;
            this.valor = null;
            this.identificador = null;
        }

        public Simbolo(String tipo, String identificador) {
            this.tipo = tipo;
            this.identificador = identificador;
            this.valor = null;
        }

        public Simbolo(String tipo, String identificador, Object valor) {
            this.tipo = tipo;
            this.identificador = identificador;
            this.valor = valor;
        }
    }
}
