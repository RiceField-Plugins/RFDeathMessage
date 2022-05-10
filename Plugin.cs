using RFDeathMessage.Enums;
using RFDeathMessage.EventListeners;
using Rocket.API.Collections;
using Rocket.API.Extensions;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace RFDeathMessage
{
    public class Plugin : RocketPlugin<Configuration>
    {
        private static int Major = 1;
        private static int Minor = 1;
        private static int Patch = 1;
        
        public static Plugin Inst;
        public static Configuration Conf;
        internal static Color MsgColor;

        protected override void Load()
        {
            Inst = this;
            Conf = Configuration.Instance;
            if (Conf.Enabled)
            {
                MsgColor = UnturnedChat.GetColorFromName(Conf.MessageColor, Color.green);

                PlayerLife.onPlayerDied += PlayerEvent.OnDied;
                
                if (Level.isLoaded)
                    foreach (var steamPlayer in Provider.clients)
                        steamPlayer.player.gameObject.TryAddComponent<PlayerComponent>()?.LoadInternal();
            }
            else
                Logger.LogWarning($"[{Name}] Plugin: DISABLED");

            Logger.LogWarning($"[{Name}] Plugin loaded successfully!");
            Logger.LogWarning($"[{Name}] {Name} v{Major}.{Minor}.{Patch}");
            Logger.LogWarning($"[{Name}] Made with 'rice' by RiceField Plugins!");
        }
        
        protected override void Unload()
        {
            if (Conf.Enabled)
            {
                StopAllCoroutines();
                
                PlayerLife.onPlayerDied -= PlayerEvent.OnDied;
            }
            
            Conf = null;
            Inst = null;

            Logger.LogWarning($"[{Name}] Plugin unloaded successfully!");
        }
        
        public override TranslationList DefaultTranslations => new TranslationList
        {
            {$"{EResponse.DEATH_CAUSE_ACID_DETAIL}", "{0} was -=color=red=-poisoned-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_ANIMAL_DETAIL}", "{0} was attacked by -=color=red=-animal-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_ARENA_DETAIL}", "{0} was eliminated by the -=color=red=-arena-=/color=-!"},
            {$"{EResponse.DEATH_CAUSE_BLEEDING_DETAIL}", "{0} -=color=red=-bled-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_BOULDER_DETAIL}", "{0} was -=color=red=-crushed-=/color=- by zombie!"},
            {$"{EResponse.DEATH_CAUSE_BREATH_DETAIL}", "{0} -=color=red=-suffocated-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_BURNER_DETAIL}", "{0} was -=color=red=-burned-=/color=- by zombie!"},
            {$"{EResponse.DEATH_CAUSE_BURNING_DETAIL}", "{0} -=color=red=-burned-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_BONES_DETAIL}", "{0} -=color=red=-fractured-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_CHARGE_DETAIL}", "{0} was blown up by {1} [HP: {2}] with a -=color=red=-remote detonator-=/color=-! [{3}m away]"},
            {$"{EResponse.DEATH_CAUSE_FOOD_DETAIL}", "{0} -=color=red=-starved-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_FREEZING_DETAIL}", "{0} -=color=red=-froze-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_GRENADE_DETAIL}", "{0} was blown up by {1} [HP: {2}] with a -=color=red=-grenade-=/color=-! [{3}m away]"},
            {$"{EResponse.DEATH_CAUSE_GUN_DETAIL}", "{0} [HP: {1}] -=color=red=-shot-=/color=- and killed {2} in the {3} with {4}! [{5}m away]"},
            {$"{EResponse.DEATH_CAUSE_INFECTION_DETAIL}", "{0} was -=color=red=-infected-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_LANDMINE_DETAIL}", "{0} was blown up by -=color=red=-landmine-=/color=-!"},
            {$"{EResponse.DEATH_CAUSE_KILL_DETAIL}", "{0} was killed by -=color=red=-admin-=/color=-."},
            {$"{EResponse.DEATH_CAUSE_MELEE_DETAIL}", "{0} [HP: {1}] -=color=red=-chopped-=/color=- and killed {2} in the {3} with {4}!"},
            {$"{EResponse.DEATH_CAUSE_MISSILE_DETAIL}", "{0} was blown up by {1} [HP: {2}] with a -=color=red=-missile-=/color=-! [{3}m away]"},
            {$"{EResponse.DEATH_CAUSE_PUNCH_DETAIL}", "{0} [HP: {1}] -=color=red=-punched-=/color=- and killed {2} in the {3}!"},
            {$"{EResponse.DEATH_CAUSE_ROADKILL_DETAIL}", "{0} was -=color=red=-roadkilled-=/color=- by {1} [HP: {2}]!"},
            {$"{EResponse.DEATH_CAUSE_SENTRY_DETAIL}", "{0} was shot by -=color=red=-sentry gun-=/color=-!"},
            {$"{EResponse.DEATH_CAUSE_SHRED_DETAIL}", "{0} was -=color=red=-shredded-=/color=- to bits!"},
            {$"{EResponse.DEATH_CAUSE_SPARK_DETAIL}", "{0} was -=color=red=-electrocuted-=/color=- by zombie!"},
            {$"{EResponse.DEATH_CAUSE_SPIT_DETAIL}", "{0} was -=color=red=-dissolved-=/color=- by zombie!"},
            {$"{EResponse.DEATH_CAUSE_SPLASH_DETAIL}", "{0} was blown up by {1} [HP: {2}] with an -=color=red=-explosive bullet-=/color=-! [{3}m away]"},
            {$"{EResponse.DEATH_CAUSE_SUICIDE_DETAIL}", "{0} committed -=color=red=-suicide-=/color=-. Everyone is disappointed."},
            {$"{EResponse.DEATH_CAUSE_VEHICLE_DETAIL}", "{0} was blown up by -=color=red=-vehicle-=/color=-!"},
            {$"{EResponse.DEATH_CAUSE_WATER_DETAIL}", "{0} -=color=red=-dehydrated-=/color=- to death!"},
            {$"{EResponse.DEATH_CAUSE_ZOMBIE_DETAIL}", "{0} was mauled by -=color=red=-zombie-=/color=-!"},
            
            {$"{EResponse.DEATH_CAUSE_ACID_SIMPLE}", "[ACID] {0}"},
            {$"{EResponse.DEATH_CAUSE_ANIMAL_SIMPLE}", "[ANIMAL] {0}"},
            {$"{EResponse.DEATH_CAUSE_ARENA_SIMPLE}", "[ARENA] {0}"},
            {$"{EResponse.DEATH_CAUSE_BLEEDING_SIMPLE}", "[BLEEDING] {0}"},
            {$"{EResponse.DEATH_CAUSE_BOULDER_SIMPLE}", "[BOULDER] {0}"},
            {$"{EResponse.DEATH_CAUSE_BREATH_SIMPLE}", "[BREATH] {0}"},
            {$"{EResponse.DEATH_CAUSE_BURNER_SIMPLE}", "[BURNER] {0}"},
            {$"{EResponse.DEATH_CAUSE_BURNING_SIMPLE}", "[BURNING] {0}"},
            {$"{EResponse.DEATH_CAUSE_BONES_SIMPLE}", "[BONES] {0}"},
            {$"{EResponse.DEATH_CAUSE_CHARGE_SIMPLE}", "{0} [CHARGE] {1}"},
            {$"{EResponse.DEATH_CAUSE_FOOD_SIMPLE}", "[FOOD] {0}"},
            {$"{EResponse.DEATH_CAUSE_FREEZING_SIMPLE}", "[FREEZING] {0}"},
            {$"{EResponse.DEATH_CAUSE_GRENADE_SIMPLE}", "{0} [GRENADE] {1}"},
            {$"{EResponse.DEATH_CAUSE_GUN_SIMPLE}", "{0} [GUN] [{1}] [{2}] {3}"},
            {$"{EResponse.DEATH_CAUSE_INFECTION_SIMPLE}", "[INFECTION] {0}"},
            {$"{EResponse.DEATH_CAUSE_LANDMINE_SIMPLE}", "[LANDMINE] {0}"},
            {$"{EResponse.DEATH_CAUSE_KILL_SIMPLE}", "[ADMIN KILL] {0}"},
            {$"{EResponse.DEATH_CAUSE_MELEE_SIMPLE}", "{0} [MELEE] [{1}] [{2}] {3}"},
            {$"{EResponse.DEATH_CAUSE_MISSILE_SIMPLE}", "{0} [MISSILE] {1}"},
            {$"{EResponse.DEATH_CAUSE_PUNCH_SIMPLE}", "{0} [PUNCH] [{1}] {2}"},
            {$"{EResponse.DEATH_CAUSE_ROADKILL_SIMPLE}", "{0} [ROADKILL] {1}"},
            {$"{EResponse.DEATH_CAUSE_SENTRY_SIMPLE}", "[SENTRY] {0}"},
            {$"{EResponse.DEATH_CAUSE_SHRED_SIMPLE}", "[SHRED] {0}"},
            {$"{EResponse.DEATH_CAUSE_SPARK_SIMPLE}", "[SPARK] {0}"},
            {$"{EResponse.DEATH_CAUSE_SPIT_SIMPLE}", "[SPIT] {0}"},
            {$"{EResponse.DEATH_CAUSE_SPLASH_SIMPLE}", "[SPLASH] {0}"},
            {$"{EResponse.DEATH_CAUSE_SUICIDE_SIMPLE}", "[SPLASH] {0}"},
            {$"{EResponse.DEATH_CAUSE_VEHICLE_SIMPLE}", "[VEHICLE] {0}"},
            {$"{EResponse.DEATH_CAUSE_WATER_SIMPLE}", "[WATER] {0}"},
            {$"{EResponse.DEATH_CAUSE_ZOMBIE_SIMPLE}", "[ZOMBIE] {0}"},
            
            {$"{EResponse.DEATH_LOCATION}", "{0} died near {1}!"},
            {$"{EResponse.INVALID_PARAMETER}", "Invalid parameter! Usage: {0}"},
            {$"{EResponse.SET_DISPLAY}", "Successfully set death message display mode to {0}!"},
        };

        internal static string TranslateRich(object s, params object[] objects)
        {
            return Inst.Translate(s.ToString(), objects).Replace("-=", "<").Replace("=-", ">");
        }
    }
}