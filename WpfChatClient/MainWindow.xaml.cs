using System.Windows;

namespace WpfChatClient
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model = new ChatViewModel();

            DataContext = Model;
        }

        public ChatViewModel Model { get; set; }
    }
}