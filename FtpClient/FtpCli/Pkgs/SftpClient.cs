using System;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace FtpCli.Packages.ClientWrapper
{

    public class Client : IClient
    {
        private SftpClient _client;
        private ConnectionInfo _connection;
        private bool _isConnected;
        private delegate void UseCallback(SftpClient client);

        // Explicit constructor
        // Should be used in all cases
        public Client(string host, int port, string username, string password)
        {
             _connection = new ConnectionInfo(host, port, username,
                 new PasswordAuthenticationMethod(username, password)); 

             // All operations will be performed through the '_client'
             _client = new SftpClient(_connection);

             try {
                _client.Connect();
                _isConnected = true;
             } catch (Exception err) 
             {
                Console.WriteLine($"Unable to connect to {host}:{port}");
                Console.WriteLine(err.ToString());
                Environment.Exit(-1);
             }
        }


        public bool IsConnected {
          get { return _isConnected; }
        }

        public void Connect()
        {
            if(!_isConnected)
            {
               _client.Connect(); 
            }
        }

        public void Disconnect()
        {
            _client.Disconnect();
            _isConnected = false;
        }
    }
}
