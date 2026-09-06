using Crestron.SimplSharpPro;

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
        
    }
}