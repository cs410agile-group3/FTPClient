using System;
using System.IO;
using System.Collections;
using Renci.SshNet;


namespace FtpCli
{
  public class Cli
  {
    public bool AlwaysTrue() {
      return true;
    }
    
    public string Echo(string msg)
    {
      return msg;
    }

    public string LocalRename(string source, string target)
    {
      try {
          File.Move(source, target);
      }
      catch(FileNotFoundException)
      {
          return "The source file could not be found.";
      }
      return "Moved " + source + " to " + target + ".";
    }

    public string LLS(string dir)
    {
      if(!Directory.Exists(dir))
      {
        return "The directory " + dir + " does not exit.";
      }
      String s = "";
      foreach(String file in Directory.GetDirectories(dir))
      {
          s+=file+"\n";
      }
      foreach(String file in Directory.GetFiles(dir))
      {
          s+=file+"\n";
      }
      return s;
    }
    
  }
}
