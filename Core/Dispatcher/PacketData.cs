using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Dispatcher
{
    public class PacketData
    {
        public object Instance;
        public string Key;
        public bool RunSynchronously;
        public MethodInfo Methode;

        public PacketData(object instance, string key, bool runSync, MethodInfo method)
        {
            this.Instance = instance;
            this.Key = key;
            this.RunSynchronously = runSync;
            this.Methode = method;
        }
    }
}