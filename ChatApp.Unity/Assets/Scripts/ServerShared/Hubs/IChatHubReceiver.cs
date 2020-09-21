using ChatApp.Shared.MessagePackObjects;
using System.Collections.Generic;

namespace ChatApp.Shared.Hubs
{
    /// <summary>
    /// Server -> Client API
    /// </summary>
    public interface IChatHubReceiver
    {
        //void OnLogin();
        void OnJoin(string name);

        void OnCreateRoom(Room room);

        void OnLeave(Player player);

        void OnSendMessage(MessageResponse message);
    }
}
