using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using RFDeathMessage.Enums;
using RFRocketLibrary.Helpers;
using SDG.Unturned;
using UnityEngine;

namespace RFDeathMessage.Utils
{
    internal static class DeathUtil
    {
        internal static string GetDistance(Component victim, Component killer)
        {
            return Math.Round(Vector3.Distance(victim.transform.position,
                killer.transform.position)).ToString(CultureInfo.InvariantCulture);
        }

        internal static string GetLimb(ELimb limb)
        {
            switch (Plugin.Conf.Mode)
            {
                case EMode.DETAIL:
                    return limb.ToString().Replace('_', ' ').ToLower();
                case EMode.SIMPLE:
                    return limb.ToString().Replace('_', ' ').ToUpper();
                default:
                    return string.Empty;
            }
        }

        internal static string GetLocation(Component victim)
        {
            var node = LevelNodes.nodes.OfType<LocationNode>()
                .OrderBy(k => Vector3.Distance(k.point, victim.transform.position)).FirstOrDefault();
            return node?.name ?? "Unknown";
        }

        internal static string TranslateRich(string s, params object[] objects)
        {
            return Plugin.Inst.Translate(s, objects).Replace("-=", "<").Replace("=-", ">");
        }

        internal static void SendDeathMessage(string deathMessage, Player victim, object killer = null)
        {
            IEnumerator DeathMessageEnumerator()
            {
                foreach (var steamPlayer in Provider.clients)
                {
                    var receiver = steamPlayer.player.GetComponent<PlayerComponent>();
                    switch (receiver.DisplayMode)
                    {
                        case EDisplay.GLOBAL:
                            ChatHelper.Say(receiver.Player, deathMessage, Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                        case EDisplay.GROUP:
                            if ((victim.quests.groupID.m_SteamID != 0 && victim.quests.groupID.m_SteamID == receiver.Player.Player.quests.groupID.m_SteamID) || 
                                (killer is Player killerPlayer && killerPlayer.quests.groupID.m_SteamID == receiver.Player.Player.quests.groupID.m_SteamID))
                                ChatHelper.Say(receiver.Player, deathMessage, Plugin.MsgColor,
                                    Plugin.Conf.MessageIconUrl);

                            break;
                    }

                    yield return null;
                }
            }

            Plugin.Inst.StartCoroutine(DeathMessageEnumerator());
        }
    }
}