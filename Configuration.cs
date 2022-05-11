using System.Collections.Generic;
using RFDeathMessage.Enums;
using Rocket.API;
using SDG.Unturned;

namespace RFDeathMessage
{
    public class Configuration : IRocketPluginConfiguration
    {
        public bool Enabled;
        public string MessageColor;
        public string MessageIconUrl;
        public EMode Mode;
        public EDisplay DefaultDisplay;
        public bool EnableLocationMessage;
        public HashSet<EDeathCause> DeathCauses;

        public void LoadDefaults()
        {
            Enabled = true;
            MessageColor = "green";
            MessageIconUrl = "https://cdn.jsdelivr.net/gh/RiceField-Plugins/UnturnedImages@images/plugin/Announcer.png";
            Mode = EMode.DETAIL;
            DefaultDisplay = EDisplay.GLOBAL;
            EnableLocationMessage = true;
            DeathCauses = new HashSet<EDeathCause>
            {
                EDeathCause.ACID,
                EDeathCause.ARENA,
                EDeathCause.ANIMAL,
                EDeathCause.BONES,
                EDeathCause.BREATH,
                EDeathCause.BURNER,
                EDeathCause.BOULDER,
                EDeathCause.BURNING,
                EDeathCause.BLEEDING,
                EDeathCause.CHARGE,
                EDeathCause.FOOD,
                EDeathCause.FREEZING,
                EDeathCause.GUN,
                EDeathCause.GRENADE,
                EDeathCause.INFECTION,
                EDeathCause.KILL,
                EDeathCause.LANDMINE,
                EDeathCause.MELEE,
                EDeathCause.MISSILE,
                EDeathCause.PUNCH,
                EDeathCause.ROADKILL,
                EDeathCause.SPIT,
                EDeathCause.SHRED,
                EDeathCause.SPARK,
                EDeathCause.SENTRY,
                EDeathCause.SPLASH,
                EDeathCause.SUICIDE,
                EDeathCause.VEHICLE,
                EDeathCause.WATER,
                EDeathCause.ZOMBIE,
            };
        }
    }
}