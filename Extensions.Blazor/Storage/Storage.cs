using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public class Storage
    {
        internal StorageType storageType;
        //private readonly IJSRuntime jsRuntime;
        private readonly JsonSerializerOptions jsOptions;

        public Storage(StorageType storageType)
        {
            this.storageType = storageType;
            this.jsOptions = new JsonSerializerOptions();
            this.jsOptions.Converters.Add(new TimespanJsonConverter());
        }

        public async Task Clear(IJSRuntime jsRuntime)
            => await jsRuntime.InvokeVoidAsync($"{storageType}.clear");

        public async ValueTask<int> Length(IJSRuntime jsRuntime)
            => await jsRuntime.InvokeAsync<int>("eval", $"{storageType}.length");

        public async ValueTask<string> Key(IJSRuntime jsRuntime, int index)
            => await jsRuntime.InvokeAsync<string>($"{storageType}.key", index);

        public async ValueTask<bool> Contains<T>(IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await jsRuntime.InvokeAsync<bool>($"{storageType}.hasOwnProperty", expression.GetKey());

        public async ValueTask<bool> Contains<T>(IJSRuntime jsRuntime, string key)
            => await jsRuntime.InvokeAsync<bool>($"{storageType}.hasOwnProperty", key);

        public async ValueTask Remove<T>(IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await jsRuntime.InvokeAsync<string>($"{storageType}.removeItem", expression.GetKey());

        public async ValueTask Remove<T>(IJSRuntime jsRuntime, string key)
            => await jsRuntime.InvokeAsync<string>($"{storageType}.removeItem", key);

        public async Task Set<T>(IJSRuntime jsRuntime, Expression<Func<T>> expression)
            => await jsRuntime.InvokeVoidAsync($"{storageType}.setItem", expression.GetKey(), JsonSerializer.Serialize(expression.GetValue(), jsOptions));

        public async Task Set<T>(IJSRuntime jsRuntime, string key, object value)
            => await jsRuntime.InvokeVoidAsync($"{storageType}.setItem", key, JsonSerializer.Serialize(value, jsOptions));

        public async ValueTask<T> Get<T>(IJSRuntime jsRuntime, Expression<Func<T>> expression)
        {
            var json = await jsRuntime.InvokeAsync<string>($"{storageType}.getItem", expression.GetKey());

            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<T>(json, jsOptions);
            }

            return default;
        }
        public async ValueTask<T> Get<T>(IJSRuntime jsRuntime, string key)
        {
            var json = await jsRuntime.InvokeAsync<string>($"{storageType}.getItem", key);

            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<T>(json, jsOptions);
            }

            return default;
        }
    }
}
