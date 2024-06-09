using Lerno.SignalR.Shared.MethodTypes;
using Microsoft.AspNetCore.SignalR;

namespace Lerno.SignalR.Shared.Hubs
{
    public class StreamDataHub : Hub
    {
        public async Task SendMessage(string methodName, params object[] args)
        {
            switch (methodName)
            {
                case StreamDataMethodNames.Started:
                    await Clients.All.SendAsync(methodName).ConfigureAwait(false);
                    break;
                case StreamDataMethodNames.Cancelled:
                    var cancellationMessage = "Some default cancellation message";
                    await Clients.All.SendAsync(methodName, cancellationMessage).ConfigureAwait(false);
                    break;
                case StreamDataMethodNames.Completed:
                    var completionMessage = "Some default completion message";
                    await Clients.All.SendAsync(methodName, completionMessage, new { Export = "Some data", Message = "Data exported." }, CancellationToken.None).ConfigureAwait(false);
                    break;
                case StreamDataMethodNames.Fallback:
                default:
                    var exceptionFromArgs = args.FirstOrDefault(arg => arg is Exception);
                    await Clients.All.SendAsync(methodName, exceptionFromArgs ?? new { }).ConfigureAwait(false);
                    break;
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception is not null)
            {
                await SendMessage(StreamDataMethodNames.Fallback, exception);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
