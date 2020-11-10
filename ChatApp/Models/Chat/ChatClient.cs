using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Models.Chat
{
    public class ChatClient
    {
        private readonly Uri _socketUri = new Uri(Keys.BaseSocketUrl);

        private ClientWebSocket _socket;

        public ChatClient(/*ClientWebSocket socket*/)
        {
            //_socket = socket;
        }

        public async void Connect()
        {
            try
            {
                _socket = new ClientWebSocket();
                // Connect to server
                await _socket.ConnectAsync(_socketUri, CancellationToken.None);
                // Start read thread
                ReadThread();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }

        public async void Disconnect()
        {
            // Disconnect from WSS
            await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure,"Disconnected", 
                CancellationToken.None);
            // Clear socket data
            _socket.Dispose();
        }
        
        private void ReadThread()
        {
            // Initialize read thread
            Task.Run(() =>
            {
                do
                {
                    Console.WriteLine("PEAR");
                    //await LogPear();
                } while (true);
            });
        }

        private void ReadMessage()
        {
            // Read message from server
            // Push to view model to update UI
        }

        public async Task SendMessage(string message)
        {
            // Send message to WSS
            var byteMessage = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(byteMessage);

            await _socket.SendAsync(segment, WebSocketMessageType.Text,
                true, CancellationToken.None);
        }
    }
}