using AutoMapper;
using Domain;
using MVCPresentationLayer.Models.Anuncio;
using MVCPresentationLayer.Models.Chat;
using MVCPresentationLayer.Models.Pessoa;

namespace MVCPresentationLayer.Infrastructure
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<PessoaInsertViewModel, Pessoa>();
            CreateMap<Pessoa, PessoaQueryViewModel>();
            CreateMap<PessoaUpdateViewModel, Pessoa>();
            CreateMap<AnuncioInsertViewModel, Anuncio>();
            CreateMap<Anuncio, DetalhesQueryViewModel>();
            CreateMap<DetalhesQueryViewModel, Anuncio>();
            CreateMap<ChatInsertViewModel, Mensagem>();
            CreateMap<ChatQueryViewModel, Mensagem>();
            CreateMap<Mensagem, ChatQueryViewModel>();
            CreateMap<Pessoa, PessoaUpdateViewModel>();
            CreateMap<PessoaUpdateViewModel, Pessoa>();
            CreateMap<Pessoa, PessoaProfileViewModel>();
            CreateMap<Anuncio, PessoaProfileViewModel>();

        }
    }
}
