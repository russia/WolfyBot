using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
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
        public List<PlayerRole> SickRatInfected = new List<PlayerRole>();
        public List<PlayerRole> MayorCandidates = new List<PlayerRole>();
        public List<PlayerRole> AccusatedPlayers = new List<PlayerRole>();
        public PlayerRole InLoveId;
        public int CurrentDayCount;
        public bool canProcessAction = false;
        public bool isPlayerAlive = true;

      

        public GameIA(Client client)
        {
            _client = client;
        }

        //todo compter le nombre de roles grace aux morts, et utiliser ces infos
        // il accuse alors que le mec est deja accusé
        //todo check if we are still alive! ! sauf que le hunter est mort quand il joue...
        //TODO CHANGE VOTES : CEST PAS TARGET ID MAIS foreach VOTE.targets

        #region Actions

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteVillagers msg)
        {
            CurrentAction = new GameAction(msg.Id, msg.TimeLeft);
            CurrentAction.ActionTimer.Elapsed += new ElapsedEventHandler(OnTimedGameActionEvent);
            CurrentAction.ActionTimer.Interval = msg.TimeLeft - 5000;
            CurrentAction.ActionTimer.Enabled = true;

            while (!canProcessAction)
                Thread.Sleep(150);

            string targetid = IAReflection.SelectvoteVillagersTarget(this);
            if (targetid == "")
                return;
            bool willvote = (PartyPlayers.Count() <= 6) || (CurrentDayCount > 1);

            if (!AccusatedPlayers.Any(x=> x.Id == targetid) && willvote)
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"accuse\",\"targetId\":\"" + targetid + "\",\"text\":\"" + Humanizer.CreatevoteVillagersSentence() + "\"}}]", 1000);
            else if (AccusatedPlayers.Any(x => x.Id == targetid) && willvote)
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + targetid + "\"}}]", 1000);
            else
                Console.WriteLine("Not voting, CurrentDayCount -> " + CurrentDayCount);
        }

        private void OnTimedGameActionEvent(object source, ElapsedEventArgs e)
        {
            CurrentAction.ActionTimer.Enabled = false;
            canProcessAction = true;
            Console.WriteLine("OnTimedGameActionEvent is called !");
            Console.WriteLine("Players left : " + PartyPlayers.Count());
            foreach (var player in PartyPlayers)
                Console.WriteLine(player.Id);
            Console.WriteLine("Allies left : " + AlliesIds.Count());
            foreach (var player in AlliesIds)
                Console.WriteLine(player.Id);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteMayor msg)
        {
            CurrentAction = new GameAction(msg.Id, msg.TimeLeft);
            CurrentAction.ActionTimer.Elapsed += new ElapsedEventHandler(OnTimedGameActionEvent);
            CurrentAction.ActionTimer.Interval = msg.TimeLeft - 10000;
            CurrentAction.ActionTimer.Enabled = true;

            while (!canProcessAction)
                Thread.Sleep(150);

            string target = IAReflection.SelectvoteMayorTarget(this);
            if (target == "")
                return;
            if (target == _client.Userid)
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"run\",\"text\":\"bon bah..\"}}]");
            else
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + target + "\"}}]");
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.voteKick msg)
        {
            //todo add IA target id selection with role reflection
            //TODO GET MESSAGE VALUE
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callMayorKill msg)
        {
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + msg.TargetsIds.First(x => x != _client.Userid) + "\"}}]", 150);
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
            string targetid = IAReflection.SelectvoteWerewolvesTarget(this);
            if (targetid == "")
                return;
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"vote\",\"targetId\":\"" + targetid + "\"}}]", 150);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callBlackWolf msg)
        {
            bool targetid = IAReflection.ChoicecallBlackWolfTarget(this, msg.VictimId);
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"infect\":" + targetid.ToString().ToLower() + "}}]", 150);
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callWitch msg)
        {
            WitchAction action = IAReflection.WitchChoiceTarget(this, msg);

            if (action.ActionType == WitchAction.type.none)
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{}}]");
            if (action.ActionType == WitchAction.type.death)
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"death\",\"targetId\":\"" + action.TargetId + "\"}}]");
            if (action.ActionType == WitchAction.type.heal)
                _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"type\":\"heal\"}}]");
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
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"targetsIds\":[\"" + _client.Userid + "\",\"" + PartyPlayers.First().Id + "\"]}}]");
        }

        public void ProcessAction(WolfyBot.Core.Packets.Game.actionRequired.callSickRat msg)
        {
            string[] targetids = IAReflection.SelectcallSickRatTargets(this);
            _client.SendMessage("42[\"action\",{\"id\":\"" + msg.Id + "\",\"info\":{\"targetsIds\":[\"" + targetids[0] + "\",\"" + targetids[1] + "\"]}}]", 150);
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
            _client.SendMessage("42[\"chat\",{\"text\":\"" + msg.Word + "\",\"private\":false}]");
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
            _client.SendMessage("42[\"action\",{\"id\":\""  +msg.Id+"\",\"info\":{\"targetId\":\""  +PartyPlayers.First().Id+ "\"}}]", 1500);
        }

        public void UpdateAction(WolfyBot.Core.Packets.Game.NoTypePackets.actionUpdate msg)
        {
            if (CurrentAction == null)
                return;
            CurrentAction.Timeleft = msg.TimeLeft;
            CurrentAction.Informations = msg.Info;
            if (msg.TimeLeft <= 82000)
                CurrentAction.ActionTimer.Interval = 500;
            else
                CurrentAction.ActionTimer.Interval = msg.TimeLeft - 80000;
        }

        public void EndAction(WolfyBot.Core.Packets.Game.NoTypePackets.actionEnd msg)
        {
            CurrentAction = null;
            MayorCandidates.Clear();
            AccusatedPlayers.Clear();
            canProcessAction = false;
        }

        #endregion Actions

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
            //EnemiesIds.Clear();
            SickRatInfected.Clear();
            MayorCandidates.Clear();
            AccusatedPlayers.Clear();
            //todo
        }
    }
}