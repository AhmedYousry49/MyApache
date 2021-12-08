using System;

namespace MyApache
{
    class Program
    {
        static void Main(string[] args)
        {
            Kernel.StartServer();
            Console.WriteLine("server online.");
            WorkConsole();
            Console.ReadKey();
        }
        private static void WorkConsole()
        {
            while (true)
            {
                try
                {
                    CommandsAI(Console.ReadLine());
                }
                catch (Exception) { Console.WriteLine(); }
            }
        }
        public static void CommandsAI(string command)
        {
            if (command == null)
                return;
            string[] data = command.Split(' ');
            switch (data[0])

            {
                case "@exit":
                case "@shut down":
                    {

                        System.Environment.Exit(0);
                        break;
                    }
                case "@cls":
                case "@clear":
                case "@Clear":
                    {
                        Console.Clear();
                        break;
                    }

            }
        }
    }
}
