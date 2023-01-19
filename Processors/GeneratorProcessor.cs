// Author: Samad Hatsijev
// Date: 2019-01-01
// Description: This is a tool that will help you to break a string into multiple threads

using System.Text;

namespace CombinationsGenerator.Processors;

public class GeneratorProcessor
{
    public string Characters { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
    public int NumberOfThreads { get; set; }
    public string FilePath { get; set; }

    public GeneratorProcessor()
    {
        MinLength = 4;
        MaxLength = 4;
        Characters = InitiateString();
        NumberOfThreads = 4;
        FilePath = "./combinations.csv";
    }

    public void Start()
    {
        using (StreamWriter outputFile = new StreamWriter(FilePath))
        {
            // Loop through the range of lengths
            for (int length = MinLength; length <= MaxLength; length++)
            {
                // Generate all possible combinations of the given length
                var combinations = GetCombinations(Characters, length);

                // Write the combinations to the output file
                foreach (string combination in combinations)
                {
                    Console.WriteLine(combination);
                    outputFile.WriteLine(combination);
                }
            }
        }
    }

    private static IEnumerable<string> GetCombinations(string characters, int length, string prefix = "")
    {
        // Base case -- if length is zero, return prefix
        if (length == 0)
        {
            yield return prefix;
        }
        // Recursive case -- for each character in the list,
        // add it to the prefix and make a recursive call
        else
        {
            foreach (char c in characters)
            {
                var combinations = GetCombinations(characters, length - 1, prefix + c);
                foreach (string combination in combinations)
                {
                    yield return combination;
                }
            }
        }
    }

    public void StartProcess()
    {
        // Declare a list of char arrays
        List<char[]> listOfCharArray = new List<char[]>();

        // Create an array of integers to store the character start and end indexes
        int[] array = new int[NumberOfThreads + 1];

        // Set the first element of the array to zero
        array[0] = 0;

        // Get the number of sets for the number of threads
        int numberOfSets = (int)Math.Ceiling((double)Characters.Length / NumberOfThreads);

        // Declare two variables to store the start and end indexes
        int startIndex = 0;
        int endIndex = numberOfSets;

        // Iterate through the array of integers to store the character start and end indexes
        for (int index = 0; index < NumberOfThreads; index++)
        {
            // Set the end index of the array to the end index
            array[index + 1] = endIndex;

            // Set the start index to the end index
            startIndex = endIndex;

            // Set the end index to the start index plus the number of sets
            endIndex = startIndex + numberOfSets;

            // If the end index is greater than the length of the character array
            if (endIndex > Characters.Length)
            {
                // Set the end index to the length of the character array
                endIndex = Characters.Length;
            }
        }

        for (int index = 0; index < array.Length - 1; index++)
        {
            // Break the characters into an array
            char[] characters = BreakCharacters(Characters, array[index], array[index + 1]);

            // Add the array to the list of char arrays
            listOfCharArray.Add(characters);
        }
    }

    /// <summary>
    /// This function breaks the character string into an array of characters.
    /// </summary>
    /// <param name="characters">The string of characters to break</param>
    /// <param name="startIndex">The start index of the characters to break</param>
    /// <param name="endIndex">The end index of the characters to break</param>
    /// <returns>Returns a char array from specified range in string of characters.</returns>
    private char[] BreakCharacters(string characters, int startIndex, int endIndex)
    {
        // Create a new char array to hold the characters from startIndex to endIndex
        char[] output = new char[endIndex - startIndex];

        // Copy the characters from startIndex to endIndex into the output array
        for (int index = startIndex; index < endIndex; index++)
            output[index - startIndex] = characters[index];

        // Return the new char array
        return output;
    }

    private string InitiateString()
    {
        string characters = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";
        string specialCharacters = "!@#$%^&*()_+";

        StringBuilder sb = new StringBuilder();
        sb.Append(characters);
        sb.Append(characters.ToUpper());
        sb.Append(numbers);
        sb.Append(specialCharacters);

        return sb.ToString();
    }
}