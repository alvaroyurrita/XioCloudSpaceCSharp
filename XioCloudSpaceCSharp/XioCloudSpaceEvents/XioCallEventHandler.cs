using Crestron.SimplSharpPro;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private void XioCallEventHandler(int eventId)
        {
            switch (eventId)
            {
                case XioCloudSpaceEventIds.InCallEventId:
                    _logger.Debug("In call event received");
                    IsInCall = true;
                    break;
                case XioCloudSpaceEventIds.NotInCallEventId:
                    _logger.Debug("Not in call event received");
                    IsInCall = false;
                    break;
            }
        }
    }
}