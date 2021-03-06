﻿using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfChatService.Models;

namespace WcfChatService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private readonly List<MessageModel> _messages;
        private readonly List<User> _users;

        private long _localId;

        public ChatService()
        {
            _users = new List<User>();
            _messages = new List<MessageModel>();
            _localId = 0;
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
            message.Id = _localId;
            _messages.Add(message);

            _localId++;
        }

        public List<MessageModel> GetMessages(long id)
        {
            return _messages.Where(x => x.Id > id).ToList();
        }
    }
}