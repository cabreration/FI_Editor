using System;
using System.Collections;
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
                        if (tipo.Equals("char"))
                            tipo = "char*";
                        guardarVariable(raiz.ChildNodes[1], tipo, Global.ambitoGlobal);
                    }
                    break;

                case "METODO":
                    String tipoM = raiz.ChildNodes[0].ChildNodes[0].FindTokenAndGetText();
                    if (tipoM.Equals("char")) tipoM = "char*";
                    String identificador = raiz.ChildNodes[1].FindTokenAndGetText();
                    Procedimiento metodo = null;
                    if (raiz.ChildNodes.Count == 4 || raiz.ChildNodes.Count == 3)
                    {
                        List<Simbolo> aux = new List<Simbolo>();
                        if (raiz.ChildNodes[2].Term.Name.Equals("LISTA_PARAMETROS"))
                        {
                            foreach (ParseTreeNode hijo in raiz.ChildNodes[2].ChildNodes)
                            {
                                String tipoP = hijo.ChildNodes[0].ChildNodes[0].FindTokenAndGetText();
                                if (tipoP.Equals("char")) tipoP = "char*";
                                String identificadorP = hijo.ChildNodes[1].FindTokenAndGetText();
                                Simbolo simP = new Simbolo(tipoP, identificadorP);
                                aux.Add(simP);
                            }
                            if (raiz.ChildNodes.Count == 4)
                                metodo = new Procedimiento(tipoM, identificador, raiz.ChildNodes[3], aux);
                            else
                                metodo = new Procedimiento(tipoM, identificador, null, aux);
                        }
                        else if (raiz.ChildNodes[2].Term.Name.Equals("LISTA_SENTENCIAS"))
                            metodo = new Procedimiento(tipoM, identificador, raiz.ChildNodes[2]);
                    }
                    else if (raiz.ChildNodes.Count == 2) {
                        metodo = new Procedimiento(tipoM, identificador, null);
                    }
                    Global.metodos.Add(metodo);
                    break;

                case "FUNCION_IMPRIMIR":
                    break;

                case "PRINCIPAL":
                    if (raiz.ChildNodes.Count == 3)
                        Global.metodoMain = raiz.ChildNodes[2];
                    else
                        Global.metodoMain = null;
                    break;

                case "ASIGNACION_VAR":
                    break;
            }
        }


        public void guardarVariable(ParseTreeNode raiz, String tipo, Tabla ambito) {
            if (raiz.ChildNodes[0].Term.Name.Equals("LISTA_VARS"))
            {
                foreach (ParseTreeNode root in raiz.ChildNodes[0].ChildNodes) {
                    Simbolo sim = new Simbolo(tipo, root.FindTokenAndGetText());
                    ambito.insertarSinValor(sim);
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
                            ambito.insertarConValor(sim);
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
                        Object retorno = null;
                        if (root.ChildNodes[1].Term.Name.Equals("+"))
                        {
                            retorno = Calculadora.sumar(obtenerValor(root.ChildNodes[0], ambito),
                                 obtenerValor(root.ChildNodes[2], ambito));
                            return retorno;
                        }
                        else if (root.ChildNodes[1].Term.Name.Equals("-"))
                        {
                            retorno = Calculadora.restar(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                            return retorno;
                        }
                        else if (root.ChildNodes[1].Term.Name.Equals("*"))
                        {
                            retorno = Calculadora.multiplicar(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                            return retorno;
                        }
                        else if (root.ChildNodes[1].Term.Name.Equals("/"))
                        {
                            retorno = Calculadora.dividir(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                            return retorno;
                        }
                        else if (root.ChildNodes[1].Term.Name.Equals("%"))
                        {
                            retorno = Calculadora.modular(obtenerValor(root.ChildNodes[0], ambito),
                                obtenerValor(root.ChildNodes[2], ambito));
                            return retorno;
                        }
                            
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

        public Object llamada(String identificador, ArrayList parametros) {

            try
            {
                Procedimiento metodo = Global.buscarProcedimiento(identificador);
                if (compararParametros(parametros, metodo.parametros)) {
                    Tabla ambitoLocal = new Tabla();
                    ambitoLocal.padre = Global.ambitoGlobal;
                    ambitoLocal.heredar();

                    //guardar los parametros en el ambito local
                    if (metodo.parametros.Count > 0) {
                        foreach (Simbolo sim in metodo.parametros) {
                            ambitoLocal.tabla.Add(sim);
                        }
                    }

                }
                   
            }
            catch (Exception e)
            {
                //guardar el error aqui
            }
            return null;
        }

        public bool compararParametros(ArrayList valores, List<Simbolo> parametros) {

            if (valores.Count != parametros.Count)
                throw new Exception("La lista de parametros no concuerda con la llamada al metodo");

            for (int i = 0; i < parametros.Count; i++) {
                switch (parametros[i].tipo)
                {
                    case "int":
                        if (!(valores[i] is int))
                            throw new Exception("Los tipos de parametros y valores dados no concuerdan");
                        break;

                    case "char*":
                        if (!(valores[i] is String))
                            throw new Exception("Los tipos de parametros y valores dados no concuerdan");
                        break;

                    case "float":
                        if (!(valores[i] is double))
                            throw new Exception("Los tipos de parametros y valores dados no concuerdan");
                        break;

                    case "bool":
                        if (!(valores[i] is bool))
                            throw new Exception("Los tipos de parametros y valores dados no concuerdan");
                        break;
                }
            }

            return true;
        }

        public bool compararRetorno(String tipo, ParseTreeNode lista_sentencias) {

            foreach (ParseTreeNode root in lista_sentencias.ChildNodes) {
                if (root.ChildNodes[0].Term.Name.Equals("RETORNO"))
                    return true;
            }
            throw new Exception("El metodo no posee una sentencia de retorno");
        }

        public Object ejecutarSentencias(ParseTreeNode root, Tabla ambitoActual) {
            Object retorno = null;
            foreach (ParseTreeNode hijo in root.ChildNodes) {
                ParseTreeNode inst = hijo.ChildNodes[0];
                switch (inst.Term.Name) {
                    case "WHILE":
                        Tabla ambitoW = new Tabla(ambitoActual);
                        ambitoW.heredar();
                        retorno = While(inst, ambitoW);
                        if (retorno != null) return retorno;
                        break;

                    case "DO_WHILE":
                        Tabla ambitoD = new Tabla(ambitoActual);
                        ambitoD.heredar();
                        break;

                    case "IF_ELSE":
                        Tabla ambitoI = new Tabla(ambitoActual);
                        ambitoI.heredar();
                        retorno = If_Else(inst, ambitoI);
                        if (retorno != null) return retorno;
                        break;

                    case "DECLARACION":
                        if (inst.ChildNodes.Count == 2) {
                            String tipo = inst.ChildNodes[0].ChildNodes[0].Term.Name;
                            guardarVariable(inst.ChildNodes[1], tipo, ambitoActual);
                        }
                        break;

                    case "ASIGNACION_VAR":
                        break;

                    case "DINCREMENTOS":
                        break;

                    case "LLAMADA":
                        break;

                    case "FUNCION_IMPRIMIR":
                        break;

                    case "RETORNO":
                        break;
                }
            }
            return retorno;
        }


        public Object While(ParseTreeNode root, Tabla ambito) {

            try
            {
                if (root.ChildNodes.Count == 3) {
                    bool condicion = (bool)obtenerValor(root.ChildNodes[1].ChildNodes[0] , ambito);
                    while (condicion) {
                        Object retorno = ejecutarSentencias(root.ChildNodes[2], ambito);
                        condicion = (bool)obtenerValor(root.ChildNodes[1].ChildNodes[0], ambito);

                        if (retorno != null) return retorno;
                    }
                }
            }
            catch (Exception e)
            {
                //capturar error semantico
            }
            return null;
        }

        public Object DoWhile(ParseTreeNode root, Tabla ambito) {
            return null;
        }

        public Object If_Else(ParseTreeNode root, Tabla ambito) {

            if (root.ChildNodes.Count == 3)
            {
                if (root.ChildNodes[2].Term.Name.Equals("ELSE"))
                {
                    try
                    {
                        bool condicion = (bool)obtenerValor(root.ChildNodes[1].ChildNodes[0], ambito);
                        if (!condicion) {
                            if (root.ChildNodes[2].ChildNodes.Count == 2)
                                return ejecutarSentencias(root.ChildNodes[2].ChildNodes[1], ambito);
                        }
                    }
                    catch (Exception e)
                    {
                        //guardar error semantico
                    }
                }
                else
                {
                    try
                    {
                        bool condit = (bool)obtenerValor(root.ChildNodes[1].ChildNodes[0], ambito);
                        if (condit) return ejecutarSentencias(root.ChildNodes[2], ambito);
                    }
                    catch (Exception e)
                    {
                        //guardar error semantico
                    }
                }
            }
            else if (root.ChildNodes.Count == 4)
            {
                try
                {
                    bool cond = (bool)obtenerValor(root.ChildNodes[1].ChildNodes[0], ambito);
                    if (cond) return ejecutarSentencias(root.ChildNodes[2], ambito);
                    else {
                        if (root.ChildNodes[3].ChildNodes.Count == 2)
                            return ejecutarSentencias(root.ChildNodes[3].ChildNodes[1], ambito);
                    }
                }
                catch (Exception e)
                {
                    //guardar error semantico
                } 
            }
            return null;
        }
    }
}
