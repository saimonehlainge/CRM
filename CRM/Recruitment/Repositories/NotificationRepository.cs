using Microsoft.AspNetCore.SignalR;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Services;

namespace Recruitment.Repositories
{
	public class NotificationRepository : INotificationHub
	{
		private IHubContext<NotificationHub> NotificationHub { get; }
		private readonly IHttpContextAccessor _httpContextAccessor;

		public NotificationRepository(IHubContext<NotificationHub> notificationHub,
			IHttpContextAccessor httpContextAccessor)
		{
			NotificationHub = notificationHub;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task SendMessage(Notification notification)
		{
			var (_, roles, _, department) = _httpContextAccessor.HttpContext!.User.GetUser();

			await NotificationHub.Clients.Groups(roles!)
				.SendAsync("UpdateNotification",
				notification.Message,
				notification.CreatedDate?.TimeOfDay.ToString("hh\\:mm"));
		}
	}
}
