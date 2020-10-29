using System;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public static class CookieExtensions
    {
        public async static Task SetCookie(this IJSRuntime jsRuntime, string name, string value, int exdays)
        {
            var d = DateTimeOffset.UtcNow;
            var expires = "expires=" + d.AddDays(exdays).ToUnixTimeMilliseconds();
            var cookie = name + "=" + value + ";" + expires + ";path=/"; 

            await jsRuntime.InvokeVoidAsync("eval", $"cookie='{cookie}'");
        }

        public async static ValueTask<string> GetCookie(this IJSRuntime jsRuntime, string name)
        {
            var decodedCookie = await jsRuntime.InvokeAsync<string>("eval", "cookie");

            return decodedCookie;
        }
    }
}