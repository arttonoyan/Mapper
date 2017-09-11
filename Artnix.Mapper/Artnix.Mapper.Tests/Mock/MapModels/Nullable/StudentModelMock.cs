using System;

namespace Artnix.Mapper.Tests.Mock.MapModels.Nullable
{
    public class StudentModelMock
    {
        public int Id { get; set; }
        public int? City { get; set; }
        public string Appartment { get; set; }
        public string CityName { get; set; }
        public string Commune { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }

        public string Fullname { get; set; }
    }
}