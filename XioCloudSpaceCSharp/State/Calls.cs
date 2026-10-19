using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private bool _isInCall;
        private bool IsInCall
        {
            get => _isInCall;
            set
            {
                if (_isInCall == value) return;
                _logger.Debug("In call state changed. New state: {State}", value);
                _isInCall = value;
                _xioCloudRoomGui.Main.Call.InCallToggle_Selected(_isInCall);
                // TODO: Update the call feedback state in the XioCloudSpaceSlotDevice
            }
        }
    }
}