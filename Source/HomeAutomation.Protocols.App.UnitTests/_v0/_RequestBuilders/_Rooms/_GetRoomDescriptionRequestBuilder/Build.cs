﻿using FluentAssertions;
using HomeAutomation.Protocols.App.v0.RequestBuilders;
using HomeAutomation.Protocols.App.v0.RequestBuilders.Rooms;
using Moq;
using Xunit;

namespace HomeAutomation.Protocols.App.UnitTests._v0._RequestBuilders._Rooms._GetRoomDescriptionRequestBuilder
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var counterMock = new Mock<ICounter>();
      counterMock.Setup(x => x.GetNext()).Returns(new byte[] {0x06, 0x07});
      var connectionIdentificationMock = new Mock<IConnectionIdentification>();
      connectionIdentificationMock.Setup(x => x.Current).Returns(new byte[] { 0xCC, 0xCC, 0xCC, 0xCC });
      var builder = new GetRoomDescriptionRequestBuilder(counterMock.Object, connectionIdentificationMock.Object);

      var bytes = builder.Build(0xAA, 0xBB, 0xCC, 0xDD);

      bytes[0].Should().Be(0x00, "protocol version");
      bytes[1].Should().Be(0x02, "request type 0");
      bytes[2].Should().Be(0x02, "request type 1");
      bytes[3].Should().Be(0x00, "request type 2");
      bytes[4].Should().Be(0x00, "request type 3");
      bytes[5].Should().Be(0x06, "counter");
      bytes[6].Should().Be(0x07, "counter");
      bytes[7].Should().Be(0xCC, "connection identifier");
      bytes[8].Should().Be(0xCC, "connection identifier");
      bytes[9].Should().Be(0xCC, "connection identifier");
      bytes[10].Should().Be(0xCC, "connection identifier");
      bytes[11].Should().Be(0x04, "data length");
      bytes[12].Should().Be(0x00, "data length");
      bytes[13].Should().Be(0xAA, "room identifier");
      bytes[14].Should().Be(0xBB, "room identifier");
      bytes[15].Should().Be(0xCC, "room identifier");
      bytes[16].Should().Be(0xDD, "room identifier");
      bytes[17].Should().Be(0xC1, "crc");
      bytes[18].Should().Be(0xB5, "crc");
    }
  }
}