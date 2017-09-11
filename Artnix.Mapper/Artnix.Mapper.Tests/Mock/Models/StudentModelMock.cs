using System;

namespace Artnix.Mapper.Tests.Mock.Models
{
    public class StudentModelMock
    {
        public int Id { get; set; }
        public int? City { get; set; }
        public string Email { get; set; }
        public DateTime? Createdate { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }

        public CityModelMock CityModel { get; set; }
    }
}