﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperWebSocket.Client
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageEventArgs(string message)
            : base()
        {
            Message = message;
        }
    }
}
