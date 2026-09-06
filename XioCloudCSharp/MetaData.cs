using Crestron.SimplSharpPro;

namespace XioCloudCSharp;
public partial class ControlSystem
{
                
    private void HandleMetaData(int index)
    {
        var metaData = ControllerXioCloudSpaceSlotDevice.MetaData;
        var metaDataUi = _xioCloudRoomGui.Main.MetaData;
        // Handle metadata changes from XiO Cloud Space slot
        switch (index)
        {
          
            case XioCloudSpaceEventIds.SpaceCapacityEventId:
                metaDataUi.SpaceCapacityValue_Analog_CIP(metaData.SpaceCapacity.UShortValue);
                break;
            case XioCloudSpaceEventIds.SpaceNameEventId:
                metaDataUi.SpaceNameText_Indirect(metaData.SpaceName.StringValue);
                break;
            case XioCloudSpaceEventIds.SpaceIDEventId:
                metaDataUi.SpaceIdText_Indirect(metaData.SpaceID.StringValue);
                break;
            case XioCloudSpaceEventIds.SpaceTypeEventId:
                metaDataUi.SpaceTypeText_Indirect(metaData.SpaceType.StringValue);
                break;
            case XioCloudSpaceEventIds.SpaceDealerContactInfoEventId:
                metaDataUi.SpaceDealerContactInfoText_Indirect(metaData.SpaceDealerContactInfo.StringValue);
                break;
            case XioCloudSpaceEventIds.SpaceTimeZoneEventId:
                metaDataUi.SpaceTimeZoneText_Indirect(metaData.SpaceTimeZone.StringValue);
                break;
        }
    }
}