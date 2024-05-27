using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint IPEndPointConfiguration = new(IPAddress.Loopback, 4444);

using (var Servidor = new Socket(IPEndPointConfiguration.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    Servidor.Bind(IPEndPointConfiguration);
    Servidor.Listen();

    Console.WriteLine("Escutando na porta 4444");

    while (true)
    {
        using (var SocketClient = Servidor.Accept())
        {
            if (SocketClient.Connected)
            {
                Console.WriteLine("Alguém se conectou!");
            }

            while (true)
            {
                byte[] BufferRecebido = new byte[1024];
                var BufferRecebidoResposta = SocketClient.Receive(BufferRecebido, SocketFlags.None);
                var BufferRecebidoRespostaDecodificado = Encoding.UTF8.GetString(BufferRecebido, 0, BufferRecebidoResposta);
                Console.WriteLine($"Mensagem: {BufferRecebidoRespostaDecodificado}");
                Console.Write("Escreva a mensagem: ");
                string MensagemPCliente = Console.ReadLine();
                byte[] MensagemPClienteCodificado = Encoding.UTF8.GetBytes(MensagemPCliente);
                SocketClient.Send(MensagemPClienteCodificado);
                Console.WriteLine("Aguardando a mensagem...");
            }
        }
    }   
}