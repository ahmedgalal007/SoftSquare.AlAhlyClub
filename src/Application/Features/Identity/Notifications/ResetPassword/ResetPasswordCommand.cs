﻿namespace SoftSquare.AlAhlyClub.Application.Features.Identity.Notifications.ResetPassword;

public record ResetPasswordNotification(string RequestUrl,string Email,string UserName) : INotification;

public class ResetPasswordNotificationHandler : INotificationHandler<ResetPasswordNotification>
{
    private readonly IStringLocalizer<ResetPasswordNotificationHandler> _localizer;
    private readonly ILogger<ResetPasswordNotificationHandler> _logger;
    private readonly IMailService _mailService;
    private readonly IApplicationSettings _settings;

    public ResetPasswordNotificationHandler(
        IStringLocalizer<ResetPasswordNotificationHandler> localizer,
        ILogger<ResetPasswordNotificationHandler> logger,
        IMailService mailService,
        IApplicationSettings settings)
    {

        _localizer = localizer;
        _logger = logger;
        _mailService = mailService;
        _settings = settings;
    }


    public async Task Handle(ResetPasswordNotification notification, CancellationToken cancellationToken)
    {
        var sendMailResult = await _mailService.SendAsync(
           notification.Email,
           _localizer["Verify your recovery email"],
           "_recoverypassword",
           new
           {
               notification.RequestUrl,
               _settings.AppName,
               _settings.Company,
               notification.UserName,
               notification.Email,
           });
        _logger.LogInformation("Password rest email sent to {Email}. sending result {Successful} {ErrorMessages}", notification.Email, sendMailResult.Successful, string.Join(' ', sendMailResult.ErrorMessages));

    }
}