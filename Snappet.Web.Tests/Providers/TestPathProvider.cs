using Snappet.Interfaces;
using System;

namespace Snappet.Web.Tests.Providers
{
    public class TestPathProvider : IPathProvider
    {
        string _path;
        public TestPathProvider(string path)
        {
            _path = path;
        }

        public string MapPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + _path;
        }
    }
}
