using Microsoft.AspNetCore.SignalR;
using Recruitment.Areas.Identity.Data;

namespace Recruitment.Services
{
	public interface INotificationHub
	{
		public Task SendMessage(Notification notification);
	}

	public class NotificationHub : Hub
	{
		public async Task AddToRoleGroup(string role)
		{
			// Add the current user to the group associated with the specified role
			await Groups
				.AddToGroupAsync(Context.ConnectionId,
					role);
		}

		public async Task RemoveFromRoleGroup(string role)
		{
			// Remove the current user from the group associated with the specified role
			await Groups
				.RemoveFromGroupAsync(Context.ConnectionId,
					role);
		}
	}
}
