using ChatApp.BL;
using ChatApp.Shared.MessagePackObjects;
using MagicOnion.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server
{
    public static class Global
    {
        public static List<Room> listRoom = new List<Room>();
        public static Dictionary<string, IGroup> mapRoom = new Dictionary<string, IGroup>();
        public static List<E2V> dict = new List<E2V>();

        public static void Loaddict()
        {
            dict = modelEV_dict.loadDictionary();
        }
    }
}
