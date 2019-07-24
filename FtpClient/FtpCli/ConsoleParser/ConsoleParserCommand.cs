using System;
using System.Collections.Generic;

namespace ConsoleParserNamespace
{
    public class ConsoleParserCommand
    {
        public string commandName { get; }
        public string helpText { get; }
        private Action<List<string>> manyArgsAction;

        public ConsoleParserCommand(string commandName, string helpText, Action<List<string>> action)
        {
            this.commandName = commandName;
            this.helpText = helpText;
            this.manyArgsAction = action;
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
    }
}