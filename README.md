# Mapper
.Net Core 2.0

# Features
MapperFramework is a <a href="https://www.nuget.org/packages/Artnix.MapperFramework/">NuGet library</a> that you can add in to your project.

## How do I get started?

```
Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateMap<StudentModel, Dto.StudentModel()
                    .Property(wm => wm.CityName, m => m.CityModel == null ? null : m.CityModel.Name)
                    // You can use "IfNotNull" extensions.
                    //.Property(wm => wm.CityName, m => m.CityModel.IfNotNull(p => p.Name))
                    .Property(wm => wm.Fullname, m => $"{m.Family} {m.Name}");
            });
```
            
Yoc can use "Ignore" functions:
```
Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateMap<StudentModel, Dto.StudentModel>()
                    .Ignore(nameof(Mock.MapModels.Base.StudentModelMock.Fullname))
                    .Ignore(m => m.CityName)
            });
```
