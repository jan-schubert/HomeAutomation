﻿namespace HomeAutomation.Protocols.App.v0.RequestBuilders
{
  public class ConnectionIdentification : IConnectionIdentification
  {
    private byte[] _current;

    public byte[] Current
    {
      get
      {
        if (_current == null || _current.Length < 5)
        {
          return new byte[] { 0x00, 0x00, 0x00, 0x00 };
        }
        return _current;
      }
      set => _current = value;
    }
  }
}