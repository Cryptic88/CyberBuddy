using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POE_CyberBuddy;

namespace POE_CyberBuddy
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
        public string Explanation { get; set; }

        public Question(string text, List<string> options, int correctOptionIndex, string explanation)
        {
            Text = text;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
            Explanation = explanation;
        }
    }
    public class QuizManager
    {
        private List<Question> questions;
        private int currentIndex = 0;
        private int score = 0;
        public QuizManager()
        {
            questions = new List<Question>
            {
                new Question("What should you do if you receive an email asking for your password?",
                    new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it\n" },
                    2, "Always report phishing emails to help prevent scams.\n"),

                new Question("Which of the following is a strong password?",
                    new List<string> { "A) password123", "B) 123456", "C) P@55w0rd!!", "D) qwerty\n" },
                    2, "A strong password includes special characters, numbers, and letters.\n"),

                new Question("True or False: It's safe to use public WiFi for online banking.",
                    new List<string> { "A) True", "B) False\n" },
                    1, "Public WiFi is not safe for online banking as it can be easily intercepted.\n"),

                new Question("What is the best way to protect your computer from malware?",
                    new List<string> { "A) Install and update antivirus software", "B) Click on every link you receive", "C) Use any password you like", "D) Share files from unknown sources\n" },
                    0, "Always use updated antivirus software to protect your computer.\n"),

                new Question("What is two-factor authentication (2FA)?",
                    new List<string> { "A) A password that is 2 characters long", "B) A method that requires two forms of verification", "C) A way to bypass a password", "D) A common phishing technique\n" },
                    1, "2FA adds an extra layer of security by requiring a second verification method.\n"),

                new Question("Which of the following is an example of phishing?",
                    new List<string> { "A) An email from your boss about work", "B) An unexpected email requesting bank details", "C) An online chat with a trusted friend", "D) An official company newsletter\n" },
                    1, "Phishing is when someone tries to obtain sensitive information through deceptive emails.\n"),

                new Question("What does HTTPS stand for?",
                    new List<string> {
                        "A) Hyperlink Transfer Protocol Standard","B) HyperText Transmission Protocol System", "C) Hyperlink Transfer Protocol Secure", "D) HyperText Transfer Protocol Secure\n"},
                    3, "HTTPS encrypts data between your browser and the website, ensuring secure communication.\n"),

                new Question("True or False: It's okay to reuse the same password for multiple accounts.",
                    new List<string> { "A) True", "B) False" },
                    1, "It's best to use unique passwords for each account to reduce risk.\n"),

                new Question("Which of the following is a strong method for creating a password?",
                    new List<string> { "A) Use your birthdate", "B) Use a mix of uppercase, lowercase, numbers, and special characters", "C) Use the word 'password'", "D) Use a common word\n" },
                    1, "Strong passwords use a combination of character types.\n"),

                new Question("What is a firewall used for?",
                    new List<string> { "A) To block unauthorized access to a network", "B) To send spam emails", "C) To fix a broken computer", "D) To store files\n" },
                    0, "A firewall helps protect a network by controlling incoming and outgoing traffic.\n"),

                new Question("Which of the following is a sign that an email is a phishing attempt?",
                    new List<string> { "A) It contains a link to reset your password", "B) It's from a known sender", "C) It's personalized with your name", "D) It's using a generic greeting like 'Dear Customer'\n" },
                    3, "Generic greetings can be a sign that the email is not legitimate.\n"),

                new Question("True or False: Using 'password' as your password is a good security practice.",
                    new List<string> { "A) True", "B) False\n" },
                    1, "Using 'password' is very insecure. Always use strong, unique passwords.\n")
            };
        }
        public bool HasMoreQuestions() => currentIndex < questions.Count;

        public Question GetCurrentQuestion() => questions[currentIndex];

        public bool CheckAnswer(int userChoice, out string explanation)
        {
            var question = questions[currentIndex];
            if (userChoice == question.CorrectOptionIndex)
            {
                score++;
                explanation = question.Explanation;
                return true;
            }
            else
            {
                explanation = question.Explanation;
                return false;
            }
        }
        public int GetCurrentQuestionNumber()
        {
            return currentIndex + 1;
        }

        public void MoveToNextQuestion() => currentIndex++;

        public int GetFinalScore() => score;

        public int GetTotalQuestions() => questions.Count;

        public string GetFinalFeedback()
        {
            if (score >= 8) return "Great Job! You're a cybersecurity pro.";
            if (score >= 5) return "Good effort! Keep learning to stay safe online.";
            return "Keep learning to stay safe online.";
        }

        public void Reset()
        {
            currentIndex = 0;
            score = 0;
        }
    }
}