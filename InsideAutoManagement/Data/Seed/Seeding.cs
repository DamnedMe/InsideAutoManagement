using InsideAutoManagement.TestData;

namespace InsideAutoManagement.Data.Seed
{
    public static class Seeding
    {
        public static void Initialize(InsideAutoManagementContext context)
        {
            bool isChanged = false;
            context.Database.EnsureCreated();

            var carDealer = CarDealersData.GetCarDealersData();

            if (context.CarDealers.Any() == false)
            {
                context.CarDealers.AddRange(carDealer);
                isChanged = true;
            }

            if (context.Configurations.Any() == false)
            {
                context.Configurations.AddRange(DefaultConfigurationData.GetDefaultConfigurationData(context.CarDealers.FirstOrDefault() ?? carDealer.First()));
                isChanged = true;
            }

            if (isChanged)
                context.SaveChanges();
        }
    }
}