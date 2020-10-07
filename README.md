[![Build Status](https://dev.azure.com/dotnet-school/Chakra.NET/_apis/build/status/dotnet-school.chakra.net?branchName=release-0.2.0)](https://dev.azure.com/dotnet-school/Chakra.NET/_build/latest?definitionId=3&branchName=release-0.2.0)

[![NuGet Badge](https://buildstats.info/nuget/Chakra.NET)](https://www.nuget.org/packages/Chakra.NET/)

# Chakra.NET

Compile and run .NET snippets dynamically

https://github.com/laurentkempe/DynamicRun/blob/master/DynamicRun/Program.cs

### Todo 

- [x] Use text writer to capture console

- [x] async

- [x] lists, enumerable, arrays and dictionary

- [x] tasks

- [x] linq

- [x] reading a file

- [x] files, path

- [x] regex

- [x] ~~line numbers in runtime errors~~ (not feasible)

- [x] compile errors and line numbers

- [x] xunit

- [ ] expectations and reporting

  - [ ] testcases by attributes
  - [ ] 

- [ ] creating classes

- [ ] classes, getter, setter

- [ ] namespace

- [ ] assemblies as string param

- [ ] extract assemblies to API

- [ ] moq

- [ ] exceptions (caught and uncaught)

  



### Console output of a snippet



### Console output of a program



### Apply assertions on a snippet

- Assertions available

  - [ ] `_ShouldDefineVariable(string name, string type, string failId, string failMessage)`
  - [ ] `_ShouldHaveValue(string name, string failId, string failMessage)`
  - [ ] `xunit.Assert.*`

  ```
  // Assert
  ```

  

### 

### Releasing a version

- Update the version in Chakra/Chakra.csproj
- Create a release branch with name release-x.y.z
- push the branch, nuget will be auto published unless tests/build are failling in the version

