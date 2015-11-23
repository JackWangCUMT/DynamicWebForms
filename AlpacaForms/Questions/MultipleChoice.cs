using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Questions
{
    /// <summary>
    /// Reflects a multiple choice question
    /// Each answer is represented as a radio button
    /// </summary>
    public class MultipleChoice : Base
    {
        public List<String> @enum;
        public MultipleChoice()
        {
            type = "string";
            @enum = new List<string>();
        }

        public void AddAnswers(List<String> input)
        {
            @enum.AddRange(input);
        }
        
        public void AddAnswer(string input)
        {
            @enum.Add(input);
        }
    }
}
