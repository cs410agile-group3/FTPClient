using System;
using System.Collections.Generic;

namespace ConsoleParser
{
  public class ConsoleParser
  {
    private List<ConsoleParserCommand> commands = new List<ConsoleParserCommand>();

    public void executeCommand(string command)
    {
      string[] args = command.Split(' ');
      string commandName = args[0];
      bool found = false;
      foreach (ConsoleParserCommand c in commands)
      {
        if (c.commandName == commandName)
        {
          if (args.Length < 2)
          {
            throw new InvalidOperationException("Too few arguments");
          } else if (args.Length > 2)
          {
            List<string> commandArgs = new List<string>();
            for (int i = 1; i < args.Length; i++) commandArgs.Add(args[i]);
            c.execute(commandArgs);
          } else
          {
            c.execute(args[1]);
          }
          found = true;
          break;
        }
      }
      if (!found) throw new InvalidOperationException($"The command {commandName} was not found");
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

      public Builder withCommand(string commandName, string helpText, Action<string> action)
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
