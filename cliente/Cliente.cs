using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using trabalho6.comum;

namespace trabalho6
{
    public class Cliente
    {
        public string IpServidor { get; set; }
        public int Porta { get; set; }
        public Escrevivel Console { get; set; }

        private byte[] bufferEnvio = new byte[1000];
        private byte[] bufferLeitura = new byte[1000];

        private TcpClient cliente;
        private Thread thread;
        
        public Cliente(string ipServidor, int porta) : this(ipServidor, porta, new EscritorNulo()) {}

        public Cliente(string ipServidor, int porta, Escrevivel console)
        {
            IpServidor = ipServidor;
            Porta = porta;
            Console = console;
        }

        public void Conectar()
        {
            cliente = new TcpClient(Dns.GetHostName(), Porta);

            thread = new Thread(ProcessaLeitura);
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

                    Console.Escreve($"Recebido: {resposta}");
                }
            }
        }

        public void Enviar(string mensagem)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes("mensagem do cliente");

            NetworkStream stream = cliente.GetStream();

            stream.Write(data, 0, data.Length);




            // Close everything.
        }
  
    }
}
