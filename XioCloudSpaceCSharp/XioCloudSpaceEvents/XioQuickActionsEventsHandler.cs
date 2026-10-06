using Crestron.SimplSharpPro;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {

        private void XioQuickActionsEventsHandler(int eventId, ushort index)
        {
            switch (eventId)
            {
                case XioCloudSpaceEventIds.QuickActionEventId:
                    _logger.Debug("Quick action event received. Index: {Index}", index);
                    TriggerQuickAction(index);
                    break;
            }
            
        }

    }
}