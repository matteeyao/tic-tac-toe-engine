using System;
using System.Linq;
using App.Players;
using App.UI;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            int numFields = (int) Math.Pow(3, 2);
            // Console.Write(Enumerable.Range(1, numFields).Select(i => i.ToString()).ToArray().Equals(Enumerable.Range(1, numFields).Select(i => i.ToString()).ToArray()));
            string[] arrayOne = Enumerable.Range(1, numFields).Select(i => i.ToString()).ToArray();
            string[] arrayTwo = Enumerable.Range(1, numFields).Select(i => i.ToString()).ToArray();
            Console.Write(new string[3]{"1", "2", "3"}.Equals(new string[3]{"1", "2", "3"}));

            // CommandLine.Run();
        }
    }
}
