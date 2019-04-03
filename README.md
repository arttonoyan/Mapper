# Artnix.MapperFramework
[![Build status](https://dev.azure.com/art-nix/MapperFramework/_apis/build/status/Build%2C%20Pack%20and%20Push-CI)](https://dev.azure.com/art-nix/MapperFramework/_build/latest?definitionId=2)
[![Nuget](https://img.shields.io/nuget/v/Artnix.MapperFramework.svg)](https://www.nuget.org/packages/Artnix.MapperFramework/)
[![Nuget](https://img.shields.io/nuget/dt/Artnix.MapperFramework.svg)](https://www.nuget.org/packages/Artnix.MapperFramework/)

## How do I get started?
First, configure MapperFramework to know what types you want to map, in the startup of your application:
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
                    .Ignore(nameof(Mock.MapModels.Base.StudentModelMock.CityName))
                    // Or You can use this style.
                    .Ignore(m => m.CityName)
            });
```
Then in your application code, execute the mappings:
```
 var dtoModel = Mapper.Convert<Dto.StudentModel>(student);
 ```
## Datareader Configuration

```
Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateDataReaderMap<Student>()
                    .Ignore(p => p.Birthday)
                    //// Or
                    //.Ignore(nameof(Student.Birthday))
                    .Property(p => p.FirstName, "Name")
                    .Property(p => p.LastName, "Family");
            });
```
If you have standart <a href="http://www.sqlstyle.guide/#naming-conventions">SQL Style Guide</a> (first name becomes first_name)
you can use "UseStandardCodeStyleForMembers()" function.
```
Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateDataReaderMap<Student>()
                    .UseStandardCodeStyleForMembers();
            });
```

Or you can do it manually

```
Mapper.MapConfiguration(cfg =>
            {
                cfg.CreateDataReaderMap<Student>()
                    .Property(p => p.FirstName, "first_name")
                    .Property(p => p.LastName, "last_name");
            });
```
