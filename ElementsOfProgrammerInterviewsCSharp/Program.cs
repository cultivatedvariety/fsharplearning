using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsOfProgrammerInterviewsCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var primes = Chapter06._12_EnumerateAllPrimesToN(100);
            Console.WriteLine(string.Join(",", primes));
            Console.ReadLine();
        }

    }
}
