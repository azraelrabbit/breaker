using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperWebSocket.SubProtocol;
using SuperWebSocket;

namespace WhoRunfastServer.Command
{
    /// <summary>
    /// When the client send "CHAT {'Sender':'kerry', 'Receiver': 'Linda', 'Content':'Where are you now?'}",
    /// the method of this class will be executed
    /// </summary>
    public class Test : JsonWebSocketSubCommand<RunfastSession, TestCommandInfo>
    {
        protected override void ExecuteJsonCommand(RunfastSession session, TestCommandInfo commandInfo)
        {
            session.SendResponseAsync(commandInfo.p1 + ":|" + commandInfo.p2);
        }
    }
}
