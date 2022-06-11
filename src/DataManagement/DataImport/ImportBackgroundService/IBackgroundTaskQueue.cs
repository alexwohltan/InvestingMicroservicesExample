using System;
namespace DataImport.ImportBackgroundService
{
	public interface IBackgroundTaskQueue
	{
		ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);

		ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
			CancellationToken cancellationToken);
	}
}

