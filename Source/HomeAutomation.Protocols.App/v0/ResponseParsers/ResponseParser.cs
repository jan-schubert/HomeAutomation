﻿using System;
using System.Linq;

namespace HomeAutomation.Protocols.App.v0.ResponseParsers
{
  public class ResponseParser : IV0ResponseParser
  {
    private readonly IResponseDataParserFactory _responseDataParserFactory;

    public ResponseParser(IResponseDataParserFactory responseDataParserFactory)
    {
      _responseDataParserFactory = responseDataParserFactory;
    }

    public IResponse Parse(byte[] dataBytes)
    {
      if(dataBytes == null || dataBytes.Length < 15)
        return new CommonErrorResponse(dataBytes, 0xFF, 0x05);

      var protocolVersion = dataBytes[0];
      if (protocolVersion != 0x00)
        return new CommonErrorResponse(dataBytes, 0xFF, 0x06);

      var requestType0 = dataBytes[1];
      var requestType1 = dataBytes[2];
      var requestType2 = dataBytes[3];
      var requestType3 = dataBytes[4];

      var responseDataParser = _responseDataParserFactory.TryCreate(requestType0, requestType1, requestType2, requestType3);
      if (responseDataParser == null)
        return new CommonErrorResponse(dataBytes, 0xFF, 0x04);

      var counter = BitConverter.ToUInt16(dataBytes, 5);

      var responseCode0 = dataBytes[7];
      var responseCode1 = dataBytes[8];

      var dataLength = BitConverter.ToUInt32(dataBytes, 9);
      var data = dataBytes.Skip(13).Take((int) dataLength);

      var crc0 = dataBytes[13 + dataLength];
      var crc1 = dataBytes[14 + dataLength];

      var computeChecksum = Crc16.ComputeChecksum(dataBytes.Take(dataBytes.Length - 2));
      if(crc0 != computeChecksum[0] || crc1 != computeChecksum[1])
        return new CommonErrorResponse(dataBytes, 0xFF, 0x01);

      return responseDataParser.Parse(protocolVersion, counter, responseCode0, responseCode1, data);
    }
  }
}