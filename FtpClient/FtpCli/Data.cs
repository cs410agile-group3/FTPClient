using System;

namespace FtpCli
{
  // The Data class saves session data for the server, user
  // and commands with arguments, if provided
  public class Data
  {
    string server;
    string user;
    string[] commands;

    public Data() {
      server = "";
      user = "";
      commands = null;
    }

    public void setServer(string server) {
      this.server = server;
    }

    public string getServer() {
      return this.server;
    }

    public void printServer() {
      Console.WriteLine(this.server);
    }

    public void setUser(string user) {
      this.user = user;
    }

    public string getUser() {
      return this.user;
    }

    public void printUser() {
      Console.WriteLine(this.user);
    }

    public void setCommands(string[] args) {
      this.commands = new string[args.Length];
      Array.Copy(args, 0, this.commands, 0, args.Length);
    }

    public string[] getCommands() {
      return this.commands;
    }

    public void printCommands() {
      if (this.commands == null) {
        Console.WriteLine("no commands");
        return;
      }

      if (this.commands[0] == "") {
        Console.WriteLine("no commands");
        return;
      }

      foreach (string arg in this.commands) {
        Console.Write($"{arg} ");
      }
      Console.Write("\n");
    }
  }
}
