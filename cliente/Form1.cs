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
            txtHostName.Text = Dns.GetHostName();
            btnEnviar.Enabled = false;
            txtPorta.Text = "7474";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            status.Text = "Conectando...";
            string ip = txtHostName.Text;
            int porta = Convert.ToInt32(txtPorta.Text);
            
            cliente = new Cliente(ip, porta, new comum.Console(txtResposta));
            cliente.Conectar();

            btnConectar.Enabled = false;
            txtHostName.Enabled = false;
            txtPorta.Enabled = false;
            btnEnviar.Enabled = true;
            

            status.Text = $"Conectado a {ip} pela porta {porta}";
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cliente == null)
                return;

            cliente.Enviar(UnidadeEscolhida());
        }

        private string UnidadeEscolhida()
        {
            if (radioFahrenheit.Checked)
            {
                return "FAHRENHEIT";
            }
            else if (radioKelvin.Checked)
            {
                return "KELVIN";
            }
            else
            {
                return "CELSIUS";
            }
        }
    }
}
