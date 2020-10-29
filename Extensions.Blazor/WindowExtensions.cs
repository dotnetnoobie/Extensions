using System;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public static class WindowExtensions
    {
        public async static Task WindowAlert(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("alert", message);
        }

        public async static ValueTask<bool> WindowConfirm(this IJSRuntime jsRuntime, string message)
        {
           return await jsRuntime.InvokeAsync<bool>("confirm", message);
        }

        public async static Task WindowOpen(this IJSRuntime jsRuntime, string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                await jsRuntime.InvokeVoidAsync("open", url);
            }
        }

        public async static Task WindowClose(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("close");
        }

        public async static ValueTask<int> WindowInnerHeight(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<int>("eval", "innerHeight");
        }

        public async static ValueTask<int> WindowInnerWidth(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<int>("eval", "innerWidth");
        }
    }
}