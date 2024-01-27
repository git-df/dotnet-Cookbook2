using Application.Common;
using AutoMapper;
using System.Reflection;

namespace Application.Helpers
{
    public static class MapperHelper
    {
        public static MapperConfiguration CreateMappingConfiguration()
        {
            var configuration = new MapperConfiguration(config =>
            {
                foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (type.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                    {
                        var instance = Activator.CreateInstance(type);
                        var methodInfo = type.GetMethod("Mapping")
                            ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                        methodInfo?.Invoke(instance, new object[] { config });
                    }
                }
            });

            return configuration;
        }
    }
}
