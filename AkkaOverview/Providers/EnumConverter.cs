using System;

namespace AkkaOverview.Providers
{
    public static class EnumConverter
    {
        public static T ConvertTo<T>(this object value) where T : struct, IConvertible
        {
            var sourcetype = value.GetType();
            if (!sourcetype.IsEnum && !typeof(T).IsEnum)
                throw new ArgumentException(string.Format($"Either {sourcetype.FullName} or {typeof(T).FullName} is not an Enum"));

            return (T)Enum.Parse(typeof(T), value.ToString());
        }
    }
}
