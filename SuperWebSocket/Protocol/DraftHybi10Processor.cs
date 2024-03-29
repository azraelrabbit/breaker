﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SuperSocket.Common;
using SuperSocket.SocketBase.Protocol;

namespace SuperWebSocket.Protocol
{
    class DraftHybi10Processor : ProtocolProcessorBase
    {
        private const string m_SecWebSocketVersion = "8";
        private const string m_Magic = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        public override bool Handshake(IWebSocketSession session, WebSocketReaderBase previousReader, out ICommandReader<WebSocketCommandInfo> dataFrameReader)
        {
            dataFrameReader = null;

            if (!m_SecWebSocketVersion.Equals(session.SecWebSocketVersion) && NextProcessor != null)
            {
                return NextProcessor.Handshake(session, previousReader, out dataFrameReader);
            }

            session.ProtocolProcessor = this;

            var secWebSocketKey = session.Items.GetValue<string>(WebSocketConstant.SecWebSocketKey, string.Empty);

            if (string.IsNullOrEmpty(secWebSocketKey))
            {
                return false;
            }

            var responseBuilder = new StringBuilder();

            string secKeyAccept = string.Empty;

            try
            {
                secKeyAccept = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(secWebSocketKey + m_Magic)));
            }
            catch (Exception)
            {
                return false;
            }

            responseBuilder.AppendLine("HTTP/1.1 101 Switching Protocols");
            responseBuilder.AppendLine("Upgrade: WebSocket");
            responseBuilder.AppendLine("Connection: Upgrade");
            responseBuilder.AppendLine(string.Format("Sec-WebSocket-Accept: {0}", secKeyAccept));

            var subProtocol = session.GetAvailableSubProtocol(session.Items.GetValue<string>(WebSocketConstant.SecWebSocketProtocol, string.Empty));

            if (!string.IsNullOrEmpty(subProtocol))
                responseBuilder.AppendLine(string.Format("Sec-WebSocket-Protocol: {0}", subProtocol));

            responseBuilder.AppendLine();
            session.SendRawResponse(responseBuilder.ToString());

            dataFrameReader = new WebSocketDataFrameReader(session.AppServer);

            return true;
        }

        public override void SendMessage(IWebSocketSession session, string message)
        {
            SendMessage(session, OpCode.Text, message);
        }

        public override void SendCloseHandshake(IWebSocketSession session, string closeReason)
        {
            SendMessage(session, OpCode.Close, closeReason);
        }

        public override void SendPong(IWebSocketSession session, string ping)
        {
            SendMessage(session, OpCode.Pong, ping);
        }

        private void SendMessage(IWebSocketSession session, int opCode, string message)
        {
            byte[] playloadData = Encoding.UTF8.GetBytes(message);

            int length = playloadData.Length;

            byte[] headData;

            if (length < 126)
            {
                headData = new byte[2];
                headData[1] = (byte)length;
            }
            else if (length < 65536)
            {
                headData = new byte[4];
                headData[1] = (byte)126;
                headData[2] = (byte)(length / 256);
                headData[3] = (byte)(length % 256);
            }
            else
            {
                headData = new byte[10];
                headData[1] = (byte)127;

                int left = length;
                int unit = 256;

                for (int i = 9; i > 1; i--)
                {
                    headData[i] = (byte)(left % unit);
                    left = left / unit;

                    if (left == 0)
                        break;
                }
            }

            headData[0] = (byte)(opCode | 0x80);

            session.SocketSession.SendResponse(headData);
            session.SocketSession.SendResponse(playloadData);
        }
    }
}
