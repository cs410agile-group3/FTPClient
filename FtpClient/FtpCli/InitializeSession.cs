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

    // prompt user for valid server, username, and commands (if provided)
    public Data initialPrompt(string[] args) {
      string server = "";
      string user = "";
      string commands = "";

      // set any server/user values that were passed through the command line
      if (args.Length >= 2) {
        server = args[0];
        user = args[1];
      }

      // prompt user for valid server value
      while (!validateNonEmptyResponse(server)) {
        server = this.promptUser("Enter Server: ");
      }
      this.data.setServer(server);

      // prompt user for valid username value
      while (!validateNonEmptyResponse(user)) {
        user = this.promptUser("Enter Username: ");
      }
      this.data.setUser(user);

      // prompt for commands
      commands = this.promptUser("Enter Command (or `Enter` if none): ");
      this.data.setCommands(commands);

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
