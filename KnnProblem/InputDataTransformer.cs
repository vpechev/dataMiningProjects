using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnnProblem
{
    public class InputDataTransformer
    {
        public static List<Iris> ParseInput(string inputString) {
            //Converting the unput data
            List<Iris> inputItems = new List<Iris>();
            string[] inputs = inputString.Split('\n');
            foreach (var currentInput in inputs)
            {
                if (currentInput != null && !currentInput.Equals("")) { 
                    string[] parts = currentInput.Split(',');
                    inputItems.Add(new Iris()
                    {
                        SepalLength = Double.Parse(parts[0]),
                        SepalWidth = Double.Parse(parts[1]),
                        PetalLength = Double.Parse(parts[2]),
                        PetalWidth = Double.Parse(parts[3]),
                        Type = ParseIrisType(parts[4])
                    });
                }
            }
            return inputItems;
        }

        private static IrisType ParseIrisType(string irisTypeString){
            irisTypeString = irisTypeString.Trim();

            if (irisTypeString.Equals("Iris-setosa"))
                return IrisType.Setosa;
            else if (irisTypeString.Equals("Iris-versicolor"))
                return IrisType.Versicolour;
            else if (irisTypeString.Equals("Iris-virginica"))
                return IrisType.Virginica;
            
            throw new ArgumentException("Unparsable iris type");
        }
    }
}
