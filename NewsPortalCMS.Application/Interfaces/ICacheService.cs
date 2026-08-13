namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface ICacheService
    {
        void Remove(string key);

        void RemoveByPrefix(string prefix);

        void TrackKey(string key);
    }
}