﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.WebSocketServer.Messages.Server
{
    public class DialogMessage : Abstract.IMessage
    {
        public string Message { get; set; }

        public int Sender { get; set; }

        public long Time { get; set; } = (long)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;
    }
}
