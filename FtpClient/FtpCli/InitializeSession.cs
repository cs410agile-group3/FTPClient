using System;

namespace FtpCli
{
  // The IntializeSession class prompts the user for session details
  // which are stored as a Data object
  public class InitializeSession
  {
    Data data;

    public InitializeSession() {
      this.data = new Data();
    }

    // prompt user for valid server, username, and command (if provided)
    public Data initialPrompt(string[] args) {
      string server = "";
      string port = "";
      string user = "";
      string password = "";
      string command = "";

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
        server = this.promptUser("Enter Server: ");
      }
      this.data.Server = server;

      // prompt user for valid port
      while (!validateNonEmptyResponse(port)) {
        port = this.promptUser("Enter Port: ");
      }
      this.data.Port = Convert.ToInt32(port);

      // prompt user for valid username value
      while (!validateNonEmptyResponse(user)) {
        user = this.promptUser("Enter Username: ");
      }
      this.data.User = user;

      // prompt user for valid password
      while (!validateNonEmptyResponse(password)) {
        password = this.promptUser("Enter Password: ");
      }
      this.data.Password = password;

      // print values as confirmation
      Console.WriteLine($"Connecting {this.data.User} to {this.data.Server}:{this.data.Port}");

      // prompt for command
      command = this.promptUser("\nEnter Command (or `Enter` if none): ");
      this.data.setCommand(command);

      // print values as confirmation
      Console.Write("Command: ");
      this.data.printCommand();

      return this.data;
    }

    // prompts user and returns input
    public string promptUser(string prompt) {

      Console.WriteLine(prompt);
      string response = Console.ReadLine();
      return response.Trim();
    }

    // validates that the response contains valid string content
    public bool validateNonEmptyResponse(string response) 
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
