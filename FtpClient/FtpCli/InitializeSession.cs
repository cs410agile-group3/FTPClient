using System;

namespace FtpCli
{
  public class InitializeSession
  {
      Data data;

      public InitializeSession() {
        data = new Data();
      }


    // prompt user for server, username, and commands (if provided)
    public Data initialPrompt(string[] args) {
      string response = "";

      // prompt for server
      do {
        response = this.promptUser("Enter Server: ");
      } while (!validateNonEmptyResponse(response));
      this.data.setServer(response);

      do {
        response = this.promptUser("Enter Username: ");
      } while (!validateNonEmptyResponse(response));
      this.data.setUser(response);

      response = this.promptUser("Enter Command (or `Enter` if none): ");
      string[] responseAsArray = response.Split(" ");
      this.data.setCommands(responseAsArray);

      return this.data;
    }

    // prompts user and returns input
    public string promptUser(string prompt) {

      Console.WriteLine(prompt);
      return Console.ReadLine();
    }

    // validates that the response contains valid string content
    public bool validateNonEmptyResponse(string response) 
    {
      // incorrect number of values passed in
      if (response == "") {
        return false;
      }
      string[] responseAsArray = response.Split(" ");
      if (responseAsArray.Length > 1) {
        return false;
      }
      return true;
    }

    public void confirmServer() {
      Console.Write("\nServer: ");
      this.data.printServer();
    }

    public void confirmUser() {
      Console.Write("\nUser: ");
      this.data.printUser();
    }

    public void confirmCommands() {
      Console.Write("\nCommands: ");
      this.data.printCommands();
    }
  }
}