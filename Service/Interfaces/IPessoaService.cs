using Domain;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPessoaService
    {
        Task<Response> Insert(Pessoa p);
        Task<Response> Update(Pessoa p);
        Task<Response> Exists(Pessoa p);
        Task<DataResponse<Pessoa>> GetAll();
        Task<SingleResponse<Pessoa>> GetByID(int id);
        Task<SingleResponse<Pessoa>> Authenticate(string email, string senha);
    }
}
