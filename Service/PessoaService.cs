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
using System.Web.Helpers;

namespace Service
{
    public class PessoaService : IPessoaService
    {
        public async Task<SingleResponse<Pessoa>> Authenticate(string email, string senha)
        {
            SingleResponse<Pessoa> resposta = new SingleResponse<Pessoa>();

            using (UnePetsDBContext db = new UnePetsDBContext())
            {
                Pessoa p = db.Pessoas.FirstOrDefault(p => p.Email == email);
                if (p == null || !Crypto.VerifyHashedPassword(p.Senha, senha))
                {
                    resposta.Sucesso = false;
                    resposta.Mensagem = "Email ou senha inválidos.";
                    return resposta;
                }
                resposta.Sucesso = true;
                resposta.Mensagem = "Usuário autenticado com sucesso.";
                resposta.Item = p;
                return resposta;
            }
        }

        public async Task<Response> Exists(Pessoa pessoa)
        {
            using (var db = new UnePetsDBContext())
            {
                Pessoa pessoaCadastrado = await db.Pessoas
                   .FirstOrDefaultAsync(p =>
                    p.CPF == pessoa.CPF ||
                    p.Email == pessoa.Email);

                if (pessoaCadastrado != null)
                {
                    //Retorna pois a chave única estouraria este registro
                    return new Response()
                    {
                        Sucesso = false,
                        Mensagem = "CPF ou email já está em uso."
                    };
                }
                return new Response()
                {
                    Mensagem = "Usuário único!",
                    Sucesso = true
                };
            }
        }

        public async Task<DataResponse<Pessoa>> GetAll()
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    List<Pessoa> pessoas = await db.Pessoas.ToListAsync();
                    DataResponse<Pessoa> responsePessoas = new DataResponse<Pessoa>();
                    responsePessoas.Data = pessoas;
                    responsePessoas.Mensagem = "Pessoas selecionadas com sucesso.";
                    responsePessoas.Sucesso = true;
                    return responsePessoas;
                }
            }
            catch
            {
                return ResponseFactory.SummonResponseDatabaseDataError<Pessoa>();
            }
        }

        public async Task<SingleResponse<Pessoa>> GetByID(int id)
        {
            try
            {
                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Pessoa pessoa = await db.Pessoas.Include(c=> c.Anuncios).Include(c=> c.Interesses).FirstOrDefaultAsync(c => c.ID == id);
                    SingleResponse<Pessoa> responsePessoas = new SingleResponse<Pessoa>();
                    responsePessoas.Item = pessoa;
                    responsePessoas.Mensagem = "Pessoa selecionada com sucesso.";
                    responsePessoas.Sucesso = true;
                    return responsePessoas;
                }
            }
            catch
            {
                return ResponseFactory.SummonResponseDatabaseSingleError<Pessoa>();
            }
        }

        public async Task<Response> Insert(Pessoa pessoa)
        {
            PessoaValidator validation = new PessoaValidator();
            ValidationResult result = validation.Validate(pessoa);
            pessoa.CEP = pessoa.CEP.Replace("-", "");
            Response r = result.ToResponse();
            if (!r.Sucesso)
            {
                return r;
            }

            try
            {

                Response responseExists = await this.Exists(pessoa);
                if (!responseExists.Sucesso)
                {
                    return responseExists;
                }

                using (var db = new UnePetsDBContext())
                {
                    pessoa.Senha = Crypto.HashPassword(pessoa.Senha);
                    db.Pessoas.Add(pessoa);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Sucesso = true,
                        Mensagem = "Usuário cadastrado com sucesso."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.SummonResponseDatabaseError();
            }
        }
        public async Task<Response> Update(Pessoa p)
        {
            PessoaValidator validation = new PessoaValidator();
            ValidationResult result = validation.Validate(p);

            Response r = result.ToResponse();
            if (!r.Sucesso)
            {
                return r;
            }
            try
            {
                Response responseExists = await this.Exists(p);
                if (!responseExists.Sucesso)
                {
                    return responseExists;
                }


                using (UnePetsDBContext db = new UnePetsDBContext())
                {
                    Pessoa pessoaBanco = await db.Pessoas.FindAsync(p.ID);
                    pessoaBanco.CEP = p.CEP;
                    pessoaBanco.Cidade = p.Cidade;
                    pessoaBanco.UF = p.UF;
                    pessoaBanco.Telefone = p.Telefone;
                    pessoaBanco.Senha = Crypto.HashPassword(p.Senha);

                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Sucesso = true,
                        Mensagem = "Usuário editado com sucesso."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.SummonResponseDatabaseError();
            }
        }
    }
}
