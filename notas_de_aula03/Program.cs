using System;
using System.Threading;

class Program
{
    static void ThreadUm()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Thread um executando!");
        }
    }

    static void ThreadDois()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Thread dois executando!");
        }
    }

    static void Main()
    {
        Thread t1 = new Thread(new ThreadStart(ThreadUm));
        Thread t2 = new Thread(new ThreadStart(ThreadDois));
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }
}