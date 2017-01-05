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
            var rand = new Random(System.DateTime.Now.Second);
            string input = System.IO.File.ReadAllText(@"data.txt");
            string[] inputArr = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            
            //Step 1
            List<Vote> population = NormalizeDataAndGetPopulation(inputArr);

            //Step 2
            var sets = InitialDividingOfDataIntoSets(population);

            //Step 3
            for (int i = 0; i < sets.Count(); i++)
            {
                ClassificateByNaiveBayes(sets, i);
            }

            Console.ReadLine();
        }

        public static void ClassificateByNaiveBayes(List<Vote>[] sets, int currentIndex)
        {
            var vaac = new VoteAttributesAnswersCounter();

            //learning phase
            var testingData = sets[currentIndex];
            for (int j = 0; j < sets.Length; j++)
            {
                if (j == currentIndex)
                    continue;

                foreach (var vote in sets[currentIndex])
                {
                    ComputeFrequencyForObject(vote, vaac);
                }
            }

            //testing phase
            int matchedCount = 0;
            foreach (var vote in testingData)
            {
                bool isRepublican = ComputeIsRepublican(vote, vaac);
                PrintResult(isRepublican, vote.IsRepublican);

                if (isRepublican == vote.IsRepublican)
                    matchedCount++;
            }
            Console.WriteLine("Matched count: {0} from {1}", matchedCount, testingData.Count);
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
                
                IsRepublican = getAttributeValue(attributes[16]),
                YCount = attributes.Count(x=> x.Equals("'y'")),
                //AllAttrCount = attributes.Length
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

        public static List<Vote> NormalizeDataAndGetPopulation(string[] inputArr)
        {
            List<Vote> population = new List<Vote>();
            foreach (var row in inputArr)
            {
                var attributes = row.Split(',');
                var vote = ParseObjectFromInputData(attributes);

                population.Add(vote);
            }

            return population;
        }

        public static List<Vote>[] InitialDividingOfDataIntoSets(List<Vote> population)
        {
            var sets = new List<Vote>[10];
            var equalSetLimit = population.Count / sets.Count();

            //initialize sets of data
            for (int i = 0; i < population.Count; i++)
            {
                int randSetIdx = rand.Next(sets.Length);
                if (sets[randSetIdx] == null)
                    sets[randSetIdx] = new List<Vote>();

                sets[randSetIdx].Add(population[i]);
            }

            return sets;
        }

        public static bool ComputeIsRepublican(Vote vote, VoteAttributesAnswersCounter vaac)
        {
            var yesProbability = GetYesProbabilityValue(vote, vaac);
            var noProbability = GetNoProbabilityValue(vote, vaac);

            return yesProbability > noProbability ? true : false;
        }

        public static double GetYesProbabilityValue(Vote vote, VoteAttributesAnswersCounter vaac)
        {
            var product = GetYesProductAttributeProbability(vote.HandicappedInfants, vaac.HandicappedInfantsIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.WaterProjectCostSharing, vaac.WaterProjectCostSharingIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.AdoptionOfTheBudgetResolution, vaac.AdoptionOfTheBudgetResolutionIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.PhysicianFeeFreeze, vaac.PhysicianFeeFreezeIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.ElSalvadorAid, vaac.ElSalvadorAidIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.ReligiousGroupsInSchools, vaac.ReligiousGroupsInSchoolsIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.AntiSatelliteTestBan, vaac.AntiSatelliteTestBanIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.AidToNicaraguanContras, vaac.AidToNicaraguanContrasIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.MxMissile, vaac.MxMissileIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.Immigration, vaac.ImmigrationIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.SynfuelsCorporationCutback, vaac.SynfuelsCorporationCutbackIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.EducationSpending, vaac.EducationSpendingIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.SuperfundRightToSue, vaac.SuperfundRightToSueIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.Crime, vaac.CrimeIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.DutyFreeExports,vaac.DutyFreeExportsIsRepublicanArr)
                    * GetYesProductAttributeProbability(vote.ExportAdministrationActSouthAfrica, vaac.ExportAdministrationActSouthAfricaIsRepublicanArr);

            return product;
        }

        public static double GetNoProbabilityValue(Vote vote, VoteAttributesAnswersCounter vaac)
        {
            var product = GetNoProductAttributeProbability(vote.HandicappedInfants, vaac.HandicappedInfantsIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.WaterProjectCostSharing, vaac.WaterProjectCostSharingIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.AdoptionOfTheBudgetResolution, vaac.AdoptionOfTheBudgetResolutionIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.PhysicianFeeFreeze, vaac.PhysicianFeeFreezeIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.ElSalvadorAid, vaac.ElSalvadorAidIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.ReligiousGroupsInSchools, vaac.ReligiousGroupsInSchoolsIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.AntiSatelliteTestBan, vaac.AntiSatelliteTestBanIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.AidToNicaraguanContras, vaac.AidToNicaraguanContrasIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.MxMissile, vaac.MxMissileIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.Immigration, vaac.ImmigrationIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.SynfuelsCorporationCutback, vaac.SynfuelsCorporationCutbackIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.EducationSpending, vaac.EducationSpendingIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.SuperfundRightToSue, vaac.SuperfundRightToSueIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.Crime, vaac.CrimeIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.DutyFreeExports, vaac.DutyFreeExportsIsRepublicanArr)
                    * GetNoProductAttributeProbability(vote.ExportAdministrationActSouthAfrica, vaac.ExportAdministrationActSouthAfricaIsRepublicanArr);

            return product;
        }

        public static double GetYesProductAttributeProbability(bool attribute, int[] array)
        {
            //arr YesYes, YesNo, NoYes, NoNo
            int total = array[0] + array[2];
            return attribute ? (double)array[0]/total : (double)array[2]/total;
        }

        public static double GetNoProductAttributeProbability(bool attribute, int[] array)
        {
            //arr YesYes, YesNo, NoYes, NoNo
            int total = array[1] + array[3];
            return attribute ? (double)array[1] / total : (double)array[3] / total;
        }

        public static void ComputeFrequencyForCurrentItem(bool attribute, bool isRepublican, int[] answersArray)
        {
            if (attribute && isRepublican)
            {
                answersArray[0]++;
            }
            else if (attribute && !isRepublican)
            {
                answersArray[1]++;
            }
            else if (!isRepublican && isRepublican)
            {
                answersArray[2]++;
            }
            else if (!isRepublican && !isRepublican)
            {
                answersArray[3]++;
            }
        }

        public static void ComputeFrequencyForObject(Vote vote, VoteAttributesAnswersCounter vaac) {
            ComputeFrequencyForCurrentItem(vote.HandicappedInfants, vote.IsRepublican, vaac.HandicappedInfantsIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.WaterProjectCostSharing, vote.IsRepublican, vaac.WaterProjectCostSharingIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.AdoptionOfTheBudgetResolution, vote.IsRepublican, vaac.AdoptionOfTheBudgetResolutionIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.PhysicianFeeFreeze, vote.IsRepublican, vaac.PhysicianFeeFreezeIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.ElSalvadorAid, vote.IsRepublican, vaac.ElSalvadorAidIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.ReligiousGroupsInSchools, vote.IsRepublican, vaac.ReligiousGroupsInSchoolsIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.AntiSatelliteTestBan, vote.IsRepublican, vaac.AntiSatelliteTestBanIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.AidToNicaraguanContras, vote.IsRepublican, vaac.AidToNicaraguanContrasIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.MxMissile, vote.IsRepublican, vaac.MxMissileIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.Immigration, vote.IsRepublican, vaac.ImmigrationIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.SynfuelsCorporationCutback, vote.IsRepublican, vaac.SynfuelsCorporationCutbackIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.EducationSpending, vote.IsRepublican, vaac.EducationSpendingIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.SuperfundRightToSue, vote.IsRepublican, vaac.SuperfundRightToSueIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.Crime, vote.IsRepublican, vaac.CrimeIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.DutyFreeExports, vote.IsRepublican, vaac.DutyFreeExportsIsRepublicanArr);
            ComputeFrequencyForCurrentItem(vote.ExportAdministrationActSouthAfrica, vote.IsRepublican, vaac.ExportAdministrationActSouthAfricaIsRepublicanArr);
        }

        public static void PrintResult(bool predictedIsRepublican, bool realIsRepublican)
        {
            Console.WriteLine("Predicted: {0} ", predictedIsRepublican ? "Republican" : "Democrat");
            Console.WriteLine("Real: {0}", realIsRepublican ? "Republican" : "Democrat");
        }
    }
}
