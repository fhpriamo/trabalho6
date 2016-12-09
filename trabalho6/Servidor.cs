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
        // Espaço de memória para armazenar as mensagens recebidas
        private byte[] buffer = new byte[100];

        // Socket da conexão
        private Socket socket;

        // Número de conexões
        private int conexoes = 0;

        // Endereço ip da máquina em que servidor está rodando
        private IPAddress Ip { get; set; }

        // Porta do servidor
        private int Porta { get; set; }

        // Verdadeiro se o servidor está rodando; controla a atividade do servidor entre threads
        public bool Rodando { get; private set; }

        public Escrevivel Console { get; set; }

        // Torna possível instanciar um servidor passando somente a porta
        public Servidor(int porta) : this(porta, new EscritorNulo()) { }


        public Servidor (int porta, Escrevivel console)
        {
            // Obtem o ip da máquina em que o servidor está rodando
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            Ip = host.AddressList[0];
            Porta = porta;
            Console = console;
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

            Console.Escreve($"O servidor está rodando na porta {Porta}");
            Console.Escreve($"O endereço para conexão é: {listener.LocalEndpoint}");

            while (Rodando)
            {
                Console.Escreve($"Clientes servidos: {conexoes}");
                Console.Escreve("Aguardando conexão...");

                socket = listener.AcceptSocket();

                conexoes++;

                Console.Escreve($"Nova conexão de {socket.RemoteEndPoint}");

                // lê a mensagem recebida
                int numBytesRecebidos = socket.Receive(buffer);
                String msgRecebida = Encoding.ASCII.GetString(buffer, 0, numBytesRecebidos);

                Console.Escreve($"Mensagem recebida: {msgRecebida}");

                // Envia a resposta
                byte[] msgParaEnviar = Encoding.ASCII.GetBytes($"Recebida: {msgRecebida}\n");
                socket.Send(msgParaEnviar);

                Console.Escreve($"Mensagem enviada: {msgParaEnviar.ToString()}");
            }
        }
    }
}
