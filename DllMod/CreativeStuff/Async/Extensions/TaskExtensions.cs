using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using UnityEngine;

public static class TaskExtensions
{
    public static IEnumerator AsIEnumerator(this Task task)
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            ExceptionDispatchInfo.Capture(task.Exception).Throw();
        }
    }

    public static IEnumerator<T> AsIEnumerator<T>(this Task<T> task)
        where T : class
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            ExceptionDispatchInfo.Capture(task.Exception).Throw();
        }

        yield return task.Result;
    }

    public static async Task WaitForAll(this IEnumerable<Task> tasks)
    {
        try
        {
            await Task.Run(() => Task.WaitAll(tasks.ToArray()));
        }
        catch (AggregateException aggregrateException)
        {
            Debug.LogError("One or more Exceptions where thrown while executing tasks");
            foreach (var exception in aggregrateException.InnerExceptions)
            {
                Debug.LogException(exception);
            }
        }
    }

    public static async Task WaitForOne(this IEnumerable<Task> tasks)
    {
        try
        {
            await Task.Run(() => Task.WaitAny(tasks.ToArray()));
        }
        catch (AggregateException aggregrateException)
        {
            Debug.LogError("One or more Exceptions where thrown while executing tasks");
            foreach (var exception in aggregrateException.InnerExceptions)
            {
                Debug.LogException(exception);
            }
        }
    }
}