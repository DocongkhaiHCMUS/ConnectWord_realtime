using ChatApp.Shared.Hubs;
using ChatApp.Shared.MessagePackObjects;
using ChatApp.Shared.Services;
using Grpc.Core;
using MagicOnion.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ChatComponent : MonoBehaviour, IChatHubReceiver
    {
        private Channel channel;
        private IChatHub streamingClient;
        private IChatService client;

        private bool isJoin;
        private bool isSelfDisConnected;

        public Text ChatText;

        public Button JoinOrLeaveButton;

        public Text JoinOrLeaveButtonText;

        public Button SendMessageButton;

        public InputField Input;

        public InputField ReportInput;

        public Button SendReportButton;

        public Button DisconnectButon;
        public Button ExceptionButton;
        public Button UnaryExceptionButton;


        void Start()
        {
            this.InitializeClient();
            this.InitializeUi();
        }


        async void OnDestroy()
        {
            // Clean up Hub and channel
            await this.streamingClient.DisposeAsync();
            await this.channel.ShutdownAsync();
        }


        private async void InitializeClient()
        {
            // Initialize the Hub
            this.channel = new Channel("localhost", 3000, ChannelCredentials.Insecure);
            // for SSL/TLS connection
            //var serverCred = new SslCredentials(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "server.crt")));
            //this.channel = new Channel("test.example.com", 12345, serverCred);
            this.streamingClient = StreamingHubClient.Connect<IChatHub, IChatHubReceiver>(this.channel, this);
            this.RegisterDisconnectEvent(streamingClient);
            this.client = MagicOnionClient.Create<IChatService>(this.channel);
        }


        private void InitializeUi()
        {
            this.isJoin = false;

            this.SendMessageButton.interactable = false;
            this.ChatText.text = string.Empty;
            this.Input.text = string.Empty;
            this.Input.placeholder.GetComponent<Text>().text = "Please enter your name.";
            this.JoinOrLeaveButtonText.text = "Enter the room";
            this.ExceptionButton.interactable = false;
        }


        private async void RegisterDisconnectEvent(IChatHub streamingClient)
        {
            try
            {
                // you can wait disconnected event
                await streamingClient.WaitForDisconnect();
            }
            finally
            {
                // try-to-reconnect? logging event? close? etc...
                Debug.Log("disconnected server.");

                if (this.isSelfDisConnected)
                {
                    // there is no particular meaning
                    await Task.Delay(2000);

                    // reconnect
                    this.ReconnectServer();
                }
            }
        }


        public async void DisconnectServer()
        {
            this.isSelfDisConnected = true;

            this.JoinOrLeaveButton.interactable = false;
            this.SendMessageButton.interactable = false;
            this.SendReportButton.interactable = false;
            this.DisconnectButon.interactable = false;
            this.ExceptionButton.interactable = false;
            this.UnaryExceptionButton.interactable = false;

            //var request = new JoinRequest("A","1",new Player("1","Kai",0));
            //await this.streamingClient.CreateRoom(request);

            this.InitializeUi();

            if (this.isJoin)
                this.JoinOrLeave();

            await this.streamingClient.DisposeAsync();
        }

        public async void ReconnectInitializedServer()
        {
            if (this.channel != null)
            {
                var chan = this.channel;
                if (chan == Interlocked.CompareExchange(ref this.channel, null, chan))
                {
                    await chan.ShutdownAsync();
                    this.channel = null;
                }
            }
            if (this.streamingClient != null)
            {
                var streamClient = this.streamingClient;
                if (streamClient == Interlocked.CompareExchange(ref this.streamingClient, null, streamClient))
                {
                    await streamClient.DisposeAsync();
                    this.streamingClient = null;
                }
            }

            if (this.channel == null && this.streamingClient == null)
            {
                this.InitializeClient();
                this.InitializeUi();
            }
        }


        private void ReconnectServer()
        {
            this.streamingClient = StreamingHubClient.Connect<IChatHub, IChatHubReceiver>(this.channel, this);
            this.RegisterDisconnectEvent(streamingClient);
            Debug.Log("Reconnected server.");

            this.JoinOrLeaveButton.interactable = true;
            this.SendMessageButton.interactable = false;
            this.SendReportButton.interactable = true;
            this.DisconnectButon.interactable = true;
            this.ExceptionButton.interactable = true;
            this.UnaryExceptionButton.interactable = true;

            this.isSelfDisConnected = false;
        }


        #region Client -> Server (Streaming)
        public async void JoinOrLeave()
        {
            if (this.isJoin)
            {
                await this.streamingClient.LeaveAsync();
                this.InitializeUi();
            }
            else
            {
                var request = new JoinRequest("A", "1", new Player("1", "Kai123", 0));
                Player [] listPlayer = await this.streamingClient.JoinAsync(request);
                Debug.Log(listPlayer.Length);

                this.isJoin = true;
                this.SendMessageButton.interactable = true;
                this.JoinOrLeaveButtonText.text = "Leave the room";
                this.Input.text = string.Empty;
                this.Input.placeholder.GetComponent<Text>().text = "Please enter a comment.";
                this.ExceptionButton.interactable = true;
            }
        }


        public async void SendMessage()
        {
            if (!this.isJoin)
                return;

            Answer answer = new Answer();
            await this.streamingClient.SendMessageAsync(answer);

            this.Input.text = string.Empty;
        }

        public async void GenerateException()
        {
            // hub
            if (!this.isJoin) return;
            await this.streamingClient.GenerateException("client exception(streaminghub)!");
        }

        public void SampleMethod()
        {
            throw new System.NotImplementedException();
        }
        #endregion


        #region Server -> Client (Streaming)
        //public void OnJoin(string name, List<E2V> dict)
        //{
        //    this.ChatText.text += $"\n<color=grey>{name} entered the room .</color>";
        //    Debug.Log(dict);
        //}

        public void OnJoin(string name)
        {
            this.ChatText.text += $"\n<color=grey>{name} entered the room .</color>";
            //Debug.Log(dict.word);
        }


        public void OnLeave(Player player)
        {
            this.ChatText.text += $"\n<color=grey>{player.userName} left the room.</color>";
        }

        public void OnSendMessage(MessageResponse message)
        {
            this.ChatText.text += $"\n{message.answer}：{message.answer}";
        }
        #endregion


        #region Client -> Server (Unary)
        public async void SendReport()
        {
            await this.client.SendReportAsync(this.ReportInput.text);

            this.ReportInput.text = string.Empty;
        }

        public async void UnaryGenerateException()
        {
            // unary
            await this.client.GenerateException("client exception(unary)！");
        }

        public void OnCreateRoom(List<Room> lstRoom)
        {
            throw new System.NotImplementedException();
        }

        public void OnCreateRoom(Room room)
        {
            Debug.Log(room.roomName);
        }
        #endregion
    }
}
