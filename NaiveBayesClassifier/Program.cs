using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier
{
    class Program
    {
        private static Random rand = new Random(DateTime.Now.Second);
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"data.txt");
            string[] inputArr = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            List<Vote> population = new List<Vote>();

            //normalize and parse data
            foreach (var row in inputArr)
            {
                var attributes = row.Split(',');
                var vote = ParseObjectFromInputData(attributes);
                population.Add(vote);
            }
            //first we compute the probability of each attribute divided by the class sunny -> yes/no 
        }

        public static Vote ParseObjectFromInputData(string[] attributes)
        {
            var vote = new Vote()
            {
                HandicappedInfants = getAttributeValue(attributes[0]),
                WaterProjectCostSharing = getAttributeValue(attributes[1]),
                AdoptionOfTheBudgetResolution = getAttributeValue(attributes[2]),
                PhysicianFeeFreeze = getAttributeValue(attributes[3]),
                ElSalvadorAid = getAttributeValue(attributes[4]),
                ReligiousGroupsInSchools = getAttributeValue(attributes[5]),
                AntiSatelliteTestBan = getAttributeValue(attributes[6]),
                AidToNicaraguanContras = getAttributeValue(attributes[7]),
                MxMissile = getAttributeValue(attributes[8]),
                Immigration = getAttributeValue(attributes[9]),
                SynfuelsCorporationCutback = getAttributeValue(attributes[10]),
                EducationSpending = getAttributeValue(attributes[11]),
                SuperfundRightToSue = getAttributeValue(attributes[12]),
                Crime = getAttributeValue(attributes[13]),
                DutyFreeExports = getAttributeValue(attributes[14]),
                ExportAdministrationActSouthAfrica  = getAttributeValue(attributes[15]),
                Class = getAttributeValue(attributes[16]),
                YCount = attributes.Count(x=> x.Equals("'y'")),
                AllAttrCount = attributes.Length
            };

            vote.NCount = attributes.Length - vote.YCount;

            return vote;
        }

        private static bool getAttributeValue(string attribute)
        {
            if (attribute.Equals("y"))
                return true;
            else if (attribute.Equals("n"))
                return false;
            else
                return rand.Next(100) < 50 ? false : true;
        }
    }
}
