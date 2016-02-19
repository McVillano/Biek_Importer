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
    public partial class Form2 : Form
    {
        Form1 padre;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 padre,string user, string pass, string server, string bbdd)
        {
            InitializeComponent();
            this.padre = padre;
            textUsuario.Text = user;
            textPass.Text = pass;
            textServer.Text = server;
            textBBDD.Text = bbdd;
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            padre.updateConfig(textUsuario.Text, textPass.Text, textServer.Text, textBBDD.Text);
            this.Close();
        }
    }
}
