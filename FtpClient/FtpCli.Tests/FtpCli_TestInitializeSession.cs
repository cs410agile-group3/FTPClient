using System;
using FtpCli;
using Xunit;

namespace FtpCli.UnitTests
{
  public class FtpCli_TestInitializeSession
  {
    [Theory]
    [InlineData("")]
    public void EmptyString_ReturnsFalse(string server) 
    {
      Assert.False(InitializeSession.validateNonEmptyResponse(server));
    }

    [Theory]
    [InlineData("  Name Too Long ")]
    public void MultiString_ReturnsFalse(string user) 
    {
      Assert.False(InitializeSession.validateNonEmptyResponse(user));
    }

    [Theory]
    [InlineData("FtpServer")]
    public void SingleString_ReturnsTrue(string server) 
    {
      Assert.True(InitializeSession.validateNonEmptyResponse(server));
    }

    [Theory]
    [InlineData("FtpServer Typos or Invalid Input")]
    public void MultipleStrings_ReturnsFalse(string server) 
    {
      Assert.False(InitializeSession.validateNonEmptyResponse(server));
    }
  }
}
