using InsideAutoManagement.TestData;

namespace InsideAutoManagement.Data.Seed
{
    public static class Seeding
    {
        public static void Initialize(InsideAutoManagementContext context)
        {
            bool isChanged = false;
            context.Database.EnsureCreated();

            if (context.CarDealers.Any() == false)
            {
                context.CarDealers.AddRange(CarDealersData.GetCarDealersData());
                isChanged = true;
            }

            if (context.Configurations.Any() == false)
            {
                context.Configurations.AddRange(DefaultConfigurationData.GetDefaultConfigurationData());
                isChanged = true;
            }

            if (isChanged)
                context.SaveChanges();
        }
    }
}