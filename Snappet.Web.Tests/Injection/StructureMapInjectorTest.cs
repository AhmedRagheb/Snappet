using Snappet.Business.Managers;
using Snappet.Providers;
using Snappet.Interfaces;
using StructureMap;
using Snappet.Web.Tests.Providers;

namespace Snappet.Tests.Injection
{
    public static class StructureMapInjectorTest
    {
        public static Container _container;
        public static void Setup()
        {
            _container = new Container(_ =>
            {
                _.For<IPathProvider>().Use<TestPathProvider>()
                         .Ctor<string>("path").Is("\\Data\\work.json");
                _.For<IStudentsProvider>().Use<StudentsProvider>();
                _.ForSingletonOf<StudentsManager>();
            });
        }
    }
}
