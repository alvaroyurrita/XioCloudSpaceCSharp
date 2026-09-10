using Crestron.SimplSharpPro;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {

        private void XioSchedulingEventsHandler(int index)
        {
            var schedulingUi = _xioCloudRoomGui.Main.Scheduling;
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
                    schedulingUi.CurrentReservationText_Indirect(ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStartTime.StringValue);
                    break;
                case XioCloudSpaceEventIds.CurrentMeetingStopTimeEventId:
                    schedulingUi.CurrentReservationText_Indirect(ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStopTime.StringValue);
                    break;
                case XioCloudSpaceEventIds.NextMeetingStartTimeEventId:
                    schedulingUi.CurrentReservationText_Indirect(ControllerXioCloudSpaceSlotDevice.Scheduling.NextMeetingStartTime.StringValue);
                    break;
            }
        }
    }
}