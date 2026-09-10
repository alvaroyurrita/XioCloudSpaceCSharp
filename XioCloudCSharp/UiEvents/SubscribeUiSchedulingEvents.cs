namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeSchedulingEvents()
        {
            var uiMain = _xioCloudRoomGui.Main;
            _logger.Debug("Subscribing to UI Main Scheduling events...");
            uiMain.Scheduling.CheckedInToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("CheckedInToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling Scheduling state...");
                    IsCheckedIn = !IsCheckedIn;
                }
            };
            
            uiMain.Scheduling.DoNotDisturbeToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("DoNotDisturbeToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling Do Not Disturb state...");
                    IsOnDoNotDisturb = !IsOnDoNotDisturb;
                }
            };
            
            uiMain.Scheduling.InScheduledEventToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("InScheduledEventToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling In Scheduled Event state...");
                    IsInScheduledEvent = !IsInScheduledEvent;
                }
            };
            
            uiMain.Scheduling.SeriviceNeededToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("SeriviceNeededToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling Service Needed state...");
                    IsInServiceNeeded = !IsInServiceNeeded;
                }
            };
            
            uiMain.Scheduling.CurrentReservationSlider_LowerTouchEvent += (_, args) =>
            {
                var value = args.UShort();
                _logger.Debug("CurrentReservationSlider_LowerTouchEvent. State {State}:", value);
                CurrentReservation(value);
            };
            
            uiMain.Scheduling.NextReservationSlider_LowerTouchEvent += (_, args) =>
            {
                var value = args.UShort();
                _logger.Debug("NextReservationSlider_LowerTouchEvent. State {State}:", value);
                NextReservation(value);
            };
        }

    }
}