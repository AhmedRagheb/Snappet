using Snappet.Business.Managers;
using Snappet.Providers;
using Snappet.Interfaces;
using StructureMap;

namespace Snappet.Business.Injection
{
    public static class StructureMapInjector
    {
        public static IContainer _container;
        public static IContainer Setup()
        {
            _container = new Container(_ =>
            {
                _.For<IPathProvider>().Use<ServerPathProvider>()
                                .Ctor<string>("path").Is("~/Data/work.json");
                _.For<IStudentsProvider>().Use<StudentsProvider>();

                _.ForSingletonOf<StudentsManager>();
            });

            return _container;
        }
    }
}
