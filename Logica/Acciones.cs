using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace FI_Editor.Logica
{
    class Acciones
    {

        public void reconocer(ParseTreeNode raiz) {

            switch (raiz.Term.Name) {

                case "INICIO":
                    if (raiz.ChildNodes.Count > 0)
                        reconocer(raiz.ChildNodes[0]);
                    break;

                case "LISTA_ACCIONES":
                    if (raiz.ChildNodes.Count > 0) {
                        foreach (ParseTreeNode root in raiz.ChildNodes)
                            reconocer(root);
                    }
                    break;

                case "ACCION":
                    if (raiz.ChildNodes.Count > 0)
                        reconocer(raiz.ChildNodes[0]);
                    break;

                case "DECLARACION":
                    if (raiz.ChildNodes.Count == 2)
                    {
                        String tipo = raiz.ChildNodes[0].ChildNodes[0].Term.Name;
                        guardarVariable(raiz.ChildNodes[1], tipo);
                    }
                    break;

                case "METODO":
                    break;

                case "FUNCION_IMPRIMIR":
                    break;

                case "PRINCIPAL":
                    break;

                case "ASIGNACION_VAR":
                    break;
            }
        }


        public void guardarVariable(ParseTreeNode raiz, String tipo) {
            if (raiz.ChildNodes.Count == 1)
            {
                foreach (ParseTreeNode root in raiz.ChildNodes[0].ChildNodes) {
                    Simbolo sim = new Simbolo(tipo, root.FindTokenAndGetText());
                    Global.ambitoGlobal.insertarSinValor(sim);
                }
            }
            else if (raiz.ChildNodes.Count == 4)
            {

            }
        }
    }
}
