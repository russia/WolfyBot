using System;
using System.Collections.Generic;
using System.Linq;

namespace WolfyBot.Core.Game.Types
{
    public class IAReflection
    {
        public static string SelectvoteVillagersTarget(GameIA Game) //L'IA est bonne
        {
            var CurrentActionInfos = Game.CurrentAction.Informations;

          
                if (Game.InLoveId != null && CurrentActionInfos != null
                    && CurrentActionInfos.Votes.Any(x => x.VoterId.Any()) //si il y a au moins 1 vote
                    && CurrentActionInfos.Votes.Where(x => x.VoterId.Count() > ((Game.PartyPlayers.Count() / 2) - 1)  //si il existe un vote qui a la majorité
                    && x.TargetId != Game.InLoveId.Id// et que ce vote n'est pas contre notre lover
                    && x.TargetId != Game._client.Userid).Any()) // et que ce vote n'est pas contre nous ^^
                    return CurrentActionInfos.Votes.Where(x => x.VoterId.Count() > ((Game.PartyPlayers.Count() / 2) - 1) && x.TargetId != Game._client.Userid && x.TargetId != Game.InLoveId.Id).First().TargetId; // alors on suit le vote
         
                if (Game.InLoveId == null && CurrentActionInfos != null
                    && CurrentActionInfos.Votes.Any(x => x.VoterId.Any()) //si il y a au moins 1 vote
                    && CurrentActionInfos.Votes.Where(x => x.VoterId.Count() > ((Game.PartyPlayers.Count() / 2) - 1)  //si il existe un vote qui a la majorité
                    && x.TargetId != Game._client.Userid).Any()) // et que ce vote n'est pas contre nous ^^
                    return CurrentActionInfos.Votes.Where(x => x.VoterId.Count() > ((Game.PartyPlayers.Count() / 2) - 1) && x.TargetId != Game._client.Userid).First().TargetId; // alors on suit le vote
           
            List<PlayerRole> targetlist = Game.PartyPlayers; // sinon on créé une liste de targets

            if (Game.CurrentRole.Side == Enums.GameSide.SOLO) // si on joue solo on évite de trop se montrer, on ne vote pas
                return "";
            if (Game.CurrentRole.Side == Enums.GameSide.DUO) //si on est en couple
                targetlist.Remove(Game.InLoveId); //on enleve son lover de la liste de targets

            if (Game.AlliesIds.Any()) // si on a au moins un allié
                targetlist = IAHelper.RemoveBfromA(targetlist, Game.AlliesIds); // on enleve les alliés des targets

            if (IAHelper.GetIntersect(Game.PartyPlayers, Game.SickRatInfected).Count() <= 2) // si il y au moins deux type non contaminé
                targetlist.AddRange(IAHelper.GetIntersect(Game.PartyPlayers, Game.SickRatInfected)); // on ajoute les potentiels rats dans la liste de targets

            targetlist = targetlist.Distinct().ToList(); // on remove les doublons
            if (targetlist.Any())
                return targetlist.OrderBy(x => Guid.NewGuid()).First().Id; // on prend une cible aléatoire dans la liste des targets
            else
                return ""; // si on a trouvé aucune cible, on retourne rien
        }

        public static string SelectvoteMayorTarget(GameIA Game) //L'IA est bonne
        {
            if (!Game.MayorCandidates.Any()) // si il n'y a aucun candidat
                return ""; // on vote pas

            if (Game.PartyPlayers.Count == 1) // si c'est 1v1
                return Game._client.Userid;//on vote pour sois meme

            if (Game.InLoveId != null && Game.MayorCandidates.Contains(Game.InLoveId)) //si le lover est un candidat
                return Game.InLoveId.Id; // on vote pour lui

            if (Game.CurrentAction.Informations.Votes.Any(x => x.VoterId.Any()) && Game.CurrentAction.Informations.Votes.Where(x => x.VoterId.Count() > ((Game.PartyPlayers.Count() / 2) - 1)).Any())
                return Game.CurrentAction.Informations.Votes.Where(x => x.VoterId.Count() > ((Game.PartyPlayers.Count() / 2) - 1)).First().TargetId;

            if (Game.AlliesIds.Any() && IAHelper.GetUnion(Game.MayorCandidates, Game.AlliesIds).Any()) //si un allier est candidat
                return IAHelper.GetUnion(Game.MayorCandidates, Game.AlliesIds).First().Id; //

            return Game.MayorCandidates.First().Id; // sinon on vote le premier maire qui a fait sa candidature
        }

        public static string SelectvoteWerewolvesTarget(GameIA Game) //L'IA est bonne
        {
            if (IAHelper.RemoveBfromA(Game.PartyPlayers, Game.AlliesIds).Any())
            { //sinon on vote pour son allier
                var temparray = IAHelper.RemoveBfromA(Game.PartyPlayers, Game.AlliesIds);
                foreach (var temp in temparray)
                    Console.WriteLine("Targets IDS : " + temp.Id);
                if (Game.InLoveId != null && temparray.Any(x => x != Game.InLoveId))
                {
                    return temparray.First(x => x.Id != Game.InLoveId.Id).Id;
                }
                else
                    return temparray.OrderBy(x => Guid.NewGuid()).First().Id; 
            }

            return Game.PartyPlayers.First().Id; //( loup contre loup)
        }

        public static string SelectcallWhiteWolfTarget(GameIA Game)
        {
            //il faut vérifier ici le nombre de loups
            return Game.PartyPlayers.First().Id;
        }

        public static string SelectcallHunterTarget(GameIA Game) //L'IA est bonne
        {
            return Game.PartyPlayers.First().Id;
        }

        public static string SelectcallGuardTarget(GameIA Game, string lastTarget) //L'IA est bonne
        {
            if (Game.InLoveId != null
                && lastTarget != Game.InLoveId.Id
                && Game.PartyPlayers.Where(x => x.Id != Game.InLoveId.Id).Any())
                return Game.PartyPlayers.Where(x => x.Id != Game.InLoveId.Id).First().Id; //on protege son lover
            else if (Game.InLoveId != null
                && lastTarget == Game.InLoveId.Id
                && Game.PartyPlayers.Where(x => x.Id != Game.InLoveId.Id).Any())
                return Game._client.Userid; //on se protege soit meme



            if(lastTarget == Game._client.Userid)
                return Game.PartyPlayers.First(x => x.Id != lastTarget).Id; //sinon on protege des mecs random
            else
                return Game._client.Userid; //on se protege soit meme
        }

        public static string[] SelectcallSickRatTargets(GameIA Game) //L'IA est a tester
        {
            string[] targets = null;
            var notinfected = IAHelper.RemoveBfromA(Game.PartyPlayers, Game.SickRatInfected);
            for (int i = 0; i < 2; i++)
            {
                if (notinfected.Count() == 1)
                {
                    targets[0] = notinfected.First().Id;
                    return targets;
                }
                else
                    targets[i] = notinfected.First(x => !targets.Contains(x.Id)).Id;
            }
            return targets;
        }

        public static bool ChoicecallBlackWolfTarget(GameIA Game, string targetid) //L'IA est bonne
        {
            if (Game.InLoveId != null && Game.InLoveId.Id == targetid)
                return true;
            if (Game.InLoveId == null && Game.CurrentDayCount == 3)
                return true;
            return false;
        }

        public static WitchAction WitchChoiceTarget(GameIA Game, WolfyBot.Core.Packets.Game.actionRequired.callWitch msg)
        {
            //todo add : !msg.DeathPotionUsed &&
            if (msg.VictimId == null)
                return new WitchAction(Game.PartyPlayers.OrderBy(x => Guid.NewGuid()).First().Id, WitchAction.type.death);

            //Todo add : !msg.HealPotionUsed &&
            if (msg.VictimId == Game._client.Userid || msg.VictimId == Game.InLoveId.Id) //si le lover ou le bot est la target && que on a pas use la potion
                return new WitchAction(msg.VictimId, WitchAction.type.heal); // on le soigne

            //on verif pas l'état des potions
            if (msg.VictimId != null)
                return new WitchAction(msg.VictimId, WitchAction.type.heal);

            return new WitchAction("", WitchAction.type.none); // n'arrive jamais ici vu qu'on verif pas l'état des potions
        }
    }
}