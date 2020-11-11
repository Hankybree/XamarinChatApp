using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Services;

namespace ChatApp.Models.Chat
{
    public class ChatClient : IObservable<Message>
    {
        private readonly Uri _socketUri = new Uri(Keys.BaseSocketUrl);
        private readonly IPreferences _preferences;

        private ClientWebSocket _socket;
        
        List<IObserver<Message>> observers;

        public ChatClient(IPreferences preferences)
        {
            _preferences = preferences;
            observers = new List<IObserver<Message>>();
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
                } while (_socket.State == WebSocketState.Open);
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
                foreach (var observer in observers)
                    observer.OnNext(messageObject);
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

        // Observable
        public IDisposable Subscribe(IObserver<Message> observer)
        {
            if( observers.Count == 0)
                Connect();
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new UnSubscriber(observers, observer);
        }
        
        private class UnSubscriber : IDisposable
        {
            private List<IObserver<Message>> _observers;
            private IObserver<Message> _observer;

            public UnSubscriber(List<IObserver<Message>> observers, IObserver<Message> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null) _observers.Remove(_observer);
            }
        }
    }
}