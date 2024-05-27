using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint IPEndPointConfiguration = new(IPAddress.Loopback, 4444);

using (var Cliente = new Socket(IPEndPointConfiguration.AddressFamily, SocketType.Stream, ProtocolType.Tcp)) {
    Cliente.Connect(IPEndPointConfiguration);

    if (Cliente.Connected)
    {
        Console.WriteLine("Conectado!");
    }

    while (true) {
        Console.Write("Escreva a mensagem: ");
        string EnviarPServidor = Console.ReadLine();
        byte[] EnviarPServidorCodificado = Encoding.UTF8.GetBytes(EnviarPServidor);
        Cliente.Send(EnviarPServidorCodificado);
        Console.WriteLine("Aguardando a mensagem...");
        byte[] BufferRecebido = new byte[1024];
        var BufferRecebidoResposta = Cliente.Receive(BufferRecebido, SocketFlags.None);
        var BufferRecebidoRespostaDecodificada = Encoding.UTF8.GetString(BufferRecebido, 0, BufferRecebidoResposta);
        Console.WriteLine($"Mensagem: {BufferRecebidoRespostaDecodificada}");
    }
}