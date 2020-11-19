using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Zork.Common
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            return (dictionary != null && key != null && dictionary.TryGetValue(key, out TValue value)) ? value : defaultValue;
        }
    }
}
