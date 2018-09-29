using FI_Editor.Gramatica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace FI_Editor.Logica
{
    class Global
    {
        public static List<ErrorC> errores;
        public static Tabla ambitoGlobal;
        public static List<String> metodos;
        public static ParseTreeNode root;
    }
}
