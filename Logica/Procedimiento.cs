using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace FI_Editor.Logica
{
    class Procedimiento
    {
        public String tipo;
        public String identificador;
        public List<Simbolo> parametros;
        public ParseTreeNode root;

        public Procedimiento(String tipo, String identificador, ParseTreeNode raiz) {
            this.tipo = tipo;
            this.identificador = identificador;
            this.parametros = new List<Simbolo>();
        } 
    }
}
