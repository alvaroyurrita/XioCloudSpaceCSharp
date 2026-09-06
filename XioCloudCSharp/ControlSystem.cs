using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.GeneralIO;
using Crestron.SimplSharpPro.UI;
using Serilog;
using Serilog.Core;
using Serilog.Crestron.Sinks;
using XIOCloudRoomGUI;

namespace XioCloudCSharp;
public partial class ControlSystem : CrestronControlSystem
{
    Ts1070 _touchPanel;
    private readonly Logger _logger;
    private readonly Dictionary<int, string> _xioCloudSpaceEventNames= HelperClasses.XioCloudSpaceEvent();
    private readonly Contract _xioCloudRoomGui;

    public ControlSystem()
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.CrestronConsole(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        
        // Register for incoming signals from XiO
        ControllerXioCloudSpaceSlotDevice.BaseEvent += XioCloudSpace_PropertyChangeEventHandler;

        // Register for XiO connection status change
        ControllerXioCloudSpaceSlotDevice.OnlineStatusChange += XioCloudSpace_OnlineStatusChange;
        _xioCloudRoomGui = new XIOCloudRoomGUI.Contract();
        _logger.Debug("XiO Cloud Room GUI contract created");
        _touchPanel = new Ts1070(0x20, this);
        _xioCloudRoomGui.AddDevice(_touchPanel);
        _logger.Debug("Touch panel added to XiO Cloud Room GUI contract on ID 0x20");
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
    }

    private string GetEventName(int eventId) => _xioCloudSpaceEventNames.GetValueOrDefault(eventId, "Unknown Event");

    private void XioCloudSpace_PropertyChangeEventHandler(GenericBase device, BaseEventArgs args)
    {
        _logger.Debug("Receive Event from XIO Cloud: Event Id [{EventID}]. Event Index: [{EventIndex}]", GetEventName(args.EventId), args.Index);
        // Handle all events which come in from XiO
        switch (args.EventId)
        {
            case XioCloudSpaceEventIds.SpaceCapacityEventId:
            case XioCloudSpaceEventIds.SpaceNameEventId:
            case XioCloudSpaceEventIds.SpaceIDEventId:
            case XioCloudSpaceEventIds.SpaceTypeEventId:
            case XioCloudSpaceEventIds.SpaceDealerContactInfoEventId:
            case XioCloudSpaceEventIds.SpaceTimeZoneEventId:
                HandleMetaData(args.EventId);
                break;
        }

    }

    // private void TurnSystemPowerOn()
    // {
    //     // Send system power state to XiO
    //     ControllerXioCloudRoomSlotDevice.SystemPowerFeedback.BoolValue = true;
    // }
    //
    // private void TurnSystemPowerOff()
    // {
    //     // Send system power state to XiO
    //     ControllerXioCloudRoomSlotDevice.SystemPowerFeedback.BoolValue = false;
    // }
    //
    // private void TurnDisplayPowerOn()
    // {
    //     // Send display power state to XiO
    //     ControllerXioCloudRoomSlotDevice.DisplayPowerFeedback.BoolValue = true;
    // }
    //
    // private void TurnDisplayPowerOff()
    // {
    //     // Send display power state to XiO
    //     ControllerXioCloudRoomSlotDevice.DisplayPowerFeedback.BoolValue = false;
    // }
    //
    // private void HandleOccupiedEvent(bool value)
    // {
    //     const uint OccupiedStateJoin = 10;
    //
    //     _touchPanel.BooleanInput[OccupiedStateJoin].BoolValue = value;
    // }
    //
    // private void HandleVacantEvent(bool value)
    // {
    //     const uint VacantStateJoin = 11;
    //
    //     _touchPanel.BooleanInput[VacantStateJoin].BoolValue = value;
    // }
    //
    // private void HandleRoomNameEvent(string value)
    // {
    //     const uint RoomNameJoin = 12;
    //
    //     _touchPanel.StringInput[RoomNameJoin].StringValue = value;
    // }
    //
    // private void HandleMaintenanceModeEvent(bool value)
    // {
    //     const uint MaintenanceModeJoin = 13;
    //
    //     if (value)
    //     {
    //         _touchPanel.StringInput[MaintenanceModeJoin].StringValue = "Room is in maintenance mode";
    //     }
    //     else
    //     {
    //         _touchPanel.StringInput[MaintenanceModeJoin].StringValue = "";
    //     }
    // }
    //
    // private void RunSystemCheck()
    // {
    //     ControllerXioCloudRoomSlotDevice.SystemCheckStateFeedback.Value = XioCloudRoomSlot.eSystemCheckState.Running;
    //
    //     if (_occSensor.IsOnline)
    //     {
    //         ControllerXioCloudRoomSlotDevice.SystemCheckStateFeedback.Value = XioCloudRoomSlot.eSystemCheckState.Success;
    //     }
    //     else
    //     {
    //         // SystemCheckMessageFeedback may be sent to provide details about system check
    //         ControllerXioCloudRoomSlotDevice.SystemCheckMessageFeedback.StringValue = "Occupancy sensor offline";
    //         ControllerXioCloudRoomSlotDevice.SystemCheckStateFeedback.Value = XioCloudRoomSlot.eSystemCheckState.Error;
    //     }
    // }
    //
    // public void OccSensor_PropertyChangeEventHandler(GenericBase device, BaseEventArgs args)
    // {
    //     if (args.EventId == GlsOccupancySensorBase.RoomOccupiedFeedbackEventId)
    //     {
    //         // Send occupancy state to XiO
    //         ControllerXioCloudRoomSlotDevice.OccupiedFeedback.BoolValue = _occSensor.OccupancyDetectedFeedback.BoolValue;
    //     }
    // }
}