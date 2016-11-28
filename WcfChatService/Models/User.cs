using System;
using System.Runtime.Serialization;

namespace WcfChatService.Models
{
    [DataContract]
    public class User
    {
        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public Guid Id { get; private set; }
    }
}