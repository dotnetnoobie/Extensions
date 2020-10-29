using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Threading.Tasks
{
    public static class TaskExtensions
    {
        public static async Task<T> WaitAny<T>(Func<T, bool> predicate, ICollection<Task<T>> tasks)
        {
            if (!(tasks is List<Task<T>>))
            {
                tasks = tasks.ToList();
            }

            while (tasks.Count() > 0)
            {
                var finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                if (predicate(await finishedTask))
                {
                    return await finishedTask;
                }
            }
            return default;
        }

        public static Task<T> WaitAny<T>(Func<T, bool> predicate, params Task<T>[] tasks) => WaitAny(predicate, tasks as ICollection<Task<T>>);

        public static async Task<Task<T>> WhenAny<T>(Func<T, bool> predicate, ICollection<Task<T>> tasks)
        {
            if (!(tasks is List<Task<T>>))
            {
                tasks = tasks.ToList();
            }

            while (tasks.Count() > 0)
            {
                var finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                if (predicate(await finishedTask))
                {
                    return finishedTask;
                }
            }
            return null;
        }

        public static Task<Task<T>> WhenAny<T>(Func<T, bool> predicate, params Task<T>[] tasks) => WhenAny(predicate, tasks as ICollection<Task<T>>);
    }
}