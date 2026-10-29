using System.Collections.Concurrent;
using DevExpress.AIIntegration.Chat;

namespace DevExpress.AI.Samples.Blazor.Services {
    // Stores the IChatResponseProvider created for each DxAIChat instance under a unique service key.
    // Program.cs resolves keyed IChatResponseProvider services from this store, so a DxAIChat component
    // can bind to its provider with the ChatResponseProviderServiceKey property.
    public class AIChatSessionStore : IAsyncDisposable {
        readonly ConcurrentDictionary<string, (IChatResponseProvider Provider, Func<Task> Cleanup)> sessions = new();

        public string Register(IChatResponseProvider provider, Func<Task> cleanup) {
            string key = Guid.NewGuid().ToString();
            sessions.TryAdd(key, (provider, cleanup));
            return key;
        }

        public IChatResponseProvider GetProvider(string key) {
            if(key != null && sessions.TryGetValue(key, out var session))
                return session.Provider;
            throw new InvalidOperationException($"Chat session '{key}' was not found.");
        }

        public async Task CloseAsync(string key) {
            if(key != null && sessions.TryRemove(key, out var session))
                await session.Cleanup();
        }

        public async ValueTask DisposeAsync() {
            foreach(var key in sessions.Keys)
                await CloseAsync(key);
        }
    }
}
