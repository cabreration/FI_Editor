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
                    try
                    {
                        Object valor = obtenerValor(root.ChildNodes[2], Global.ambitoGlobal);
                        foreach (ParseTreeNode vari in root.ChildNodes[0].ChildNodes)
                        {
                            Simbolo sim = new Simbolo(tipo, vari.FindTokenAndGetText(), valor);
                            Global.ambitoGlobal.insertarConValor(sim);
                        }
                    }
                    catch (Exception e)
                    {
                        //aqui debo guardar el error semantico de la operacion
                    }
                }
            }
        }

        public Object obtenerValor(ParseTreeNode root, Tabla ambito) {
            switch (root.Term.Name) {

                case "EXPRESION_LOGICA":
                    if (root.ChildNodes.Count == 3)
                    {
                        if (root.ChildNodes[1].Term.Name.Equals("&&"))
                            return Calculadora.conjuncion(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("||"))
                            return Calculadora.disyuncion((bool)obtenerValor(root.ChildNodes[0], ambito),
                                (bool)obtenerValor(root.ChildNodes[2], ambito));
                    }
                    else if (root.ChildNodes.Count == 1)
                        return obtenerValor(root.ChildNodes[0], ambito);
                    break;

                case "EXPRESION_RELACIONAL":
                    if (root.ChildNodes.Count == 3)
                    {
                        if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("<"))
                            return Calculadora.menorQue(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals(">"))
                            return Calculadora.mayorQue(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("<="))
                            return Calculadora.menorIgual(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals(">="))
                            return Calculadora.mayorIgual(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("=="))
                            return Calculadora.igual(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].ChildNodes[0].Term.Name.Equals("!="))
                            return Calculadora.diferente(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                    }
                    else if (root.ChildNodes.Count == 1)
                        return obtenerValor(root.ChildNodes[0], ambito);
                    break;

                case "EXPRESION":
                    if (root.ChildNodes.Count == 3)
                    {
                        if (root.ChildNodes[1].Term.Name.Equals("+"))
                            return Calculadora.sumar(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("-"))
                            return Calculadora.restar(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("*"))
                            return Calculadora.multiplicar(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("/"))
                            return Calculadora.dividir(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("%"))
                            return Calculadora.modular(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                    }
                    else if (root.ChildNodes.Count == 2)
                    {
                        if (root.ChildNodes[0].Term.Name.Equals("-"))
                            return Calculadora.negativo(obtenerValor(root.ChildNodes[1], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("++"))
                            return Calculadora.incremento(obtenerValor(root.ChildNodes[0], ambito));
                        else if (root.ChildNodes[1].Term.Name.Equals("--"))
                            return Calculadora.decremento(obtenerValor(root.ChildNodes[0], ambito));
                    }
                    else if (root.ChildNodes.Count == 1)
                    {
                        if (root.ChildNodes[0].Term.Name.Equals("identificador"))
                            return ambito.obtenerValor(root.ChildNodes[0].FindTokenAndGetText());
                        else if (root.ChildNodes[0].Term.Name.Equals("falso")
                            || root.ChildNodes[0].Term.Name.Equals("false"))
                            return false;
                        else if (root.ChildNodes[0].Term.Name.Equals("verdadero")
                            || root.ChildNodes[0].Term.Name.Equals("true"))
                            return true;
                        else if (root.ChildNodes[0].Term.Name.Equals("cadena"))
                            return root.ChildNodes[0].FindTokenAndGetText();
                        else if (root.ChildNodes[0].Term.Name.Equals("numero"))
                        {
                            double eval = Convert.ToDouble(root.FindTokenAndGetText());
                            if (eval % 1 == 0) return Convert.ToInt32(eval);
                            else return eval;
                        }
                        else if (root.ChildNodes[0].Term.Name.Equals("LLAMADA")) { }
                        else if (root.ChildNodes[0].Term.Name.Equals("EXPRESION_LOGICA"))
                            return obtenerValor(root.ChildNodes[0], ambito);
                    }
                    break;
            }
            return null;
        }
    }
}
