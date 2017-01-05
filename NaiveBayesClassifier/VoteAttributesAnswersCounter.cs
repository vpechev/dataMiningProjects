using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier
{
    public class VoteAttributesAnswersCounter
    {

        public VoteAttributesAnswersCounter()
        {
            HandicappedInfantsIsRepublicanArr = new int[4]; 
            WaterProjectCostSharingIsRepublicanArr = new int[4]; 
            AdoptionOfTheBudgetResolutionIsRepublicanArr = new int[4]; 
            PhysicianFeeFreezeIsRepublicanArr = new int[4]; 
            ElSalvadorAidIsRepublicanArr = new int[4]; 
            ReligiousGroupsInSchoolsIsRepublicanArr = new int[4]; 
            AntiSatelliteTestBanIsRepublicanArr = new int[4]; 
            AidToNicaraguanContrasIsRepublicanArr = new int[4]; 
            MxMissileIsRepublicanArr = new int[4]; 
            ImmigrationIsRepublicanArr = new int[4]; 
            SynfuelsCorporationCutbackIsRepublicanArr = new int[4]; 
            EducationSpendingIsRepublicanArr = new int[4]; 
            SuperfundRightToSueIsRepublicanArr = new int[4]; 
            CrimeIsRepublicanArr = new int[4]; 
            DutyFreeExportsIsRepublicanArr = new int[4];
            ExportAdministrationActSouthAfricaIsRepublicanArr = new int[4]; 
        }
        //arr YesYes, YesNo, NoYes, NoNo
        public int[] HandicappedInfantsIsRepublicanArr { get; set; }
        public int[] WaterProjectCostSharingIsRepublicanArr { get; set; }
        public int[] AdoptionOfTheBudgetResolutionIsRepublicanArr { get; set; }
        public int[] PhysicianFeeFreezeIsRepublicanArr { get; set; }
        public int[] ElSalvadorAidIsRepublicanArr { get; set; }
        public int[] ReligiousGroupsInSchoolsIsRepublicanArr { get; set; }
        public int[] AntiSatelliteTestBanIsRepublicanArr { get; set; }
        public int[] AidToNicaraguanContrasIsRepublicanArr { get; set; }
        public int[] MxMissileIsRepublicanArr { get; set; }
        public int[] ImmigrationIsRepublicanArr { get; set; }
        public int[] SynfuelsCorporationCutbackIsRepublicanArr { get; set; }
        public int[] EducationSpendingIsRepublicanArr { get; set; }
        public int[] SuperfundRightToSueIsRepublicanArr { get; set; }
        public int[] CrimeIsRepublicanArr { get; set; }
        public int[] DutyFreeExportsIsRepublicanArr { get; set; }
        public int[] ExportAdministrationActSouthAfricaIsRepublicanArr { get; set; }
    }
}
