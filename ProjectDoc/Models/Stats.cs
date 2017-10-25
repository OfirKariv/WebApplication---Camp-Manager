using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDoc.Models

{
    public class Stats
    {

        public int ID { get; set; }
        public int CampID { get; set; }
        public int PlayerID { get; set; }
        public int PlayerGoals { get; set; }
        public int PlayerAssists { get; set; }
        public int PlayerInter { get; set; }  // Player Interception
    }
}