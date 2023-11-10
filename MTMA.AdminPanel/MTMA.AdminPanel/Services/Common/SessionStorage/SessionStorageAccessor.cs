using Microsoft.JSInterop;

namespace MTMA.AdminPanel.Services.Common.SessionStorage
{
    public class SessionStorageAccessor : ISessionStorageAccessor, IAsyncDisposable
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        public SessionStorageAccessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            await WaitForReference();
            var result = await _accessorJsRef.Value.InvokeAsync<T>("get", key);

            return result;
        }

        public async Task SetValueAsync<T>(string key, T value)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, value);
        }

        public async Task Clear()
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("clear");
        }

        
        public async Task RemoveAsync(string key)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("remove", key);
        }

        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }

        #region Helpers

        /// <summary>
        /// Ensures that the JavaScript object reference is available before performing operations.
        /// </summary>
        /// <returns></returns>
        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/SessionStorageAccessor.js"));
            }
        }

        #endregion
    }
}
