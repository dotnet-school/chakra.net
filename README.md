# Chakra.NET

Experimental NuGet to compile and run .NET packages in browsers.



Creating a Nuget Package

- signup on nuget.org, create an API key

- Create a new solution

  ```bash
  dotnet new sln -o Chakra.NET
  cd Chakra.NET
  dotnet new classlib -o Chakra
  dotnet sln add ./Chakra/Chakra.csproj
  dotnet new xunit -o Chakra.Test
  
  dotnet add Chakra.Test/Chakra.Test.csproj reference Chakra/Chakra.csproj
  dotnet sln add ./Chakra.Test/Chakra.Test.csproj
  
  
  ```

- Add the package info to csproj file

  ```xml
    <PropertyGroup>
      <TargetFramework>netcoreapp3.1</TargetFramework>
      <PackageId>Chakra.NET</PackageId>
      <Version>0.1.0</Version>
      <Authors>Nishant Singh</Authors>
      <Company>Chivoy</Company>
      <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
    </PropertyGroup>
  ```

- Add any more configuration as required from https://docs.microsoft.com/en-us/dotnet/core/tools/csproj#nuget-metadata-properties

- Package the solution

  ```bash
  dotnet pack -o ./dist
  
dotnet nuget push ./dist/Chakra.NET.0.1.0.nupkg --api-key oy2phiwctgsbq5bq67tqzbgf3pampz6elpks42xrf3b5fm --source https://api.nuget.org/v3/index.json
  
  
  ```
  
  