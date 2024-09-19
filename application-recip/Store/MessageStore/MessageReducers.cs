using application_recip.Enums;
using application_recip.Store.MessageStore.Actions;
using Fluxor;

namespace application_recip.Store.MessageStore;

public static class MessageReducers
{
    #region SetMessage
    [ReducerMethod]
    public static MessageState ReduceSetMessageAction(MessageState state, SetMessageAction action) => new MessageState(currentState: state, message: action.Message, messageType: action.MessageType);

    #endregion

    #region CleanMessage
    [ReducerMethod]
    public static MessageState ReduceCleanMessageAction(MessageState state, CleanMessageAction action) => new MessageState(currentState: state, message: string.Empty, messageType: MessageTypeEnum.NotSet);

    #endregion
}
