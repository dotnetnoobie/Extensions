using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public static class HistoryExtensions
    {
        public async static Task HistoryForward(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("eval", "history.forward()");
        }

        public async static Task HistoryBack(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("eval", "history.back()");
        }
    }
}