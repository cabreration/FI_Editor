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
            if (raiz.ChildNodes[0].Term.Name.Equals("LISTA_VARS"))
            {
                foreach (ParseTreeNode root in raiz.ChildNodes[0].ChildNodes) {
                    Simbolo sim = new Simbolo(tipo, root.FindTokenAndGetText());
                    Global.ambitoGlobal.insertarSinValor(sim);
                }
            }
            else if (raiz.ChildNodes[0].Term.Name.Equals("OPCION_DOS"))
            {
                foreach (ParseTreeNode root in raiz.ChildNodes[0].ChildNodes) {
                    Object valor = obtenerValor(root.ChildNodes[2]);
                    foreach (ParseTreeNode vari in root.ChildNodes[0].ChildNodes) {
                        Simbolo sim = new Simbolo(tipo, vari.FindTokenAndGetText(), valor);
                        Global.ambitoGlobal.insertarConValor(sim);
                    }
                }
            }
        }

        public Object obtenerValor(ParseTreeNode root) {
            switch (root.Term.Name) {

                case "EXPRESION_LOGICA":
                    if (root.ChildNodes.Count == 3)
                    {
                        if (root.ChildNodes[1].Term.Name.Equals("&&"))
                            return Calculadora.conjuncion(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                        else if (root.ChildNodes[1].Term.Name.Equals("||"))
                            return Calculadora.disyuncion((bool)obtenerValor(root.ChildNodes[0]),
                                (bool)obtenerValor(root.ChildNodes[2]));
                    }
                    else if (root.ChildNodes.Count == 1)
                        return obtenerValor(root.ChildNodes[0]);
                    break;

                case "EXPRESION_RELACIONAL":
                    if (root.ChildNodes.Count == 3)
                    {
                        if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("<"))
                            return Calculadora.menorQue(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals(">"))
                            return Calculadora.mayorQue(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("<="))
                            return Calculadora.menorIgual(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals(">="))
                            return Calculadora.mayorIgual(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("=="))
                            return Calculadora.igual(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("!="))
                            return Calculadora.diferente(obtenerValor(root.ChildNodes[0]),
                                obtenerValor(root.ChildNodes[2]));
                    }
                    else if (root.ChildNodes.Count == 1)
                        return obtenerValor(root.ChildNodes[0]);
                    break;

                case "EXPRESION":
                    break;
            }
            return null;
        }
    }
}
