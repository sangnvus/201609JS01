using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingS.Models.DTOs;

namespace WingS.DataHelper
{
    public class WsRanking
    {
        public RankingDTO RankingWithPoint(int point)
        {
            var rank = new RankingDTO();

            if (point>0 && point<= WsConstant.Level.Bronze)
            {
                rank.Point = point;
                rank.CurrentRank = WsConstant.Level.Bronze;
                rank.RankPercent = Math.Round((double)(point * 100) / WsConstant.Level.Bronze, 2);
            }
            else if (point > WsConstant.Level.Bronze && point <= WsConstant.Level.Silver)
            {
                rank.Point = point;
                rank.CurrentRank = WsConstant.Level.Silver;
                rank.RankPercent = Math.Round((double)(point * 100) / WsConstant.Level.Silver, 2);
            }
            else if (point > WsConstant.Level.Silver && point <= WsConstant.Level.Golden)
            {
                rank.Point = point;
                rank.CurrentRank = WsConstant.Level.Golden;
                rank.RankPercent = Math.Round((double)(point * 100) / WsConstant.Level.Golden, 2);
            }
            else if (point > WsConstant.Level.Golden && point <= WsConstant.Level.Diamon)
            {
                rank.Point = point;
                rank.CurrentRank = WsConstant.Level.Diamon;
                rank.RankPercent = Math.Round((double)(point * 100) / WsConstant.Level.Diamon, 2);
            }

            return rank;
        }

    }
}