using DataInfrastructure;
using Domain;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Service.Extensions;
using Service.Interfaces;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Service
{
    public class ChatService : IChatService
    {
        public async Task<DataResponse<Mensagem>> GetAll()
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    List<Mensagem> mensagens = await db.Mensagens.ToListAsync();
                    DataResponse<Mensagem> responseMensagens = new DataResponse<Mensagem>();
                    responseMensagens.Data = mensagens;
                    responseMensagens.Mensagem = "Pessoas selecionadas com sucesso.";
                    responseMensagens.Sucesso = true;
                    return responseMensagens;
                }
            }
            catch
            {
                return ResponseFactory.SummonResponseDatabaseDataError<Mensagem>();
            }
        }

        public async Task<SingleResponse<Mensagem>> GetByID(int id)
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Mensagem mensagens = await db.Mensagens.FindAsync(id);
                    SingleResponse<Mensagem> responseMensagens = new SingleResponse<Mensagem>();
                    responseMensagens.Item = mensagens;
                    responseMensagens.Mensagem = "Conversa selecionada com sucesso.";
                    responseMensagens.Sucesso = true;
                    return responseMensagens;
                }
            }
            catch
            {
                return ResponseFactory.SummonResponseDatabaseSingleError<Mensagem>();
            }
        }

        public async Task<Response> Insert(Mensagem a)
        {
            MensagemValidator validation = new MensagemValidator();
            ValidationResult result = validation.Validate(a);
            Response r = result.ToResponse();
            if (!r.Sucesso)
            {
                return r;
            }
            try
            {
                using (var db = new UnePetsDBContext())
                {



                    db.Mensagens.Add(a);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Sucesso = true,
                        Mensagem = "Mensagem enviada com sucesso :)."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.SummonResponseDatabaseError();
            }
        }

        public Task<Response> Update(Mensagem a)
        {
            throw new NotImplementedException();
        }
    }
}





