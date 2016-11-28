using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfChatService.Models;

namespace WcfChatService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private readonly List<MessageModel> _messages = new List<MessageModel>();
        private readonly List<User> _users;

        public ChatService()
        {
            _users = new List<User>();
        }

        public void Login(string newUser)
        {
            var isExist = _users.Any(x => x.Name == newUser);
            if (!isExist)
            {
                _users.Add(new User(newUser));
            }
        }

        public void LogOut(string userName)
        {
            var user = _users.FirstOrDefault(x => x.Name == userName);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public void SendMessage(MessageModel message)
        {
            _messages.Add(message);
        }

        public List<MessageModel> GetMessages()
        {
            return _messages;
        }
    }
}