using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using trabalho6.comum;

namespace trabalho6
{
    public partial class Form1 : Form
    {
        private Servidor servidor;

        public Form1()
        {
            InitializeComponent();
            servidor = new Servidor(7474, new comum.Console(txtConsole));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(IniciarServidor));
            thread.IsBackground = true;
            thread.Start();

            button1.Text = "Rodando...";
            button1.Enabled = false;
        }

        private void IniciarServidor()
        {
            servidor.Iniciar();
        }
    }
}
