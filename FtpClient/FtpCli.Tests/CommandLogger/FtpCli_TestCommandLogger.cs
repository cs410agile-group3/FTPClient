using System;
using FtpCli.Packages.Logger;
using Xunit;

namespace CmdLogger.UnitTests
{
  
  public class LoggerFixture : IDisposable
  {
    public CommandLogger logger;

    public LoggerFixture()
    {
      logger = new CommandLogger();
    }
    
    public void Dispose()
    {
      // Nothing needed
    }
  }
  
  
  public class CommandLoggerTest : IClassFixture<LoggerFixture>
  {
    LoggerFixture cmdFixture;
      
    public CommandLoggerTest(LoggerFixture l)
    {
      this.cmdFixture = l;
    }

    [Fact]
    public void doesCreateLogFilePath()
    {
      string home = this.cmdFixture.logger.FilePath();
      Console.WriteLine("\n~~~~");
      Console.WriteLine($"Log File Path: {home}");
      Assert.True(home != "");
    }

    // Need to have entire write/read from log cycle in
    // a single theory, due to the logger being reset
    // if set in another function
    [Fact]
    public void writesCommandsToFile()
    {
      string[] cmds = new string[]{ "my xy zz", "cp file1.txt dir1/" };
      
      cmdFixture.logger.Log(cmds[0]);
      cmdFixture.logger.Log(cmds[1]);
   
      string cpCmd = cmdFixture.logger.PrevLogItem();
      Assert.True(cpCmd == cmds[1]);

      string mvCmd = cmdFixture.logger.PrevLogItem();
      Assert.True(mvCmd == cmds[0]);
      
      string lastCmd = cmdFixture.logger.PrevLogItem();
      Assert.True(mvCmd == lastCmd);

      string nextCmd = cmdFixture.logger.NextLogItem();
      Assert.True(nextCmd == cmds[1]);

      string endCmd = cmdFixture.logger.NextLogItem();
      Assert.True(endCmd == "");
    }

  }
}
