using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RXDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Subject<Tuple<string, int>> items = new Subject<Tuple<string, int>>();

            items.
                GroupBy(grp => grp.Item1)
                .SelectMany(sm => sm.Buffer(2))
                .Subscribe(sub =>
                {
                    var sum = sub.Sum(i => i.Item2);
                    var key = sub.First().Item1;

                    Console.WriteLine(key + " " + sum);
                });

            items.OnNext(Tuple.Create("a", 1));
            items.OnNext(Tuple.Create("a", 2));
            items.OnNext(Tuple.Create("a", 3));
            items.OnNext(Tuple.Create("a", 4));

            items.OnNext(Tuple.Create("b", 10));
            items.OnNext(Tuple.Create("b", 20));
            items.OnNext(Tuple.Create("b", 30));
            items.OnNext(Tuple.Create("b", 40));

            Console.ReadKey();
        }
    }
}
