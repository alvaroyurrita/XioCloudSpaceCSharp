using Crestron.SimplSharpPro;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private void HandleSystemEvents(int eventId)
        {
            var uiMain = _xioCloudRoomGui.Main;
            var xioSystem = ControllerXioCloudSpaceSlotDevice;
            switch (eventId)
            {
                case XioCloudSpaceEventIds.SystemPowerOnEventId:
                    break;
                case XioCloudSpaceEventIds.SystemPowerOffEventId:
                    break;
                case XioCloudSpaceEventIds.DisplayPowerOnEventId:
                    break;
                case XioCloudSpaceEventIds.DisplayPowerOffEventId:
                    break;
                case XioCloudSpaceEventIds.OccupiedEventId:
                    break;
                case XioCloudSpaceEventIds.VacantEventId:
                    break;
                case XioCloudSpaceEventIds.SystemCheckEventId:
                    SetUiSystemCheckButtonsEnabled(true);
                    break;
                case XioCloudSpaceEventIds.MaintenanceModeEventId:
                    break;
                case XioCloudSpaceEventIds.PeopleCountEventId:
                    break;
            }
        }
    }
}