using Crestron.SimplSharpPro;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private void XioSystemEventsHandler(int eventId)
        {
            var xioSystem = ControllerXioCloudSpaceSlotDevice;
            switch (eventId)
            {
                case XioCloudSpaceEventIds.SystemPowerOnEventId:
                    IsSystemPowerOn = true;
                    break;
                case XioCloudSpaceEventIds.SystemPowerOffEventId:
                    IsSystemPowerOn = false;
                    break;
                case XioCloudSpaceEventIds.DisplayPowerOnEventId:
                    IsDisplayPowerOn = true;
                    break;
                case XioCloudSpaceEventIds.DisplayPowerOffEventId:
                    IsDisplayPowerOn = false;
                    break;
                case XioCloudSpaceEventIds.OccupiedEventId: 
                    _logger.Debug("XIO: Received OccupiedEventId. Occupied: {Occupied}", xioSystem.Occupied.BoolValue);
                    if (xioSystem.Occupied.BoolValue)
                        IsOccupied = true;
                    break;
                case XioCloudSpaceEventIds.VacantEventId:
                    _logger.Debug("XIO: Received VacantEventId. Vacant: {Vacant}", xioSystem.Vacant.BoolValue);
                    if (!xioSystem.Vacant.BoolValue)
                        IsOccupied = false;
                    break;
                case XioCloudSpaceEventIds.SystemCheckEventId:
                    _logger.Debug("XIO: Received SystemCheckEventId");
                    SetUiSystemCheckButtonsEnabled(true);
                    break;
                case XioCloudSpaceEventIds.MaintenanceModeEventId:
                    _logger.Debug("XIO: Received MaintenanceModeEventId. MaintenanceMode: {MaintenanceMode}", xioSystem.MaintenanceMode.BoolValue);
                    if (xioSystem.MaintenanceMode.BoolValue)
                        IsMaintenanceMode = true;
                    else
                        IsMaintenanceMode = false;
                    break;
                case XioCloudSpaceEventIds.PeopleCountEventId:
                    _logger.Debug("Received PeopleCountEventId. PeopleCount: {PeopleCount}", xioSystem.PeopleCount.UShortValue);
                    IsOccupied = xioSystem.PeopleCount.UShortValue > 0;
                    break;
            }
        }
    }
}