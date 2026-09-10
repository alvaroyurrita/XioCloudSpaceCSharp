using Crestron.SimplSharpPro;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {

        private void XioVolumeEventsHandler(int eventId)
        {
            var xioVolume = ControllerXioCloudSpaceSlotDevice.Volume;
            switch (eventId)
            {
                case XioCloudSpaceEventIds.VolumeLevelEventId:
                    VolumeLevel = xioVolume.VolumeLevel.UShortValue;
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