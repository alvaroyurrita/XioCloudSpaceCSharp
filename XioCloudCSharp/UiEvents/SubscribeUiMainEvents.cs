using Crestron.SimplSharpPro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiMainEvents()
        {
            var uiMain = _xioCloudRoomGui.Main;
            var xioMain = ControllerXioCloudSpaceSlotDevice;
            var currentAlertMessage = string.Empty;
            var currentResolveAlertMessage = string.Empty;

            _logger.Debug("Subscribing to UI Main events...");

            
            uiMain.SystemPowerToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("SystemPowerToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling system power...");
                    IsSystemPowerOn = !IsSystemPowerOn;
                }
            };

            uiMain.DisplayPowerToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("DisplayPowerToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling display power...");
                    IsDisplayPowerOn = !IsDisplayPowerOn;
                }
            };

            uiMain.VacancyStateToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("VacancyStateToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling vacancy state...");
                    IsOccupied = !IsOccupied;
                }
            };
                
            uiMain.MaintenanceToggle_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("MaintenanceToggle_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Toggling maintenance mode...");
                    IsMaintenanceMode = !IsMaintenanceMode;
                }
            };
                
                
            uiMain.CurrentSource_LowerTouchEvent    += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("CurrentSource_LowerTouchEvent. State {State}:", state);
                uiMain.CurrentSource_LowerTouchfb(state);
                xioMain.CurrentSourceFeedback.UShortValue = state;
            };
            
            
            uiMain.PeopleCount_LowerTouchEvent   += (_, args) =>
            {
                var state = args.UShort();
                _logger.Debug("PeopleCount_LowerTouchEvent. State {State}:", state);
                uiMain.PeopleCount_LowerTouchfb(state);
                xioMain.PeopleCountFeedback.UShortValue = state;
            };
            
            uiMain.AlertMessage_OutputTextEvent += (_, args) =>
            {
                var message = args.String();
                _logger.Debug("AlertMessage_OutputTextEvent. Message: {Message}", message);
                uiMain.AlertMessage_IndirectText(message);
                currentAlertMessage = message;
            };
            uiMain.SendAlertMessage_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("SendAlertMessage_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Sending alert message: {Message}", currentAlertMessage);
                    xioMain.AlertsFeedback.StringValue = currentAlertMessage;
                    currentAlertMessage = string.Empty;
                    uiMain.AlertMessage_IndirectText(currentAlertMessage);
                }
            };
            
            uiMain.ResolveAlertMessage_OutputTextEvent += (_, args) =>
            {
                var message = args.String();
                _logger.Debug("ResolveAlertMessage_OutputTextEvent. Message: {Message}", message);
                uiMain.ResolveAlertMessage_IndirectText(message);
                currentResolveAlertMessage = message;
            };
            uiMain.SendResolveAlertMessage_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("SendResolveAlertMessage_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Sending resolve alert message: {Message}", currentResolveAlertMessage);
                    xioMain.ResolveAlertsFeedback.StringValue = currentResolveAlertMessage;
                    currentResolveAlertMessage = string.Empty;
                    uiMain.ResolveAlertMessage_IndirectText(currentResolveAlertMessage);
                }
            };
                
            uiMain.ResolveAllAlerts_PressEvent += (_, args) =>
            {
                var state = args.Boolean();
                _logger.Debug("ResolveAllAlerts_PressEvent. State {State}:", state);
                if (state)
                {
                    _logger.Debug("Resolving all alerts...");
                    xioMain.ResolveAllAlertsFeedback.BoolValue = true;
                    currentAlertMessage = string.Empty;
                    uiMain.AlertMessage_IndirectText(currentAlertMessage);
                    currentResolveAlertMessage = string.Empty;
                    uiMain.ResolveAlertMessage_IndirectText(currentResolveAlertMessage);
                }
            };
        }
        
    }
}