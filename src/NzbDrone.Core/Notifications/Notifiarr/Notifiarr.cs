using System.Collections.Generic;
using FluentValidation.Results;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.MediaFiles;
using NzbDrone.Core.Notifications.Webhook;
using NzbDrone.Core.Tv;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.Notifications.Notifiarr
{
    public class Notifiarr : WebhookBase<NotifiarrSettings>
    {
        private readonly INotifiarrProxy _proxy;

        public Notifiarr(INotifiarrProxy proxy, IConfigFileProvider configFileProvider, IConfigService configService)
            : base(configFileProvider, configService)
        {
            _proxy = proxy;
        }

        public override string Link => "https://notifiarr.com";
        public override string Name => "Notifiarr";

        public override void OnGrab(GrabMessage message)
        {
            _proxy.SendNotification(BuildOnGrabPayload(message), Settings);
        }

        public override void OnDownload(DownloadMessage message)
        {
            _proxy.SendNotification(BuildOnDownloadPayload(message), Settings);
        }

        public override void OnRename(Series series, List<RenamedEpisodeFile> renamedFiles)
        {
            _proxy.SendNotification(BuildOnRenamePayload(series, renamedFiles), Settings);
        }

        public override void OnEpisodeFileDelete(EpisodeDeleteMessage deleteMessage)
        {
            _proxy.SendNotification(BuildOnEpisodeFileDelete(deleteMessage), Settings);
        }

        public override void OnSeriesDelete(SeriesDeleteMessage deleteMessage)
        {
            _proxy.SendNotification(BuildOnSeriesDelete(deleteMessage), Settings);
        }

        public override void OnHealthIssue(HealthCheck.HealthCheck healthCheck)
        {
            _proxy.SendNotification(BuildHealthPayload(healthCheck), Settings);
        }

        public override void OnApplicationUpdate(ApplicationUpdateMessage updateMessage)
        {
            _proxy.SendNotification(BuildApplicationUpdatePayload(updateMessage), Settings);
        }

        public override ValidationResult Test()
        {
            var failures = new List<ValidationFailure>();

            failures.AddIfNotNull(SendWebhookTest());

            return new ValidationResult(failures);
        }

        private ValidationFailure SendWebhookTest()
        {
            try
            {
                _proxy.SendNotification(BuildTestPayload(), Settings);
            }
            catch (NotifiarrException ex)
            {
                return new NzbDroneValidationFailure("APIKey", ex.Message);
            }

            return null;
        }
    }
}
