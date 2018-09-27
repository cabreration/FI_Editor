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
            #endregion

            #region Gramatica Principal
            this.Root = INICIO;

            INICIO.Rule = LISTA_ACCIONES;

            LISTA_ACCIONES.Rule = LISTA_ACCIONES + ACCION
                | ACCION;

            ACCION.Rule = DECLARACION
                | METODO;
            #endregion
        }
    }
}
