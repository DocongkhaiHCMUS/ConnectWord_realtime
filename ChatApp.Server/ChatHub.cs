using ChatApp.BL;
using ChatApp.Shared.Hubs;
using ChatApp.Shared.MessagePackObjects;
using MagicOnion.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server
{
    /// <summary>
    /// Chat server processing.
    /// One class instance for one connection.
    /// </summary>
    public class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
    {
        private IGroup room;
        private Player self;
        private IInMemoryStorage<Player> storage;

        public async Task<Room[]> CreateRoom(JoinRequest request)
        {
            string roomID = Global.listRoom.Count.ToString();
            Room newRoom = new Room(roomID, request.RoomName, request.player.userID, 20);
            room = await this.Group.AddAsync(request.RoomName);

            Global.listRoom.Add(newRoom);
            Global.mapRoom.Add(roomID, room);

            this.Broadcast(room).OnCreateRoom(newRoom);

            return Global.listRoom.ToArray();
        }

        public async Task<Player[]> JoinAsync(JoinRequest request)
        {
            self = new Player(request.player);

            // Group can bundle many connections and it has inmemory-storage so add any type per group. 
            (room, storage) = await Group.AddAsync(request.RoomID, self);

            // Typed Server->Client broadcast.
            Broadcast(room).OnJoin(request.player.userName);

            return storage.AllValues.ToArray();
        }

        public async Task LeaveAsync()
        {
            await room.RemoveAsync(this.Context);
            Broadcast(room).OnLeave(self);
        }

        public async Task SendMessageAsync(Answer answer)
        {
            await Task.CompletedTask;
        }

        public Task GenerateException(string message)
        {
            throw new Exception(message);
        }

        //// It is not called because it is a method as a sample of arguments.
        //public Task SampleMethod(List<int> sampleList, Dictionary<int, string> sampleDictionary)
        //{
        //    throw new System.NotImplementedException();
        //}

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
