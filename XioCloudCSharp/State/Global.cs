using System;
using System.Threading.Tasks;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private bool _isSystemPowerOn;
        private bool IsSystemPowerOn
        {
            get => _isSystemPowerOn;
            set
            {
                if (_isSystemPowerOn == value) return;
                _logger.Debug("System power state changed. New state: {State}", value);
                _isSystemPowerOn = value;
                _xioCloudRoomGui.Main.SystemPowerToggle_Selected(IsSystemPowerOn);
                ControllerXioCloudSpaceSlotDevice.SystemPowerFeedback.BoolValue = IsSystemPowerOn;
            }
        }

        private bool _isDisplayPowerOn;
        private bool IsDisplayPowerOn
        {
            get => _isDisplayPowerOn;
            set
            {
                if (_isDisplayPowerOn == value) return;
                _logger.Debug("Display power state changed. New state: {State}", value);
                _isDisplayPowerOn = value;
                _xioCloudRoomGui.Main.DisplayPowerToggle_Selected(IsDisplayPowerOn);
                ControllerXioCloudSpaceSlotDevice.DisplayPowerFeedback.BoolValue = IsDisplayPowerOn;
            }
        }

        private bool _isOccupied;
        private bool IsOccupied
        {
            get => _isOccupied;
            set
            {
                if (_isOccupied == value) return;
                _logger.Debug("Room occupancy state changed. New state: {State}", value);
                _isOccupied = value;
                _xioCloudRoomGui.Main.VacancyStateToggle_Selected(IsOccupied);
                ControllerXioCloudSpaceSlotDevice.OccupiedFeedback.BoolValue = IsOccupied;
            }
        }

        private bool _isMaintenanceMode;
        private bool IsMaintenanceMode
        {
            get => _isMaintenanceMode;
            set
            {
                if (_isMaintenanceMode == value) return;
                _logger.Debug("Maintenance mode state changed. New state: {State}", value);
                _isMaintenanceMode = value;
                _xioCloudRoomGui.Main.MaintenanceToggle_Selected(IsMaintenanceMode);
                if (_isMaintenanceMode)
                {
                    ControllerXioCloudSpaceSlotDevice.MaintenanceModeOnFeedback.BoolValue = true;
                    Task.Run(async () =>
                        {
                            //It really wants to be a pulse
                            await Task.Delay(100);
                            ControllerXioCloudSpaceSlotDevice.MaintenanceModeOnFeedback.BoolValue = false;
                        }
                    );
                }
                else
                {
                    ControllerXioCloudSpaceSlotDevice.MaintenanceModeOffFeedback.BoolValue = true;
                    Task.Run(async () =>
                        {
                            //It really wants to be a pulse
                            await Task.Delay(100);
                            ControllerXioCloudSpaceSlotDevice.MaintenanceModeOffFeedback.BoolValue = false;
                        }
                    );
                }
            }
        }
    }
}