using AutoMapper;
using CarteiraAtivos.Models;
using CarteiraAtivos.Dtos;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<AtivoApiDto, AtivoModel>()
         .ReverseMap(); // Permite via dupla de conversão

      CreateMap<LoginUsuarioIndexDto, LoginUsuarioModel>();

      CreateMap<LoginUsuarioCreateDto, LoginUsuarioModel>();

      CreateMap<AtivoCreateDto, AtivoModel>()
         .ReverseMap();

      CreateMap<AtivoNegociarDto, AtivoCreateDto>();
   }
}