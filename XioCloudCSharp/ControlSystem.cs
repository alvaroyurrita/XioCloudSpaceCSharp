using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.UI;
using Serilog;
using Serilog.Core;
using Serilog.Crestron.Sinks;
using System.Collections.Generic;
using XIOCloudRoomGUI;

namespace XioCloudCSharp
{
    public partial class ControlSystem : CrestronControlSystem
    {
        Ts1070 _touchPanel;
        private readonly Logger _logger;
        private readonly Dictionary<int, string> _xioCloudSpaceEventNames = HelperClasses.XioCloudSpaceEvent();
        private readonly Contract _xioCloudRoomGui;

        public ControlSystem()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.CrestronConsole(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            // Register for incoming signals from XiO
            ControllerXioCloudSpaceSlotDevice.BaseEvent += XioEvenIdHandler;

            // Register for XiO connection status change
            ControllerXioCloudSpaceSlotDevice.OnlineStatusChange += XioCloudSpace_OnlineStatusChange;
            ControllerXioCloudSpaceSlotDevice.Description = "XiO Cloud Space Slot Device";
            _xioCloudRoomGui = new XIOCloudRoomGUI.Contract();
            _logger.Debug("XiO Cloud Room GUI contract created");
            _touchPanel = new Ts1070(0x20, this);
            _xioCloudRoomGui.AddDevice(_touchPanel);
            _logger.Debug("Touch panel added to XiO Cloud Room GUI contract on ID 0x20");
            SubscribeToUiEvents();
        }

        public void SubscribeToUiEvents()
        {
            SubscribeUiCallEvents();
            SubscribeUiSystemCheckEvents();
            SubscribeUiMainEvents();
            SubscribeUiVolumeEvents();
            SubscribeUiSchedulingEvents();
            SubscribeUiLightsEvents();
            SubscribeUiShadesEvents();
        }

        public override void InitializeSystem()
        {
            // Register the CloudRoom slot with XiO; by default all signals on the slot are registered with XiO
            // Signals may be passed here to exclude their registration with XiO
            if (ControllerXioCloudSpaceSlotDevice.Register() == eDeviceRegistrationUnRegistrationResponse.Success)
            {
                // AlertsFeedback may be sent at any time
                _logger.Debug("XiO Cloud Space slot registered successfully");
                ControllerXioCloudSpaceSlotDevice.AlertsFeedback.StringValue = "SIMPL# Program has started";
            }

            _touchPanel.Register();
        }

        private void XioCloudSpace_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            _logger.Debug("XiO Cloud Space online status changed: IsOnline [{IsOnline}]", args.DeviceOnLine);
            UpdateLightSceneName();
            UpdateShadeSceneName();
        }

        private string GetEventName(int eventId) =>
            _xioCloudSpaceEventNames.TryGetValue(eventId, out var eventName) ? eventName : "Unknown Event";

        
    }
}