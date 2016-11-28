using System.Collections.Generic;
using System.ServiceModel;
using WcfChatService.Models;

namespace WcfChatService
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        void Login(string newUser);

        [OperationContract]
        void LogOut(string userName);

        [OperationContract]
        void SendMessage(MessageModel message);

        //[OperationContract]
        //List<User> GetUsers();

        [OperationContract]
        List<MessageModel> GetMessages(long id);
    }


    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}