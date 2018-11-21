﻿using System.Collections.Generic;
using HomeAutomation.Protocols.App.v0.RequestBuilders;

namespace HomeAutomation.Protocols.App.v0.RequestParser
{
  public interface IRequestDataParser
  {
    IRequest Parse(byte protocolVersion, ushort counter, IEnumerable<byte> data);
  }
}