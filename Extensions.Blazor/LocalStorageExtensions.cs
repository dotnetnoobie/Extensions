using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public static class LocalStorageExtensions
    {
        private static Storage storage = new Storage(StorageType.localStorage);

        public async static Task LocalStorageClear(this IJSRuntime jsRuntime)
            => await storage.Clear(jsRuntime);

        public async static ValueTask<int> LocalStorageLength(this IJSRuntime jsRuntime)
            => await storage.Length(jsRuntime);

        public async static ValueTask<string> LocalStorageKey(this IJSRuntime jsRuntime, int index)
            => await storage.Key(jsRuntime, index);

        public async static ValueTask<bool> LocalStorageContains<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Contains<T>(jsRuntime, expression);

        public async static ValueTask<bool> LocalStorageContains<T>(this IJSRuntime jsRuntime, string key)
            => await storage.Contains<bool>(jsRuntime, key);

        public async static Task LocalStorageRemove<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Remove<T>(jsRuntime, expression);

        public async static Task LocalStorageRemove<T>(this IJSRuntime jsRuntime, string key)
            => await storage.Remove<string>(jsRuntime, key);

        public async static Task LocalStorageSet<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Set<T>(jsRuntime, expression);

        public async static Task LocalStorageSet<T>(this IJSRuntime jsRuntime, string key, object value)
            => await storage.Set<T>(jsRuntime, key, value);

        public async static ValueTask<T> LocalStorageGet<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Get<T>(jsRuntime, expression);

        public async static ValueTask<T> LocalStorageGet<T>(this IJSRuntime jsRuntime, string key)
            => await storage.Get<T>(jsRuntime, key);
    }
}