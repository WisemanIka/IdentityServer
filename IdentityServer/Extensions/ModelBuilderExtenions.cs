using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IdentityServer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class ModelBuilderExtenions
    {
        public static void AddEntityConfigurations(this ModelBuilder builder, Assembly assembly, string context)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>), context);

            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityConfiguration>())
            {
                config.Map(builder);
            }
        }

        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface, string context)
        {
            return assembly.GetTypes().Where(type => !type.GetTypeInfo().IsAbstract && type.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface) && type.Namespace.Contains(context));
        }
    }
}
