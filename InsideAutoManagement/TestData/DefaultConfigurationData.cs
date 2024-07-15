using InsideAutoManagement.Enums;
using InsideAutoManagement.Model;

namespace InsideAutoManagement.TestData
{
    public static class DefaultConfigurationData
    {
        public static List<Configuration> GetDefaultConfigurationData(CarDealer defaultCarDealer)
        {
            var configurations = new List<Configuration>
            {
                new Configuration
                {
                    Name = "DefaultPath",
                    CarDealer = defaultCarDealer,
                    ValueType = ConfigurationValueType.String,
                    StringValue = @"C:\InsideAutoManagement\temp"
                }
            };

            return configurations;
        }
    }
}