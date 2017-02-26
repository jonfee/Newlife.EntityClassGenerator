using System;

namespace DesignToEntityFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityFactory factory = new EntityFactory();

            factory.Run();

            Console.ReadKey();
        }
    }
}
