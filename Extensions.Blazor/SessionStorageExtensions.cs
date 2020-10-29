using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public static class SessionStorageExtensions
    {
        private static Storage storage = new Storage(StorageType.sessionStorage);

        public async static Task SessionStorageClear(this IJSRuntime jsRuntime)
            => await storage.Clear(jsRuntime);

        public async static ValueTask<int> SessionStorageLength(this IJSRuntime jsRuntime)
            => await storage.Length(jsRuntime);

        public async static ValueTask<string> SessionStorageKey(this IJSRuntime jsRuntime, int index)
            => await storage.Key(jsRuntime, index);

        public async static ValueTask<bool> SessionStorageContains<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Contains<T>(jsRuntime, expression);

        public async static ValueTask<bool> SessionStorageContains<T>(this IJSRuntime jsRuntime, string key)
            => await storage.Contains<bool>(jsRuntime, key);

        public async static Task SessionStorageRemove<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Remove<T>(jsRuntime, expression);

        public async static Task SessionStorageRemove<T>(this IJSRuntime jsRuntime, string key)
            => await storage.Remove<string>(jsRuntime, key);

        public async static Task SessionStorageSet<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Set<T>(jsRuntime, expression);

        public async static Task SessionStorageSet<T>(this IJSRuntime jsRuntime, string key, object value)
            => await storage.Set<T>(jsRuntime, key, value);

        public async static ValueTask<T> SessionStorageGet<T>(this IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await storage.Get<T>(jsRuntime, expression);

        public async static ValueTask<T> SessionStorageGet<T>(this IJSRuntime jsRuntime, string key)
            => await storage.Get<T>(jsRuntime, key);
    }
}