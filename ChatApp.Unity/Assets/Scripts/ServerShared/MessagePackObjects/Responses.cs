using MessagePack;

namespace ChatApp.Shared.MessagePackObjects
{
    /// <summary>
    /// Message information
    /// </summary>
    [MessagePackObject]
    public struct MessageResponse
    {
        [Key(0)]
        public string userName { get; set; }

        [Key(1)]
        public string answer { get; set; }

        public MessageResponse(string userName, string answer)
        {
            this.userName = userName;
            this.answer = answer;
        }
    }
}
