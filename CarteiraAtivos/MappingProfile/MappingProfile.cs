using AutoMapper;
using CarteiraAtivos.Models;
using CarteiraAtivos.Dtos;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<AtivoApiDto, AtivoModel>()
         .ReverseMap();
      CreateMap<LoginUsuarioIndexDto, LoginUsuarioModel>();
      CreateMap<LoginUsuarioCreateDto, LoginUsuarioModel>();
      CreateMap<AtivoCreateDto, AtivoModel>()
         .ReverseMap();
   }
}