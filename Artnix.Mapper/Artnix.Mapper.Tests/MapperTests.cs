using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Artnix.Mapper.Tests
{
    [TestClass]
    public class MapperTests
    {
        public MapperTests()
        {
            DataBase.Init();

            Mapper.MapConfiguration<Mock.Models.StudentModelMock, Mock.MapModels.Base.StudentModelMock>(cfg =>
            {
                cfg.CreateMap()
                    .Property(wm => wm.CityName, m => m.CityModel.Name)
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");
            });

            Mapper.MapConfiguration<Mock.Models.StudentModelMock, Mock.MapModels.Nullable.StudentModelMock>(cfg =>
            {
                cfg.CreateMap()
                    .Property(wm => wm.CityName, m => m.CityModel.Name)
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");
            });
        }

        [TestMethod]
        public void TestMethodToViewModel()
        {
            var st = DataBase.Students[0];
            //Mapper.Convert<Mock.Models.StudentModelMock, Mock.MapModels.Base.StudentModelMock>()
        }
    }
}
