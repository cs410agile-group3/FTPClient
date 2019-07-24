using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParserNamespace
{
  public class ConsoleParser
  {
    private List<ConsoleParserCommand> commands = new List<ConsoleParserCommand>();

    public void executeCommand(string command)
    {
      string[] args = command.Split(' ');
      if (args.Length < 1) {
        throw new InvalidOperationException($"Could not parse command: {command}");
      }
      string commandName = args[0];
      bool found = false;
      foreach (ConsoleParserCommand c in commands)
      {
        if (c.commandName == commandName)
        {
          List<string> commandArgs = new List<string>();
          for (int i = 1; i < args.Length; i++) commandArgs.Add(args[i]);
          c.execute(commandArgs);
          found = true;
          break;
        }
      }
      if (!found) throw new InvalidOperationException($"The command {commandName} was not found");
    }

    public string getHelp()
    {
      StringBuilder builder = new StringBuilder();
      foreach (ConsoleParserCommand command in commands)
      {
        builder.Append($"{command.commandName} - {command.helpText}\n");
      }
      return builder.ToString();
    }

    public class Builder
    {
      private ConsoleParser parser;
      public Builder() {
        this.parser = new ConsoleParser();
      }

      public Builder withCommand(string commandName, string helpText, Action<List<string>> action)
      {
        parser.commands.Add(new ConsoleParserCommand(commandName, helpText, action));
        return this;
      }

      public ConsoleParser build()
      {
        return this.parser;
      }
    }
  }
}
