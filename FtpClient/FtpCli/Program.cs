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
            Cli cli = new Cli();
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

                .withCommand("echo", "print argument to screen", (List<string> echoArgs) => {
                    Console.WriteLine(echoArgs[0]);
                })
                .withCommand("localrename", "Rename a local file.", (List<string> a) => {
                    Console.WriteLine(cli.LocalRename(a[0],a[1]));
                })
                .withCommand("lls", "List local files.", (List<string> a) => {
                    String dir = "./";
                    if(a.Count>=1)
                    {
                        dir = a[0];
                    }
                    Console.WriteLine(cli.LLS(dir));
                })
                .withCommand("rmsfile", "Remove file from server.", (List<string> a) => {
                    if(a.Count==0)
                    {
                        Console.WriteLine("USAGE: rmsfile <file to delete>");
                    }
                    else
                    {
                        connection.RemoteDeleteFile(a[0]);
                        Console.WriteLine("Deleted Successfuly.");
                    }
                })
                .withCommand("rmsdir", "Remove directory from server.", (List<string> a) => {
                    if(a.Count==0)
                    {
                        Console.WriteLine("USAGE: rmsdir <directory to delete>");
                    }
                    else
                    {
                        try {
                            connection.RemoteDeleteDirectory(a[0]);
                            Console.WriteLine("Deleted Successfuly.");
                        } catch (Exception)
                        {
                            Console.WriteLine("Directory is non-empty.  Please empty the directory.");
                        }
                    }
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
