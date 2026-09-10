using Crestron.SimplSharpPro;
using System;
using System.Linq;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private void XioLightingEventsHandler(int eventId, int index)
        {
            switch (eventId)
            {
                case XioCloudSpaceEventIds.LightsOnSceneEventId:
                    _logger.Debug("Lighting On event received. ");
                    CurrentLightScene = 1;
                    break;
                case XioCloudSpaceEventIds.LightsOffSceneEventId:
                    _logger.Debug("Lighting off event received");
                    CurrentLightScene = 6;
                    break;
                case XioCloudSpaceEventIds.LightSceneEventId:
                    _logger.Debug("Light scene event received. Index: {Index}", index);
                    CurrentLightScene = (ushort)index;
                    break;
                case XioCloudSpaceEventIds.RequestedLightSceneNameEventId:
                    var name = ControllerXioCloudSpaceSlotDevice.Lighting.RequestedLightSceneName.StringValue;
                    _logger.Debug("Requested light scene by index event received. Index: {Name}", name);
                    try
                    {
                        var lightIndex = LightSceneNames.ToList().FindIndex(x => x == name);
                        CurrentLightScene = (ushort)lightIndex;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(
                            "Error finding light scene index by name: {Name}. Exception: {Exception}",
                            name,
                            ex.Message
                        );
                    }
                    break;
            }
        }


    }
}