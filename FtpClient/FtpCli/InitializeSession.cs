using System;
using System.Diagnostics;
using System.Threading;

namespace FtpCli
{
  // The IntializeSession class prompts the user for session details
  public static class InitializeSession
  {

    // prompt user for valid server, username, and command (if provided)
    public static Packages.ClientWrapper.Client initialize(string[] args) {
      string server = "";
      string port = "";
      string user = "";
      string password = "";

      // set any server/user values that were passed through the command line
      int length = args.Length;

      if (args.Length >= 4) {
        server = args[0];
        port = args[1];
        user = args[2];
        password = args[3];
      }

      // prompt user for valid server value
      while (!validateNonEmptyResponse(server)) {
        server = promptUser("Enter Server: ");
      }

      // prompt user for valid port
      while (!validateNonEmptyResponse(port)) {
        port = promptUser("Enter Port: ");
      }

      // prompt user for valid username value
      while (!validateNonEmptyResponse(user)) {
        user = promptUser("Enter Username: ");
      }

      // prompt user for valid password
      while (!validateNonEmptyResponse(password)) {
        password = promptUser("Enter Password: ");
      }

      // print values as confirmation
      Console.WriteLine($"Connecting {user} to {server}:{port}");

      return new Packages.ClientWrapper.Client(server, Convert.ToInt32(port), user, password);
    }

    // prompts user and returns input
    public static string promptUser(string prompt) {

      Console.WriteLine(prompt);
      string response = Console.ReadLine();
      return response.Trim();
    }

    // validates that the response contains valid string content
    public static bool validateNonEmptyResponse(string response) 
    {
      // incorrect number of values passed in
      if (response == "") {
        return false;
      }

      // if more than one string value is passed in
      response.Trim();
      if (response.Contains(" ")) {
        return false;
      }
      return true;
    }
  }
}
