using MessagePack;

namespace ChatApp.Shared.MessagePackObjects
{
    /// <summary>
    /// Room participation information
    /// </summary>
    [MessagePackObject]
    public class JoinRequest
    {
        private string roomName;
        private string roomID;
        private Player _player;

        [Key(0)]
        public string RoomName { get => roomName; set => roomName = value; }

        [Key(1)]
        public string RoomID { get => roomID; set => roomID = value; }

        [Key(2)]
        public Player player { get => _player; set => _player = value; }

        public JoinRequest(string roomName, string roomID, Player player)
        {
            RoomName = roomName;
            RoomID = roomID;
            this.player = player;
        }

        public JoinRequest()
        {

        }
    }

    //[MessagePackObject]
    //public struct CreateHostRequest
    //{
    //    [Key(0)]
    //    public string RoomName { get; set; }

    //    [Key(1)]
    //    public string UserName { get; set; }
    //}

}
