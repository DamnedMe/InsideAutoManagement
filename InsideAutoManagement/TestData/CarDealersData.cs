using InsideAutoManagement.Model;

namespace InsideAutoManagement.TestData
{
    public static class CarDealersData
    {
        public static List<CarDealer> GetCarDealersData()
        {
            var carDealers = new List<CarDealer>
            {
                new CarDealer
                {
                    Id = new Guid(),
                    Name = "InsideAuto",
                    PIVA = "12345678901",
                    Phone = "+39 377 965 2192",
                    Email = "insideauto74@gmail.com",
                    City = "Conversano",
                    Country = "Italy",
                    Adress = "Via San Lorenzo NC",
                    OpeningHoursShifts = new List<OpeningHoursShift>
                    {
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Monday,
                            StartTime = new TimeSpan(8, 0, 0),
                            EndTime = new TimeSpan(12, 30, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Monday,
                            StartTime = new TimeSpan(15, 0, 0),
                            EndTime = new TimeSpan(20, 0, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Tuesday,
                            StartTime = new TimeSpan(8, 0, 0),
                            EndTime = new TimeSpan(12, 30, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Tuesday,
                            StartTime = new TimeSpan(15, 0, 0),
                            EndTime = new TimeSpan(20, 0, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Wednesday,
                            StartTime = new TimeSpan(8, 0, 0),
                            EndTime = new TimeSpan(12, 30, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Wednesday,
                            StartTime = new TimeSpan(15, 0, 0),
                            EndTime = new TimeSpan(20, 0, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Thursday,
                            StartTime = new TimeSpan(8, 0, 0),
                            EndTime = new TimeSpan(12, 30, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Thursday,
                            StartTime = new TimeSpan(15, 0, 0),
                            EndTime = new TimeSpan(20, 0, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Friday,
                            StartTime = new TimeSpan(8, 0, 0),
                            EndTime = new TimeSpan(12, 30, 0)
                        },
                        new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Friday,
                            StartTime = new TimeSpan(15, 0, 0),
                            EndTime = new TimeSpan(20, 0, 0)
                        },
                         new OpeningHoursShift
                        {
                            DayOfWeek = DayOfWeek.Saturday,
                            StartTime = new TimeSpan(9, 0, 0),
                            EndTime = new TimeSpan(12, 0, 0)
                        }
                    }
                }
            };

            return carDealers;
        }
    }
}