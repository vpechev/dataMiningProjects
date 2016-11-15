using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class InputDataTransformer
    {
        public static List<Chromosome> TransformInputData(string input)
        {
            //Converting the unput data
            List<Chromosome> inputItems = new List<Chromosome>();
            string[] inputs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var currentInput in inputs)
            {
                string[] parts = currentInput.Split(' ');
                inputItems.Add(new Chromosome()
                {
                    Ci = Double.Parse(parts[0]),
                    Mi = Double.Parse(parts[1])
                });
            }
            return inputItems;
        }
    }
}
