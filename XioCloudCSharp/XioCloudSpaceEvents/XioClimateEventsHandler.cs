using Crestron.SimplSharpPro;
using System;
using System.Linq;

namespace XioCloudCSharp
{
    public partial class ControlSystem
    {
        private void XioClimateEventsHandler(int eventId)
        {
            var xioClimate = ControllerXioCloudSpaceSlotDevice.Climate;

            switch (eventId)
            {
                case XioCloudSpaceEventIds.TemperatureInCelsiusEventId:
                    _logger.Debug("Temperature in Celsius event received");
                    TemperatureInCelsius = true;
                    break;
                case XioCloudSpaceEventIds.TemperatureInFahrenheitEventId:
                    _logger.Debug("Temperature in Fahrenheit event received");
                    TemperatureInCelsius = false;
                    break;
                case XioCloudSpaceEventIds.TemperatureSetpointEventId:
                    _logger.Debug("Temperature setpoint event received");
                    SetTemperature = TemperatureInCelsius ? 
                        HelperClasses.CelsiusToFahrenheit(xioClimate.TemperatureSetpoint.UShortValue) : 
                        xioClimate.TemperatureSetpoint.UShortValue;
                    break;
                case XioCloudSpaceEventIds.HumiditySetpointEventId:
                    _logger.Debug("Humidity setpoint event received");
                    SetHumidity = xioClimate.HumiditySetpoint.UShortValue;
                    break;
            }
        }
    }
}