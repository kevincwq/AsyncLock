using System;
using System.Threading.Tasks;

namespace AsyncLock
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(1, 60, async id => await new AsyncLock(id).TestMehodAsync());
            Console.ReadKey();
        }
    }
}
