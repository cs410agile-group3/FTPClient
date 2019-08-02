using System;
using FtpCli.Pkgs.Logger;
using Xunit;

namespace CmdLogger.UnitTests
{
  public class CommandLoggerTest
  {
    private ILogger _logger = new CommandLogger();

    [Fact]
    public void doesCreateLogFilePath()
    {
      string home = _logger.FilePath();
      Console.WriteLine($"Home Dir: {home}");
      Assert.True(home != "");
    }

  }
}
