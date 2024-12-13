using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.HttpClients
{
    public class MockReq
    {
        public string Name { get; set; }
        public string Job { get; set; }
    }

    public class MockPostResponse
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public string Id { get; set; }
        public DateTime createdAt { get; set; }

    }

    public class MockData
    {
        public string Id { get; set; }
        public string email { get; set; }
    }

    public class MockSupport
    {
        public string Url { get; set; }
        public string Text { get; set; }
    }


    public class MockAPiResponse
    {
        public MockData Data { get; set; }
        public MockSupport support { get; set; }
    }
}
