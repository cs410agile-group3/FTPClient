using System;
using FtpCli;
using Xunit;

namespace FtpCli.UnitTests
{
  public class FtpCli_TestData
  {
    private readonly Data _data;

    public FtpCli_TestData() {
      _data = new Data();
    }

    [Theory]
    [InlineData("randomServer")]
    public void ServerSetterAndGetter_SetsCorrectValue(string server) 
    { 
      _data.setServer(server);
      Assert.Equal("randomServer", _data.getServer());
    }

    [Theory]
    [InlineData("randomUser")]
    public void UserSetterAndGetter_SetsCorrectValue(string user) 
    { 
      _data.setUser(user);
      Assert.Equal("randomUser", _data.getUser());
    }

    [Theory]
    [InlineData("randomCommands arbitrary unexpected handled correctly")]
    public void CommandSetterAndGetter_SetsCorrectValue(string commands) 
    { 
      _data.setCommands(commands);
      string[] expectedCommands = new string[5]{"randomCommands", "arbitrary", "unexpected", "handled", "correctly"};
      Assert.Equal(expectedCommands, _data.getCommands());
    }

  }
}
