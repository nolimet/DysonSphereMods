using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public static class AwaitExtensions
{
    public static TaskAwaiter<int> GetAwaiter(this Process process)
    {
        var tcs = new TaskCompletionSource<int>();
        process.EnableRaisingEvents = true;

        process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);

        if (process.HasExited)
        {
            tcs.TrySetResult(process.ExitCode);
        }

        return tcs.Task.GetAwaiter();
    }

    /// <summary>
    /// Executes a task as a async void. Try and avoid calling this to many times as this will cause the async code to have many entrances which should be limited
    /// </summary>
    /// <param name="task">Task that will be executed</param>
    public static async void ExcecuteTask(this Task task)
    {
        await task;
    }
}