using System;
using System.Threading.Tasks;

namespace FAADatabase
{
    /// <summary>
    /// Extensions class copied from https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases.
    /// Provides a way to safely call async methods in constructors and catch exceptions
    /// </summary>
    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
