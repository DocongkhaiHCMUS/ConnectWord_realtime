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
        void OnJoin(string name,E2V dict);

        void OnCreateRoom(List<Room> lstRoom);

        void OnLeave(string name);

        void OnSendMessage(MessageResponse message);
    }
}
