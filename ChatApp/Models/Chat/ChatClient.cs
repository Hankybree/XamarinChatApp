using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Services;

namespace ChatApp.Models.Chat
{
    public class ChatClient
    {
        private readonly Uri _socketUri = new Uri(Keys.BaseSocketUrl);
        private IPreferences _preferences;

        private ClientWebSocket _socket;

        public ChatClient(IPreferences preferences)
        {
            _preferences = preferences;
        }

        public async void Connect()
        {
            try
            {
                _socket = new ClientWebSocket();
                _socket.Options.SetRequestHeader("authorization", 
                    _preferences.GetString(Keys.TokenString));
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
            try
            {
                // Disconnect from server
                await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure,"Disconnected", 
                    CancellationToken.None);
                // Clear socket data
                _socket.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
        
        private void ReadThread()
        {
            // Initialize read thread
            Task.Run(async () =>
            {
                do
                {
                    await ReadMessage();
                } while (true);
            });
        }

        private async Task ReadMessage()
        {
            // Read message from server
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new byte[4096]);
            do 
            {
                result = await _socket.ReceiveAsync(message, CancellationToken.None);
                if (result.MessageType != WebSocketMessageType.Text)
                    break;
                var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                var receivedMessage = Encoding.UTF8.GetString(messageBytes);
                var values = receivedMessage.Split('|');
                var messageObject = new Message(values[0], values[1]);
                Console.WriteLine(messageObject.UserName + " " + messageObject.Content);
            } 
            while (!result.EndOfMessage);
            // Push to view model to update UI
        }

        public async Task SendMessage(string message)
        {
            try
            {
                // Send message to server
                var byteMessage = Encoding.UTF8.GetBytes(message);
                var segment = new ArraySegment<byte>(byteMessage);

                await _socket.SendAsync(segment, WebSocketMessageType.Text,
                    true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
    }
}