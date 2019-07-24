using System;

namespace FtpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            // start a connection based on user input
            Packages.ClientWrapper.Client connection = InitializeSession.initialize(args);

            // prompt
            Console.Write(">> ");
        }
    }
}
