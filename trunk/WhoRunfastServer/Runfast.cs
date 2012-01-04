using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperWebSocket;
using SuperWebSocket.SubProtocol;
using System.Reflection;
using WhoRunfastServer.Command;

namespace WhoRunfastServer
{
    public class RunfastSession : WebSocketSession<RunfastSession>
    { 

    }
    public class RunfactServer : WebSocketServer<RunfastSession>
    {
        public RunfactServer()
            : base(new RunfastSubProtocol(new List<Assembly>() { typeof(Test).Assembly })
           )
        { }
    }
    public class RunfastSubProtocol:BasicSubProtocol<RunfastSession>
    {
        public RunfastSubProtocol(IEnumerable<Assembly> commandAssemblies):base(commandAssemblies)
        {
        }
    }
}
