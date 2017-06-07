using System;
using System.Collections.Generic;

namespace RefreshingCache.Test
{
    public class MockFetchValues : IFetchValues
    {
        private Dictionary<string, string> _dict;
        
        public MockFetchValues()
        {
            _dict = new Dictionary<string, string>();
            MockIsCalled = 0;
        }

        public int MockIsCalled { get; private set; }

        public string Fetch(string key)
        {
            ++MockIsCalled;
            return _dict[key];
        }

        public void Add(string key, string value)
        {
            _dict.Add(key, value);
        }

        public void LoadValues(Dictionary<string, string> dict)
        {
            _dict = dict;
        }
    }
}