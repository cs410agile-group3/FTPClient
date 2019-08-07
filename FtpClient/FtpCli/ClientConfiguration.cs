using System.Runtime.Serialization;

namespace FtpCli {
    [DataContract]
    public class ClientConfiguration {
        [DataMember]
        public string host;
        [DataMember]
        public int port;
        [DataMember]
        public string username;
    }
}