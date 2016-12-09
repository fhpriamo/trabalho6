using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using trabalho6.comum;
using System.Net;
using System.Net.Sockets;

namespace trabalho6
{
    public class Cliente
    {
        public IPAddress IpServidor { get; set; }

        public string HostName { get; set; }
        public int Porta { get; set; }
        public Escrevivel Console { get; set; }

        private byte[] bufferEnvio = new byte[1000];
        private byte[] bufferLeitura = new byte[1000];

        private TcpClient cliente;
        private Thread thread;
        
        public Cliente(string hostName, int porta) : this(hostName, porta, new EscritorNulo()) {}

        public Cliente(string hostName, int porta, Escrevivel console)
        {
            HostName = hostName;
            Porta = porta;
            Console = console;
        }

        public void Conectar()
        {
            cliente = new TcpClient(HostName, Porta);

            thread = new Thread(ProcessaLeitura);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ProcessaLeitura()
        {
            NetworkStream stream = cliente.GetStream();

            while (true)
            {
                if (stream.DataAvailable)
                {
                    // String to store the response ASCII representation.
                    string resposta = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(bufferLeitura, 0, bufferLeitura.Length);
                    resposta = Encoding.ASCII.GetString(bufferLeitura, 0, bytes);

                    Console.Escreve($"Mensagem recebida: {resposta}");
                }
            }
        }

        public void Enviar(string mensagem)
        {
            Console.Escreve($"Mensagem enviada: {mensagem}");
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(mensagem);

            NetworkStream stream = cliente.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
}
