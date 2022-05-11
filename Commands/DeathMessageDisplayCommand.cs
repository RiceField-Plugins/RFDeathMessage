using System.Collections.Generic;
using RFDeathMessage.Enums;
using RFRocketLibrary.Helpers;
using Rocket.API;
using Rocket.Unturned.Player;

namespace RFDeathMessage.Commands
{
    public class DeathMessageDisplayCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "deathmessagedisplay";
        public string Help => "Sets death message display option";
        public string Syntax => "/dmdisplay <global|group>";
        public List<string> Aliases => new List<string> {"dmdisplay"};
        public List<string> Permissions => new List<string> {"deathmessagedisplay"};

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length != 1)
                goto Invalid;

            switch (command[0].ToLower())
            {
                case "global":
                    var component = ((UnturnedPlayer) caller).GetComponent<PlayerComponent>();
                    component.DisplayMode = EDisplay.GLOBAL;
                    ChatHelper.Say(caller, Plugin.TranslateRich(EResponse.SET_DISPLAY, component.DisplayMode),
                        Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                    return;
                case "group":
                    component = ((UnturnedPlayer) caller).GetComponent<PlayerComponent>();
                    component.DisplayMode = EDisplay.GROUP;
                    ChatHelper.Say(caller, Plugin.TranslateRich(EResponse.SET_DISPLAY, component.DisplayMode),
                        Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
                    return;
                default:
                    goto Invalid;
            }

            Invalid:
            ChatHelper.Say(caller, Plugin.TranslateRich(EResponse.INVALID_PARAMETER, Syntax),
                Plugin.MsgColor, Plugin.Conf.MessageIconUrl);
        }
    }
}