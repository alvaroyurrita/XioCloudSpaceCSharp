using System.Threading.Tasks;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private bool _isSystemPowerOn;
        private bool IsSystemPowerOn
        {
            get => _isSystemPowerOn;
            set
            {
                if (_isSystemPowerOn == value) return;
                _logger.Debug("System power state changed. New state: {State}", value);
                _isSystemPowerOn = value;
                _xioCloudRoomGui.Main.SystemPowerToggle_Selected(IsSystemPowerOn);
                ControllerXioCloudSpaceSlotDevice.SystemPowerFeedback.BoolValue = IsSystemPowerOn;
            }
        }

        private bool _isDisplayPowerOn;
        private bool IsDisplayPowerOn
        {
            get => _isDisplayPowerOn;
            set
            {
                if (_isDisplayPowerOn == value) return;
                _logger.Debug("Display power state changed. New state: {State}", value);
                _isDisplayPowerOn = value;
                _xioCloudRoomGui.Main.DisplayPowerToggle_Selected(IsDisplayPowerOn);
                ControllerXioCloudSpaceSlotDevice.DisplayPowerFeedback.BoolValue = IsDisplayPowerOn;
            }
        }

        private bool _isOccupied;
        private bool IsOccupied
        {
            get => _isOccupied;
            set
            {
                if (_isOccupied == value) return;
                _logger.Debug("Room occupancy state changed. New state: {State}", value);
                _isOccupied = value;
                _xioCloudRoomGui.Main.VacancyStateToggle_Selected(IsOccupied);
                ControllerXioCloudSpaceSlotDevice.OccupiedFeedback.BoolValue = IsOccupied;
            }
        }

        private bool _isMaintenanceMode;
        private bool IsMaintenanceMode
        {
            get => _isMaintenanceMode;
            set
            {
                if (_isMaintenanceMode == value) return;
                _logger.Debug("Maintenance mode state changed. New state: {State}", value);
                _isMaintenanceMode = value;
                _xioCloudRoomGui.Main.MaintenanceToggle_Selected(IsMaintenanceMode);
                if (_isMaintenanceMode)
                {
                    ControllerXioCloudSpaceSlotDevice.MaintenanceModeOnFeedback.BoolValue = true;
                    Task.Run(async () =>
                        {
                            //It really wants to be a pulse
                            await Task.Delay(100);
                            ControllerXioCloudSpaceSlotDevice.MaintenanceModeOnFeedback.BoolValue = false;
                        }
                    );
                }
                else
                {
                    ControllerXioCloudSpaceSlotDevice.MaintenanceModeOffFeedback.BoolValue = true;
                    Task.Run(async () =>
                        {
                            //It really wants to be a pulse
                            await Task.Delay(100);
                            ControllerXioCloudSpaceSlotDevice.MaintenanceModeOffFeedback.BoolValue = false;
                        }
                    );
                }
            }
        }
        private bool _volumeMuteState;
        private bool VolumeMuteState
        {
            get => _volumeMuteState;
            set
            {
                if (_volumeMuteState == value) return;
                _logger.Debug("Volume mute state changed. New state: {State}", value);
                _volumeMuteState = value;
                _xioCloudRoomGui.Main.Volume.VolumeMuteToggle_Selected(VolumeMuteState);
                ControllerXioCloudSpaceSlotDevice.Volume.VolumeMuteFeedback.BoolValue = VolumeMuteState;
            }
        }

        private bool _microphonePrivateState;
        private bool MicrophonePrivateState
        {
            get => _microphonePrivateState;
            set
            {
                if (_microphonePrivateState == value) return;
                _logger.Debug("Microphone private state changed. New state: {State}", value);
                _microphonePrivateState = value;
                _xioCloudRoomGui.Main.Volume.MicrophoneToggle_Selected(MicrophonePrivateState);
                ControllerXioCloudSpaceSlotDevice.Volume.MicrophonePrivacyFeedback.BoolValue = MicrophonePrivateState;
            }
        }
        private ushort _volumeLevel;
        private ushort VolumeLevel
        {
            get => _volumeLevel;
            set
            {
                if (_volumeLevel == value) return;
                _logger.Debug("Volume level changed. New level: {Level}", value);
                _volumeLevel = value;
                _xioCloudRoomGui.Main.Volume.VolumeLevel_LowerTouchfb(VolumeLevel);
                ControllerXioCloudSpaceSlotDevice.Volume.VolumeLevelFeedback.UShortValue = VolumeLevel;
            }
        }
        private bool _isInCall;
        private bool IsInCall
        {
            get => _isInCall;
            set
            {
                if (_isInCall == value) return;
                _logger.Debug("In call state changed. New state: {State}", value);
                _isInCall = value;
                ControllerXioCloudSpaceSlotDevice.Call.InCallFeedback.BoolValue = _isInCall;
                _xioCloudRoomGui.Main.Call.InCallToggle_Selected(_isInCall);
            }
        }

        private bool _isCheckedIn;
        private bool IsCheckedIn
        {
            get => _isCheckedIn;
            set
            {
                if (_isCheckedIn == value) return;
                _logger.Debug("Checked in state changed. New state: {State}", value);
                _isCheckedIn = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.CheckedInFeedback.BoolValue = _isCheckedIn;
                _xioCloudRoomGui.Main.Scheduling.CheckedInToggle_Selected(_isCheckedIn);
            }
        }

        private bool _isOnDoNotDisturb;
        private bool IsOnDoNotDisturb
        {
            get => _isOnDoNotDisturb;
            set
            {
                if (_isOnDoNotDisturb == value) return;
                _logger.Debug("Do Not Disturb state changed. New state: {State}", value);
                _isOnDoNotDisturb = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.DoNotDisturbFeedback.BoolValue = _isOnDoNotDisturb;
                _xioCloudRoomGui.Main.Scheduling.DoNotDisturbeToggle_Selected(_isOnDoNotDisturb);
            }
        }

        private bool _isInScheduledEvent;
        private bool IsInScheduledEvent
        {
            get => _isInScheduledEvent;
            set
            {
                if (_isInScheduledEvent == value) return;
                _logger.Debug("In scheduled event state changed. New state: {State}", value);
                _isInScheduledEvent = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.InScheduledEventFeedback.BoolValue = _isInScheduledEvent;
                _xioCloudRoomGui.Main.Scheduling.InScheduledEventToggle_Selected(_isInScheduledEvent);
            }
        }

        private bool _isInServiceNeeded;
        private bool IsInServiceNeeded
        {
            get => _isInServiceNeeded;
            set
            {
                if (_isInServiceNeeded == value) return;
                _logger.Debug("In service needed state changed. New state: {State}", value);
                _isInServiceNeeded = value;
                ControllerXioCloudSpaceSlotDevice.Scheduling.ServiceNeededFeedback.BoolValue = _isInServiceNeeded;
                _xioCloudRoomGui.Main.Scheduling.SeriviceNeededToggle_Selected(_isInServiceNeeded);
            }
        }

        private void CurrentReservation(ushort index)
        {
            ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStartTimeFeedback.StringValue =
                HelperClasses.DateGenerator(index);
            ControllerXioCloudSpaceSlotDevice.Scheduling.CurrentMeetingStopTimeFeedback.StringValue =
                HelperClasses.DateGenerator((ushort)(index + 1));
            _xioCloudRoomGui.Main.Scheduling.CurrentReservationText_Indirect(HelperClasses.DateGenerator(index));
            _xioCloudRoomGui.Main.Scheduling.CurrentReservationSlider_LowerTouchfb(index);
        }

        private void NextReservation(ushort index)
        {
            ControllerXioCloudSpaceSlotDevice.Scheduling.NextMeetingStartTimeFeedback.StringValue =
                HelperClasses.DateGenerator(index);
            _xioCloudRoomGui.Main.Scheduling.NextReservation_Indirect(HelperClasses.DateGenerator(index));
            _xioCloudRoomGui.Main.Scheduling.NextReservationSlider_LowerTouchfb(index);
        }

        private void ClearAllLightScenes()
        {
            ControllerXioCloudSpaceSlotDevice.Lighting.LightsOnSceneFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[2].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[3].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[4].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[5].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Lighting.LightsOffSceneFeedback.BoolValue = false;
            _xioCloudRoomGui.Main.Lights.LightsScene1_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene2_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene3_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene4_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene5_Selected(false);
            _xioCloudRoomGui.Main.Lights.LightsScene6_Selected(false);
        }

        private ushort _currentLightScene;
        private ushort CurrentLightScene
        {
            get => _currentLightScene;
            set
            {
                if (_currentLightScene == value || value > 6) return;
                _logger.Debug("Current light scene changed. New scene: {Scene}", value);
                _currentLightScene = value;
                ClearAllLightScenes();
                switch (_currentLightScene)
                {
                    case 1:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightsOnSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene1_Selected(true);
                        break;
                    case 2:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[2].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene2_Selected(true);
                        break;
                    case 3:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[3].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene3_Selected(true);
                        break;
                    case 4:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[4].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene4_Selected(true);
                        break;
                    case 5:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneFeedbacks[5].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Lights.LightsScene5_Selected(true);
                        break;
                    case 6:
                        ControllerXioCloudSpaceSlotDevice.Lighting.LightsOffSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Lighting.AnyLightOnFeedback.BoolValue = false;
                        _xioCloudRoomGui.Main.Lights.LightsScene6_Selected(true);
                        break;
                    default:
                        break;
                }
            }
        }

        public readonly string[] LightSceneNames =
        {
            "", "On", "My Light Scene 2", "My Light Scene 3", "My Light Scene 4", "My Light Scene 5", "Off"
        };

        public void ChangeLightSceneName(ushort index, string name)
        {
            _logger.Debug("Changing Light Scene Name. Index: {Index}, Name: {Name}", index, name);
            if (index < 2 || index > 5)
            {
                _logger.Warning("Invalid light scene index: {Index}. Valid range is 2 to 5", index);
                return;
            }
            switch (index)
            {
                case 2:
                    LightSceneNames[2] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene2_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[2].StringValue = name;
                    break;
                case 3:
                    LightSceneNames[3] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene3_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[3].StringValue = name;
                    break;
                case 4:
                    LightSceneNames[4] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene4_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[4].StringValue = name;
                    break;
                case 5:
                    LightSceneNames[5] = name;
                    _xioCloudRoomGui.Main.Lights.LightsScene5_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Lighting.LightSceneNames[5].StringValue = name;
                    break;
            }
        }

        private void ClearAllShadesScenes()
        {
            ControllerXioCloudSpaceSlotDevice.Shades.ShadesClosedSceneFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[2].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[3].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[4].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[5].BoolValue = false;
            ControllerXioCloudSpaceSlotDevice.Shades.ShadesOpenSceneFeedback.BoolValue = false;
            _xioCloudRoomGui.Main.Shades.ShadesScene1_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene2_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene3_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene4_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene5_Selected(false);
            _xioCloudRoomGui.Main.Shades.ShadesScene6_Selected(false);
        }

        private ushort _currentShadeScene;
        private ushort CurrentShadeScene
        {
            get => _currentShadeScene;
            set
            {
                if (_currentShadeScene == value || value > 6) return;
                _logger.Debug("Current shades scene changed. New scene: {Scene}", value);
                _currentShadeScene = value;
                ClearAllShadesScenes();
                switch (_currentShadeScene)
                {
                    case 1:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadesOpenSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene1_Selected(true);
                        break;
                    case 2:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[2].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene2_Selected(true);
                        break;
                    case 3:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[3].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene3_Selected(true);
                        break;
                    case 4:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[4].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene4_Selected(true);
                        break;
                    case 5:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneFeedbacks[5].BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = true;
                        _xioCloudRoomGui.Main.Shades.ShadesScene5_Selected(true);
                        break;
                    case 6:
                        ControllerXioCloudSpaceSlotDevice.Shades.ShadesClosedSceneFeedback.BoolValue = true;
                        ControllerXioCloudSpaceSlotDevice.Shades.AnyShadeOpenFeedback.BoolValue = false;
                        _xioCloudRoomGui.Main.Shades.ShadesScene6_Selected(true);
                        break;
                    default:
                        break;
                }
            }
        }

        public readonly string[] ShadeSceneNames =
        {
            "", "Open", "My Shade Scene 2", "My Shade Scene 3", "My Shade Scene 4", "My Shade Scene 5", "Closed"
        };

        public void ChangeShadeSceneName(ushort index, string name)
        {
            _logger.Debug("Changing Shades Scene Name. Index: {Index}, Name: {Name}", index, name);
            if (index < 2 || index > 5)
            {
                _logger.Warning("Invalid shade scene index: {Index}. Valid range is 2 to 5", index);
                return;
            }
            switch (index)
            {
                case 2:
                    ShadeSceneNames[2] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene2_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[2].StringValue = name;
                    break;
                case 3:
                    ShadeSceneNames[3] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene3_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[3].StringValue = name;
                    break;
                case 4:
                    ShadeSceneNames[4] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene4_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[4].StringValue = name;
                    break;
                case 5:
                    ShadeSceneNames[5] = name;
                    _xioCloudRoomGui.Main.Shades.ShadesScene5_Serial_CIP(name);
                    ControllerXioCloudSpaceSlotDevice.Shades.ShadeSceneNames[5].StringValue = name;
                    break;
            }
        }

        private bool _temperatureInCelsius;
        public bool TemperatureInCelsius
        {
            get => _temperatureInCelsius;
            set
            {
                _temperatureInCelsius = value;
                _logger.Debug("Temperature unit changed. Is Celsius: {IsCelsius}", value);
                _xioCloudRoomGui.Main.Climate.TemperatureInFarenheit_Selected(!_temperatureInCelsius);
                _xioCloudRoomGui.Main.Climate.TemperatrueInCelsius_Selected(_temperatureInCelsius);
                ControllerXioCloudSpaceSlotDevice.Climate.TemperatureInCelsiusFeedback.BoolValue =
                    _temperatureInCelsius;
                UpdateTemperatureDisplay();
                UpdateSetTemperatureDisplay();
            }
        }

        private ushort _currentTemperature;
        public ushort CurrentTemperature
        {
            get => _currentTemperature;
            set
            {
                if (_currentTemperature == value) return;
                _logger.Debug("Current temperature changed. New temperature: {Temperature}", value);
                _currentTemperature = value;
                _xioCloudRoomGui.Main.Climate.TemperatureCurrent_LowerTouchfb(_currentTemperature);
                UpdateTemperatureDisplay();
            }
        }

        private void UpdateTemperatureDisplay()
        {
            if (TemperatureInCelsius)
            {
                _xioCloudRoomGui.Main.Climate.TemperatureCurrentText_Indirect(
                    $"{HelperClasses.FahrenheitToCelsius(CurrentTemperature) / 10.0:F1} °C"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.CurrentTemperatureFeedback.UShortValue =
                    HelperClasses.FahrenheitToCelsius(CurrentTemperature);
            }
            else
            {
                _xioCloudRoomGui.Main.Climate.TemperatureCurrentText_Indirect(
                    $"{_currentTemperature / 10.0:F1} °F"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.CurrentTemperatureFeedback.UShortValue = _currentTemperature;
            }
        }

        private ushort _setTemperature ;
        public ushort SetTemperature
        {
            get => _setTemperature;
            set
            {
                if (_setTemperature == value) return;
                _logger.Debug("Set temperature changed. New temperature: {Temperature}", value);
                _setTemperature = value;
                _xioCloudRoomGui.Main.Climate.TemperatureSet_LowerTouchfb(_setTemperature);
                UpdateSetTemperatureDisplay();
            }
        }
        
        private void UpdateSetTemperatureDisplay()
        {
            if (TemperatureInCelsius)
            {
                _xioCloudRoomGui.Main.Climate.TemperatureSetText_Indirect(
                    $"{HelperClasses.FahrenheitToCelsius(SetTemperature) / 10.0:F1} °C"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.TemperatureSetpointFeedback.UShortValue =
                    HelperClasses.FahrenheitToCelsius(SetTemperature);
            }
            else
            {
                _xioCloudRoomGui.Main.Climate.TemperatureSetText_Indirect(
                    $"{SetTemperature / 10.0:F1} °F"
                );
                ControllerXioCloudSpaceSlotDevice.Climate.TemperatureSetpointFeedback.UShortValue = SetTemperature;
            }
        }

        private ushort _currentHumidity = 500;
        public ushort CurrentHumidity
        {
            get => _currentHumidity;
            set
            {
                if (_currentHumidity == value) return;
                _logger.Debug("Current humidity changed. New humidity: {Humidity}", value);
                _currentHumidity = value;
                _xioCloudRoomGui.Main.Climate.HumidityCurrent_LowerTouchfb(_currentHumidity);
                ControllerXioCloudSpaceSlotDevice.Climate.CurrentHumidityFeedback.UShortValue = _currentHumidity;
            }
        }
        
        private ushort _setHumidity = 500;
        public ushort SetHumidity
        {
            get => _setHumidity;
            set
            {
                if (_setHumidity == value) return;
                _logger.Debug("Set humidity changed. New humidity: {Humidity}", value);
                _setHumidity = value;
                _xioCloudRoomGui.Main.Climate.HumiditySet_LowerTouchfb(_setHumidity);
                ControllerXioCloudSpaceSlotDevice.Climate.HumiditySetpointFeedback.UShortValue = _setHumidity;
            }
        }

        public readonly string[] ClimateModes = { "", "Off", "Heat", "Aux Heat", "Cool", "Auto" };

        private ushort _currentClimateMode = 0;
        public ushort CurrentClimateMode
        {
            get => _currentClimateMode;
            set
            {
                if (_currentClimateMode == value || value == 0 || value > 5) return;
                _logger.Debug("Current climate mode changed. New mode: {Mode}", ClimateModes[value]);
                _currentClimateMode = value;
                _xioCloudRoomGui.Main.Climate.ClimateMode_LowerTouchfb(_currentClimateMode);
                _xioCloudRoomGui.Main.Climate.ClientModeText_Indirect(ClimateModes[_currentClimateMode]);
                ControllerXioCloudSpaceSlotDevice.Climate.ClimateModeFeedback.UShortValue = _currentClimateMode;
            }
        }
    }
}