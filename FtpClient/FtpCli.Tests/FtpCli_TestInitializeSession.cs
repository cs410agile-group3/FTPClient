using System;
using FtpCli;
using Xunit;

namespace FtpCli.UnitTests
{
  public class FtpCli_TestInitializeSession
  {
    private readonly InitializeSession _session;

    public FtpCli_TestInitializeSession() {
      _session = new InitializeSession();
    }

    [Theory]
    [InlineData("")]
    public void EmptyString_ReturnsFalse(string server) 
    {
      Assert.False(_session.validateNonEmptyResponse(server));
    }

    [Theory]
    [InlineData("  Name Too Long ")]
    public void MultiString_ReturnsFalse(string user) 
    {
      Assert.False(_session.validateNonEmptyResponse(user));
    }

    [Theory]
    [InlineData("FtpServer")]
    public void SingleString_ReturnsTrue(string server) 
    {
      Assert.True(_session.validateNonEmptyResponse(server));
    }

    [Theory]
    [InlineData("FtpServer Typos or Invalid Input")]
    public void MultipleStrings_ReturnsFalse(string server) 
    {
      Assert.False(_session.validateNonEmptyResponse(server));
    }
  }
}
