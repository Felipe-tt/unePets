using Domain;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAnuncioService
    {
        Task<Response> Insert(Anuncio a);
        Task<Response> Update(Anuncio a);
        Task<DataResponse<Anuncio>> GetAll();
        Task<SingleResponse<Anuncio>> GetByID(int id);
        Task<Response> VincularInteresse(int idPessoaInteressada, int idAnuncio);
        Task<Response> DoarAnimal(int id);
        Task<Response> Excluir(int id);

    }
}
