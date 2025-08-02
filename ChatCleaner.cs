using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.UserMessages;

namespace ChatCleaner
{
    public class ChatCleaner : BasePlugin
    {
        public override string ModuleName => "ChatCleaner";
        public override string ModuleVersion => "0.0.2";
        public override string ModuleAuthor => "unfortunate";

        private static readonly string[] IgnoredMessages =
        {
            "#Player_Cash_Award_Bomb_Planted",
            "#Player_Cash_Award_Bomb_Defused",
            "#Player_Cash_Award_Killed_VIP",
            "#Player_Cash_Award_Killed_Enemy",
            "#Player_Cash_Award_Killed_Enemy_Generic",
            "#Player_Cash_Award_Rescued_Hostage",
            "#Player_Cash_Award_Interact_Hostage",
            "#Player_Cash_Award_Respawn",
            "#Player_Cash_Award_Get_Killed",
            "#Player_Cash_Award_Kill_Teammate",
            "#Player_Cash_Award_Damage_Hostage",
            "#Player_Cash_Award_Kill_Hostage",
            "#Player_Point_Award_Killed_Enemy",
            "#Player_Point_Award_Killed_Enemy_Plural",
            "#Player_Point_Award_Killed_Enemy_NoWeapon",
            "#Player_Point_Award_Killed_Enemy_NoWeapon_Plural",
            "#Player_Point_Award_Assist_Enemy",
            "#Player_Point_Award_Assist_Enemy_Plural",
            "#Player_Point_Award_Picked_Up_Dogtag",
            "#Player_Point_Award_Picked_Up_Dogtag_Plural",
            "#Player_Team_Award_Killed_Enemy",
            "#Player_Team_Award_Killed_Enemy_Plural",
            "#Player_Team_Award_Bonus_Weapon",
            "#Player_Team_Award_Bonus_Weapon_Plural",
            "#Player_Team_Award_Picked_Up_Dogtag",
            "#Player_Team_Award_Picked_Up_Dogtag_Plural",
            "#Player_Team_Award_Picked_Up_Dogtag_Friendly",
            "#Player_Cash_Award_ExplainSuicide_YouGotCash",
            "#Player_Cash_Award_ExplainSuicide_TeammateGotCash",
            "#Player_Cash_Award_ExplainSuicide_EnemyGotCash",
            "#Player_Cash_Award_ExplainSuicide_Spectators",
            "#Team_Cash_Award_T_Win_Bomb",
            "#Team_Cash_Award_Elim_Hostage",
            "#Team_Cash_Award_Elim_Bomb",
            "#Team_Cash_Award_Win_Time",
            "#Team_Cash_Award_Win_Defuse_Bomb",
            "#Team_Cash_Award_Win_Hostages_Rescue",
            "#Team_Cash_Award_Win_Hostage_Rescue",
            "#Team_Cash_Award_Loser_Bonus",
            "#Team_Cash_Award_Bonus_Shorthanded",
            "#Team_Cash_Award_Loser_Bonus_Neg",
            "#Team_Cash_Award_Loser_Zero",
            "#Team_Cash_Award_Rescued_Hostage",
            "#Team_Cash_Award_Hostage_Interaction",
            "#Team_Cash_Award_Hostage_Alive",
            "#Team_Cash_Award_Planted_Bomb_But_Defused",
            "#Team_Cash_Award_Survive_GuardianMode_Wave",
            "#Team_Cash_Award_CT_VIP_Escaped",
            "#Team_Cash_Award_T_VIP_Killed",
            "#Team_Cash_Award_No_Income",
            "#Team_Cash_Award_no_income_suicide",
            "#Team_Cash_Award_Generic",
            "#Team_Cash_Award_Custom",
            "#Team_Cash_Award_Dead_Terrorists",
            "#Cstrike_TitlesTXT_Game_teammate_attack",
        };

        public override void Load(bool hotReload)
        {
            HookUserMessage(124, OnMessagePrint, HookMode.Pre);
            HookUserMessage(323, OnHudMessage, HookMode.Pre);
        }

        private HookResult OnHudMessage(UserMessage message)
        {
            string msg = message.ReadString("message");

            if (msg.StartsWith("#"))
                if (IgnoredMessages.Contains(msg))
                    return HookResult.Stop;

            return HookResult.Continue;
        }

        private HookResult OnMessagePrint(UserMessage message)
        {
            int count = message.GetRepeatedFieldCount("param");

            for (int i = 0; i < count; i++)
            {
                string msg = message.ReadString("param", i);

                if (msg.StartsWith("#"))
                    if (IgnoredMessages.Contains(msg))
                        return HookResult.Stop;
            }

            return HookResult.Continue;
        }

        [GameEventHandler(HookMode.Pre)]
        public HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
        {
            info.DontBroadcast = true;
            return HookResult.Continue;
        }

        [GameEventHandler(HookMode.Pre)]
        public HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
        {
            @event.Silent = true;
            info.DontBroadcast = true;
            return HookResult.Continue;
        }
    }
}
