using XIOCloudRoomGUI;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiLightsEvents()
        {
            var uiLights = _xioCloudRoomGui.Main.Lights;
            var xioLights = ControllerXioCloudSpaceSlotDevice.Lighting;
            var currentSceneName = string.Empty;
            
            _logger.Debug("Subscribing to UI Main Lights events...");
            uiLights.LightsScene1_PressEvent += (_, args) =>
            {
                SetLightScene(args, 1);
            };
            uiLights.LightsScene2_PressEvent += (_, args) =>
            {
                SetLightScene(args, 2);
            };
            uiLights.LightsScene3_PressEvent += (_, args) =>
            {
                SetLightScene(args, 3);
            };
            uiLights.LightsScene4_PressEvent += (_, args) =>
            {
                SetLightScene(args, 4);
            };
            uiLights.LightsScene5_PressEvent += (_, args) =>
            {
                SetLightScene(args, 5);
            };
            uiLights.LightsScene6_PressEvent += (_, args) =>
            {
                SetLightScene(args, 6);
            };

            uiLights.LightsSceneNameText_OutputTextEvent += (_, args) =>
            {
                currentSceneName = args.String();
                _logger.Debug("LightsSceneNameText_OutputTextEvent. Current Scene Name: {SceneName}:", currentSceneName);
                uiLights.LightsSceneNameText_IndirectText(currentSceneName);
            };
            uiLights.LightsRequestByNamePress_PressEvent += (_, args) =>
            {
                if (! string.IsNullOrEmpty(currentSceneName))
                {
                   xioLights.ActiveLightSceneName.StringValue = currentSceneName;
                   currentSceneName = string.Empty;
                   uiLights.LightsSceneNameText_IndirectText(currentSceneName);
                }
            };
            
            return;
            void SetLightScene(UIEventArgs args, ushort scene)
            {
                var state = args.Boolean();
                _logger.Debug("LightsScene{Scene}_PressEvent. State {State}:", scene, state); 
                if (state)
                {
                    _logger.Debug("Toggling Lights Scene {Scene}...", scene);
                    CurrentLightScene = scene;
                }
                if (!string.IsNullOrEmpty(currentSceneName))
                {
                    ChangeLightSceneName(CurrentLightScene, currentSceneName);
                    currentSceneName = string.Empty;
                    uiLights.LightsSceneNameText_IndirectText(currentSceneName);
                }
            }
        }

    }
}