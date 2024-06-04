using CanguroApi.DTO;
using CanguroApi.Helpers;

namespace CanguroApi.Aplicattions.Contracts
{
    public interface ICanguroAppService
    {
        Request<List<MovCanguroDTO>> GetCanguro();
        Request<MovCanguroDTO> UpdateCanguro(MovCanguroDTO movCanguroDTO);
        Request<string> DeleteCanguro(int codigo);
        Request<MovCanguroDTO> AddCanguro(MovCanguroDTO movCanguroDTO);
        Request<bool> GetUsuario(string nombre, string password);
    }
}
