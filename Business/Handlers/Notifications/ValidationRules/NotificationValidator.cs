
using Business.Handlers.Notifications.Commands;
using FluentValidation;

namespace Business.Handlers.Notifications.ValidationRules
{

    public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.ErrorLogId).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.IsRead).NotEmpty();
            RuleFor(x => x.NotificationType).NotEmpty();

        }
    }
    public class UpdateNotificationValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.ErrorLogId).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.IsRead).NotEmpty();
            RuleFor(x => x.NotificationType).NotEmpty();

        }
    }
}