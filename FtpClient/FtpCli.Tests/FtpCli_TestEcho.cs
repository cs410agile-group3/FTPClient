using System;
using FtpCli;
using Xunit;

namespace FtpCli.UnitTests
{
  public class FtpCli_TestEcho
  {
    private readonly Cli _cli;

    public FtpCli_TestEcho() {
      _cli = new Cli();
    }

    [Theory]
    [InlineData("This is totally working...")]
    public void EchosString(string msg) 
    {
      string echo = _cli.Echo(msg);

      Assert.True(echo == "This is totally working...");
    }

    [Fact]
    public void AlwaysTrue() {
      Assert.True(_cli.AlwaysTrue());
    }
  }
}
