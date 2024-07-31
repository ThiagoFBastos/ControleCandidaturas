using AutoMapper;
using ControleCandidaturas.Models;
using ControleCandidaturas.DTOs;

namespace ControleCandidaturas.Repositories.Map
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Candidatura, CandidaturaDTO>()
                .ForMember(e => e.Status, opt => opt.MapFrom(x => (int)x.Status))
                .ForMember(e => e.Grau, opt => opt.MapFrom(x => (int?)x.Grau))
                .ForMember(e => e.Modo, opt => opt.MapFrom(x => (int?)x.Modo));

            CreateMap<CandidaturaRequestDTO, Candidatura>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.DataSubmissao, opt => opt.Ignore())
                .ForMember(e => e.Status, opt => opt.MapFrom(x => (Candidatura.CandidaturaStatus)x.Status))
                .ForMember(e => e.Grau, opt => opt.MapFrom(x => (Candidatura.Senioridade?)x.Grau))
                .ForMember(e => e.Modo, opt => opt.MapFrom(x => (Candidatura.ModoDeTrabalho?)x.Modo));

            CreateMap<CandidaturaRequestDTO, CandidaturaIntermediateDTO>()
                .ForMember(e => e.Status, opt => opt.MapFrom(x => (Candidatura.CandidaturaStatus)x.Status))
                .ForMember(e => e.Grau, opt => opt.MapFrom(x => (Candidatura.Senioridade?)x.Grau))
                .ForMember(e => e.Modo, opt => opt.MapFrom(x => (Candidatura.ModoDeTrabalho?)x.Modo));

            CreateMap<Relatorio, RelatorioDTO>();

            CreateMap<RelatorioRequestDTO, Relatorio>()
                .ForMember(e => e.Candidatura, opt => opt.Ignore())
                .ForMember(e => e.CandidaturaId, opt => opt.MapFrom(x => x.CandidaturaId ?? Guid.Empty))
                .ForMember(e => e.DataCadastro, opt => opt.Ignore())
                .ForMember(e => e.Id, opt => opt.Ignore());
        }
    }
}
