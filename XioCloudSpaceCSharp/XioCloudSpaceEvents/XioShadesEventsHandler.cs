using Crestron.SimplSharpPro;
using System;
using System.Linq;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        private void XioShadesEventsHandler(int eventId, int index)
        {
            switch (eventId)
            {
                case XioCloudSpaceEventIds.ShadesOpenSceneEventId:
                    _logger.Debug("Shades Open event received. ");
                    CurrentShadeScene = 1;
                    break;
                case XioCloudSpaceEventIds.ShadesClosedSceneEventId:
                    _logger.Debug("Shades Closed event received");
                    CurrentShadeScene = 6;
                    break;
                case XioCloudSpaceEventIds.ShadeSceneEventId:
                    _logger.Debug("Shade scene event received. Index: {Index}", index);
                    CurrentShadeScene = (ushort)index;
                    break;
                case XioCloudSpaceEventIds.RequestedShadeSceneNameEventId:
                    var name = ControllerXioCloudSpaceSlotDevice.Shades.RequestedShadeSceneName.StringValue;
                    _logger.Debug("Requested shade scene by name event received. Name: {Name}", name);
                    try
                    {
                        var shadeIndex = ShadeSceneNames.ToList().FindIndex(x => x == name);
                        CurrentShadeScene = (ushort)shadeIndex;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(
                            "Error finding shade scene index by name: {Name}. Exception: {Exception}",
                            name,
                            ex.Message
                        );
                    }
                    break;
            }
        }


    }
}