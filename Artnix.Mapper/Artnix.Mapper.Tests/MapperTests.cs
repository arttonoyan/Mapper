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
                    .Property(wm => wm.CityName, m => m.CityModel.Name)
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");

                cfg.CreateMap<Mock.Models.StudentModelMock, Mock.MapModels.Nullable.StudentModelMock>()
                    .Property(wm => wm.CityName, m => m.CityModel.Name)
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");
            });
        }

        [TestMethod]
        public void TestMethodToViewModel()
        {
            
        }
    }
}
