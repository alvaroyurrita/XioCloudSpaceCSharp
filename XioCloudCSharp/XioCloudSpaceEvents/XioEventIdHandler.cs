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
                    HandleMetaDataEvents(args.EventId);
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
                    HandleSystemEvents(args.EventId);
                    break;
            }
        }
    }
}