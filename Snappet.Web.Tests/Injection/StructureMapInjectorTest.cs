using Snappet.Business.Managers;
using Snappet.Repository;
using Snappet.Interfaces;
using StructureMap;
using Snappet.Web.Tests.Providers;
using Snappet.Business.Cache;

namespace Snappet.Tests.Injection
{
    public static class StructureMapInjectorTest
    {
        public static Container _container;
        public static void Setup()
        {
            _container = new Container(_ =>
            {
                _.For<IPathRepository>().Use<TestPathProvider>()
                         .Ctor<string>("path").Is("\\Data\\work.json");
                _.For<IStudentsRepository>().Use<StudentsRepository>();
                _.For<ICacheHelper>().Use<CacheHelper>();
                _.For<IStudentsManager>().Use<StudentsManager>();
            });
        }
    }
}
