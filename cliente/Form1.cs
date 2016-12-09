using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using trabalho6.comum;
using System.Net;

namespace trabalho6
{
    public partial class Form1 : Form
    {
        private Cliente cliente;

        public Form1()
        {
            InitializeComponent();
            txtIp.Text = Dns.GetHostName();
            txtIp.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            string ip = txtIp.Text;
            int porta = Convert.ToInt32(txtPorta.Text);
            
            cliente = new Cliente(ip, porta, new comum.Console(txtResposta));
            cliente.Conectar();
         
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cliente == null)
                return;

            cliente.Enviar("Mensagem para você.");
        }
    }
}
