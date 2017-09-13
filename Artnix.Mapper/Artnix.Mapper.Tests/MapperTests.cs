using System;
using System.Collections.Generic;
using System.Linq;
using Artnix.Mapper.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Artnix.Mapper.Tests
{
    [TestClass]
    public class MapperTests
    {
        public MapperTests()
        {
            DataBase.Init();
            Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateMap<Mock.Models.StudentModelMock, Mock.MapModels.Base.StudentModelMock>()
                    .Ignore(nameof(Mock.MapModels.Base.StudentModelMock.Fullname))
                    .Ignore(m => m.CityName)

                     // You can use "IfNotNull" extensions.
                    .Property(wm => wm.CityName, m => m.CityModel.IfNotNull(p => p.Name))
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");

                cfg.CreateMap<Mock.Models.StudentModelMock, Mock.MapModels.Nullable.StudentModelMock>()
                    .Property(wm => wm.CityName, m => m.CityModel == null ? null : m.CityModel.Name)
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");
            });
        }

        [TestMethod]
        public void TestModelToViewModel()
        {
            var items = new List<Mock.MapModels.Base.StudentModelMock>();
            var Converter = Mapper.Converter<Mock.Models.StudentModelMock, Mock.MapModels.Base.StudentModelMock>();
            foreach (var stItem in DataBase.Students)
            {
                try
                {
                    Mock.MapModels.Base.StudentModelMock mapModel = Converter(stItem);
                    items.Add(mapModel);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }
            }

            Assert.IsTrue(items.Count == DataBase.Students.Count);
        }

        [TestMethod]
        public void TestEnumerableModelToEnumerableViewModel()
        {
            try
            {
                var models = DataBase.Students.MapConvert<Mock.Models.StudentModelMock, Mock.MapModels.Base.StudentModelMock>().ToList();
                Assert.IsTrue(models.Count == DataBase.Students.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
