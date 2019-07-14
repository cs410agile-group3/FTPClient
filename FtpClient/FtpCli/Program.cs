using System;

namespace FtpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeSession session = new InitializeSession();
            Data data = session.initialPrompt(args);

            // print values as confirmation
            Console.Write($"\nConnecting to {data.getServer()} with username {data.getUser()}");
            Console.Write("Commands: ");
            data.printCommands();
        }
    }
}
