﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;

namespace Mpgp.Shared.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException()
            : base("Forbidden")
        {
        }

        public ForbiddenException(string message)
            : base(message)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
