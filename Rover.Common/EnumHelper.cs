using System;

namespace Rover.Common
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T val) where T : Enum, IConvertible
        {
            var res = string.Empty;
            var enumValues = Enum.GetValues(val.GetType());

            foreach (T enumValue in enumValues)
            {
                if (val.Equals(enumValue))
                {
                    var fi = val.GetType().GetField(val.ToString());
                    var attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        res = attributes[0].Description;
                    }
                    else
                    {
                        res = val.ToString();
                    }
                }
            }

            return res;
        }

        public static T GetEnumValue<T>(string val) where T : Enum, IConvertible
        {
            T res = default(T);

            var enumValues = Enum.GetValues(typeof(T));

            foreach (T enumValue in enumValues)
            {
                if (val.Equals(enumValue.GetDescription()))
                {
                    res = enumValue;
                }
            }

            return res;
        }
    }
}
