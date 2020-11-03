using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Models.Chat
{
    public class ChatClient
    {
        private readonly Uri _socketUri = new Uri(Keys.BaseSocketUrl);

        private ClientWebSocket _socket;

        public ChatClient(ClientWebSocket socket)
        {
            _socket = socket;
        }

        // private async Task Connect()
        // {
        //     await _socket.ConnectAsync(_socketUri, CancellationToken.None);
        //
        //     _socket.CloseAsync((WebSocketCloseStatus) 0, null, CancellationToken.None);
        // }
    }
}