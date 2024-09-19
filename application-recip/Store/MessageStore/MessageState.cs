using application_recip.Enums;
using Fluxor;

namespace application_recip.Store.MessageStore;

[FeatureState]
public class MessageState
{
    public string Message { get; }

    public MessageTypeEnum MessageType { get; }

    private MessageState()
    {
        Message = string.Empty;
    }

    public MessageState(MessageState? currentState = null, string? message = null, MessageTypeEnum? messageType = default)
    {
        Message = message ?? (currentState != null ? currentState.Message : string.Empty);
        MessageType = messageType ?? (currentState != null ? currentState.MessageType : MessageTypeEnum.NotSet);
    }
}
