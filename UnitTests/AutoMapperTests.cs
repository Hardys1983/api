using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class AutoMapperTest
    {
        [Fact]
        public void IsValid()
        {
            var mappings = new MapperConfigurationExpression();

            //VS Issue while searching for assemblies, shouldn't exclude any assembly
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => assembly.FullName.Contains("API"));

            var types = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type == typeof(Profile)
                    && !type.IsAbstract
                    && type.IsPublic);

            foreach (var type in types)
            {
                mappings.AddProfile(type);
            }

            Mapper.Initialize(mappings);
            Mapper.AssertConfigurationIsValid();
        }
    }
}
