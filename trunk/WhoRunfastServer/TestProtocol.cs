using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using SuperWebSocket;
using SuperWebSocket.SubProtocol;
using SuperSocket.SocketBase.Command;
using Newtonsoft.Json;

namespace WhoRunfastServer
{ 
    public abstract class JsonWebSocketSubCommand<TJsonWebSocketCommandInfo> : JsonWebSocketSubCommand<WebSocketSession, TJsonWebSocketCommandInfo>
        where TJsonWebSocketCommandInfo : class, new()
    {

    }

    public abstract class JsonWebSocketSubCommand<TWebSocketSession, TJsonWebSocketCommandInfo> : SubCommandBase<TWebSocketSession>
        where TWebSocketSession : WebSocketSession<TWebSocketSession>, new()
        where TJsonWebSocketCommandInfo : class, new()
    {
        public override void ExecuteCommand(TWebSocketSession session, StringCommandInfo commandInfo)
        {
            var jsonCommandInfo = JsonConvert.DeserializeObject<TJsonWebSocketCommandInfo>(commandInfo.Data);
            ExecuteJsonCommand(session, jsonCommandInfo);
        }

        protected abstract void ExecuteJsonCommand(TWebSocketSession session, TJsonWebSocketCommandInfo commandInfo);

        protected string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
    //public class TestProtocol:ICustomProtocol<>
    //{
    //}
}
