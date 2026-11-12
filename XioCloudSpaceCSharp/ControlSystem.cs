using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.UI;
using Serilog;
using Serilog.Core;
using Serilog.Crestron.Sinks;
using System.Collections.Generic;
using XioCloudSpaceConstruct;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem : CrestronControlSystem
    {
        Ts1070 _touchPanel;
        private readonly Logger _logger;
        private readonly Dictionary<int, string> _xioCloudSpaceEventNames = HelperClasses.XioCloudSpaceEvent();
        private readonly Contract _xioCloudRoomGui;

        public ControlSystem()
        {
            //Logger configuration
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.CrestronConsole(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.CrestronErrorLog(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            // Register for incoming signals from XiO
            ControllerXioCloudSpaceSlotDevice.BaseEvent += XioEvenIdHandler;
            // Register for XiO online status change
            ControllerXioCloudSpaceSlotDevice.OnlineStatusChange += XioCloudSpace_OnlineStatusChange;

            // Create the XiO Cloud Room GUI contract and add the touch panel to it
            _xioCloudRoomGui = new XioCloudSpaceConstruct.Contract();
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
            SubscribeUiClimateEvents();
        }

        public override void InitializeSystem()
        {
            //TODO:  Register the CloudRoom slot with XiO; by default all signals on the slot are registered with XiO


            //Register the Touch Panel
            _touchPanel.Register();

            //Initialize Climate Values
            InitializeClimate();
        }

        private void XioCloudSpace_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            _logger.Debug("XiO Cloud Space online status changed: IsOnline [{IsOnline}]", args.DeviceOnLine);
            UpdateLightSceneName();
            UpdateShadeSceneName();
            UpdateQuickActions();
            ControllerXioCloudSpaceSlotDevice.MetaData.XpanelUrlFeedback.StringValue = "https://cp4-home/XioCloudSpaceConstruct/index.html?ipid=20";
        }

        private string GetEventName(int eventId) =>
            _xioCloudSpaceEventNames.TryGetValue(eventId, out var eventName) ? eventName : "Unknown Event";

        
    }
}