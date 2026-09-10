using System.Threading.Tasks;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private void UpdateLightSceneName()
        {
            var uiLights = _xioCloudRoomGui.Main.Lights;
            var xioLights = ControllerXioCloudSpaceSlotDevice.Lighting;
            xioLights.LightSceneNames[2].StringValue = LightSceneNames[2];
            uiLights.LightsScene2_Serial_CIP(LightSceneNames[2]);
            xioLights.LightSceneNames[3].StringValue = LightSceneNames[3];
            uiLights.LightsScene3_Serial_CIP(LightSceneNames[3]);
            xioLights.LightSceneNames[4].StringValue = LightSceneNames[4];
            uiLights.LightsScene4_Serial_CIP(LightSceneNames[4]);
            xioLights.LightSceneNames[5].StringValue = LightSceneNames[5];
            uiLights.LightsScene5_Serial_CIP(LightSceneNames[5]);
        }
    }
}