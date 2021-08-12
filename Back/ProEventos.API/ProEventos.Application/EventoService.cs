using AutoMapper;
using ProEventos.Application.DTOS;
using ProEventos.Application.Interface;
using ProEventos.Domain;
using ProEventos.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;

        public EventoService(IGeralRepository geralRepository, IEventoRepository eventoRepository, IMapper mapper)
        {
            _geralRepository = geralRepository;
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _geralRepository.Add<Evento>(evento);
                if(await _geralRepository.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoRepository.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _geralRepository.Update<Evento>(evento);
                if(await _geralRepository.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoRepository.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                _geralRepository.Delete<Evento>(evento);
                return (await _geralRepository.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes)
        {
            try
            {
                var allEventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);

                if (allEventos == null) return null;

                var result = _mapper.Map<EventoDto[]>(allEventos);

                return result;
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var allEventos = await _eventoRepository.GetAllEventosByTemaAsync(tema,includePalestrantes);

                if (allEventos == null) return null;

                var result = _mapper.Map<EventoDto[]>(allEventos);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, includePalestrantes);

                if (evento == null) return null;

                var result = _mapper.Map<EventoDto>(evento);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
