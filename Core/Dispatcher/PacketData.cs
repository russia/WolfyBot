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
        public string Messagename;
        public string Typename;
        public bool RunSynchronously;
        public MethodInfo Methode;

        public PacketData(object instance, string messagename, bool runSync, MethodInfo method, string typename = null)
        {
            this.Instance = instance;
            this.Messagename = messagename;
            this.RunSynchronously = runSync;
            this.Methode = method;
            this.Typename = typename;
        }
    }
}