using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using trabalho6.comum;

namespace trabalho6
{
    class ConexaoCliente
    {
        private Thread thread;

        private byte[] bufferOut = new byte[1000];
        private byte[] bufferIn = new byte[1000];

        private Escrevivel console;
        private Socket socket;

        public ConexaoCliente(Socket socket) : this(socket, new EscritorNulo()) { }

        public ConexaoCliente(Socket socket, Escrevivel console)
        {
            this.console = console;
            this.socket = socket;
            thread = new Thread(Servir);
            thread.IsBackground = true;
        }

        public void Iniciar()
        {
            thread.Start();
            console.Escreve($"conexao iniciada com {socket.RemoteEndPoint}");
        }

        private void Servir()
        {
            while (socket.Connected)
            {
                int numBytesRecebidos;
                // lê a mensagem recebida
                try
                {
                    numBytesRecebidos = socket.Receive(bufferIn);
                }
                catch (SocketException ex)
                {
                    console.Escreve(ex.Message + socket.RemoteEndPoint.ToString());
                    break;                    
                }
                
                string mensagemRecebida = Encoding.ASCII.GetString(bufferIn, 0, numBytesRecebidos);

                console.Escreve($"Mensagem recebida: {mensagemRecebida}");

                // Envia a resposta
                TimeSpan horaDoDia = DateTime.Now.TimeOfDay;
                Temperatura temperatura = Sensor.Leitura(DateTime.Now.TimeOfDay);
                string resposta = horaDoDia.ToString();

                switch (mensagemRecebida)
                {
                    case "CELSIUS":
                        resposta += $" {temperatura.Celsius} {Temperatura.CelsiusUnit}";
                        break;
                    case "FAHRENHEIT":
                        resposta += $" {temperatura.Fahrenheit} {Temperatura.FahrenheitUnit}";
                        break;
                    case "KELVIN":
                        resposta += $" {temperatura.Kelvin} {Temperatura.KelvinUnit}";
                        break;
                    default:
                        resposta = "MENSAGEM MAL FORMULADA";
                        break;
                }

                byte[] bytesResposta = Encoding.ASCII.GetBytes(resposta);
                socket.Send(bytesResposta);

                console.Escreve($"Mensagem enviada: {resposta}");
            }

            socket?.Close();
        }

        private bool ClienteConectado
        {
           get
           {
                try
                {
                    return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
                }
                catch (SocketException) { return false; }
           }
        }
    }
}
