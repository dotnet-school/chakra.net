using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Chakra
{
  internal class Runner
  {
    public void Execute(byte[] compiledAssembly, string[] args, int callbackPort)
    {
      List<string> argsWitPort = args.ToList();
      argsWitPort.Insert(0, $"{callbackPort}");

      var assemblyLoadContextWeakRef = LoadAndExecute(compiledAssembly, argsWitPort.ToArray());

      for (var i = 0; i < 8 && assemblyLoadContextWeakRef.IsAlive; i++)
      {
        GC.Collect();
        GC.WaitForPendingFinalizers();
      }

      Console.WriteLine(assemblyLoadContextWeakRef.IsAlive ? "Unloading failed!" : "Unloading success!");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference LoadAndExecute(byte[] compiledAssembly, string[] args)
    {
      using (var asm = new MemoryStream(compiledAssembly))
      {
        var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

        var assembly = assemblyLoadContext.LoadFromStream(asm);

        var entry = assembly.EntryPoint;

        try
        {
          _ = entry != null && entry.GetParameters().Length > 0
                  ? entry.Invoke(null, new object[] {args})
                  : entry.Invoke(null, null);
        }
        catch (System.Reflection.TargetInvocationException e)
        {
          throw e.GetBaseException();
        }

        assemblyLoadContext.Unload();

        return new WeakReference(assemblyLoadContext);
      }
    }
  }
}