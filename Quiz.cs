using System;
using System.Collections.Generic;
using System.IO;   // For File.ReadAllLines and File operations
using System.Linq; // For converting arrays to lists with .ToList()

class Quiz
{
    private List<Question> questions = new List<Question>(); // Stores all quiz questions
    private int score = 0; // Keeps track of user's correct answers

    // Constructor: Initializes the quiz with either all or a specific number of random questions
    public Quiz(int questionCount = 0)
    {
        // Get the runtime directory (e.g., /bin/Debug/net8.0/)
        string basePath = AppContext.BaseDirectory;

        // Navigate back to the project root folder to locate the questions file
        string questionsPath = Path.Combine(basePath, "../../../questions.txt");

        // Read all lines from questions.txt
        string[] lines = File.ReadAllLines(questionsPath);
        List<string> lineList = lines.ToList(); // Make modifiable copy of lines
        Random rnd = new Random(); // For random question selection

        if (questionCount != 0)
        {
            // Load a random subset of questions
            while (questions.Count < questionCount)
            {
                int index = rnd.Next(0, lineList.Count); // Pick a random index
                var parts = lineList[index].Split('|');  // Split the line into components

                if (parts.Length == 6) // Each line must contain exactly 6 parts
                {
                    string questionText = parts[0];
                    string[] options = { parts[1], parts[2], parts[3], parts[4] };
                    int correctIndex = int.Parse(parts[5]) - 1; // Convert 1-based index to 0-based

                    lineList.RemoveAt(index); // Remove this question to avoid duplicates
                    questions.Add(new MultipleChoiceQuestion(questionText, options, correctIndex)); // Add to quiz
                }
            }
        }
        else
        {
            // Load all valid questions from the file
            foreach (var line in lines)
            {
                var parts = line.Split('|');

                if (parts.Length == 6)
                {
                    string questionText = parts[0];
                    string[] options = { parts[1], parts[2], parts[3], parts[4] };
                    int correctIndex = int.Parse(parts[5]) - 1;

                    questions.Add(new MultipleChoiceQuestion(questionText, options, correctIndex));
                }
            }
        }
    }

    // Starts the quiz session
    public void Start()
    {
        Console.WriteLine("Welcome to the Quiz!\n");

        // Get the user's name for personalized feedback and score recording
        Console.Write("Please enter your name: ");
        string quizTaker = Console.ReadLine();
        Console.WriteLine();

        // Ask each question
        foreach (var question in questions)
        {
            question.Display(); // Display question and options

            Console.Write("Your answer (1-4): ");
            string input = Console.ReadLine();

            // Parse and validate user input using the UserAnswer struct
            UserAnswer answer = new UserAnswer(input);

            // Check if the answer is valid and correct
            if (answer.IsValid && question.CheckAnswer(answer))
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine("Wrong answer.\n");
            }
        }

        // Display the final score
        Console.WriteLine($"{quizTaker}, You scored {score} out of {questions.Count}");

        // Save the score to a file using the Scores class
        Scores scores = new Scores(score, questions.Count, quizTaker);
        scores.Save();
    }
}
