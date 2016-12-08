using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier
{
    public class Vote
    {
        public bool HandicappedInfants { get; set; }
        public bool WaterProjectCostSharing { get; set; }
        public bool AdoptionOfTheBudgetResolution { get; set; }
        public bool PhysicianFeeFreeze { get; set; }
        public bool ElSalvadorAid { get; set; }
        public bool ReligiousGroupsInSchools { get; set; }
        public bool AntiSatelliteTestBan { get; set; }
        public bool AidToNicaraguanContras { get; set; }
        public bool MxMissile { get; set; }
        public bool Immigration { get; set; }
        public bool SynfuelsCorporationCutback { get; set; }
        public bool EducationSpending { get; set; }
        public bool SuperfundRightToSue { get; set; }
        public bool Crime { get; set; }
        public bool DutyFreeExports { get; set; }
        public bool ExportAdministrationActSouthAfrica { get; set; }
        public bool Class { get; set; }

        public int YCount { get; set; }
        public int NCount { get; set; }
        public int AllAttrCount { get; set; }
    }
}
