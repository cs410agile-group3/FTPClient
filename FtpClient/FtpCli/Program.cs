using System;

namespace FtpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeSession session = new InitializeSession();
            Data data = session.initialPrompt(args);
            Packages.ClientWrapper.Client connection = new Packages.ClientWrapper.Client(data.Server, data.Port, data.User, data.Password);


        }
    }
}
