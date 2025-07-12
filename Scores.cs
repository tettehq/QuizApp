using System.IO;

// Class for formatting and saving the user's score
class Scores
{
    private string line; // Holds the formatted score line

    // Constructor formats the score line with the user's name and result
    public Scores(int score, int questionCount, string quizTaker = "No name")
    {
        line = $"{quizTaker}: {score} out of {questionCount}";
    }

    // Appends the score line to a file named scores.txt
    public void Save()
    {
        // Get the base directory of the running application
        string basePath = AppContext.BaseDirectory;

        // Navigate back to the root folder where scores.txt is located
        string scoresPath = Path.Combine(basePath, "../../../scores.txt");

        // Append the score entry with a newline
        File.AppendAllText(scoresPath, line + Environment.NewLine);
    }
}
