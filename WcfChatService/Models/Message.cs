using System.Runtime.Serialization;

namespace WcfChatService.Models
{
    [DataContract]
    public class MessageModel
    {
        public MessageModel(string user, string message)
        {
            UserName = user;
            Text = message;
        }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string UserName { get; private set; }

        [DataMember]
        public string Text { get; private set; }
    }
}