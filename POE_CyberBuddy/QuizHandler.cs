using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_CyberBuddy
{
    public class QuizHandler
    {
        private readonly QuizManager _quizManager;
        private readonly Action<string> _appendText; 
        private readonly Func<string, Task> _speakAsync; 
        private readonly Action _scrollToBottom; 
        private readonly Action<string> _logAction;
        // (Tobby, 2023)

        public bool IsInQuiz { get; private set; }

        public QuizHandler(
            QuizManager quizManager,
            Action<string> appendText,
            Func<string, Task> speakAsync,
            Action scrollToBottom,
            Action<string> logAction)
            // (Tobby, 2023)
        {
            _quizManager = quizManager;
            _appendText = appendText;
            _speakAsync = speakAsync;
            _scrollToBottom = scrollToBottom;
            _logAction = logAction;
        }

        public async Task StartQuiz(string userName)
        {
            IsInQuiz = true;
            _quizManager.Reset();
            _logAction($"Quiz started by {userName}");
            await AskNextQuestion();
        }

        public async Task HandleResponse(string input)
        {
            int userChoice = ParseInput(input);
            if (userChoice != -1)
            {
                bool isCorrect = _quizManager.CheckAnswer(userChoice, out var explanation);
                _logAction($"User answered question {_quizManager.GetCurrentQuestionNumber()} with option {(char)('A' + userChoice)} - {(isCorrect ? "Correct" : "Incorrect")}"); // (Microsoft Learn, 2023)

                var resultText = isCorrect
                    ? $"Cyber Buddy: Correct! {explanation}"
                    : $"Cyber Buddy: Incorrect. {explanation}"; // (Microsoft Learn, 2023)

                _appendText(resultText + Environment.NewLine);
                await _speakAsync(resultText);

                _appendText("\n**************************************\n\n");

                _quizManager.MoveToNextQuestion();

                if (_quizManager.HasMoreQuestions())
                {
                    await AskNextQuestion();
                }
                else
                {
                    EndQuiz();
                }
            }
            else
            {
                _appendText("Cyber Buddy: Please enter a valid option (e.g., A, B, C, D or 1, 2, 3, 4)!\n");
                await _speakAsync("Please enter a valid option.");
            }

            _scrollToBottom();
        }

        private async Task AskNextQuestion()
        {
            var q = _quizManager.GetCurrentQuestion();
            _appendText($"Cyber Buddy Quiz:\n{q.Text}\n");
            await _speakAsync($"Question {_quizManager.GetCurrentQuestionNumber()}: {q.Text}");
            for (int i = 0; i < q.Options.Count; i++)
            {
                _appendText($"{q.Options[i]}\n");
            }
            _scrollToBottom();

            _logAction($"Question {_quizManager.GetCurrentQuestionNumber()} displayed: \"{q.Text}\"");
        }

        private void EndQuiz()
        {
            IsInQuiz = false;

            var finalScore = _quizManager.GetFinalScore();
            var totalQuestions = _quizManager.GetTotalQuestions();
            var finalFeedback = _quizManager.GetFinalFeedback();
            var resultMessage = $"Cyber Buddy: Quiz finished!\nYour Score: {finalScore}/{totalQuestions}\n{finalFeedback}\n";

            _appendText(resultMessage);
            _speakAsync($"Quiz finished! You scored {finalScore} out of {totalQuestions}. {finalFeedback}");

            _logAction($"Quiz finished. Final score: {finalScore}/{totalQuestions}");
            _scrollToBottom();
        }

        private int ParseInput(string input)
        {
            if (int.TryParse(input, out var parsedNumber))
                return parsedNumber - 1;

            var upperInput = input.ToUpper();
            if (upperInput.Length == 1 && upperInput[0] >= 'A' && upperInput[0] <= 'D')
                return upperInput[0] - 'A';

            return -1;
        }
    }
}
/*Refrence List:
    
    Tobby, U, 2023. Action and Func Delegates in C# – Explained with Examples. [online] Available at: 
    https://www.freecodecamp.org/news/action-and-func-delegates-in-c-sharp/
    (Accessed: 25 June 2025).
 
    Microsoft Learn, 2023. ?: operator - the ternary conditional operator. [online] Available at: 
    https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
    (Accessed: 25 June 2025).
 */

