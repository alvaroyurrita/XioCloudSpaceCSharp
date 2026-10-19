using XioCloudSpaceConstruct;

namespace XioCloudSpaceCSharp
{
    public partial class ControlSystem
    {
        public void SubscribeUiLightsEvents()
        {
            var uiLights = _xioCloudRoomGui.Main.Lights;
            var xioLights = ControllerXioCloudSpaceSlotDevice.Lighting;
            var currentLightSceneName = string.Empty;
            
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
                currentLightSceneName = args.String();
                _logger.Debug("LightsSceneNameText_OutputTextEvent. Current Scene Name: {SceneName}:", currentLightSceneName);
                uiLights.LightsSceneNameText_IndirectText(currentLightSceneName);
            };
            uiLights.LightsRequestByNamePress_PressEvent += (_, args) =>
            {
                if (! string.IsNullOrEmpty(currentLightSceneName))
                {
                    //TODO: Set the ActiveLightSceneName in XIO Cloud before clearing the UI
                    
                   currentLightSceneName = string.Empty;
                   uiLights.LightsSceneNameText_IndirectText(currentLightSceneName);
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
                if (!string.IsNullOrEmpty(currentLightSceneName))
                {
                    ChangeLightSceneName(CurrentLightScene, currentLightSceneName);
                    currentLightSceneName = string.Empty;
                    uiLights.LightsSceneNameText_IndirectText(currentLightSceneName);
                }
            }
        }

    }
}