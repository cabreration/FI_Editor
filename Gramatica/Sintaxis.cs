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

            #region Terminales

            //tipos de datos
            var entero = ToTerm("int", "int");
            var flotante = ToTerm("float", "float");
            var booleano = ToTerm("bool", "bool");
            var charAr = ToTerm("char", "char");

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
            var coma = ToTerm(",", ",");
            var finSentencia = ToTerm(";", ";");
            #endregion

            #region ExpresionesRegulares
            var numero = TerminalFactory.CreateCSharpNumber("numero");
            var identificador = TerminalFactory.CreateCSharpIdentifier("identificador");
            var cadena = TerminalFactory.CreateCSharpString("cadena");
            var falso = ToTerm("false", "falso");
            var verdadero = ToTerm("true", "verdadero");
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
            NonTerminal LISTA_PARAMETROS = new NonTerminal("LISTA_PARAMETROS");
            NonTerminal PARAMETRO = new NonTerminal("PARAMETRO");
            NonTerminal CONDICION = new NonTerminal("CONDICION");
            NonTerminal ACCION = new NonTerminal("ACCION");
            NonTerminal LISTA_ACCIONES = new NonTerminal("LISTA_ACCIONES");
            NonTerminal TIPO_DATO = new NonTerminal("TIPO_DATO");
            NonTerminal OPCIONES_DECLARACION = new NonTerminal("OPCIONES_DECLARACION");
            NonTerminal OPCION_DOS = new NonTerminal("OPCION_DOS");
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
            NonTerminal SIMPLE = new NonTerminal("SIMPLE");
            #endregion

            #region Precedencias
            RegisterOperators(1, Associativity.Left, sumar, restar);
            RegisterOperators(2, Associativity.Left, multiplicar, dividir);
            RegisterOperators(3, Associativity.Left, modular);
            RegisterOperators(4, Associativity.Left, conjuncion);
            RegisterOperators(5, Associativity.Left, disyuncion);
            RegisterOperators(7, Associativity.Left, parentesisA, parentesisC);

            this.MarkPunctuation(parentesisA, parentesisC, coma, finSentencia);
            this.MarkPunctuation("{", "}");
            #endregion

            #region Gramatica Principal
            this.Root = INICIO;

            INICIO.Rule = LISTA_ACCIONES
                | Empty;

            LISTA_ACCIONES.Rule = MakePlusRule(LISTA_ACCIONES, ACCION);

            ACCION.Rule = DECLARACION
                | METODO
                | FUNCION_IMPRIMIR
                | PRINCIPAL
                | ASIGNACION_VAR
                | DINCREMENTOS;

            #endregion

            #region Declaracion

            DECLARACION.Rule = TIPO_DATO + OPCIONES_DECLARACION;

            OPCIONES_DECLARACION.Rule = LISTA_VARS + finSentencia
                | OPCION_DOS + finSentencia;

            LISTA_VARS.Rule = MakePlusRule(LISTA_VARS, coma, identificador);

            SIMPLE.Rule = LISTA_VARS + asignacion + EXPRESION_LOGICA;

            OPCION_DOS.Rule = MakePlusRule(OPCION_DOS, coma, SIMPLE);

            TIPO_DATO.Rule = entero
                | booleano
                | charAr + multiplicar
                | flotante;
            #endregion

            #region Metodo

            PRINCIPAL.Rule = entero + principal + parentesisA + parentesisC + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | entero + principal + parentesisA + parentesisC + ToTerm("{") + ToTerm("}");               

            METODO.Rule = TIPO_DATO + identificador + parentesisA + LISTA_PARAMETROS + parentesisC + ToTerm("{")
                + LISTA_SENTENCIAS + ToTerm("}")
                | TIPO_DATO + identificador + parentesisA + parentesisC + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | TIPO_DATO + identificador + parentesisA + parentesisC + ToTerm("{") + ToTerm("}")
                | TIPO_DATO + identificador + parentesisA + LISTA_PARAMETROS + parentesisC + ToTerm("{") + ToTerm("}");

            LISTA_PARAMETROS.Rule = MakePlusRule(LISTA_PARAMETROS, coma, PARAMETRO);

            PARAMETRO.Rule = TIPO_DATO + identificador;

            LISTA_SENTENCIAS.Rule = MakePlusRule(LISTA_SENTENCIAS, SENTENCIA);

            SENTENCIA.Rule = WHILE
                | DO_WHILE
                | IF_ELSE
                | DECLARACION
                | ASIGNACION_VAR
                | DINCREMENTOS + finSentencia
                | LLAMADA + finSentencia
                | FUNCION_IMPRIMIR
                | RETORNO;

            DINCREMENTOS.Rule = identificador + incremento
                | identificador + decremento;

            RETORNO.Rule = retorno + EXPRESION_LOGICA + finSentencia;

            ASIGNACION_VAR.Rule = identificador + ASIGNADORES + EXPRESION_LOGICA + finSentencia;

            ASIGNADORES.Rule = asignacion
                | masIgual
                | menosIgual;

            FUNCION_IMPRIMIR.Rule = print + parentesisA + EXPRESION + parentesisC + finSentencia;

            LLAMADA.Rule = identificador + parentesisA + PARS_LLAMADA + parentesisC
                | identificador + parentesisA + parentesisC;

            PARS_LLAMADA.Rule = MakePlusRule(PARS_LLAMADA, coma, EXPRESION_LOGICA);
            #endregion

            #region While

            WHILE.Rule = mientras + parentesisA + CONDICION + parentesisC + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | mientras + parentesisA + CONDICION + parentesisC + ToTerm("{") + ToTerm("}");

            CONDICION.Rule = EXPRESION_LOGICA;
            #endregion

            #region Do_While

            DO_WHILE.Rule = hacer + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}") + mientras + parentesisA
                + CONDICION + parentesisC + finSentencia
                | hacer + ToTerm("{") + ToTerm("}") + mientras + parentesisA + CONDICION + parentesisC + finSentencia;
            #endregion

            #region If_Else

            IF_ELSE.Rule = si + parentesisA + CONDICION + parentesisC + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}")
                | si + parentesisA + CONDICION + parentesisC + ToTerm("{") + ToTerm("}")
                | si + parentesisA + CONDICION + parentesisC + ToTerm("{") + LISTA_SENTENCIAS + ToTerm("}") + ELSE
                | si + parentesisA + CONDICION + parentesisC + ToTerm("{") + ToTerm("}") + ELSE;

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
                | parentesisA + EXPRESION_LOGICA + parentesisC
                | identificador
                | LLAMADA
                | identificador + incremento
                | identificador + decremento
                | verdadero
                | falso
                | cadena
                | numero;
            #endregion
        }
    }
}
