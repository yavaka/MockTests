namespace MTMA.AdminPanel.Services.Common.SessionStorage
{
    /// <summary>
    /// Represents an accessor for interacting with session storage in a Blazor WebAssembly application.
    /// </summary>
    public interface ISessionStorageAccessor
    {
        /// <summary>
        /// Gets the value associated with the specified key from session storage.
        /// </summary>
        Task<T> GetValueAsync<T>(string key);
        
        /// <summary>
        /// Sets the value for the specified key in session storage.
        /// </summary>
        Task SetValueAsync<T>(string key, T value);
        
        /// <summary>
        /// Clears all key-value pairs from session storage.
        /// </summary>
        Task Clear();
        
        /// <summary>
        /// Clears all key-value pairs from session storage.
        /// </summary>
        Task RemoveAsync(string key);
    }
}
