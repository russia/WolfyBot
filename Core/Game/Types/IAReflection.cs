using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Game.Types
{
    public class IAReflection
    {
        public static string SelectvoteVillagersTarget(GameIA Game)
        {          
            if (Game.CurrentRole.Side == Enums.GameSide.SOLO)
                return "";
            if (Game.CurrentRole.Side == Enums.GameSide.DUO)
                return "";
            if(Game.EnemiesIds.Any())
                return Game.EnemiesIds.First().Id; //todo improve

            return Game.PartyPlayers.First().Id; //on ne vote pour personne car on a pas trop d'info 
        }
        public static string SelectvoteMayorTarget(GameIA Game)
        {
            if (!Game.MayorCandidates.Any())
                return "";
            if (Game.InLoveId.Id != "" && Game.MayorCandidates.Contains(Game.InLoveId)) //on vote pour son lover
                return Game.InLoveId.Id;

            if (Game.MayorCandidates.Union(Game.AlliesIds).Any()) //sinon on vote pour son allier
                return Game.MayorCandidates.Union(Game.AlliesIds).First().Id; 

            return ""; //on ne vote pour personne car on a pas trop d'info 
        }
        public static string SelectvoteWerewolvesTarget(GameIA Game)
        {
            if (Game.PartyPlayers.Intersect(Game.AlliesIds).Any())
            { //sinon on vote pour son allier
                var temparray =  Game.PartyPlayers.Intersect(Game.AlliesIds);
                if(temparray.Where(x => x != Game.InLoveId).Any())
                {
                    return temparray.First(x => x.Id != Game.InLoveId.Id).Id;
                }
            }

            return Game.PartyPlayers.First().Id; //todo ( loup contre loup)
        }
        public static string SelectcallWhiteWolfTarget(GameIA Game)
        {
            return Game.PartyPlayers.First().Id;
        }
        public static string SelectcallHunterTarget(GameIA Game)
        {
            return Game.PartyPlayers.First().Id;
        }
        
        public static string SelectcallGuardTarget(GameIA Game, string lastTarget)
        {
            return Game.PartyPlayers.First(x => x.Id != lastTarget).Id;
        }


    }
}
