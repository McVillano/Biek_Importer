using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biek
{
    public partial class Form1 : Form
    {
        private string path = "C:"+'/';
        public Form1()
        {
            InitializeComponent();
            textPath.Text = path;
        }

        private void buttonCarpeta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saver = new FolderBrowserDialog();
            saver.ShowDialog();

            if (!String.IsNullOrWhiteSpace(saver.SelectedPath))
            {
                path = saver.SelectedPath;
                textPath.Text = path;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Esta seguro de que quiere salir?\r\nSe perderán los cambios no guardados", "Leaving App", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonExportar_Click(object sender, EventArgs e)
        {

        }
    }
}
