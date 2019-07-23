using System;
using ConsoleParser;

namespace FtpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeSession session = new InitializeSession();
            Data data = session.initialPrompt(args);

            // print values as confirmation
            Console.WriteLine($"\nConnecting to {data.getServer()} with username {data.getUser()}");
            Console.Write("Commands: ");
            data.printCommands();
            Console.Write("Execute Commands: ");
            data.executeCommands();


            



        }
    }
}
