using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snappet.Interfaces;
using Snappet.Tests.Injection;
using Snappet.Business.Managers;
using System.Web;
using System.IO;

namespace Snappet.Web.Tests.Managers
{
    [TestClass]
    public class StudentsManagerTest
    {
        StudentsManager _studentsManager;
        [TestInitialize]
        public void Init()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://test.nl/", ""),
                new HttpResponse(new StringWriter())
            );
            StructureMapInjectorTest.Setup();
            _studentsManager = StructureMapInjectorTest._container.GetInstance<StudentsManager>();
        }

        [TestMethod]
        public void TestReadStudentDataFromProvider()
        {
            var data = _studentsManager.GetStudentsData();

            Assert.IsTrue(data.Obj != null);
            Assert.IsTrue(data.Obj.Count > 0);
            Assert.AreEqual(data.Obj.Count, 37812);
        }

        [TestMethod]
        public void TestIsDataSavedInCache()
        {
            if(HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Remove("AllData");
            }

            var data = _studentsManager.GetStudentsData();
            Assert.IsFalse(data.IsLoadedFromCache);

            var cachedData = _studentsManager.GetStudentsData();
            Assert.IsTrue(cachedData.IsLoadedFromCache);
        }

        [TestMethod]
        public void TestValidateData()
        {
            var data = _studentsManager.GetStudentsData();

            Assert.IsTrue(data.Obj != null);
            Assert.IsTrue(data.Obj.All(x => x.UserId != 0));
            Assert.IsTrue(data.Obj.All(x => !string.IsNullOrEmpty(x.LearningObjective)));
        }
    }
}
