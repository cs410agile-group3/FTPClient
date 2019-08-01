using System;
using FtpCli.Pkgs.Logger;
using Xunit;

namespace CmdLogger.UnitTests
{
  public class CommandLoggerTest
  {
    private ILogger _logger = new CommandLogger();

    [Fact]
    public void doesFindHomeDir()
    {
    }
  }
}
