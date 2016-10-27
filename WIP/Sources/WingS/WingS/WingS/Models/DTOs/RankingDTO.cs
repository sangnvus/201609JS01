using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingS.Models.DTOs
{
    public class RankingDTO
    {
        public int Point { get; set; }
        public int CurrentRank { get; set; }
        public double RankPercent { get; set; }

        public RankingDTO()
        {
            Point = 0;
            CurrentRank = 0;
            RankPercent = 0;
        }
    }
}