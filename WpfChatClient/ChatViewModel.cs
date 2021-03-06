﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WpfChatClient.ChatServiceReference;
using WpfChatClient.Utils;

namespace WpfChatClient
{
    public class ChatViewModel : Notifier
    {
        private ChatServiceClient _client;
        private Visibility _loginButVisibility;
        private string _messageText;
        private string _userName;
        private Visibility _userNameTxtBoxVis;
        private Visibility _userTxtBlVisibility;

        public ChatViewModel()
        {
            UserNameTxtBlockVisibility = Visibility.Collapsed;
            LoginCommand = new RelayCommand(Login);
            Messages = new ObservableCollection<MessageModel>();
            SendCommand = new RelayCommand(SendMessage);
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Текст сообщения
        /// </summary>
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Свойство видимости кнопки Login
        /// </summary>
        public Visibility LoginButtonVisibility
        {
            get { return _loginButVisibility; }
            set
            {
                _loginButVisibility = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Свойство видимости TextBlock с UserName
        /// </summary>
        public Visibility UserNameTxtBlockVisibility
        {
            get { return _userTxtBlVisibility; }
            set
            {
                _userTxtBlVisibility = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Свойство видимости TextBox с UserName
        /// </summary>
        public Visibility UserNameTxtBoxVisibility
        {
            get { return _userNameTxtBoxVis; }
            set
            {
                _userNameTxtBoxVis = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Комманда обработчик для кнопки Login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        ///     Комманда обработчик для отправки сообщений
        /// </summary>
        public ICommand SendCommand { get; set; }

        /// <summary>
        ///     Список сообщений
        /// </summary>
        public ObservableCollection<MessageModel> Messages { get; set; }

        /// <summary>
        ///     Логинимся
        /// </summary>
        private void Login()
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                LoginButtonVisibility = Visibility.Collapsed;
                UserNameTxtBoxVisibility = Visibility.Collapsed;
                UserNameTxtBlockVisibility = Visibility.Visible;

                try
                {
                    _client = new ChatServiceClient();
                    _client.Login(UserName);

                    var tmrShow = new Timer { Interval = 500, Enabled = true };
                    tmrShow.Elapsed += tmrShow_Tick;
                }
                catch (Exception ex)
                {
                    _client.Abort();
                    _client.Close();
                }
              
            }
        }

        /// <summary>
        ///     Периодический запрашиваем новые сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrShow_Tick(object sender, EventArgs e)
        {
            var lastMes = Messages.LastOrDefault();
            var allMessages = lastMes != null
                ? _client.GetMessages(lastMes.Id).ToList()
                : _client.GetMessages(0).ToList();

            Application.Current.Dispatcher.BeginInvoke(new Action(
                () => allMessages.ForEach(x => Messages.Add(x))));
        }

        /// <summary>
        ///     Отправка сообщения
        /// </summary>
        private void SendMessage()
        {
            _client.SendMessage(new MessageModel { Text = MessageText, UserName = UserName });
            MessageText = string.Empty;
        }

        public void CloseApplication()
        {
            _client.LogOut(UserName);
            _client.Close();
        }
    }
}