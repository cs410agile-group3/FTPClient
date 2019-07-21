using System;

namespace FtpCli
{
  // The Data class saves session data for the server, port, 
  // user, password, and command with arguments, if provided

  public class Data
  {
    private string server;
    private int port;
    private string user;
    private string password;
    public string[] command;

    public string Server {
      get { return server; }
      set { server = value; }
    }

    public int Port {
      get { return port; }
      set { port = value; }
    }

    public string User {
      get { return user; }
      set { user = value; }
    }

    public string Password {
      get { return password; }
      set { password = value; }
    }

    public Data() {
      this.command = null;
    }

    public void setCommand(string args) {
      string[] splitArgs = args.Split(" ");
      this.command = new string[splitArgs.Length];
      Array.Copy(splitArgs, 0, this.command, 0, splitArgs.Length);
    }

    public string[] getCommand() {
      return this.command;
    }

    public void printCommand() {
      if (this.command == null || this.command[0] == "") {
        Console.WriteLine("no command");
        return;
      }

      foreach (string arg in this.command) {
        Console.Write($"{arg} ");
      }
      Console.WriteLine("\n");
    }
  }
}
