﻿namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public interface IRequestBuilder
  {
    byte[] Build(int data);
    byte[] Build(params byte[] dataBytes);
  }
}