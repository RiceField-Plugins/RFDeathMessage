using System;
using System.Globalization;
using System.Linq;
using RFDeathMessage.Enums;
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
    }
}