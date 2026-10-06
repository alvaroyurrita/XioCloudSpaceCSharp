namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiVolumeEvents()
        {
            var uiVolume = _xioCloudRoomGui.Main.Volume;

            _logger.Debug("Subscribing to UI Main volume events...");
            uiVolume.VolumeLevel_LowerTouchEvent += (_, args) =>
            {
                var value = args.UShort();
                _logger.Debug("VolumeUpPress_PressEvent. State {State}:", value);
                VolumeLevel = value;
            };
            uiVolume.VolumeMuteToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("VolumeMuteToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling mute");
                    VolumeMuteState = !VolumeMuteState;
                }
            };
            uiVolume.MicrophoneToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("MicrophoneToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling microphone");
                    MicrophonePrivateState = !MicrophonePrivateState;
                }
            };
        }
    }
}