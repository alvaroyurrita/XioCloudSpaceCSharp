using Crestron.SimplSharpPro;
using System.Collections.Generic;
using System.Linq;
using XIOCloudRoomGUI;

namespace XioCloudCSharp
{
    public static class HelperClasses
    {
        public static Dictionary<int, string> XioCloudSpaceEvent()
        {
            var fields = typeof(XioCloudSpaceEventIds).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            var dictionary = fields
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(int))
                .ToDictionary(
                    f => (int)f.GetValue(null),    // Value: The constant integer value
                    f => f.Name                  // Key: Name of the constant
                );
            return dictionary;
        }

        public static bool GetContractSignalBoolState(UIEventArgs args)
        {
            if (args.SigArgs.Event == eSigEvent.BoolChange)
            {
                return args.SigArgs.Sig.BoolValue;
            }
            else
            {
                return false;
            }
        }

        public static bool Boolean(this UIEventArgs eventArgs) => 
            eventArgs.SigArgs.Event == eSigEvent.BoolChange && eventArgs.SigArgs.Sig.BoolValue;
        
        public static ushort UShort(this UIEventArgs eventArgs) =>
            eventArgs.SigArgs.Event == eSigEvent.UShortChange ? eventArgs.SigArgs.Sig.UShortValue : (ushort)0;
        
        public static string String(this UIEventArgs eventArgs) =>
            eventArgs.SigArgs.Event == eSigEvent.StringChange ? eventArgs.SigArgs.Sig.StringValue : string.Empty;
    }
}