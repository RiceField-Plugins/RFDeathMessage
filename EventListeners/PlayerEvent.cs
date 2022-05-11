using RFDeathMessage.Enums;
using RFDeathMessage.Utils;
using SDG.Unturned;
using Steamworks;

namespace RFDeathMessage.EventListeners
{
    internal static class PlayerEvent
    {
        internal static void OnDied(PlayerLife sender, EDeathCause cause, ELimb limb, CSteamID instigator)
        {
            var deathmessage = string.Empty;

            if (!Plugin.Conf.DeathCauses.Contains(cause))
            {
                if (Plugin.Conf.EnableLocationMessage)
                {
                    deathmessage += Plugin.TranslateRich("DEATH_LOCATION", sender.channel.owner.playerID.characterName,
                        DeathUtil.GetLocation(sender));
                    DeathUtil.SendDeathMessage(deathmessage, sender.player);
                }
                return;
            }

            var killer = PlayerTool.getPlayer(instigator);
            switch (cause)
            {
                case EDeathCause.ACID:
                case EDeathCause.ANIMAL:
                case EDeathCause.ARENA:
                case EDeathCause.BLEEDING:
                case EDeathCause.BONES:
                case EDeathCause.BOULDER:
                case EDeathCause.BREATH:
                case EDeathCause.BURNER:
                case EDeathCause.BURNING:
                case EDeathCause.FOOD:
                case EDeathCause.FREEZING:
                case EDeathCause.INFECTION:
                case EDeathCause.KILL:
                case EDeathCause.LANDMINE:
                case EDeathCause.SENTRY:
                case EDeathCause.SHRED:
                case EDeathCause.SPARK:
                case EDeathCause.SPIT:
                case EDeathCause.SPLASH:
                case EDeathCause.SUICIDE:
                case EDeathCause.VEHICLE:
                case EDeathCause.WATER:
                case EDeathCause.ZOMBIE:
                    deathmessage +=
                        Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                            sender.channel.owner.playerID.characterName);
                    break;
                case EDeathCause.CHARGE:
                case EDeathCause.GRENADE:
                case EDeathCause.MISSILE:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    sender.channel.owner.playerID.characterName,
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    DeathUtil.GetDistance(sender, killer));
                            break;
                        case EMode.SIMPLE:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    sender.channel.owner.playerID.characterName,
                                    killer.channel.owner.playerID.characterName);
                            break;
                    }

                    break;
                case EDeathCause.GUN:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    sender.channel.owner.playerID.characterName,
                                    DeathUtil.GetLimb(limb), killer.equipment.asset.itemName,
                                    DeathUtil.GetDistance(sender, killer));
                            break;
                        case EMode.SIMPLE:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.equipment.asset.itemName,
                                    DeathUtil.GetLimb(limb), sender.channel.owner.playerID.characterName);
                            break;
                    }

                    break;
                case EDeathCause.MELEE:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    sender.channel.owner.playerID.characterName,
                                    DeathUtil.GetLimb(limb), killer.equipment.asset.itemName);
                            break;
                        case EMode.SIMPLE:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.equipment.asset.itemName,
                                    DeathUtil.GetLimb(limb), sender.channel.owner.playerID.characterName);
                            break;
                    }

                    break;
                case EDeathCause.PUNCH:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    sender.channel.owner.playerID.characterName,
                                    DeathUtil.GetLimb(limb));
                            break;
                        case EMode.SIMPLE:
                            deathmessage +=
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, DeathUtil.GetLimb(limb),
                                    sender.channel.owner.playerID.characterName);
                            break;
                    }

                    break;
                case EDeathCause.ROADKILL:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    sender.channel.owner.playerID.characterName,
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    DeathUtil.GetLimb(limb));
                            break;
                        case EMode.SIMPLE:
                            deathmessage +=
                                Plugin.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName,
                                    sender.channel.owner.playerID.characterName);
                            break;
                    }

                    break;
            }

            if (Plugin.Conf.EnableLocationMessage)
                deathmessage += " " + Plugin.TranslateRich("DEATH_LOCATION", sender.channel.owner.playerID.characterName,
                    DeathUtil.GetLocation(sender));

            DeathUtil.SendDeathMessage(deathmessage, sender.player, killer);
        }
    }
}