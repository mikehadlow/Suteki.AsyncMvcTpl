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
            var result = from a in CreateInt(2)
                         from b in AddInts(a, 3)
                         from c in AddInts(b, 4)
                         select c;

            Console.WriteLine("Completed with result {0}", result.Result);
        }

        public void ComposeWithContinueWith()
        {
            var result = CreateInt(2)
                .ContinueWith(t1 => AddInts(t1.Result, 3)
                    .ContinueWith(t2 => AddInts(t2.Result, 4))
                    );

            // result is the first task, how do you get the third task's result?
        }

        static Task<int> CreateInt(int a)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting CreateInt   {0}", a);
                Thread.Sleep(1000);
                Console.WriteLine("Completing CreateInt {0}", a);
                return a;
            });
        }

        static Task<int> AddInts(int a, int b)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting AddInts     {0} + {1}", a, b);
                Thread.Sleep(1000);
                Console.WriteLine("Completing AddInts   {0} + {1}", a, b);
                return a + b;
            });
        }
    }
}