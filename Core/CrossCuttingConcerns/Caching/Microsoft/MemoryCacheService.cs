using System.Reflection;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheService : ICacheService
{
    private IMemoryCache _cache;

    public MemoryCacheService()
    {
        _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
    }

    public T Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public object Get(string key)
    {
        return _cache.Get(key);
    }

    public void Add(string key, object data, int duration)
    {
        _cache.Set(key, data, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return _cache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }


    public void RemoveByPattern(string pattern)
    {
        var cacheCollectionEntriesDefinition =
            typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
        dynamic? cacheCollectionEntries = cacheCollectionEntriesDefinition.GetValue(_cache);
        var cacheValuesCollection = new List<ICacheEntry>();
        foreach (var cacheItem in cacheCollectionEntries)
        {
            var cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
            cacheValuesCollection.Add(cacheItemValue);
        }

        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = cacheValuesCollection.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
        foreach (var o in keysToRemove)
        {
            _cache.Remove(o);
        }
    }
}