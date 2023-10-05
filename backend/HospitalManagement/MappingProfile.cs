using AutoMapper;
using Entities.Models;
using Shared.DTO.Request;
using Shared.DTO.Response;

namespace HospitalManagement;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Register
        CreateMap<User, UserRegisterDTO>().ReverseMap();

        //User
        CreateMap<UserDetailsDTO, User>().ReverseMap();
        CreateMap<UserListDTO, User>().ReverseMap();
        CreateMap<User, UserListDTO>().ReverseMap();
        CreateMap<User, AddUserDTO>().ReverseMap();

        //Service
        CreateMap<Services, ServiceRequestDTO>().ReverseMap();
        CreateMap<ServiceResponseDTO, Services>().ReverseMap();

        //Equipment
        CreateMap<Equipment, EquipmentRequestDTO>().ReverseMap();
        CreateMap<EquipmentResponseDTO, Equipment>().ReverseMap();

        CreateMap<WorkingHours, WorkingHoursDTO>().ReverseMap();
        CreateMap<WorkingHoursDTO,WorkingHours>().ReverseMap();
 

        //OffDays
        //CreateMap<OffDays, OffDaysDTO>().ReverseMap();
        //CreateMap<OffDaysDTO, OffDays>().ReverseMap();

        //Staff
        CreateMap<StaffResponseDTO, ServiceStaff>().ReverseMap();

        //Reservation
        CreateMap<Reservation, ReservationResponseDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => string.Format($"{src.Client.FirstName}")))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => string.Format($"{src.Client.LastName} ")))
            .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => string.Format($"{src.Client.IdentityNumber} ")))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => string.Format($"{src.Client.Gender}")))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => string.Format($"{src.Client.Birthday} ")))
            .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => string.Format($"{src.Client.IdentityNumber} ")))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => string.Format($"{src.Client.Email} ")))
            .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => string.Format($"{src.Reason} ")))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => string.Format($"{src.StartTime} ")))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => string.Format($"{src.Date} ")))
            .ReverseMap();
    }
}
