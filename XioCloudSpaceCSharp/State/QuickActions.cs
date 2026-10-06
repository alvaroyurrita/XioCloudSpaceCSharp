using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public readonly string[] QuickActionNames =
        {
            "",
            "My Quick Action 1",
            "My Quick Action 2",
            "My Quick Action 3",
            "My Quick Action 4",
            "My Quick Action 5",
            "My Quick Action 6"
        };
        
        public void TriggerQuickAction(ushort index)
        {
            var quickActions = new Action<bool>[]
            {
                _xioCloudRoomGui.Main.QuickActions.QuickActions1_Selected,
                _xioCloudRoomGui.Main.QuickActions.QuickActions2_Selected,
                _xioCloudRoomGui.Main.QuickActions.QuickActions3_Selected,
                _xioCloudRoomGui.Main.QuickActions.QuickActions4_Selected,
                _xioCloudRoomGui.Main.QuickActions.QuickActions5_Selected,
                _xioCloudRoomGui.Main.QuickActions.QuickActions6_Selected
            };
            _logger.Debug("Triggering Quick Action. Index: {Index}, Name: {Name}", index, QuickActionNames[index]);
            if (index < 1 || index > 6)
            {
                _logger.Warning("Invalid quick action index: {Index}. Valid range is 1 to 6", index);
                return;
            }
            quickActions[index - 1](true);
            Task.Run(async () =>
                {
                    await Task.Delay(5000);
                     quickActions[index - 1](false);
                }
            );
        }
    }
}