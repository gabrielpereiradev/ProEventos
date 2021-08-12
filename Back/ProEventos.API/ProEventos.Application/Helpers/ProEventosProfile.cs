using AutoMapper;
using ProEventos.Application.DTOS;
using ProEventos.Domain;

namespace ProEventos.API.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
        }
    }
}
