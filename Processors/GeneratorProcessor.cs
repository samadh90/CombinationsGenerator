// Author: Samad Hatsijev
// Date: 2023-01-21
// Description: This class contains the logic for generating all possible combinations of the given length.

using System.Text;

namespace CombinationsGenerator.Processors;

public class GeneratorProcessor
{
    /// <summary>
    /// This property contains the string of characters to use.
    /// </summary>
    public string Characters { get; set; }

    /// <summary>
    /// This property contains the minimum length of the combinations.
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// This property contains the maximum length of the combinations.
    /// </summary>
    public int MaxLength { get; set; }

    /// <summary>
    /// This property contains the number of threads to use.
    /// </summary>
    public int NumberOfThreads { get; set; }

    /// <summary>
    /// This property contains the path to the output file.
    /// </summary>
    public string FilePath { get; set; }

    public GeneratorProcessor()
    {
        MinLength = 4;
        MaxLength = 4;
        Characters = InitiateString();
        NumberOfThreads = 4;
        FilePath = "./combinations.csv";
    }

    public void StartGenerating() 
    {
        List<List<string>> listOfCombinations = new List<List<string>>();

        

    }

    /// <summary>
    /// This function starts the process of generating all possible combinations of the given length.
    /// </summary>
    public void Start()
    {
        // Declare a list of char arrays to store the characters
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
                    // Write the combination to the output file
                    outputFile.WriteLine(combination);
                }
            }
        }
    }

    /// <summary>
    /// This function generates all possible combinations of the given length.
    /// </summary>
    /// <param name="characters">The string of characters to use</param>
    /// <param name="length">The length of the combinations</param>
    /// <param name="prefix">The prefix to use</param>
    /// <returns>Returns an IEnumerable of strings containing all possible combinations of the given length.</returns>
    private static IEnumerable<string> GetCombinations(string characters, int length, string prefix = "")
    {
        // Empty string is a valid combination
        if (length == 0)
        {
            yield return prefix;
        }
        else
        {
            // For each character in the string
            foreach (char c in characters)
            {
                // Get the combinations of the remaining characters
                var combinations = GetCombinations(characters, length - 1, prefix + c);

                // Return each combination
                foreach (string combination in combinations)
                {
                    yield return combination;
                }
            }
        }
    }

    /// <summary>
    /// This function initiates the string of characters to use.
    /// </summary>
    public void StartProcess()
    {
        // Declare a list of char arrays to store the characters
        List<char[]> listOfCharArray = new List<char[]>();

        // Declare an array to store the character start and end indexes
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

        // Iterate through the array of integers to store the character start and end indexes
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

    /// <summary>
    /// This function initializes the string of characters to use.
    /// </summary>
    private string InitiateString()
    {
        // Declare a variable to hold the characters, numbers, and special characters
        StringBuilder sb = new StringBuilder();

        // Add the lowercase characters to the StringBuilder
        sb.Append("abcdefghijklmnopqrstuvwxyz");

        // Add the uppercase characters to the StringBuilder
        sb.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        // Add the numbers to the StringBuilder
        sb.Append("0123456789");

        // Add the special characters to the StringBuilder
        sb.Append("!@#$%^&*()_+");

        // Return the characters, numbers, and special characters
        return sb.ToString();
    }
}