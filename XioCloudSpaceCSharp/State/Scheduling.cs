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
                _xioCloudRoomGui.Main.Scheduling.CheckedInToggle_Selected(_isCheckedIn);
                // TODO: Update the Checked In feedback value in the XIO
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
                //TODO: Update the Do Not Disturb feedback value in the XIO
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
                _xioCloudRoomGui.Main.Scheduling.InScheduledEventToggle_Selected(_isInScheduledEvent);
                // TODO: Update the In Scheduled Event feedback value in the XIO
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
                _xioCloudRoomGui.Main.Scheduling.SeriviceNeededToggle_Selected(_isInServiceNeeded);
                // TODO: Update the In Service Needed feedback value in the XIO
            }
        }
    }
}