using System;
using System.IO;
using FtpCli;
using Xunit;

using FtpCli.Packages.ClientWrapper;

namespace FtpCli.UnitTests.Connect
{
    public class FtpCli_TestConnect
    {
        private readonly Client _client;
        private string host = "127.0.0.1";
        private int port = 7667;
        private string username = "mssuser";
        private string passwd = "mssuser";

        public FtpCli_TestConnect()
        {
            _client = new Client(host, port, username, passwd);
            Assert.True(_client.IsConnected);
        }

        [Fact]
        public void FtpCli_TestDisconnect() {
            _client.Disconnect();
            Assert.False(_client.IsConnected);
        }
    }
}
