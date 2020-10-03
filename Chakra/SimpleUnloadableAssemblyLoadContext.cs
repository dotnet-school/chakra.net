using System.Reflection;
using System.Runtime.Loader;

namespace Chakra
{
  internal class SimpleUnloadableAssemblyLoadContext : AssemblyLoadContext
  {
    public SimpleUnloadableAssemblyLoadContext()
            : base(true)
    {
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
      return null;
    }
  }
}