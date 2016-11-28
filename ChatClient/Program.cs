using System;
using System.Linq;
using ChatClient.ServiceReference;
using WcfChatService.Models;

namespace ChatClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new ChatServiceClient();
            try
            {
                Console.WriteLine("Input name!");
                var name = Console.ReadLine();
                client.Login(name);

                Console.WriteLine("Send message!");

                while (true)
                {
                    var mes = Console.ReadLine();
                    client.SendMessage(new MessageModel(name, mes));

                    var allMessages = client.GetMessages(1).ToList();
                    Console.WriteLine(allMessages.Count);
                    allMessages.ForEach(x => Console.WriteLine(x.Text));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                client.Abort();
                client.Close();
            }
        }
    }
}