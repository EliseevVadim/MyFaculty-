using System.Collections.Concurrent;

namespace MyFaculty.WebApi.Hubs.Management
{
    public class OnlineUsers
    {
        private readonly ConcurrentDictionary<int, string> _onlineUsers = new();

        public void AddUser(int realId, string connectionId)
        {
            _onlineUsers.TryAdd(realId, connectionId);
        }

        public void Remove(int realId)
        {
            _onlineUsers.TryRemove(realId, out _);
        }

        public string GetConnectionIdByRealId(int realId)
        {
            string connectionId = string.Empty;
            _onlineUsers.TryGetValue(realId, out connectionId);
            return connectionId;
        }
    }
}
