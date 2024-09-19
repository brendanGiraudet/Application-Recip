using application_recip.Enums;

namespace application_recip.Store.MessageStore.Actions;

public class SetMessageAction
{
    public string Message { get; }

    public MessageTypeEnum MessageType { get; }

    public SetMessageAction(string message, MessageTypeEnum messageType)
    {
        Message = message;
        MessageType = messageType;
    }
}
