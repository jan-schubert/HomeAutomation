﻿namespace HomeAutomation.Protocols.App.v0.Responses
{
  public interface IHaveResponseTypeInformation
  {
    byte RequestType0 { get; }
    byte RequestType1 { get; }
    byte RequestType2 { get; }
    byte RequestType3 { get; }
  }
}