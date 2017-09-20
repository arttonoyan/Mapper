using System;
using System.Collections.Generic;
using System.Linq;
using Artnix.MapperFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Artnix.MapperFramework.Tests
{
    [TestClass]
    public class MapperTests
    {
        public MapperTests()
        {
            DataBase.Init();
            Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateMap<Mock.Models.StudentModel, Mock.DTO.StudentModel>()
                    .Ignore(nameof(Mock.DTO.StudentModel.Fullname))
                    .Ignore(m => m.CityName)

                     // You can use "IfNotNull" extensions.
                    .Property(wm => wm.CityName, m => m.CityModel.IfNotNull(p => p.Name))
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");

                cfg.CreateMap<Mock.Models.StudentModel, Mock.DTO.StudentModelNullable>()
                    .Property(wm => wm.CityName, m => m.CityModel == null ? null : m.CityModel.Name)
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");
            });
        }

        [TestMethod]
        public void TestModelToViewModel()
        {
            var items = new List<Mock.DTO.StudentModel>();
            var Converter = Mapper.Converter<Mock.Models.StudentModel, Mock.DTO.StudentModel>();
            foreach (var stItem in DataBase.Students)
            {
                try
                {
                    Mock.DTO.StudentModel mapModel = Converter(stItem);
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
                var models = DataBase.Students.MapConvert<Mock.Models.StudentModel, Mock.DTO.StudentModel>().ToList();
                Assert.IsTrue(models.Count == DataBase.Students.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
