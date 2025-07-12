// Abstract base class for all question types
abstract class Question
{
    public string Text; // The question text

    public Question(string text)
    {
        Text = text;
    }

    // All question types must implement how they display themselves
    public abstract void Display();

    // All question types must implement how they check the user's answer
    public abstract bool CheckAnswer(UserAnswer answer);
}

// A specific implementation of a multiple-choice question
class MultipleChoiceQuestion : Question
{
    private string[] options; // Array of answer options
    private int correctOption; // Correct answer index (0-based)

    public MultipleChoiceQuestion(string text, string[] options, int correctOption)
        : base(text)
    {
        this.options = options;
        this.correctOption = correctOption;
    }

    // Display the question text and numbered options
    public override void Display()
    {
        Console.WriteLine(Text);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}"); // Show option numbers starting from 1
        }
    }

    // Check if user input matches the correct option (after parsing and adjusting index)
    public override bool CheckAnswer(UserAnswer answer)
    {
        // Only check ParsedChoice, since validation is already done
        return answer.ParsedChoice - 1 == correctOption;
    }
}