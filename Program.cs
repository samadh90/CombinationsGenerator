using CombinationsGenerator.Processors;

namespace CombinationsGenerator;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        GeneratorProcessor processor = new GeneratorProcessor();
        processor.StartProcess();
    }
}