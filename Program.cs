using System; // includes the core data types as wells as I/O functionality
using System.Collections.Generic; // namespace for collections like Lists and Dictionaries

class Program
{
    static void Main(string[] args)
    {
        Quiz quiz = new Quiz(100); // create a new quiz object from the quiz class and initialize with the number of questions
        quiz.Start(); // start the quiz
    }
}