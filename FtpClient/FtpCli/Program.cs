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
            session.confirmServer();
            session.confirmUser();
            session.confirmCommands();
        }
    }
}
