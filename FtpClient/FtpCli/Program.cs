using System;

namespace FtpCli
{
    class Program
    {
        string server;
        string user;
        string[] commands;

        public Program() {
            server = "";
            user = "";
            commands = null;
        }

        static void Main(string[] args)
        {
            string usage = "servername username [-cmd, args, ...]";
            while (args.Length < 2) {
                Console.WriteLine($"\nUSAGE:\n{usage}\n\nUSAGE (to start program and server):\ndotnet run {usage}\n\nPlease enter a valid command:");
                args = Console.ReadLine().Split();
            }

            Program FTP = new Program();

            FTP.parseArgs(args);
        }

        public void parseArgs(string[] args) {
            this.server = args[0];
            this.user = args[1];

            Console.WriteLine($"Server: {this.server}");
            Console.WriteLine($"User: {this.user}");

            if (args.Length > 2) {
                this.commands = new string[args.Length - 2];
                Array.Copy(args, 2, this.commands, 0, args.Length - 2);

                Console.Write("Commands: ");
                foreach (string arg in this.commands) {
                    Console.Write($"{arg} ");
                }
                Console.WriteLine();
            }
        }
    }
}
