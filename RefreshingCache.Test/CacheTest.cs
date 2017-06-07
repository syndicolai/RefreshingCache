using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefreshingCache.Test
{
    [TestFixture]
    public class CacheTest
    {
        private MockFetchValues _mock;
        private CacheService _service;
        private Dictionary<string, CacheItem> _existingDictionary;

        [SetUp]
        public void Setup()
        {
            _mock = new MockFetchValues();
            _service = new CacheService(_mock);
        }

        [Test]
        public void Get_NoData_FetchesFromExternalService()
        {
            var expectedValue = "From external service";
            _mock.Add("key", expectedValue);

            Assert.AreEqual(expectedValue, _service.Get("key").Value);
        }

        [Test]
        public void Get_DataAlreadyFetched_FetchesLocally()
        {
            _mock.Add("key1", "hey ho");

            var value = _service.Get("key1");
            value = _service.Get("key1");

            Assert.AreEqual(1, _mock.MockIsCalled);
        }

        [Test]
        public void Get_Updated_IsSetOnFetch()
        {
            _mock.Add("key1", "vvava");
            var value = _service.Get("key1");
            Assert.AreEqual(DateTime.Now.Hour, value.Updated.Value.Hour);
        }

        [Test]
        public void Get_MaxSize_RemovesLeastRecentlyUsed()
        {
            _mock.Add("key1", "valueofleastrecentlyused");
            _mock.Add("key2", "asdf1321");
            _mock.Add("key3", "asdf");
            var newValue = _service.Get("key4");

            var allValues = _service.GetAllValues();

            Assert.That(!allValues.Any(x => x.Value == "valueofleastrecentlyused"));
        }
        
    }
}
