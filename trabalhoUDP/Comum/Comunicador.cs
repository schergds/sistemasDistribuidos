using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Comum
{
    // Classe que esconde os detalhes do UDP.
    // Tanto o cliente quanto o servidor usam ela para enviar e receber textos.
    public class Comunicador
    {
        private UdpClient udp;

        // porta = 0 -> o sistema escolhe uma porta livre (usado pelo cliente)
        // porta = 5000, por exemplo -> fica escutando nessa porta (usado pelo servidor)
        public Comunicador(int porta)
        {
            udp = new UdpClient(porta);
        }

        // Envia um texto para o endereço/porta de destino
        public void Enviar(string mensagem, IPEndPoint destino)
        {
            byte[] dados = Encoding.UTF8.GetBytes(mensagem);
            udp.Send(dados, dados.Length, destino);
        }

        // Espera chegar uma mensagem. Devolve o texto e, em "origem", quem enviou.
        public string Receber(out IPEndPoint origem)
        {
            origem = new IPEndPoint(IPAddress.Any, 0);
            byte[] dados = udp.Receive(ref origem);
            return Encoding.UTF8.GetString(dados);
        }

        // Tempo máximo (em milissegundos) que Receber() fica esperando.
        // Se passar desse tempo, Receber() lança uma SocketException.
        public void DefinirTimeout(int milissegundos)
        {
            udp.Client.ReceiveTimeout = milissegundos;
        }

        public void Fechar()
        {
            udp.Close();
        }
    }
}
