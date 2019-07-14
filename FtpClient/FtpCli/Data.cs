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
      this.server = "";
      this.user = "";
      this.commands = null;
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

    public void setCommands(string args) {
      string[] splitArgs = args.Split(" ");
      this.commands = new string[splitArgs.Length];
      Array.Copy(splitArgs, 0, this.commands, 0, splitArgs.Length);
    }

    public string[] getCommands() {
      return this.commands;
    }

    public void printCommands() {
      if (this.commands == null || this.commands[0] == "") {
        Console.WriteLine("no commands");
        return;
      }

      foreach (string arg in this.commands) {
        Console.Write($"{arg} ");
      }
      Console.WriteLine("\n");
    }
  }
}
