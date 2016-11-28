using System.Collections.Generic;
using System.ServiceModel;
using WcfChatService.Models;

namespace WcfChatService
{
    [ServiceContract]
    public interface IChatService
    {
        /// <summary>
        ///     Вход в систему
        /// </summary>
        /// <param name="newUser"></param>
        [OperationContract]
        void Login(string newUser);

        /// <summary>
        ///     Выход из системы
        /// </summary>
        /// <param name="userName"></param>
        [OperationContract]
        void LogOut(string userName);

        /// <summary>
        ///     Отправка сообщения
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessage(MessageModel message);

        /// <summary>
        ///     Получение сообщений
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<MessageModel> GetMessages(long id);
    }
}