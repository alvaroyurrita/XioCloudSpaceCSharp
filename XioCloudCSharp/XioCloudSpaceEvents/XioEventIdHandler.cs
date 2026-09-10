using Crestron.SimplSharpPro;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private void XioEvenIdHandler(GenericBase device, BaseEventArgs args)
        {
            _logger.Debug(
                "Receive Event from XIO Cloud: Event Id [{EventID}]. Event Index: [{EventIndex}]",
                GetEventName(args.EventId),
                args.Index
            );
            // Handle all events which come in from XiO
            switch (args.EventId)
            {
                case XioCloudSpaceEventIds.SpaceCapacityEventId:
                case XioCloudSpaceEventIds.SpaceNameEventId:
                case XioCloudSpaceEventIds.SpaceIDEventId:
                case XioCloudSpaceEventIds.SpaceTypeEventId:
                case XioCloudSpaceEventIds.SpaceDealerContactInfoEventId:
                case XioCloudSpaceEventIds.SpaceTimeZoneEventId:
                    XioMetaDataEventsHandler(args.EventId);
                    break;
                case XioCloudSpaceEventIds.SystemPowerOnEventId:
                case XioCloudSpaceEventIds.SystemPowerOffEventId:
                case XioCloudSpaceEventIds.DisplayPowerOnEventId:
                case XioCloudSpaceEventIds.DisplayPowerOffEventId:
                case XioCloudSpaceEventIds.OccupiedEventId:
                case XioCloudSpaceEventIds.VacantEventId:
                case XioCloudSpaceEventIds.SystemCheckEventId:
                case XioCloudSpaceEventIds.MaintenanceModeEventId:
                case XioCloudSpaceEventIds.PeopleCountEventId:
                    XioSystemEventsHandler(args.EventId);
                    break; 
                case XioCloudSpaceEventIds.VolumeLevelEventId:
                case XioCloudSpaceEventIds.VolumeMuteOnEventId:
                case XioCloudSpaceEventIds.VolumeMuteOffEventId:
                case XioCloudSpaceEventIds.MicrophonePrivacyOnEventId:
                case XioCloudSpaceEventIds.MicrophonePrivacyOffEventId:
                    XioVolumeEventsHandler(args.EventId); 
                    break;
                case XioCloudSpaceEventIds.InCallEventId:
                case XioCloudSpaceEventIds.NotInCallEventId:
                    XioCallEventHandler(args.EventId);
                    break;
                case XioCloudSpaceEventIds.InScheduledEventEventId:
                case XioCloudSpaceEventIds.NotInScheduledEventEventId:
                case XioCloudSpaceEventIds.CheckedInEventId:
                case XioCloudSpaceEventIds.NotCheckedInEventId:
                case XioCloudSpaceEventIds.DoNotDisturbOnEventId:
                case XioCloudSpaceEventIds.DoNotDisturbOffEventId:
                case XioCloudSpaceEventIds.CurrentMeetingStartTimeEventId:
                case XioCloudSpaceEventIds.CurrentMeetingStopTimeEventId:
                case XioCloudSpaceEventIds.NextMeetingStartTimeEventId:
                    XioSchedulingEventsHandler(args.EventId);
                    break;
            }
        }
    }
}