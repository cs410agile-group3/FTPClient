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
    private string _logFileDir;
    private string _logFilePath;

    public CommandLogger()
    {
      // A bit messy, FIX?
      _logFileDir = Path.Combine(
        Environment
          .GetFolderPath(
            Environment
              .SpecialFolder
              .ApplicationData
          ),
          "ftpcli-410/"
     );

      _logFilePath = _findLogFilePath(_logFileDir);
      _cursor = 0;
      _numFileLines = 0;
    }

    ~CommandLogger()
    {
      _cleanLogFilePath();
    }

    // PRIVATE *******************************************

    private string _findLogFilePath(string dirPath)
    {
      // Find home dir
      string logFilePath = Path.Combine(dirPath, ".history");

      if(!Directory.Exists(dirPath))
      {
        Directory.CreateDirectory(dirPath);
      }

      if(!File.Exists(logFilePath))
      {
        File.Create(logFilePath).Close();
      }
      return logFilePath;
    }

    private void _cleanLogFilePath()
    {
      if(File.Exists(_logFilePath))
      {
        File.Delete(_logFilePath);
      }
    }

    private void _writeDataToFile(string cmd)
    {
      // Write the command to the specified file
      using (StreamWriter writer = File.AppendText(_logFilePath))
      {
        writer.WriteLine(cmd);
      }
      // Need to make sure the upper bound
      // stays up to date
      _incLineNum();
    }

    private string _readLineAtCursor()
    {
      // Check to see if within cursor bounds
      int currentRow = 0;
      foreach(string line in File.ReadLines(_logFilePath))
      {
        // Need to check to make sure
        // the file is not reading out of bounds
        if(currentRow == _numFileLines)
        {
          return "";
        }

        if(currentRow == _cursor)
        {
          return line;
        } 
        currentRow += 1;
      }

      return "";
    }

    private void _incLineNum()
    {
      _numFileLines += 1;
      _cursor = _numFileLines; // Want to make the cursor reset each time a line is added
    }

    // Will move the cursor towards the most
    // recent commands (i.e. end of the file)
    private void _incCursor()
    {
      _cursor += 1;
      if(_cursor > _numFileLines)
      {
        _cursor = _numFileLines;
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

    // PUBLIC ****************************************************

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
      return _readLineAtCursor(); 
    }

    public string NextLogItem()
    {
      _incCursor();
      return _readLineAtCursor();
    }

    public int NumOfLines()
    {
      return _numFileLines; 
    }

    public void DeleteLogs()
    {
      // Delete
      _cleanLogFilePath();
    }
  }
}
