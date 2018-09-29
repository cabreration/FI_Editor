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
            int actual = this.archivos.SelectedIndex;
            String texto = ((RichTextBox)((this.archivos.Controls[actual]).Controls[0])).Text;
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
    }
}
