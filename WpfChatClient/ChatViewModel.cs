using System.Collections.ObjectModel;
using System.Linq;
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

        private void Login()
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                LoginButtonVisibility = Visibility.Collapsed;
                UserNameTxtBoxVisibility = Visibility.Collapsed;
                UserNameTxtBlockVisibility = Visibility.Visible;

                _client = new ChatServiceClient();
                _client.Login(UserName);
            }
        }

        private void SendMessage()
        {
            _client.SendMessage(new MessageModel {Text = MessageText, UserName = UserName});
            MessageText = string.Empty;

            var allMessages = _client.GetMessages().ToList();

            allMessages.ForEach(x => Messages.Add(x));
        }
    }
}