using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDoc.Models

{
    public class Camp
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumOfPlayers { get; set; }

        public DateTime sDate { get; set; }
        public DateTime eDate { get; set; }

        public Locations? Location { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Player> Players { get; set; }

        public void AddPlayerToList(Player Player)
        {
            Players.Add(Player);
        }
        public void AddCommentrToList(Comment Comment)
        {

            Comments.Add(Comment);
        }

        public bool isFinished()
        {            
            DateTime now = DateTime.Now;
 
            return (eDate<now);
        }


        public int AvailablePlaces()
        {
            int SeatsTaken;


            if (Players == null)
            {

                SeatsTaken = 0;
                return 0;
            }
            else
                SeatsTaken = Players.Count();



            return NumOfPlayers - SeatsTaken;
        }

        public int Registered()
        {
            if (Players == null)
                return 0;
            return Players.Count();
                
        }

        public int NumOfComments()
        {

            if (Comments == null)
                return 0;
            else
                return Comments.Count();


        }


    }
    public enum Locations
    {
        Turin,
        Barcelona,
        London,
        Jerusalem

    }
}