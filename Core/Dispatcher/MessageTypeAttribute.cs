using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Dispatcher
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal class MessageTypeAttribute : Attribute
    {
        public string Value;
        public bool runSynchronously;

        public MessageTypeAttribute(string v, bool runsync = false)
        {
            this.Value = v;
            this.runSynchronously = runsync;
        }
    }
}