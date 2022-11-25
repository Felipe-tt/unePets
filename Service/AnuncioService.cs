using DataInfrastructure;
using Domain;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Service.Extensions;
using Service.Interfaces;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{

    public class AnuncioService : IAnuncioService
    {
        public async Task<Response> Insert(Anuncio anuncio)
        {
            AnuncioValidator validator = new AnuncioValidator();
            ValidationResult result = validator.Validate(anuncio);

            Response r = result.ToResponse();
            if (!r.Sucesso)
            {
                return r;
            }

            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    anuncio.DataAnuncio = DateTime.Now;
                    anuncio.Status = Domain.Enum.Status.Disponivel;
                    db.Anuncios.Add(anuncio);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Sucesso = true,
                        Mensagem = "Anuncio realizado com sucesso."

                    };
                }
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning disable CS0168
            {
                return new Response()
                {
                    Sucesso = false,
                    Mensagem = "Erro no banco de dados, contate o administrador."
                };
            }
        }
        public async Task<SingleResponse<Anuncio>> GetByID(int id)
        {
            SingleResponse<Anuncio> response = new SingleResponse<Anuncio>();

            try
            {

                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Anuncio anuncio = await db.Anuncios.Include(c => c.PessoasInteressadas).Include(c => c.Pessoa).FirstOrDefaultAsync(c => c.ID == id);
                    if (anuncio == null)
                    {
                        response.Sucesso = false;
                        response.Mensagem = "Anuncio não encontrado.";
                        return response;
                    }
                    response.Sucesso = true;
                    response.Mensagem = "Anuncio selecionado com sucesso.";
                    response.Item = anuncio;
                }
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning disable CS0168
            {
                response.Sucesso = false;
                response.Mensagem = "Erro no banco de dados, contate o adm.";
            }
            return response;
        }

        public async Task<Response> Update(Anuncio a)
        {
            AnuncioValidator validation = new AnuncioValidator();
            ValidationResult result = validation.Validate(a);

            Response r = result.ToResponse();
            if (!r.Sucesso)
            {
                return r;
            }
            try
            {

                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Anuncio anuncioBanco = await db.Anuncios.FindAsync(a.ID);
                    anuncioBanco.DataNascimento = a.DataNascimento;
                    anuncioBanco.Descricao = a.Descricao;
                    anuncioBanco.EhCastrado = a.EhCastrado;
                    anuncioBanco.EhDeficiente = a.EhDeficiente;
                    anuncioBanco.Nome = a.Nome;
                    anuncioBanco.Porte = a.Porte;
                    anuncioBanco.Sexo = a.Sexo;
                    anuncioBanco.Tipo = a.Tipo;



                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Sucesso = true,
                        Mensagem = "Anúncio editado com sucesso."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.SummonResponseDatabaseError();
            }
        }

        public async Task<DataResponse<Anuncio>> GetAll()
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    List<Anuncio> anuncios = await db.Anuncios.Include(c => c.PessoasInteressadas).Where(c => c.Status != Domain.Enum.Status.Adotado).ToListAsync();
                    DataResponse<Anuncio> responseAnuncios = new DataResponse<Anuncio>();
                    responseAnuncios.Data = anuncios;
                    responseAnuncios.Mensagem = "Anúncios selecionados com sucesso.";
                    responseAnuncios.Sucesso = true;
                    return responseAnuncios;
                }
            }
            catch
            {
                return ResponseFactory.SummonResponseDatabaseDataError<Anuncio>();
            }
        }

        public async Task<Response> VincularInteresse(int idPessoaInteressada, int idAnuncio)
        {
            using (UnePetsDBContext db = new UnePetsDBContext())
            {
                Anuncio anuncio = await db.Anuncios.Include(c => c.PessoasInteressadas).FirstOrDefaultAsync(c => c.ID == idAnuncio);
                if (anuncio == null)
                {
                    return new Response()
                    {
                        Mensagem = "Anuncio não encontrado",
                        Sucesso = false
                    };
                }

                if (anuncio.PessoasInteressadas.FirstOrDefault(c => c.ID == idPessoaInteressada) != null)
                {
                    return new Response()
                    {
                        Mensagem = "Interesse já marcado.",
                        Sucesso = false
                    };
                }

                if (anuncio.PessoaID == idPessoaInteressada)
                {
                    return new Response()
                    {
                        Mensagem = "Não é possível marcar interesse no seu anúncio.",
                        Sucesso = false
                    };
                }

                anuncio.PessoasInteressadas.Add(db.Pessoas.Find(idPessoaInteressada));

                await db.SaveChangesAsync();
                return ResponseFactory.SummonResponsePersonRegisteredSuccessfully();
            }

        }

        public async Task<Response> DoarAnimal(int id)
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Anuncio anuncio = await db.Anuncios.FirstOrDefaultAsync(c => c.ID == id);
                    anuncio.Status = Domain.Enum.Status.Adotado;
                    await db.SaveChangesAsync();
                    return ResponseFactory.SummonResponseSuccess();
                }
            }
            catch
            {
                return ResponseFactory.SummonResponseDatabaseError();
            }

        }

        public async Task<Response> Excluir(int id)
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Anuncio a = new Anuncio();
                    a.ID = id;
                    db.Entry(a).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    return ResponseFactory.SummonResponseSuccess();
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.SummonResponseDatabaseError();
            }
        }
    }
}

