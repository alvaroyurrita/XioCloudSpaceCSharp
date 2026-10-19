using System;
using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
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
                //TODO: Set the XIO feedback for Volume Mute state
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
                //TODO: Set the XIO feedback for Microphone Privacy state
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
                //TODO: Set the XIO feedback for Volume Level
            }
        }
    }
}