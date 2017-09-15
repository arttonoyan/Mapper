using System;

namespace Artnix.MapperFramework.Tests.Mock.Models
{
    public class CityModelMock
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DestroyDate { get; set; }
        public string Name { get; set; }
        public int? Region { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
