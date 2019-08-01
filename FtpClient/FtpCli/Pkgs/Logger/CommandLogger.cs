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
    private string _homeDir;
    
    public CommandLogger()
    {
      _homeDir = _findHomeDir();
      _cursor = 0;
      _numFileLines = 0;
    }

    ~CommandLogger()
    {
      _cleanHomeDir();
    }

    private string _findHomeDir()
    {
      // Find home dir
      return "";
    }

    private void _cleanHomeDir()
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
