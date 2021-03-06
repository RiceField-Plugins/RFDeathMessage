using RFDeathMessage.Enums;
using Rocket.Unturned.Player;

namespace RFDeathMessage
{
    public class PlayerComponent : UnturnedPlayerComponent
    {
        internal EDisplay DisplayMode { get; set; }
        protected override void Load()
        {
            LoadInternal();
        }
        internal void LoadInternal()
        {
            DisplayMode = Plugin.Conf.DefaultDisplay;
        }

        protected override void Unload()
        {
            UnloadInternal();
        }
        internal void UnloadInternal()
        {
        }
    }
}