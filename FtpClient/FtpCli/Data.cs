using System;
using System.Collections.Generic;

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
    public void executeCommands() {
      Cli cli = new Cli();
      ConsoleParser.ConsoleParser parser = new ConsoleParser.ConsoleParser
                .Builder()
                .withCommand("echo", "Echo a command to the screen.", (List<string> a) => {Console.WriteLine(cli.Echo(a[0]));})
                .withCommand("localrename", "Rename a local file.", (List<string> a) => {Console.WriteLine(cli.LocalRename(a[0],a[1]));})
                .withCommand("lls", "List local files.", (List<string> a) => {Console.WriteLine(cli.LLS(a[0]));})
                .build();

      if (this.commands == null || this.commands[0] == "") {
        Console.WriteLine("no commands");
        return;
      }

      string commandToExecute = string.Join(" ", this.commands);
      Console.WriteLine(commandToExecute + "\n");
      parser.executeCommand(commandToExecute);

      //foreach (string arg in this.commands) {
      //  Console.Write($"{arg} ");

      //}
      Console.WriteLine("\n");
    }
  }
}
