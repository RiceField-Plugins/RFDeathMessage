using RFDeathMessage.Enums;
using RFDeathMessage.Utils;
using RFRocketLibrary.Helpers;
using SDG.Unturned;
using Steamworks;

namespace RFDeathMessage.EventListeners
{
    internal static class PlayerEvent
    {
        internal static void OnDied(PlayerLife sender, EDeathCause cause, ELimb limb, CSteamID instigator)
        {
            if (Plugin.Conf.EnableLocationMessage)
                ChatHelper.Broadcast(
                    DeathUtil.TranslateRich("DEATH_LOCATION", sender.channel.owner.playerID.characterName,
                        DeathUtil.GetLocation(sender)), Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
            
            if (!Plugin.Conf.DeathCauses.Contains(cause))
                return;

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
                    ChatHelper.Broadcast(
                        DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                            sender.channel.owner.playerID.characterName),
                        Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                    break;
                case EDeathCause.CHARGE:
                case EDeathCause.GRENADE:
                case EDeathCause.MISSILE:
                    var killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    sender.channel.owner.playerID.characterName,
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    DeathUtil.GetDistance(sender, killer)),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                        case EMode.SIMPLE:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    sender.channel.owner.playerID.characterName,
                                    killer.channel.owner.playerID.characterName),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                    }

                    break;
                case EDeathCause.GUN:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    sender.channel.owner.playerID.characterName,
                                    DeathUtil.GetLimb(limb), killer.equipment.asset.itemName,
                                    DeathUtil.GetDistance(sender, killer)),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                        case EMode.SIMPLE:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.equipment.asset.itemName,
                                    DeathUtil.GetLimb(limb), sender.channel.owner.playerID.characterName),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                    }

                    break;
                case EDeathCause.MELEE:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    sender.channel.owner.playerID.characterName,
                                    DeathUtil.GetLimb(limb), killer.equipment.asset.itemName),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                        case EMode.SIMPLE:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.equipment.asset.itemName,
                                    DeathUtil.GetLimb(limb), sender.channel.owner.playerID.characterName),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                    }

                    break;
                case EDeathCause.PUNCH:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    sender.channel.owner.playerID.characterName,
                                    DeathUtil.GetLimb(limb)),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                        case EMode.SIMPLE:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName, DeathUtil.GetLimb(limb),
                                    sender.channel.owner.playerID.characterName),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                    }

                    break;
                case EDeathCause.ROADKILL:
                    killer = PlayerTool.getPlayer(instigator);
                    switch (Plugin.Conf.Mode)
                    {
                        case EMode.DETAIL:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    sender.channel.owner.playerID.characterName,
                                    killer.channel.owner.playerID.characterName, killer.life.health,
                                    DeathUtil.GetLimb(limb)),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                        case EMode.SIMPLE:
                            ChatHelper.Broadcast(
                                DeathUtil.TranslateRich($"DEATH_CAUSE_{cause}_{Plugin.Conf.Mode}",
                                    killer.channel.owner.playerID.characterName,
                                    sender.channel.owner.playerID.characterName),
                                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                            break;
                    }

                    break;
            }
        }
    }
}