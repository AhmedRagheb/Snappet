using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snappet.Interfaces;
using Snappet.Tests.Injection;
using System.Linq;
using System.Web;
using System.IO;


namespace Snappet.Web.Tests
{
    [TestClass]
    public class ProvidersTest
    {
        IStudentsProvider _studentProvider;
        IPathProvider _pathProvider;
        [TestInitialize]
        public void Init()
        {
            StructureMapInjectorTest.Setup();
            _studentProvider = StructureMapInjectorTest._container.GetInstance<IStudentsProvider>();
            _pathProvider = StructureMapInjectorTest._container.GetInstance<IPathProvider>();
        }

        [TestMethod]
        public void TestReadStudentDataFromFile()
        {
            var data = _studentProvider.GetStudentDate(_pathProvider.MapPath());
            Assert.IsTrue(data != null);
            Assert.IsTrue(data.Count > 0);
            Assert.AreEqual(data.Count, 37812);
        }

        [TestMethod]
        public void TestValidateData()
        {
            var data = _studentProvider.GetStudentDate(_pathProvider.MapPath());

            Assert.IsTrue(data != null);
            Assert.IsTrue(data.All(x => x.UserId != 0));
            Assert.IsTrue(data.All(x => !string.IsNullOrEmpty(x.LearningObjective)));
        }


        [TestMethod]
        public void TestValidateSubmitDate()
        {
            var data = _studentProvider.GetStudentDate(_pathProvider.MapPath());
            var first = data.First();

            Assert.IsTrue(data != null);
            Assert.AreEqual(first.SubmitDateTime.ToShortDateString(), first.SubmitDateDisplay);
        }
    }
}
