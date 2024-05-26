using AutoMapper;
using InsideAutoManagement.DTO;
using InsideAutoManagement.Models;
using Microsoft.IdentityModel.Tokens;

namespace InsideAutoManagement.Mapper
{
    public class InsideAutoMapperProfile : Profile
    {
        public InsideAutoMapperProfile()
        {
            CreateMap<OpeningHoursShift, OpeningHoursShiftDTO>()
              .ReverseMap()
              .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => GetTimeSpan(src.StartTime)))
              .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => GetTimeSpan(src.EndTime)));

            CreateMap<CarDealer, CarDealerDTO>()
                .ReverseMap()
                .ForMember(dest => dest.OpeningHoursShifts, opt => opt.MapFrom(src => src.OpeningHoursShifts));

            CreateMap<Car, CarDTO>()
                .ReverseMap()
                .ForMember(dest=>dest.CarDealer, opt => opt.MapFrom(src => src.CarDealer));
        }

        //internal IEnumerable<OpeningHoursShift>? MapOpeningHoursShifts(IEnumerable<OpeningHoursShiftDTO>? ohss)
        //{
        //    if (ohss == null)
        //        return null;

        //    List<OpeningHoursShift> result = new List<OpeningHoursShift>();
        //    foreach (var ohs in ohss)
        //    {
        //        result.Add(MapOpeningHoursShift(ohs));
        //    }
        //    return result;
        //}

        //internal OpeningHoursShift MapOpeningHoursShift(OpeningHoursShiftDTO ohs)
        //    => new OpeningHoursShift
        //    {
        //            DayOfWeek = ohs.DayOfWeek,
        //            StartTime = GetTimeSpan(ohs.StartTime),
        //            EndTime = GetTimeSpan(ohs.EndTime)

        //    };

        internal TimeSpan GetTimeSpan(string str)
        {
            if (str.IsNullOrEmpty() || !str.Contains(":") || str.Length != 5)
                throw new ArgumentException();

            string[] timeStr = str.Split(":");

            if (timeStr.Length != 2)
                throw new ArgumentException();

            int[] timeInt = new int[timeStr.Length];

            for (int i = 0; i < timeStr.Length; i++)
            {
                timeInt[i] = int.Parse(timeStr[i]);
            }

            return new TimeSpan(timeInt[0], timeInt[1], 0);
        }
    }
}
