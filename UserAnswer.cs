// Struct for holding and validating the user's answer input
public struct UserAnswer
{
    public string RawInput;     // The raw text the user entered
    public int ParsedChoice;    // Parsed numeric choice (1-4), or -1 if invalid

    // Constructor that parses input
    public UserAnswer(string input)
    {
        RawInput = input;
        ParsedChoice = int.TryParse(input, out int result) ? result : -1; // Try to parse input; fallback to -1
    }

    // Property that checks if the input is a valid option number
    public bool IsValid => ParsedChoice >= 1 && ParsedChoice <= 4;
}
