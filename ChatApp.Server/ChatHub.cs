using ChatApp.BL;
using ChatApp.Shared.Hubs;
using ChatApp.Shared.MessagePackObjects;
using MagicOnion.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Server
{
    /// <summary>
    /// Chat server processing.
    /// One class instance for one connection.
    /// </summary>
    public class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
    {
        //private IGroup curIGRoom;
        //private string myName;
        public List<Room> LoadRoom()
        {
            return Global.listRoom;
        }

        public async Task CreateRoom(JoinRequest request)
        {
            string roomID = Global.listRoom.Count.ToString();
            Room newRoom = new Room(roomID, request.RoomName, request.player.userID, 20);
            IGroup newIGRoom = await this.Group.AddAsync(request.RoomName);

            Global.listRoom.Add(newRoom);
            Global.mapRoom.Add(roomID, newIGRoom);

            this.Broadcast(newIGRoom).OnCreateRoom(Global.listRoom);
        }

        public async Task startGame()
        {
            await modelEV_dict.loadDictionary();
        }

        public async Task JoinAsync(JoinRequest request)
        {
            IGroup curIGRoom = Global.mapRoom[request.RoomID];
            int index = int.Parse(request.RoomID);
            Room curRoom = Global.listRoom[index];

            curRoom.lstPlayer.Add(new Player(request.player));

            //this.room = await this.Group.AddAsync(request.RoomName);
            //this.myName = request.player.userName;

            this.Broadcast(curIGRoom).OnJoin(request.player.userName, Global.dict[0]);
            
        }

        public async Task LeaveAsync(JoinRequest request)
        {
            IGroup curIGRoom = Global.mapRoom[request.RoomID];
            int index = int.Parse(request.RoomID);
            Room curRoom = Global.listRoom[index];

            //await curIGRoom.RemoveAsync(this.Context);
            curRoom.lstPlayer.Remove(request.player);

            this.Broadcast(curIGRoom).OnLeave(request.player.userName);
        }

        public async Task SendMessageAsync(Answer answer)
        {
            IGroup curIGRoom = Global.mapRoom[answer.Room_ID];
            int index = int.Parse(answer.Room_ID);
            Room curRoom = Global.listRoom[index];

            curRoom.List_answer.Add(new Answer(answer));
            this.Broadcast(curIGRoom).OnSendMessage(new MessageResponse(answer.player.userName,answer.answer));

            await Task.CompletedTask;
        }

        public Task GenerateException(string message)
        {
            throw new Exception(message);
        }

        // It is not called because it is a method as a sample of arguments.
        public Task SampleMethod(List<int> sampleList, Dictionary<int, string> sampleDictionary)
        {
            throw new System.NotImplementedException();
        }

        protected override ValueTask OnConnecting()
        {
            // handle connection if needed.
            Console.WriteLine($"client connected {this.Context.ContextId}");
            return CompletedTask;
        }

        protected override ValueTask OnDisconnected()
        {
            // handle disconnection if needed.
            // on disconnecting, if automatically removed this connection from group.
            return CompletedTask;
        }
    }
}
