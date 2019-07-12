using System;

namespace FtpCli
{
    public class Program
    {
        public string server;
        public string user;
        public string[] commands;

        public Program() {
            server = "";
            user = "";
            commands = null;
        }

        static void Main(string[] args)
        {
            Program FTP = new Program();

            FTP.parseArgs(args);
        }


        // parses command line arguments
        public void parseArgs(string[] args) {

            // if an incorect number of parameters were passed
            // print usage details and prompt user again
            string usage = "servername username [cmd, args, ...]";
            while (args.Length < 2) {
                Console.WriteLine($"\nUSAGE:\n{usage}\n\n");
                Console.WriteLine($"USAGE (to start program and server):\ndotnet run {usage}\n\n");
                Console.WriteLine("Please enter valid arguments:");
                args = Console.ReadLine().Split();
            }

            // set server and user values
            this.server = args[0];
            this.user = args[1];

            // print values as confirmation
            Console.WriteLine($"Server: {this.server}");
            Console.WriteLine($"User: {this.user}");

            // if the user provided more arguments
            // copy them into the `commands` array
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
