﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Abstract
{
    public interface IEntityErrors
    {
        System.Collections.Generic.Dictionary<string, string> Messages { get; }
    }
}
