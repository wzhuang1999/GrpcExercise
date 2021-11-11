﻿using ProtoBuf;
using System;

namespace Entities
{
    [ProtoContract]
    public class Address
    {
        [ProtoMember(1)]
        public string Line1 { get; set; }

        [ProtoMember(2)]
        public string Line2 { get; set; }
    }
}
