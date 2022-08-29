using Domain;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IChatService
    {
        Task<Response> Insert(Mensagem a);
        Task<Response> Update(Mensagem a);
        Task<DataResponse<Mensagem>> GetAll();
        Task<SingleResponse<Mensagem>> GetByID(int id);
        Task<Response> Excluir(int id);
    }
}
