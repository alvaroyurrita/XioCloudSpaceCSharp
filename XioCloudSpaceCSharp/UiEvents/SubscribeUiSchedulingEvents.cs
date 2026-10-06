namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiSchedulingEvents()
        {
            var uiScheduling = _xioCloudRoomGui.Main.Scheduling;
            _logger.Debug("Subscribing to UI Main Scheduling events...");
            uiScheduling.CheckedInToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckedInToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling Scheduling state...");
                    IsCheckedIn = !IsCheckedIn;
                }
            };
            
            uiScheduling.DoNotDisturbeToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("DoNotDisturbeToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling Do Not Disturb state...");
                    IsOnDoNotDisturb = !IsOnDoNotDisturb;
                }
            };
            
            uiScheduling.InScheduledEventToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("InScheduledEventToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling In Scheduled Event state...");
                    IsInScheduledEvent = !IsInScheduledEvent;
                }
            };
            
            uiScheduling.SeriviceNeededToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("SeriviceNeededToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling Service Needed state...");
                    IsInServiceNeeded = !IsInServiceNeeded;
                }
            };
            
            uiScheduling.CurrentReservationSlider_LowerTouchEvent += (_, args) =>
            {
                var value = args.UShort();
                _logger.Debug("CurrentReservationSlider_LowerTouchEvent. State {State}:", value);
                CurrentReservation(value);
            };
            
            uiScheduling.NextReservationSlider_LowerTouchEvent += (_, args) =>
            {
                var value = args.UShort();
                _logger.Debug("NextReservationSlider_LowerTouchEvent. State {State}:", value);
                NextReservation(value);
            };
        }

    }
}