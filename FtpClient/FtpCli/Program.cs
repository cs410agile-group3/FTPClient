using System;
using ConsoleParserNamespace;
using System.Collections.Generic;

namespace FtpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            // start a connection based on user input
            Packages.ClientWrapper.Client connection = InitializeSession.initialize(args);

            ConsoleParser commandParser = new ConsoleParser.Builder()
                .withCommand("exit", "exits the program", (List<string> exitArgs) => {
                    connection.Disconnect();
                    Environment.Exit(0);
                })
                .build();
            while (true) {
                try {
                    Console.Write(">> ");
                    commandParser.executeCommand(Console.ReadLine());
                } catch (Exception e) {
                    Console.Write("Unable to process command: ");
                    Console.WriteLine(e);
                }
            }
        }
    }
}
