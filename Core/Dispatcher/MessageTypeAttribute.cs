using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Dispatcher
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal class MessageAttribute : Attribute
    {
        public string Message;
        public string Type;
        public bool runSynchronously;

        public MessageAttribute(string messagename,string type = "NoTypePackets", bool runsync = false)
        {
            this.Message = messagename;
            this.Type = type;
            this.runSynchronously = runsync;
        }
    }
}