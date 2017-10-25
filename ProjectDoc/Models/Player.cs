using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDoc.Models

{
    public partial class Player
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Failed name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Failed pass")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Team { get; set; }
        public Positions? Position { get; set; }

        public int Age { get; set; }

        public int GoalsSum { get; set; }
        public int AssistsSum { get; set; }

        public int InterceptionsSum { get; set; }

        public List<Camp> tempCamps { get; set; }

        public virtual ICollection<Camp> Camps { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
        public bool ficici { get; set; }
        public bool topChef { get; set; }
        public bool lastStop { get; set; }


        public bool gotCampStat(int campId)
        {
            bool got = false;
            if (Stats.Any(a => a.CampID == campId))
            {
                got = true;
            }
            else got = false;

            return got;

        }
      
        public void updateStats()
        {
            int CountGoals=0, CountAssists=0, CountInter=0;
            
            foreach (Stats s in Stats)
            {
                CountGoals += s.PlayerGoals;
                CountAssists += s.PlayerAssists;
                CountInter += s.PlayerInter;

            }
            this.GoalsSum = CountGoals;
            this.AssistsSum = CountAssists;
            this.InterceptionsSum = CountInter;
            
        }
        
        public Player()
        {
            init();
        }

        public void init()
        {
            ficici = false;
            topChef = false;
            lastStop = false;
            Camps = new List<Camp>();
            Stats = new List<Stats>();
        }

        public int countFinishedCamps()
        {
            var camps = Camps.ToArray();
            var count=0;
            for (var i =0;i < camps.Length;i++)
            {
                if(camps[i].isFinished())
                {
                    count++;
                }
            }

            return count;
        }

        public int countUpcomingCamps()
        {
            var camps = Camps.ToArray();
            var count = 0;
            for (var i = 0; i < camps.Length; i++)
            {
                if (!camps[i].isFinished())
                {
                    count++;
                }
            }

            return count;
        }
        public int countStats()
        {
            return Stats.Count();
        }
        
        public bool isFreeDate(Camp ExpectedCamp)
        {

            DateTime start = ExpectedCamp.sDate;
            DateTime end = ExpectedCamp.eDate;


            if (this.Camps == null)
            {
                this.Camps = new List<Camp>();
                return true;
            }
        

            foreach (var SignedCamp in this.Camps)
            {

                if (BeetweenTwoDates(SignedCamp.sDate, SignedCamp.eDate, ExpectedCamp.sDate) || BeetweenTwoDates(SignedCamp.sDate, SignedCamp.eDate, ExpectedCamp.eDate))
                    return false;

                if (ExpectedCamp.sDate <= SignedCamp.sDate && SignedCamp.eDate <= ExpectedCamp.eDate)
                    return false;


            }

            return true;


        }

        public bool BeetweenTwoDates(DateTime a, DateTime b, DateTime toCheck)

        {
            if (toCheck >= a && toCheck <= b)
                return true;
            else
                return false;



        }

        public bool isExists(Camp camp)
        {

            if (camp.Players == null)
                camp.Players = new List<Player>();

            foreach (var SignedPlayer in camp.Players)
                if (SignedPlayer.UserName.Equals(this.UserName))
                    return false;
            return true;



        }

    }
    public enum Positions
    {
        Keeper,
        Defender,
        FullBack,
        Midfilder,
        Winger,
        Striker
    }
}