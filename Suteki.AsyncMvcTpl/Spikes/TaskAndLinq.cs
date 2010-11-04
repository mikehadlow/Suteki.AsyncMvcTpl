using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suteki.AsyncMvcTpl.Spikes
{
    public class TaskAndLinq
    {
        public void CanComposeTasksWithLinq()
        {
            var t1 = Task<int>.Factory.StartNew(() =>
            {
                Console.WriteLine("starting t1");
                Thread.Sleep(1000);
                return 1;
            });

            var result = from a in t1
                         from b in Add2(a)
                         from c in Add3(b)
                         select c;

            Console.WriteLine(result.Result);
        }

        Task<int> Add2(int a)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting Add2");
                Thread.Sleep(1000);
                return a + 2;
            });
        }

        Task<int> Add3(int b)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting Add3");
                Thread.Sleep(1000);
                return b + 3;
            });
        }
    }
}