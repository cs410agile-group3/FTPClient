using System;
using System.Text;
using System.Timers;
using FtpCli.Packages.Logger;

namespace FtpCli.Packages.ConsoleEventLoop
{
    public class EventLoop
    {
  
        private static Timer _timer;
        private StringBuilder consoleBuffer;
        private CommandLogger _logger;
        private ConsoleKeyInfo keyInfo;
        private int consoleLeftCursor;
        private int consoleTopCursor;

        public EventLoop()
        {
            consoleBuffer = new StringBuilder();
            _logger = new CommandLogger();
        }

        // This function will reset the timer countdown
        private static void _resetTimer(){
            _timer.Stop();
            _timer.Start();
        }
        
        private void _refreshConsole()
        {
            // These values are set after '>> ' is
            // written to the console, so
            // the left cursor will be after the
            // space. The row cursor in this instance will
            // allways be zero.
            Console.SetCursorPosition(consoleLeftCursor, consoleTopCursor);
            Console.Write(new string(' ', consoleBuffer.Length + 1));
            Console.SetCursorPosition(consoleLeftCursor, consoleTopCursor);
        }

        public void Run(Action<string> execCmdFn, object timer)
        {
            _timer = (Timer)timer;
            Console.TreatControlCAsInput = true;

            while (true) 
            {
                  try 
                  {
                      Console.Write(">> ");

                      consoleLeftCursor = Console.CursorLeft;
                      consoleTopCursor = Console.CursorTop;

                      while(true)
                      {
                          _refreshConsole();
                          Console.Write(consoleBuffer.ToString());
                          // 'true' means the keys are not logged to the console
                          // this is needed in order to work with getting
                          // command history, and hence, the need for a
                          // consoleBuffer
                          keyInfo = Console.ReadKey(true);
                          
                          if(keyInfo.Key == ConsoleKey.Enter)
                          {
                              break;
                          }
                                    
                          if(keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow)
                          {
                              continue;
                          }

                          if(keyInfo.Key == ConsoleKey.UpArrow)
                          {
                              // Clears the console & consoleBuffer, and then
                              // sets the consoleBuffer to
                              // the appropriate log value.
                              _refreshConsole();

                              consoleBuffer.Clear();
                              consoleBuffer.Append(_logger.PrevLogItem());
                              continue;
                          } 
                                    
                          if(keyInfo.Key == ConsoleKey.DownArrow)
                          {
                              // See previous if() block
                              _refreshConsole();

                              consoleBuffer.Clear();
                              consoleBuffer.Append(_logger.NextLogItem());
                              continue;
                          }

                          if(keyInfo.Key == ConsoleKey.Backspace)
                          {
                              // Without this check
                              // an error will be thrown when
                              // the console buffer has run out
                              if(consoleBuffer.Length > 0)
                              {
                                consoleBuffer.Remove(consoleBuffer.Length - 1, 1);
                              }
                              continue;
                          }

                          if(keyInfo.Key == ConsoleKey.Spacebar)
                          {
                              consoleBuffer.Append(" ");
                              continue;
                          }

                          if((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
                          {
                              consoleBuffer.Append(keyInfo.KeyChar);
                              continue;
                          }

                          if((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
                          {
                              if(keyInfo.Key == ConsoleKey.C)
                              {
                                  Console.WriteLine("Goodbye...");
                                  Environment.Exit(0);
                              } 
                          }

                          consoleBuffer.Append(Char.ToLower(keyInfo.KeyChar));
                      } 

                      _logger.Log(consoleBuffer.ToString());
                      
                      // Otherwise the first line of output
                      // is rendered in the console prompt
                      Console.WriteLine("");
                      
                      // ExecCmdFn is passed in from main loop
                      // in Program.cs
                      execCmdFn(consoleBuffer.ToString());
                      consoleBuffer.Clear();
                      _resetTimer();

                } catch (Exception) {
                    Console.WriteLine("Unable to process command: {0}", consoleBuffer.ToString());            
                    consoleBuffer.Clear();
                }
            }
        }
    }
}
