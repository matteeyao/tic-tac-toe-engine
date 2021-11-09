using System;
using System.IO;

namespace Test.TestHelper
{
    public static class TestHelper
    {
        public static void SetInput(string input)
        {
            StringReader stringReader = new StringReader(input);
            Console.SetIn(stringReader);
        }
    }
}
