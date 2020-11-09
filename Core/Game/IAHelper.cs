using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using WolfyBot.Core.Game;
using WolfyBot.Core.Game.Types;

namespace WolfyBot.Core.Game
{
   
    public static class IAHelper
    {
        public static Role GetRole(string Role)
        {
            switch (Role)
            {
                case "werewolf": //Wolfs
                case "blackWolf":
                case "talkativeWolf":
                    return new Role(Role, "werewolves");

                case "whiteWolf": //self
                case "sickRat":
                case "mercenary":
                    return new Role(Role, "solo");

                case "villager": //Village
                case "seer":
                case "witch":
                case "littleGirl":
                case "hunter":
                case "guard":
                case "cupid":
                case "mentalist":
                case "necromencer":
                case "gravedigger":
                case "dictator":
                case "redRidingHood":
                case "pyromancer":
                case "heir":
                    return new Role(Role, "villagers");
            }
            return null;
        }
        public static List<PlayerRole> GetIntersect(List<PlayerRole> A, List<PlayerRole> B) // ELLE MARCHE
        { // return les entités qui sont dans les deux listes
            return A.Intersect(B).ToList();
        }
        public static List<PlayerRole> GetUnion(List<PlayerRole> A, List<PlayerRole> B)
        { // return les entités qui ne sont pas dans les deux listes
            return A.Union(B).ToList().Except(GetIntersect(A,B)).ToList();
        }
        public static List<PlayerRole> RemoveBfromA(List<PlayerRole> A, List<PlayerRole> B)
        { //enleve toutes les entités de B qui sont dans A
            return A.Except(B).ToList();
        }
    }
}
//public List<PlayerRole> GetAlliesList()
//{
//    if (CurrentRole.Side == Enums.GameSide.SOLO)
//        return null;
//    var list = EnemiesIds.Intersect(PartyPlayers).ToList();
//    if (InLoveId != null) // return only allie ?
//        list.Add(InLoveId);
//    return list;
//}
//public List<PlayerRole> GetEnnemiesList()
//{
//    if (CurrentRole.Side == Enums.GameSide.SOLO)
//        return PartyPlayers;
//    var list = AlliesIds.Intersect(PartyPlayers).ToList();
//    if (InLoveId != null) // return only allie ?
//        list.Add(InLoveId);
//    return list;
//}