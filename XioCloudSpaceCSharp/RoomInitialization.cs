using System.Threading.Tasks;

namespace XioCloudSpaceCSharp
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
        
        private void UpdateShadeSceneName()
        {
            var uiShades = _xioCloudRoomGui.Main.Shades;
            var xioShades = ControllerXioCloudSpaceSlotDevice.Shades;
            xioShades.ShadeSceneNames[2].StringValue = ShadeSceneNames[2];
            uiShades.ShadesScene2_Serial_CIP(ShadeSceneNames[2]);
            xioShades.ShadeSceneNames[3].StringValue = ShadeSceneNames[3];
            uiShades.ShadesScene3_Serial_CIP(ShadeSceneNames[3]);
            xioShades.ShadeSceneNames[4].StringValue = ShadeSceneNames[4];
            uiShades.ShadesScene4_Serial_CIP(ShadeSceneNames[4]);
            xioShades.ShadeSceneNames[5].StringValue = ShadeSceneNames[5];
            uiShades.ShadesScene5_Serial_CIP(ShadeSceneNames[5]);
        }

        private void UpdateQuickActions()
        {
            var xioQuickActions = ControllerXioCloudSpaceSlotDevice.QuickAction;
            xioQuickActions.QuickActionNames[1].StringValue = QuickActionNames[1];
            xioQuickActions.QuickActionNames[2].StringValue = QuickActionNames[2];
            xioQuickActions.QuickActionNames[3].StringValue = QuickActionNames[3];
            xioQuickActions.QuickActionNames[4].StringValue = QuickActionNames[4];
            xioQuickActions.QuickActionNames[5].StringValue = QuickActionNames[5];
            xioQuickActions.QuickActionNames[6].StringValue = QuickActionNames[6];
        }

        private void InitializeClimate()
        {
            TemperatureInCelsius = false;
            SetTemperature = 700;
            CurrentTemperature = 700;
            SetHumidity = 50;
            CurrentHumidity = 50;
            CurrentClimateMode = 4;
        }
    }
}