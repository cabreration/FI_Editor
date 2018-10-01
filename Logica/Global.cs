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
        public static List<Procedimiento> metodos;
        public static ParseTreeNode root;
        public static ParseTreeNode metodoMain;

        public static bool contieneMetodo(String identificador) {
            if (Global.metodos == null) return false;

            if (Global.metodos.Count < 1) return false;

            foreach (Procedimiento proc in Global.metodos) {
                if (proc.identificador.Equals(identificador))
                    return true;
            }
            return false;
        }

        public static void insertarMetodo(Procedimiento procedimiento) {
            if (contieneMetodo(procedimiento.identificador))
                throw new Exception("Ya existe un metodo " + procedimiento.identificador);

            Global.metodos.Add(procedimiento);
        }

        public static Procedimiento buscarProcedimiento(String identificador) {
            foreach (Procedimiento proc in Global.metodos) {
                if (proc.identificador.Equals(identificador))
                    return proc;
            }
            throw new Exception("El metodo " + identificador + " no existe");
        }
    }
}
