using System;

namespace Artnix.MapperFramework.Tests.Mock.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public int? City { get; set; }
        public string Email { get; set; }
        public DateTime? Createdate { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }

        public CityModel CityModel { get; set; }
    }
}