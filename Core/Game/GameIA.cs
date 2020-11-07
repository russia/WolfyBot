using System;
using System.Collections.Generic;
using System.Threading;
using WolfyBot.Core.Game.Types;

namespace WolfyBot.Core.Game
{
    public class GameIA
    {
        //ne pas oublier que la side change avec l'infection / couple
        public Role CurrentRole;
        public Client _client;
        public GameAction CurrentAction;
        public List<PlayerRole> PartyPlayers = new List<PlayerRole>();
        public string CurrentMayor = "";
        public List<PlayerRole> AlliesIds = new List<PlayerRole>();
        public List<PlayerRole> EnemiesIds = new List<PlayerRole>();
        public List<PlayerRole> SickRatInfected = new List<PlayerRole>();
        public List<PlayerRole> MayorCandidates = new List<PlayerRole>();
        public List<PlayerRole> AccusatedPlayers = new List<PlayerRole>();
        public PlayerRole InLoveId;
        public int CurrentDayCount;
        public GameIA(Client client)
        {
            _client = client;
        }
        //TODO CHANGE VOTES : CEST PAS TARGET ID MAIS foreach VOTE.targets


        #region Actions
        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteVillagers msg)
        {
            
            CurrentAction = new GameAction(msg.Id, msg.TimeLeft);
            if (CurrentDayCount < 2)
                return;
                string targetid = IAReflection.SelectvoteVillagersTarget(this);
            if (targetid == "")
                return;
            if (!AccusatedPlayers.Contains(new PlayerRole(targetid)))
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id +"\",\"info\":{\"type\":\"accuse\",\"targetId\":\"" + targetid + "\",\"text\":\"" + Humanizer.CreatevoteVillagersSentence() + "\"}}]", 1000);
            else
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + targetid + "\"}}]", 1000);
            targetid = null;
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteMayor msg)
        {
            CurrentAction = new GameAction(msg.Id, msg.TimeLeft);
            //while (CurrentAction.Timeleft > 10000)
            //    Thread.Sleep(500);
            string target = IAReflection.SelectvoteMayorTarget(this);
            if (target == "")
                return;
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + target + "\"}}]", 1500);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteKick msg)
        {
            //todo add IA target id selection with role reflection
            //TODO GET MESSAGE VALUE
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callMayorKill msg)
        {
            //todo add IA target id selection with role reflection
            //TODO GET MESSAGE VALUE
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callDictatorAsk msg)
        {
            //todo add IA target id selection with role reflection
            //TODO GET MESSAGE VALUE

            //            42["action",{ "id":"2CR7EY9X10","info":{ "putsch":true} }]
            //choix du joueur a tuer:
            //            42["chat",{ "type":"userMessage","text":"Gg soso","userId":"5660014a-a9f0-4cde-b6cd-f05b51312793","channel":"global"}]
            //42["action",{ "id":"PAIBPL0NKO","info":{ "targetId":"bdac5441-0e8d-49eb-a997-dcbdce20632c"} }]

        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteWerewolves msg)
        {
            //todo add IA target id selection with role reflection
            //TODO GET MESSAGE VALUE
            string targetid = IAReflection.SelectvoteWerewolvesTarget(this);
            if (targetid == "")
                return;
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id +"\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + targetid + "\"}}]", 150);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callBlackWolf msg)
        {
            //todo add IA target id selection with role reflection
            //42["action",{"id":"42DFVBNBEJ","info":{"infect":false}}]
        }


        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callWitch msg)
        {
            //todo add IA target id selection with role reflection
            //42["actionRequired",{ "id":"YKUO3JVZ1L","type":"callWitch","healPotionUsed":false,"deathPotionUsed":false,"victimId":"9b8cc59f-6ce3-42ce-84ac-fa5ccbe181e4","timeLeft":30000}]
            //42["action",{ "id":"YKUO3JVZ1L","info":{ "type":"death","targetId":"bd47c671-faaa-49e9-8ceb-bb679883120d"} }] // tuer
            //42["action",{ "id":"PNL92JEYHI","info":{ } }] // Rien faire
            //42["action",{ "id":"YROPQS47MQ","info":{ "type":"heal"} }] // sauver
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callSeer msg)
        {
            //todo add IA target id selection with role reflection
            //["actionRequired",{ "id":"L8OKM5R95G","type":"callSeer","timeLeft":30000,"dayNumber":0}]
            //42["action",{ "id":"L8OKM5R95G","info":{ "targetId":"f2ba4512-7846-40b5-92a6-6cf44fdbe822"} }]
            //42["actionUpdate",{ "id":"L8OKM5R95G","info":{ "targetId":"f2ba4512-7846-40b5-92a6-6cf44fdbe822","role":"necromencer"} }]
            //42["chat",{ "type":"info","channel":"private","recipientId":"5a8fdb16-6cce-4226-b692-e6cac706d889","message":"seer","userId":"f2ba4512-7846-40b5-92a6-6cf44fdbe822","role":"necromencer"}]

        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callGuard msg)
        {
            string targetid = IAReflection.SelectcallGuardTarget(this, msg.LastTargetId);
            if (targetid == "")
                return;
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"targetId\":\"" + targetid + "\"}}]", 150);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callCupid msg)
        {
            //todo add IA target id selection with role reflection
            //42["action",{"id":"UABGY9AJXT","info":{"targetsIds":["1d44d3d6-8f6c-41f1-8982-eabe8ff586f1","4d71b716-7bab-4182-9c47-763d6422190b"]}}]

        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callSickRat msg)
        {
            //todo add IA target id selection with role reflection
            //42["action",{"id":"E34XECGAD0","info":{"targetsIds":["49261948-8521-48ba-a398-633ea9aff77f","a34814b4-09b7-46d4-9ed7-d57278d539c6"]}}] // DEUX JOUEURS CONTAMINES
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callHunter msg)
        {
            string targetid = IAReflection.SelectcallHunterTarget(this);
            if (targetid == "")
                return;
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"targetId\":\"" + targetid + "\"}}]", 150);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.chat.info msg) //talkative wolf only
        {
            //todo add IA target id selection with role reflection

        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callWhiteWolf msg)
        {
            string target = IAReflection.SelectcallWhiteWolfTarget(this);
            if (target == "")
                return;
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"targetId\":\"" + target + "\"}}]", 1500);
        }
    

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callGravedigger msg)
        {
            //todo
        }


        public void UpdateAction(WolfyBot.Core.Packets.Game.NoTypePackets.actionUpdate msg)
        {
            CurrentAction.Timeleft = msg.TimeLeft;
            CurrentAction.Informations = msg.Info;
        }
        public void EndAction(WolfyBot.Core.Packets.Game.NoTypePackets.actionEnd msg)
        {
            CurrentAction = null;
            MayorCandidates.Clear();
            AccusatedPlayers.Clear();
          
        }
        #endregion Actions
        //end action -> MayorCandidates.clear
        public void SetGameRole(string Role)
        {
            CurrentRole = IAHelper.GetRole(Role);
        }
        public void Dispose()
        {
            CurrentRole = null;
            InLoveId = null;
            CurrentAction = null;
            PartyPlayers.Clear();
            AlliesIds.Clear();
            EnemiesIds.Clear();
            SickRatInfected.Clear();
            MayorCandidates.Clear();
            AccusatedPlayers.Clear();
            //todo
        }
    }
}