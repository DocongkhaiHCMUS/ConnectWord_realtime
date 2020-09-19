using MessagePack;
using System.Collections.Generic;
using MagicOnion.Server.Hubs;
using System;

namespace ChatApp.Shared.MessagePackObjects
{
    [MessagePackObject]
    public class E2V
    {
        private string _word;
        private string _pronounce;
        private string _explain;

        [Key(0)]
        public string word { get => _word; set => _word = value; }
        [Key(1)]
        public string pronounce { get => _pronounce; set => _pronounce = value; }
        [Key(2)]
        public string explain { get => _explain; set => _explain = value; }

        public E2V(string word, string pronounce, string explain)
        {
            this.word = word;
            this.pronounce = pronounce;
            this.explain = explain;
        }
    }

    [MessagePackObject]
    public class Player
    {
        private string user_ID;
        private string user_Name;
        private int _score;

        [Key(0)]
        public string userID { get => user_ID; set => user_ID = value; }

        [Key(1)]
        public string userName { get => user_Name; set => user_Name = value; }

        [Key(2)]
        public int score { get => _score; set => _score = value; }

        public Player()
        {
                
        }

        public Player(string userID, string userName, int score)
        {
            this.userID = userID;
            this.userName = userName;
            this.score = score;
        }

        public Player(Player player)
        {
            this.userID = player.userID;
            this.userName = player.userName;
            this.score = player.score;
        }

    }

    [MessagePackObject]
    public class Room
    {
        private string room_ID;
        private string room_name;
        private string host_player;
        private int number_player;
        private List<Player> list_player;
        private List<Answer> list_answer;

        [Key(0)]
        public string roomID { get => room_ID; set => room_ID = value; }

        [Key(1)]
        public string roomName { get => room_name; set => room_name = value; }

        [Key(2)]
        public string hostPlayer { get => host_player; set => host_player = value; }

        [Key(3)]
        public int numberPlayer { get => number_player; set => number_player = value; }

        [Key(4)]
        public List<Player> lstPlayer { get => list_player; set => list_player = value; }

        [Key(5)]
        public List<Answer> List_answer { get => list_answer; set => list_answer = value; }

        public Room()
        {

        }

        public Room(string roomID, string roomName, string hostPlayer, int numberPlayer)
        {
            this.roomID = roomID;
            this.roomName = roomName;
            this.hostPlayer = hostPlayer;
            this.numberPlayer = numberPlayer;
        }
    }

    [MessagePackObject]
    public class Answer
    {
        private string _answer;
        private Player _player;
        private string room_ID;

        [Key(0)]
        public string answer { get => _answer; set => _answer = value; }

        [Key(1)]
        public Player player { get => _player; set => _player = value; }

        [Key(2)]
        public string Room_ID { get => room_ID; set => room_ID = value; }

        public Answer()
        {

        }

        public Answer(string answer, Player player, string room_ID)
        {
            this.answer = answer;
            this.player = player;
            Room_ID = room_ID;
        }

        public Answer(Answer a)
        {
            this.answer = a.answer;
            this.player = a.player;
            this.Room_ID = a.Room_ID;
        }
    }
}
