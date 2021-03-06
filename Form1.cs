﻿using FI_Editor.Gramatica;
using FI_Editor.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Ast;
using Irony.Parsing;

namespace FI_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog browser = new OpenFileDialog();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                Stream contenido = browser.OpenFile();
                if (contenido != null)
                {
                    StreamReader reader = new StreamReader(contenido);
                    String salida = reader.ReadToEnd();
                    RichTextBox arch = new RichTextBox();
                    arch.Size = new Size(this.archivos.Width-10, this.archivos.Height-10);
                    arch.CursorChanged += new EventHandler(cambiarPos);
                    arch.Text = salida;
                    TabPage pagina = new TabPage();
                    pagina.Controls.Add(arch);
                    this.archivos.Controls.Add(pagina);
                }
            }
        }

        private void cambiarPos(object senter, EventArgs e)
        {
            int actual = this.archivos.SelectedIndex;
            int posicion = ((RichTextBox)((this.archivos.Controls[actual]).Controls[0])).SelectionStart;
            int linea = ((RichTextBox)((this.archivos.Controls[actual]).Controls[0])).GetLineFromCharIndex(posicion);
            int columna = posicion - ((RichTextBox)((this.archivos.Controls[actual]).Controls[0])).GetFirstCharIndexOfCurrentLine();
            this.label1.Text = "Linea: " + Convert.ToString(linea) + " Columna: " + Convert.ToString(columna);
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int actual = this.archivos.SelectedIndex;
            this.archivos.Controls.RemoveAt(actual);
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.consolaSalidas.Text = "";
            this.consolaErrores.Text = "";
            int actual = this.archivos.SelectedIndex;
            String texto = ((RichTextBox)((this.archivos.Controls[actual]).Controls[0])).Text;

            Global.errores = new List<ErrorC>();
            Sintaxis grammar = new Sintaxis();
            LanguageData lenguaje = new LanguageData(grammar);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(texto);
            ParseTreeNode raiz = arbol.Root;

            if (arbol.ParserMessages.Count > 0)
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    String descripcion = arbol.ParserMessages.ElementAt(i).Message;
                    int fila = arbol.ParserMessages.ElementAt(i).Location.Line;
                    int columna = arbol.ParserMessages.ElementAt(i).Location.Column;
                    String tipo = "";
                    if (arbol.ParserMessages.ElementAt(i).Message.Contains("Invalid"))
                        tipo = "Lexico";
                    else tipo = "Sintactico";
                    ErrorC error = new ErrorC(fila, columna, "lexema", tipo, descripcion);
                    Global.errores.Add(error);

                    foreach (ErrorC err in Global.errores) {
                        String salida = "Descripcion: " + err.descripcion
                            + " tipo: " + err.tipo + " fila: " + err.linea
                            + " columna: " + err.columna;
                        this.consolaErrores.Text += "\n" + salida;
                    }
                }
            }

            if (raiz == null)
            {
                MessageBox.Show("La cadena de entrada contiene errores", "CRL",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Global.root = raiz;
                Global.ambitoGlobal = new Tabla();
                Global.metodos = new List<Procedimiento>();
                Acciones verdugo = new Acciones();
                verdugo.reconocer(raiz);
                Tabla aux = Global.ambitoGlobal;
                List<Procedimiento> aix = Global.metodos;
                ParseTreeNode main = Global.metodoMain;
                MessageBox.Show("Analisis Completa con Exito :v", "FI",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (main != null) {
                    try
                    {
                        Acciones act = new Acciones();
                        Tabla ambitoMain = new Tabla(Global.ambitoGlobal);
                        ambitoMain.heredar();
                        act.compararRetorno(main);
                        act.ejecutarSentencias(main, ambitoMain);
                    }
                    catch (Exception er) {
                        Global.ide.imprimirErrores(er.Message);
                    }
                }
                Tabla aux2 = Global.ambitoGlobal;
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox arch = new RichTextBox();
            arch.Size = new Size(this.archivos.Width - 10, this.archivos.Height - 10);
            arch.CursorChanged += new EventHandler(cambiarPos);
            TabPage pagina = new TabPage("Nuevo.fi");
            pagina.CursorChanged += new EventHandler(cambiarPos);
            pagina.Controls.Add(arch);
            this.archivos.Controls.Add(pagina);
        }

        public void imprimir(string impresion) {
            this.consolaSalidas.Text += impresion + "\n";
        }

        public void imprimirErrores(string errores) {
            this.consolaErrores.Text += errores + "\n";
        }
    }
}
