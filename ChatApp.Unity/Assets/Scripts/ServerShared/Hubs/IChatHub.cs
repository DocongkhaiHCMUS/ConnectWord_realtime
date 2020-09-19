using ChatApp.Shared.MessagePackObjects;
using MagicOnion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Shared.Hubs
{
    /// <summary>
    /// Client -> Server API (Streaming)
    /// </summary>
    public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
    {
        List<Room> LoadRoom();
        Task CreateRoom(JoinRequest request);
        Task JoinAsync(JoinRequest request);

        Task startGame();

        Task LeaveAsync(JoinRequest request);

        Task SendMessageAsync(Answer answer);

        Task GenerateException(string message);

        // It is not called because it is a method as a sample of arguments.
        Task SampleMethod(List<int> sampleList, Dictionary<int, string> sampleDictionary);
    }
}
