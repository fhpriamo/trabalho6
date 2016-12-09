using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using trabalho6.comum;

namespace trabalho6
{
    class Servidor
    {
        // Endereço ip da máquina em que servidor está rodando
        private IPAddress Ip { get; set; }

        // Porta do servidor
        private int Porta { get; set; }

        // Verdadeiro se o servidor está rodando; controla a atividade do servidor entre threads
        public bool Rodando { get; private set; }

        private Escrevivel console;

        // Torna possível instanciar um servidor passando somente a porta
        public Servidor(int porta) : this(porta, new EscritorNulo()) { }


        public Servidor (int porta, Escrevivel console)
        {
            // Obtem o ip da máquina em que o servidor está rodando
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            Ip = host.AddressList[0];
            Porta = porta;
            this.console = console;
        }

        public void Iniciar()
        {
            // Faz nada se o servidor já estiver rodando
            if (Rodando)
            {
                return;
            }

            TcpListener listener = new TcpListener(Ip, Porta);
            listener.Start();

            Rodando = true;

            console.Escreve($"O servidor está rodando na porta {Porta}");
            console.Escreve($"O endereço para conexão é: {listener.LocalEndpoint}");

            while (Rodando)
            {
                console.Escreve("Aguardando nova conexão...");

                ConexaoCliente conexao = new ConexaoCliente(listener.AcceptSocket(), console);
                conexao.Iniciar();
            }
        }
    }
}
