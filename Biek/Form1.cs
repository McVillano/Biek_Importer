using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;

namespace Biek
{
    public partial class Form1 : Form
    {
        private string path = "C:";
        private string user = "alfredo";
        private string pass = "alfredo";
        private string server = "192.168.1.100";
        private string bbdd = "gestion";
               
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

        public void updateConfig(string user, string pass, string server, string bbdd)
        {
            this.user = user;
            this.pass = pass;
            this.server = server;
            this.bbdd = bbdd;
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

        private int exportCsv(string path)
        {
            //Preparamos la query
            string query = "SELECT codigo,codcliente,fecha,neto,irpf,totalrecargo,nombrecliente,direccion,codpostal,ciudad,provincia,cifnif FROM facturascli WHERE fecha BETWEEN '" + datePickInicio.Value.ToString("dd-MM-yyyy") + "' AND '" + datePickFin.Value.ToString("dd-MM-yyyy")+"'";

            //Ruta hasta el archivo entera
            DateTime hoy = DateTime.Today;
            string filemane ="BiekExport"+hoy.Day.ToString()+hoy.Month.ToString()+hoy.Year.ToString()+".csv";
            string filePath = path+ "\\"+ filemane;
            textPath.Text = filePath;

            //Conectamos con la base de datos
            OdbcConnection conn = new OdbcConnection("Driver={PostgreSQL ANSI};Server="+server+";Port=5432;Database="+bbdd+";Uid="+user+";Pwd="+pass+";");
            OdbcDataAdapter da = new OdbcDataAdapter(query,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            StringBuilder csv = new StringBuilder();
            foreach( DataRow row in dt.Rows)
            {
                string codigcta = "'A" + row["codigo"].ToString() + "43" + row["codcliente"].ToString()+"'";
                string clavecta = "'N'";
                string provecta = "'43" + row["codcliente"].ToString()+"'";
                string[] fecha =row["fecha"].ToString().Split(' ');
                string fechacta = "'"+fecha[0]+"'";
                string imporcta = "'"+row["neto"].ToString()+"'";
                string ibasecta0 = "'"+row["neto"].ToString()+"'";
                string ibasecta1 = "0";
                string ibasecta2 = "0";
                string ibasecta3 = "0";
                string tipivcta1 = "0";
                string tipivcta2 = "0";
                string tipivcta3 = "0";
                string ivaimcta1 = "0";
                string ivaimcta2 = "0";
                string ivaimcta3 = "0";
                string tiprecta1 = "'" + row["irpf"].ToString()+ "'";
                string tiprecta2 = "0";
                string tiprecta3 = "0";
                string recimcta1 = "'" + row["totalrecargo"].ToString()+ "'";
                string recimcta2 = "0";
                string recimcta3 = "0";
                string concecta = "'" + row["nombrecliente"].ToString()+ "'";
                string nompro = "'" + row["nombrecliente"].ToString()+ "'" ;
                string dirpro = "'" + row["direccion"].ToString()+"'";
                string cpospro = "'" + row["codpostal"].ToString()+"'";
                string pobpro = "'" + row["ciudad"].ToString()+"'";
                string povpro = "'" + row["provincia"].ToString()+"'";
                string nifpro = "'" + row["cifnif"].ToString()+ "'" ;
                string centro = "0";

                var newLine = $"{codigcta},{clavecta},{provecta},{fechacta},{imporcta},{ibasecta0},{ibasecta1},{ibasecta2},{ibasecta3},{tipivcta1},{tipivcta2},{tipivcta3},{ivaimcta1},{ivaimcta2},{ivaimcta3},{tiprecta1},{tiprecta2},{tiprecta3},{recimcta1},{recimcta2},{recimcta3},{concecta},{nompro},{ dirpro},{cpospro},{pobpro},{povpro},{nifpro},{centro}";
                csv.AppendLine(newLine);
            }
            File.AppendAllText(filePath, csv.ToString());
            MessageBox.Show("Csv Exportado");
            return 1;
        }

        private void buttonExportar_Click(object sender, EventArgs e)
        {
            exportCsv(path);
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this,user,pass, server, bbdd);
            form2.Visible = true; 
        }

        private void startInformaticaSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Visible = true;
        }
    }
}
