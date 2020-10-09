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

- [ ] creating classes

- [ ] classes, getter, setter

- [ ] assertions for

  - [ ] get only fields
  - [ ] set only fields
  - [ ] get and set fields
  - [ ] 

- [ ] namespace

- [ ] assemblies as string param

- [ ] extract assemblies to API

- [ ] moq

- [ ] expectations and reporting

- [ ] exceptions (caught and uncaught)

  

### Console output of a snippet



### Console output of a program



### Apply assertions on a snippet

- should be appended to a snippet in client code

- Assertions available

  - [ ] ~~`_ShouldWriteToConsole(string[] expected, string testName)` : ignores order of output~~ : should be implemented on client side with return value of a snippet
  - [ ] `_ShouldDefineVariable(string name, string type, string testName)`
  - [ ] `_ShouldHaveValue(string variableName, string expectedValue, string testName)`
- [ ] `xunit.Assert.*`
  



### Apply assertions on a class

- Assertions

  ```c#
  _ShouldDefineClass(string className, string testName);
  _ShouldExtendClass(string className, System.Type superclass string testName);
  
    
  ```

- class definitions should not have `using` statements

- Demo

  ```c#
  string[] classSnippet = //..
  
  string [] testCases = //..
    
  Executor.ExecuteClass(classSnippet, testCases);
  
  ```

  

### Releasing a version

- Update the version in Chakra/Chakra.csproj
- Create a release branch with name release-x.y.z
- push the branch, nuget will be auto published unless tests/build are failling in the version

