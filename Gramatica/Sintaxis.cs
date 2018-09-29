using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace FI_Editor.Gramatica
{
    class Sintaxis:Grammar
    {
        public Sintaxis() : base(caseSensitive: true) {
            
            #region ExpresionesRegulares
            var numero = TerminalFactory.CreateCSharpNumber("numero");
            var identificador = TerminalFactory.CreateCSharpIdentifier("identificador");
            var cadena = TerminalFactory.CreateCSharpString("cadena");
            var falso = ToTerm("false", "falso");
            var verdadero = ToTerm("true", "verdadero");
            #endregion

            #region Terminales

            //tipos de datos
            var entero = ToTerm("int", "int");
            var flotante = ToTerm("float", "float");
            var booleano = ToTerm("bool", "bool");
            var charAr = ToTerm("char*", "char*");

            // Palabras reservadas
            var retorno = ToTerm("return", "return");
            var si = ToTerm("if", "if");
            var sino = ToTerm("else", "else");
            var mientras = ToTerm("while", "while");
            var hacer = ToTerm("do", "do");
            var print = ToTerm("print", "print");
            var principal = ToTerm("main", "main");

            //comentarios
            CommentTerminal simple = new CommentTerminal("simple", "//", "\n", "\r\n"); 
            CommentTerminal multiple = new CommentTerminal("multiple", "/*", "*/"); 
            NonGrammarTerminals.Add(simple); 
            NonGrammarTerminals.Add(multiple);
            
            //operadores
            var sumar = ToTerm("+", "+");
            var restar = ToTerm("-", "-");
            var dividir = ToTerm("/", "/");
            var multiplicar = ToTerm("*", "*");
            var modular = ToTerm("%", "%");
            var menorQue = ToTerm("<", "<");
            var menorIgual = ToTerm("<=", "<=");
            var mayorQue = ToTerm(">", ">");
            var mayorIgual = ToTerm(">=", ">=");
            var igual = ToTerm("==", "==");
            var diferente = ToTerm("!=", "!=");
            var conjuncion = ToTerm("&&", "&&");
            var disyuncion = ToTerm("||", "||");
            var masIgual = ToTerm("+=", "+=");
            var menosIgual = ToTerm("-=", "-=");
            var asignacion = ToTerm("=", "=");
            var incremento = ToTerm("++", "++");
            var decremento = ToTerm("--", "--");
            var parentesisA = ToTerm("(", "(");
            var parentesisC = ToTerm(")", ")");
            #endregion

            #region No Terminales
            NonTerminal INICIO = new NonTerminal("INICIO");
            NonTerminal DECLARACION = new NonTerminal("DECLARACION");
            NonTerminal LISTA_VARS = new NonTerminal("LISTA_VARS");
            NonTerminal EXPRESION_LOGICA = new NonTerminal("EXPRESION_LOGICA");
            NonTerminal EXPRESION_RELACIONAL = new NonTerminal("EXPRESION_RELACIONAL");
            NonTerminal EXPRESION = new NonTerminal("EXPRESION");
            NonTerminal METODO = new NonTerminal("METODO");
            NonTerminal LLAMADA = new NonTerminal("LLAMADA");
            NonTerminal LISTA_PARAMETROS = new NonTerminal("PARAMETROS");
            NonTerminal PARAMETRO = new NonTerminal("PARAMETRO");
            NonTerminal CONDICION = new NonTerminal("CONDICION");
            NonTerminal ACCION = new NonTerminal("ACCION");
            NonTerminal LISTA_ACCIONES = new NonTerminal("LISTA_ACCIONES");
            NonTerminal TIPO_DATO = new NonTerminal("TIPO_DATO");
            NonTerminal OPCIONES_DECLARACION = new NonTerminal("OPCIONES_DECLARACION");
            NonTerminal OPCION_DOS = new NonTerminal("OPCION_DOS");
            NonTerminal DECL_SIMPLE = new NonTerminal("DECL_SIMPLE");
            NonTerminal LISTA_SIMPLES = new NonTerminal("LISTA_SIMPLES");
            NonTerminal OPERADOR_RELACIONAL = new NonTerminal("OPERADOR_RELACIONAL");
            NonTerminal ASIGNADORES = new NonTerminal("ASIGNADORES");
            NonTerminal LISTA_SENTENCIAS = new NonTerminal("LISTA_SENTENCIAS");
            NonTerminal SENTENCIA = new NonTerminal("SENTENCIA");
            NonTerminal ASIGNACION_VAR = new NonTerminal("ASIGNACION_VAR");
            NonTerminal WHILE = new NonTerminal("WHILE");
            NonTerminal DO_WHILE = new NonTerminal("DO_WHILE");
            NonTerminal IF_ELSE = new NonTerminal("IF_ELSE");
            NonTerminal DINCREMENTOS = new NonTerminal("DINCREMENTOS");
            NonTerminal FUNCION_IMPRIMIR = new NonTerminal("FUNCION_IMPRIMIR");
            NonTerminal RETORNO = new NonTerminal("RETORNO");
            NonTerminal ELSE = new NonTerminal("ELSE");
            NonTerminal PARS_LLAMADA = new NonTerminal("PARS_LLAMADA");
            NonTerminal PRINCIPAL = new NonTerminal("PRINCIPAL");
            #endregion

            #region Precedencias
            RegisterOperators(1, Associativity.Left, sumar, restar);
            RegisterOperators(2, Associativity.Left, multiplicar, dividir, modular);
            RegisterOperators(4, Associativity.Left, conjuncion);
            RegisterOperators(5, Associativity.Left, disyuncion);
            RegisterOperators(7, Associativity.Left, parentesisA, parentesisC);
            #endregion

            #region Gramatica Principal
            this.Root = INICIO;

            INICIO.Rule = LISTA_ACCIONES;

            LISTA_ACCIONES.Rule = LISTA_ACCIONES + ACCION
                | ACCION;

            ACCION.Rule = DECLARACION
                | METODO
                | FUNCION_IMPRIMIR
                | PRINCIPAL;

            #endregion

            #region Declaracion

            DECLARACION.Rule = TIPO_DATO + OPCIONES_DECLARACION + ToTerm(";");

            OPCIONES_DECLARACION.Rule = LISTA_VARS
                | LISTA_VARS + ToTerm("=") + EXPRESION_LOGICA
                | OPCION_DOS;

            LISTA_VARS.Rule = LISTA_VARS + ToTerm(",") + identificador
                | identificador;

            DECL_SIMPLE.Rule = identificador + ToTerm("=") + EXPRESION_LOGICA;

            OPCION_DOS.Rule = DECL_SIMPLE + LISTA_SIMPLES;

            LISTA_SIMPLES.Rule = LISTA_SIMPLES + ToTerm(",") + DECL_SIMPLE
                | ToTerm(",") + DECL_SIMPLE;

            TIPO_DATO.Rule = entero
                | booleano
                | charAr
                | flotante;
            #endregion

            #region Metodo

            PRINCIPAL.Rule = TIPO_DATO + principal + "(" + LISTA_PARAMETROS + ")" + ToTerm("{")
                + LISTA_SENTENCIAS + ToTerm("}")
                | TIPO_DATO + principal + "(" + ")" + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | TIPO_DATO + principal + "(" + ")" + ToTerm("{") + ToTerm("}")
                | TIPO_DATO + principal + "(" + LISTA_PARAMETROS + ")" + ToTerm("{") + ToTerm("}");

            METODO.Rule = TIPO_DATO + identificador + "(" + LISTA_PARAMETROS + ")" + ToTerm("{")
                + LISTA_SENTENCIAS + ToTerm("}")
                | TIPO_DATO + identificador + "(" + ")" + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | TIPO_DATO + identificador + "(" + ")" + ToTerm("{") + ToTerm("}")
                | TIPO_DATO + identificador + "(" + LISTA_PARAMETROS + ")" + ToTerm("{") + ToTerm("}");

            LISTA_PARAMETROS.Rule = LISTA_PARAMETROS + ToTerm(",") + PARAMETRO
                | PARAMETRO;

            PARAMETRO.Rule = TIPO_DATO + identificador;

            LISTA_SENTENCIAS.Rule = LISTA_SENTENCIAS + SENTENCIA
                | SENTENCIA;

            SENTENCIA.Rule = WHILE
                | DO_WHILE
                | IF_ELSE
                | DECLARACION
                | ASIGNACION_VAR
                | DINCREMENTOS + ToTerm(";")
                | LLAMADA + ToTerm(";")
                | FUNCION_IMPRIMIR
                | RETORNO;

            DINCREMENTOS.Rule = identificador + incremento
                | identificador + decremento;

            RETORNO.Rule = retorno + EXPRESION_LOGICA + ToTerm(";");

            ASIGNACION_VAR.Rule = identificador + ASIGNADORES + EXPRESION_LOGICA + ToTerm(";");

            ASIGNADORES.Rule = asignacion
                | masIgual
                | menosIgual;

            FUNCION_IMPRIMIR.Rule = print + "(" + EXPRESION + ")" + ToTerm(";");

            LLAMADA.Rule = identificador + "(" + PARS_LLAMADA + ")"
                | identificador + "(" + ")";

            PARS_LLAMADA.Rule = PARS_LLAMADA + ToTerm(",") + EXPRESION_LOGICA
                | EXPRESION_LOGICA;
            #endregion

            #region While

            WHILE.Rule = mientras + "(" + CONDICION + ")" + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | mientras + "(" + CONDICION + ")" + ToTerm("{") + ToTerm("}");

            CONDICION.Rule = EXPRESION_LOGICA;
            #endregion

            #region Do_While

            DO_WHILE.Rule = hacer + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}") + mientras + "("
                + CONDICION + ")" + ToTerm(";")
                | hacer + ToTerm("{") + ToTerm("}") + mientras + "(" + CONDICION + ")" + ToTerm(";");
            #endregion

            #region If_Else

            IF_ELSE.Rule = si + "(" + CONDICION + ")" + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | si + "(" + CONDICION + ")" + ToTerm("{") + ToTerm("}")
                | si + "(" + CONDICION + ")" + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}") + ELSE
                | si + "(" + CONDICION + ")" + ToTerm("{") + ToTerm("}") + ELSE;

            ELSE.Rule = sino + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | sino + ToTerm("{") + ToTerm("}");
            #endregion

            #region Expresiones

            EXPRESION_LOGICA.Rule = EXPRESION_LOGICA + conjuncion + EXPRESION_LOGICA
                | EXPRESION_LOGICA + disyuncion + EXPRESION_LOGICA
                | EXPRESION_RELACIONAL;

            EXPRESION_RELACIONAL.Rule = EXPRESION + OPERADOR_RELACIONAL + EXPRESION
                | EXPRESION;

            OPERADOR_RELACIONAL.Rule = menorQue
                | menorIgual
                | mayorQue
                | mayorIgual
                | igual
                | diferente;

            EXPRESION.Rule = EXPRESION + sumar + EXPRESION
                | EXPRESION + restar + EXPRESION
                | EXPRESION + multiplicar + EXPRESION
                | EXPRESION + dividir + EXPRESION
                | EXPRESION + modular + EXPRESION
                | restar + EXPRESION
                | parentesisA + EXPRESION + parentesisC
                | identificador
                | LLAMADA
                | identificador + incremento
                | identificador + decremento
                | verdadero
                | falso;
            #endregion
        }
    }
}
