using System;
using FtpCli;
using Xunit;

namespace FtpCli.UnitTests
{
  public class FtpCli_TestLocalRename
  {
    private readonly Cli _cli;

    public FtpCli_TestLocalRename() {
      _cli = new Cli();
    }

    [Theory]
    [InlineData("The source file could not be found.")]
    public void RenameFileNotFound(string msg) 
    {
      string localRenameResult = _cli.LocalRename("a.txt","b.txt");

      Assert.Equal(msg,localRenameResult);
    }

    [Fact]
    public void AlwaysTrue() {
      Assert.True(_cli.AlwaysTrue());
    }
  }
}
