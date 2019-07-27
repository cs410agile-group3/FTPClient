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

                .withCommand("rename","renames a file",(List<string>exitArgs)=>{
                    Console.WriteLine("Enter in the file you want to rename");
                    String source = Console.ReadLine();
                    if(source.Length == 0){
                        Console.WriteLine("File name entered is empty");
                        Environment.Exit(1); 
                    }
                    Console.WriteLine("Enter in the new file name");
                    String dest = Console.ReadLine();
                    if(dest.Length == 0){
                        Console.WriteLine("Destination File name entered is empty");
                        Environment.Exit(1); 
                    }
                    connection.Rename(source, dest);
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
