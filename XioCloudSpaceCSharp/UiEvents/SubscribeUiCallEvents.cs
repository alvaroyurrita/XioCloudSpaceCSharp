namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiCallEvents()
        {
            var uiCall = _xioCloudRoomGui.Main.Call;
            _logger.Debug("Subscribing to UI Main Call events...");
            uiCall.InCallToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("InCallToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling In Call state...");
                    IsInCall = !IsInCall;
                }
            };
        }
    }
}