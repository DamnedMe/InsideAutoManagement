using InsideAutoManagement.Enums;

namespace InsideAutoManagement.Model
{
    public class Configuration
    {
        public Guid Id { get; set; }
        public CarDealer? CarDealer { get; set; }

        public string Name { get; set; } = null!;
        public ConfigurationValueType ValueType { get; set; }

        public string? StringValue { get; set; } = null;
        public double? NumberValue { get; set; } = null;
        public bool? BoolValue { get; set; } = null;
        public DateTime? DateTimeValue { get; set; } = null;
    }
}
