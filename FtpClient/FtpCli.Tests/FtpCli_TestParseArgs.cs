using System;
using System.IO;
using FtpCli;
using Xunit;

namespace FtpCli.UnitTests
{
  public class FtpCli_TestParseArgs
  {
    private readonly Program _FTP;

    public FtpCli_TestParseArgs() {
      _FTP = new Program();
    }

    [Theory]
    [InlineData("awesomeFtpServer", "imaginaryUser")]
    public void ParseTwoArgs(params string[] args) 
    {
      _FTP.parseArgs(args);
      Assert.Equal("awesomeFtpServer", _FTP.server);
      Assert.Equal("imaginaryUser", _FTP.user);
      Assert.Null(_FTP.commands);
    }

    [Theory]
    [InlineData("FtpServer", "User", "extra", "and", "random", "commands")]
    public void ParseMultipleArgs(params string[] args) 
    {
      _FTP.parseArgs(args);
      string[] expected_cmds = new string[4]{ "extra", "and", "random", "commands" };
      Assert.Equal("FtpServer", _FTP.server);
      Assert.Equal("User", _FTP.user);
      Assert.Equal(expected_cmds, _FTP.commands);
    }
  }
}
