using System;
using System.Collections.Generic;

namespace ConsoleParser
{
    public class ConsoleParserCommand
    {
        public string commandName { get; }
        private string helpText { get; set; }
        private Action<List<string>> manyArgsAction;
        private Action<string> singleArgAction;

        public ConsoleParserCommand(string commandName, string helpText, Action<List<string>> action)
        {
            this.commandName = commandName;
            this.helpText = helpText;
            this.manyArgsAction = action;
        }

        public ConsoleParserCommand(string commandName, string helpText, Action<string> action)
        {
            this.commandName = commandName;
            this.helpText = helpText;
            this.singleArgAction = action;
        }

        public void execute(List<string> args)
        {
            if (this.manyArgsAction != null)
            {
                this.manyArgsAction(args);
            } else
            {
                throw new InvalidOperationException($"The command {this.commandName} cannot be run with many arguments");
            }
        }

        public void execute(string arg)
        {
            if (this.singleArgAction != null)
            {
                this.singleArgAction(arg);
            } else
            {
                throw new InvalidOperationException($"The command {this.commandName} cannot be run with many arguments");
            }
        }
    }
}