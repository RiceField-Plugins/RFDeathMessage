using System.Threading.Tasks;
using RFDeathMessage.Enums;
using Rocket.API;
using RocketExtensions.Models;
using RocketExtensions.Plugins;

namespace RFDeathMessage.Commands
{
    [CommandActor(AllowedCaller.Player)]
    [CommandAliases("dmdisplay")]
    [CommandInfo("Sets death message display option", "/dmdisplay <global|group>")]
    [CommandPermissions("deathmessagedisplay")]
    public class DeathMessageDisplayCommand : RocketCommand
    {
        public override async Task Execute(CommandContext context)
        {
            if (context.CommandRawArguments.Length == 1)
                goto Invalid;

            switch (context.CommandRawArguments[0].ToLower())
            {
                case "global":
                    var component = context.UnturnedPlayer.GetComponent<PlayerComponent>();
                    component.DisplayMode = EDisplay.GLOBAL;
                    await context.ReplyAsync(
                        RFDeathMessage.Plugin.TranslateRich(EResponse.SET_DISPLAY, component.DisplayMode),
                        RFDeathMessage.Plugin.MsgColor, RFDeathMessage.Plugin.Conf.MessageIconUrl);
                    return;
                case "group":
                    component = context.UnturnedPlayer.GetComponent<PlayerComponent>();
                    component.DisplayMode = EDisplay.GROUP;
                    await context.ReplyAsync(
                        RFDeathMessage.Plugin.TranslateRich(EResponse.SET_DISPLAY, component.DisplayMode),
                        RFDeathMessage.Plugin.MsgColor, RFDeathMessage.Plugin.Conf.MessageIconUrl);
                    return;
                default:
                    goto Invalid;
            }

            Invalid:
            await context.ReplyAsync(RFDeathMessage.Plugin.TranslateRich(EResponse.INVALID_PARAMETER),
                RFDeathMessage.Plugin.MsgColor, RFDeathMessage.Plugin.Conf.MessageIconUrl);
        }
    }
}