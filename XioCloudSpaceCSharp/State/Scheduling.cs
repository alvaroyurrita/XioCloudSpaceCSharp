using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
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
            ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStartTimeFeedback.StringValue =
                HelperClasses.DateGenerator(index);
            ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStopTimeFeedback.StringValue =
                HelperClasses.DateGenerator((ushort)(index + 1));
            _xioCloudRoomGui.Main.Scheduling.CurrentReservationText_Indirect(HelperClasses.DateGenerator(index));
            _xioCloudRoomGui.Main.Scheduling.CurrentReservationSlider_LowerTouchfb(index);
        }

        private void NextReservation(ushort index)
        {
            ControllerXioCloudSpaceSlotDevice.Scheduling.NextMeetingStartTimeFeedback.StringValue =
                HelperClasses.DateGenerator(index);
            _xioCloudRoomGui.Main.Scheduling.NextReservation_Indirect(HelperClasses.DateGenerator(index));
            _xioCloudRoomGui.Main.Scheduling.NextReservationSlider_LowerTouchfb(index);
        }
    }
}