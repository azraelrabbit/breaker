using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using System.Configuration;
using SuperSocket.SocketEngine;
using SuperSocket.SocketEngine.Configuration;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperWebSocket;

namespace WhoRunfastServer
{
    class Program
    {
        static void Main(string[] args)
        {
            LogUtil.Setup();
            SocketServiceConfig serverConfig = ConfigurationManager.GetSection("socketServer") as SocketServiceConfig;            
            if (!SocketServerManager.Initialize(serverConfig))
            {
                return;
            }
            
            if (!SocketServerManager.Start())
            {
                SocketServerManager.Stop();
                return;
            }
            Console.ReadLine();
        }
    }
}
