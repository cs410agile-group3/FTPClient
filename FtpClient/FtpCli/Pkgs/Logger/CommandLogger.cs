using System;
using System.IO;

namespace FtpCli.Pkgs.Logger
{
  // This will append to the end of
  // a file the last command
  public class CommandLogger : ILogger
  {
    // In other words, the line of the file
    // the logger is currently reading
    private int _cursor;
    private int _numFileLines;
    // *nix: _logFilePath = $HOME/.config
    // Windows: Users ApplicationData folder (don't know the path)
    private string _logFilePath;

    public CommandLogger()
    {
      _logFilePath = _findLogFilePath();
      _cursor = 0;
      _numFileLines = 0;
    }

    ~CommandLogger()
    {
      _cleanLogFilePath();
    }

    private string _findLogFilePath()
    {
      // Find home dir
      string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      string logDirPath = appDataDir + "/ftpcli-410/";
      string logFilePath = logDirPath + ".history";

      if(!Directory.Exists(logDirPath))
      {
        Directory.CreateDirectory(logDirPath);
      }

      if(!File.Exists(logFilePath))
      {
        File.Create(logFilePath);
      }
     
      return logFilePath;
    }

    private void _cleanLogFilePath()
    {
      // Delete the file
    }

    private void _writeDataToFile(string cmd)
    {
      // Write the command to the specified file
       
    }

    private string _dataAtCursor()
    {
      // Check to see if within cursor bounds
      return "";
    }

    // Will move the cursor towards the most
    // recent commands (i.e. end of the file)
    private void _incCursor()
    {
      _cursor += 1;
      if(_cursor > _numFileLines)
      {
        _numFileLines = _cursor;
      }
    }

    // Will move the cursor towards the
    // beginning of the file
    private void _decCursor()
    {
      if(_cursor - 1 <= 0)
      {
        _cursor = 0;
        return;
      }
      _cursor -= 1; 
    }

    public string FilePath()
    {
      return _logFilePath;
    }

    public void Log(string cmd)
    {
      _writeDataToFile(cmd);
      _incCursor();
    }

    public string PrevLogItem()
    {
      _decCursor();
      return _dataAtCursor(); 
    }

    public string NextLogItem()
    {
      _incCursor();
      return _dataAtCursor();
    }

    public void DeleteLogs()
    {
      // Delete
    }
  }
}
