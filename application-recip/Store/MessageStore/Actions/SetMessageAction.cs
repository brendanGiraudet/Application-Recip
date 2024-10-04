using application_recip.Enums;

namespace application_recip.Store.MessageStore.Actions;

public record SetMessageAction(string Message, MessageTypeEnum MessageType)
{
    
}
