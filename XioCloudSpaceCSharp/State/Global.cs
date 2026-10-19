using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
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

        private ushort _currentSource;
        public ushort CurrentSource
        {
            get => _currentSource;
            set
            {
                if (_currentSource == value) return;
                _logger.Debug("Current source changed. New source: {Source}", value);
                _currentSource = value;
                _xioCloudRoomGui.Main.CurrentSource_LowerTouchfb(_currentSource);
                ControllerXioCloudSpaceSlotDevice.CurrentSourceFeedback.UShortValue = _currentSource;
            }
        }

        private ushort _peopleCount;
        public ushort PeopleCount
        {
            get => _peopleCount;
            set
            {
                if (_peopleCount == value) return;
                _logger.Debug("People count changed. New count: {Count}", value);
                _peopleCount = value;
                _xioCloudRoomGui.Main.PeopleCount_LowerTouchfb(_peopleCount);
                ControllerXioCloudSpaceSlotDevice.PeopleCountFeedback.UShortValue = _peopleCount;
            }
        }
        
        public void SendAlertMessage(string message)
        {
            _logger.Debug("Sending alert message: {Message}", message);
            // TODO: Update the feedback state in the XioCloudSpaceSlotDevice
        }
        
        public void ResolveAlertMessage(string message)
        {
            _logger.Debug("Sending alert message: {Message}", message);
            // TODO: Update the feedback state in the XioCloudSpaceSlotDevice
        }
    }
}