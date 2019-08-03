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
                .withCommand("ls", "lists files and directories in path on remote", (List<string> lsargs) => {
                    if (lsargs.Count == 0) {
                        connection.ListRemote(".");
                    } else connection.ListRemote(lsargs[0]);
                })
                .withCommand(
                    "get",
                    "get [src] [dest] : Gets file from remote (src) and writes to local (dest)",
                    (List<string> getArgs) => {
                        if (getArgs.Count != 2) throw new Exception("Must provide source and destination");
                        connection.GetFile(getArgs[0], getArgs[1]);
                    }
                )
                .withCommand(
                    "getmulti",
                    "get [src] : Gets multiple files from remote (src) and writes the files with the same names to local",
                    (List<string> getMultiArgs) => {
                        if (getMultiArgs.Count < 1) throw new Exception("Must provide at least one remote file to get");
                        connection.GetMultiFiles(getMultiArgs);
                    }
                )
                .withCommand(
                    "put",
                    "put [src] [dest] : Puts file from local (src) and writes it to remote (dest)",
                    (List<string> putArgs) => {
                        if (putArgs.Count != 2) throw new Exception("Must provide source and destination");
                        connection.PutFile(putArgs[0], putArgs[1]);
                    }
                 )
                .withCommand(
                    "putdir",
                    "putdir [src] [dest] : Puts directory from local (src) and writes it to remote (dest)",
                    (List<string> putArgs) => {
                        if (putArgs.Count != 2) throw new Exception("Must provide source and destination");
                        connection.PutDirectory(putArgs[0], putArgs[1]);
                    }
                 )
                .withCommand("chmod", "exits the program", (List<string> chmodArgs) => {
                    connection.ChangePermissions(chmodArgs[0], Convert.ToInt16(chmodArgs[1]));
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
