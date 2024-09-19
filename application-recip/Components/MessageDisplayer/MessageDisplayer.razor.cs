using application_recip.Enums;
using application_recip.Store.MessageStore;
using application_recip.Store.MessageStore.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace application_recip.Components.MessageDisplayer;

public partial class MessageDisplayer
{
    [Inject] public required NotificationService NotificationService { get; set; }

    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Inject] public required IState<MessageState> MessageState { get; set; }

    //[Inject] public required IStringLocalizer<LabelTranslations> LabelTranslationsLocalizer { get; set; }


    public NotificationSeverity MessageNotificationSeverity
    {
        get
        {
            NotificationSeverity notificationSeverity;

            switch (MessageState.Value.MessageType)
            {
                case MessageTypeEnum.Info:
                    notificationSeverity = NotificationSeverity.Info;
                    break;

                case MessageTypeEnum.Error:
                    notificationSeverity = NotificationSeverity.Error;
                    break;

                case MessageTypeEnum.Success:
                    notificationSeverity = NotificationSeverity.Success;
                    break;

                default:
                    notificationSeverity = NotificationSeverity.Warning;
                    break;
            }

            return notificationSeverity;
        }
    }

    public string MessageSummary
    {
        get
        {
            var messageSummary = string.Empty;
            switch (MessageState.Value.MessageType)
            {
                case MessageTypeEnum.NotSet:
                    break;

                case MessageTypeEnum.Error:
                    messageSummary = "Errors";
                    //messageSummary = LabelTranslationsLocalizer["Errors"];
                    break;

                case MessageTypeEnum.Success:
                    //messageSummary = LabelTranslationsLocalizer["Successes"];
                    messageSummary = "Successes";
                    break;

                default:
                    break;
            }

            return messageSummary;
        }
    }

    private void CleanMessage() => Dispatcher.Dispatch(new CleanMessageAction());

    private void ShowNotification()
    {
        var message = new NotificationMessage { Severity = MessageNotificationSeverity, Summary = MessageSummary, Detail = MessageState.Value.Message, Duration = 5000 };

        NotificationService.Notify(message);
    }
}
