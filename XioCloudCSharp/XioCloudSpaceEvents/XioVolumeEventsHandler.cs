using Crestron.SimplSharpPro;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {

        private void XioVolumeEventsHandler(int eventId)
        {
            var uiMain = _xioCloudRoomGui.Main;
            var xioSystem = ControllerXioCloudSpaceSlotDevice;
            switch (eventId)
            {
                case XioCloudSpaceEventIds.VolumeLevelEventId:
                    VolumeLevel = xioSystem.Volume.VolumeLevel.UShortValue;
                    break;
                case XioCloudSpaceEventIds.VolumeMuteOnEventId:
                    VolumeMuteState = true;
                    break;
                case XioCloudSpaceEventIds.VolumeMuteOffEventId:
                    VolumeMuteState = false;
                    break;
                case XioCloudSpaceEventIds.MicrophonePrivacyOnEventId:
                    MicrophonePrivateState = true;
                    break;
                case XioCloudSpaceEventIds.MicrophonePrivacyOffEventId:
                    MicrophonePrivateState = false;
                    break;
            }
        }
    }
}