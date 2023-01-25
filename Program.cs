using CombinationsGenerator.Processors;

namespace CombinationsGenerator;

public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        GeneratorProcessor processor = new GeneratorProcessor();
        processor.StartProcess2();
    }
}