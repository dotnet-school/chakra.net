using Microsoft.CodeAnalysis;
using Xunit;

using static Chakra.Test.TestHelper;
namespace Chakra.Test
{
    public class AssemblyTest
    {

        // [Fact]
        // public void ShouldGiveCompileErrorIfAssemblyMissing()
        // {
        //     var customImports = new[] {"using System.ComponentModel;"};
        //
        //     string snippet = @"
        //         EventDescriptor a = null;
        //         Console.WriteLine(""not error"");
        //     ";
        //
        //     Assert.Throws<DynamicCompilationException>(() => 
        //                     Executor.ExecuteSnippet(BreakLines(snippet), customImports
        //     ));
        //     Executor.ResetAssemblies();
        // }
        //
        [Fact]
        public void ShouldAllowAddingCustomAssemblyAndImports()
        {
            var custom = new MetadataReference[] {
                MetadataReference.CreateFromFile(typeof(System.ComponentModel.Container).Assembly.Location),
            };
            var customImports = new string[] {"using System.ComponentModel;"};
            
            Executor.AddAssemblies(custom);
            
            string snippet = @"
                EventDescriptor a = null;
                Console.WriteLine(""not error"");
            ";

            var expected = "not error";
            var actual = Executor.ExecuteSnippet(BreakLines(snippet), customImports);

            Assert.Equal(expected, actual);
        }
    }
}
