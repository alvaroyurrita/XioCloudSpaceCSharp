using Crestron.SimplSharpPro;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {

        private void XioSchedulingEventsHandler(int index)
        {
            var uiScheduling = _xioCloudRoomGui.Main.Scheduling;
            // Handle metadata changes from XiO Cloud Space slot
            switch (index)
            {
                case XioCloudSpaceEventIds.InScheduledEventEventId:
                    IsInScheduledEvent = true;
                    break;
                case XioCloudSpaceEventIds.NotInScheduledEventEventId:
                    IsInScheduledEvent = false;
                    break;
                case XioCloudSpaceEventIds.CheckedInEventId:
                    IsCheckedIn = true;
                    break;
                case XioCloudSpaceEventIds.NotCheckedInEventId:
                    IsCheckedIn = false;
                    break;
                case XioCloudSpaceEventIds.DoNotDisturbOnEventId:
                    IsOnDoNotDisturb = true;
                    break;
                case XioCloudSpaceEventIds.DoNotDisturbOffEventId:
                    IsOnDoNotDisturb = false;
                    break;
                case XioCloudSpaceEventIds.CurrentMeetingStartTimeEventId:
                    break;
                case XioCloudSpaceEventIds.CurrentMeetingStopTimeEventId:
                    break;
                case XioCloudSpaceEventIds.NextMeetingStartTimeEventId:
                    break;
            }
        }
    }
}