﻿using System;
using System.Timers;
using ConsoleParserNamespace;
using System.Collections.Generic;
using FtpCli.Packages.ClientWrapper;
using FtpCli.Packages.ConsoleEventLoop;

namespace FtpCli
{
    class Program
    {
        // Timer to shutdown program if idle
        private static Timer timer;
        private static Client connection;
        
        static void Main(string[] args)
        {   
            // This timer will run while the user is being prompted for input
            // after a set time as defined in Interval, the program will shut down
            // if the user inputs any values the timer is reset
            timer = new Timer();
            timer.Interval = 50000;         
            timer.Elapsed += onTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;     

            // start a connection based on user input
            connection = InitializeSession.initialize(args);
            Cli cli = new Cli();
            ConsoleParser commandParser = new ConsoleParser.Builder()
                .withCommand("", "Empty Command", (List<string> emptyArgs) => {
                    // Do nothing
                })
                .withCommand("exit", "exits the program", (List<string> exitArgs) => {
                    timer.Stop();
                    timer.Dispose();
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
                    Console.WriteLine(string.Join(" ", echoArgs));
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
                        if (putArgs.Count < 2) throw new Exception("Must provide source and destination");

                        if(putArgs.Count == 2)
                            connection.PutFile(putArgs[0], putArgs[1]);
                        else
                            connection.PutMultipleFile(putArgs);
                 })
                .withCommand("chmod", "exits the program", (List<string> chmodArgs) => {
                    connection.ChangePermissions(chmodArgs[0], Convert.ToInt16(chmodArgs[1]));
                })

                .withCommand("cp","Copy a file from a source to a destination",(List<string> cp) =>{
                    Console.WriteLine("Enter in the directory you want to copy");
                    String source = Console.ReadLine();
                    if(source.Length == 0){
                        Console.WriteLine("Source entered is empty");
                        Environment.Exit(1); 
                    }
                    Console.WriteLine("Enter in the destination of the directory you want it to be copied to");
                    String dest = Console.ReadLine();
                    if(dest.Length == 0){
                        Console.WriteLine("Destination entered is empty");
                        Environment.Exit(1); 
                    }            
                    connection.CopyFile(source,dest);
                })
                .build();

            // Dont need to assign this to anything, as Run is
            // the only public method and everything
            // needed is passed in.
            new EventLoop().Run(commandParser.executeCommand, timer);
        }

        private static void onTimedEvent(Object source, System.Timers.ElapsedEventArgs e){
            Console.WriteLine("Connection Timeout");
            timer.Stop();
            timer.Dispose();
            connection.Disconnect();
            Environment.Exit(0);
        }
        
        // This function will reset the timer countdown
        private static void resetTimer(){
            timer.Stop();
            timer.Start();
        }
    }
}
