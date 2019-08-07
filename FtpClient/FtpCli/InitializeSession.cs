using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FtpCli
{
  // The IntializeSession class prompts the user for session details
  public static class InitializeSession
  {
    private static DataContractJsonSerializer serializer =
      new DataContractJsonSerializer(typeof(Dictionary<string, ClientConfiguration>));
    private static string configFilename = "config.json";
    private static bool MatchArgs(string[] args, int idx, string longName, string shortName) {
      return idx < args.Length - 1 && (args[idx].Equals(longName) || args[idx].Equals(shortName));
    }

    private static string ReadPassword() {
        Console.Write("Enter password: ");
        StringBuilder builder = new StringBuilder();
        ConsoleKeyInfo nextKey = Console.ReadKey(true);

        while (nextKey.Key != ConsoleKey.Enter)
        {
            if (nextKey.Key == ConsoleKey.Backspace)
            {
                if (builder.Length > 0)
                {
                    --builder.Length;
                    // erase the last * as well
                    Console.Write(nextKey.KeyChar);
                    Console.Write(" ");
                    Console.Write(nextKey.KeyChar);
                }
            }
            else
            {
                builder.Append(nextKey.KeyChar);
                Console.Write("*");
            }
            nextKey = Console.ReadKey(true);
        }
        Console.WriteLine();
        return builder.ToString();
    }

    // prompt user for valid server, username, and command (if provided)
    public static Packages.ClientWrapper.Client initialize(string[] args) {
      Dictionary<string, string> keyValueArgs = new Dictionary<string, string>();
      string alias = null;
      int port = -1;

      if (args.Length == 1) {
        alias = args[0];
        try {
          string content = File.ReadAllText(configFilename);
          Dictionary<string, ClientConfiguration> configs = DeserializeClient(content);
          if (configs.ContainsKey(alias)) {
            ClientConfiguration config = configs[alias];
            keyValueArgs.Add("server", config.host);
            port = config.port;
            keyValueArgs.Add("user", config.username);
          } else throw new Exception("Alias not found");
        } catch (FileNotFoundException) {
          throw new Exception("No config file was present! Try saving a new config using the -save arg");
        }
      } else {
        for (int i = 0; i < args.Length; i++) {
          if (MatchArgs(args, i, "-server", "-s")) {
            keyValueArgs.Add("server", args[++i]);
          } else if (MatchArgs(args, i, "-port", "-p")) {
            keyValueArgs.Add("port", args[++i]);
          } else if (MatchArgs(args, i, "-user", "-u")) {
            keyValueArgs.Add("user", args[++i]);
          } else if (MatchArgs(args, i, "-save", "-s")) {
            keyValueArgs.Add("alias", args[++i]);
          }
        }
      }

      // Prompt user for values not specified on command line
      while (!keyValueArgs.ContainsKey("server") || !validateNonEmptyResponse(keyValueArgs["server"])) {
        keyValueArgs["server"] = promptUser("Enter Server: ");
      }
      if (port < 0) while (!keyValueArgs.ContainsKey("port") || !validateNonEmptyResponse(keyValueArgs["port"])) {
        keyValueArgs["port"] = promptUser("Enter Port: ");
      }
      while (!keyValueArgs.ContainsKey("user") || !validateNonEmptyResponse(keyValueArgs["user"])) {
        keyValueArgs["user"] = promptUser("Enter Username: ");
      }
      if (port < 0) port = Convert.ToInt32(keyValueArgs["port"]);
      if (alias == null && keyValueArgs.ContainsKey("alias")) {
        AppendClientToConfigFile(keyValueArgs["alias"], keyValueArgs["server"], port, keyValueArgs["user"]);
      }
      string password = ReadPassword();

      Console.WriteLine($"Connecting {keyValueArgs["user"]} to {keyValueArgs["server"]}:{port}");
      return new Packages.ClientWrapper.Client(
        keyValueArgs["server"],
        port,
        keyValueArgs["user"],
        password
      );
    }

    private static void AppendClientToConfigFile(string alias, string host, int port, string username) {
      string content;
      try {
        content = File.ReadAllText(configFilename);
      } catch (FileNotFoundException) {
        content = "{}";
      }
      Dictionary<string, ClientConfiguration> list = DeserializeClient(content);
      ClientConfiguration newConfig = new ClientConfiguration();
      newConfig.host = host;
      newConfig.port = port;
      newConfig.username = username;
      list.Add(alias, newConfig);
      File.WriteAllText(configFilename, SerializeClient(list));
    }

    private static string SerializeClient(Dictionary<string, ClientConfiguration> configList) {
      MemoryStream stream = new MemoryStream();
      serializer.WriteObject(stream, configList);
      byte[] json = stream.ToArray();
      stream.Close();
      return Encoding.UTF8.GetString(json, 0, json.Length);
    }

    private static Dictionary<string, ClientConfiguration> DeserializeClient(string json) {
      Dictionary<string, ClientConfiguration> config = new Dictionary<string, ClientConfiguration>();
      MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
      config = serializer.ReadObject(stream) as Dictionary<string, ClientConfiguration>;
      return config;
    }

    // prompts user and returns input
    public static string promptUser(string prompt) {

      Console.Write(prompt);
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
