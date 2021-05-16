using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тест_по_теории_вероятности
{
    class RandomVariables
    {
        string question;
        string[] answers;
        int corect_answer_number;
        public RandomVariables(string input_question, string[] input_answer, int correct)
        {
            this.answers = new string[4];
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i] = input_answer[i];
            }
            this.question = input_question;
            this.corect_answer_number = correct;
        }

        public string Question
        {
            get
            {
                return question;
            }
        }

        public string Answers(int index)
        {

            return answers[index];

        }

        public int CorrectAnswer
        {
            get
            {
                return corect_answer_number;
            }
        }
    }
}
