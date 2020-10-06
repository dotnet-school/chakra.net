using System;

namespace Chakra.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] snippet =  @"
            int value = 34;
            Assert.Equal(3 , -1);".Split(Environment.NewLine);

            string result = Executor.ExecuteSnippet(snippet);

            Console.WriteLine(result);
        }

        // private static string Run()
        // {
        //     BreakLines(snippet)
        // }
    }
}
