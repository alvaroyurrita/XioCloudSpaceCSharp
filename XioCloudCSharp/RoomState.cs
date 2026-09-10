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
        private bool _volumeMuteState;
        private bool VolumeMuteState
        {
            get => _volumeMuteState;
            set
            {
                if (_volumeMuteState == value) return;
                _logger.Debug("Volume mute state changed. New state: {State}", value);
                _volumeMuteState = value;
                _xioCloudRoomGui.Main.Volume.VolumeMuteToggle_Selected(VolumeMuteState);
                ControllerXioCloudSpaceSlotDevice.Volume.VolumeMuteFeedback.BoolValue = VolumeMuteState;
            }
        }

        private bool _microphonePrivateState;
        private bool MicrophonePrivateState
        {
            get => _microphonePrivateState;
            set
            {
                if (_microphonePrivateState == value) return;
                _logger.Debug("Microphone private state changed. New state: {State}", value);
                _microphonePrivateState = value;
                _xioCloudRoomGui.Main.Volume.MicrophoneToggle_Selected(MicrophonePrivateState);
                ControllerXioCloudSpaceSlotDevice.Volume.MicrophonePrivacyFeedback.BoolValue = MicrophonePrivateState;
            }
        }
        private ushort _volumeLevel;
        private ushort VolumeLevel
        {
            get => _volumeLevel;
            set
            {
                if (_volumeLevel == value) return;
                _logger.Debug("Volume level changed. New level: {Level}", value);
                _volumeLevel = value;
                _xioCloudRoomGui.Main.Volume.VolumeLevel_LowerTouchfb(VolumeLevel);
                ControllerXioCloudSpaceSlotDevice.Volume.VolumeLevelFeedback.UShortValue = VolumeLevel;
            }
        }
        private bool _isInCall;
        private bool IsInCall
        {
            get => _isInCall;
            set
            {
                if (_isInCall == value) return;
                _logger.Debug("In call state changed. New state: {State}", value);
                _isInCall = value;
                ControllerXioCloudSpaceSlotDevice.Call.InCallFeedback.BoolValue = _isInCall;
                _xioCloudRoomGui.Main.Call.InCallToggle_Selected(_isInCall);
            }
        }

        private bool _isCheckedIn;
        private bool IsCheckedIn
        {
            get => _isCheckedIn;
            set
            {
                if (_isCheckedIn == value) return;
                _logger.Debug("Checked in state changed. New state: {State}", value);
                _isCheckedIn = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.CheckedInFeedback.BoolValue = _isCheckedIn;
                _xioCloudRoomGui.Main.Scheduling.CheckedInToggle_Selected(_isCheckedIn);
            }
        }
        
        private bool _isOnDoNotDisturb;
        private bool IsOnDoNotDisturb
        {
            get => _isOnDoNotDisturb;
            set
            {
                if (_isOnDoNotDisturb == value) return;
                _logger.Debug("Do Not Disturb state changed. New state: {State}", value);
                _isOnDoNotDisturb = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.DoNotDisturbFeedback.BoolValue = _isOnDoNotDisturb;
                _xioCloudRoomGui.Main.Scheduling.DoNotDisturbeToggle_Selected(_isOnDoNotDisturb);
            }
        }
        
        private bool _isInScheduledEvent;
        private bool IsInScheduledEvent
        {
            get => _isInScheduledEvent;
            set
            {
                if (_isInScheduledEvent == value) return;
                _logger.Debug("In scheduled event state changed. New state: {State}", value);
                _isInScheduledEvent = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.InScheduledEventFeedback.BoolValue = _isInScheduledEvent;
                _xioCloudRoomGui.Main.Scheduling.InScheduledEventToggle_Selected(_isInScheduledEvent);
            }
        }
        
        private bool _isInServiceNeeded;
        private bool IsInServiceNeeded
        {
            get => _isInServiceNeeded;
            set
            {
                if (_isInServiceNeeded == value) return;
                _logger.Debug("In service needed state changed. New state: {State}", value);
                _isInServiceNeeded = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.ServiceNeededFeedback.BoolValue = _isInServiceNeeded;
                _xioCloudRoomGui.Main.Scheduling.SeriviceNeededToggle_Selected(_isInServiceNeeded);
            }
        }
        
        private void CurrentReservation(ushort index)
        {
            ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStartTimeFeedback.StringValue = HelperClasses.DateGenerator(index);
            ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStopTimeFeedback.StringValue = HelperClasses.DateGenerator((ushort)(index + 1));
            _xioCloudRoomGui.Main.Scheduling.CurrentReservationText_Indirect(HelperClasses.DateGenerator(index));
            _xioCloudRoomGui.Main.Scheduling.CurrentReservationSlider_LowerTouchfb(index);
        }

        private void NextReservation(ushort index)
        {
            ControllerXioCloudSpaceSlotDevice.Scheduling.NextMeetingStartTimeFeedback.StringValue = HelperClasses.DateGenerator(index);
            _xioCloudRoomGui.Main.Scheduling.NextReservation_Indirect(HelperClasses.DateGenerator(index));
            _xioCloudRoomGui.Main.Scheduling.NextReservationSlider_LowerTouchfb(index);
        }
    }
}