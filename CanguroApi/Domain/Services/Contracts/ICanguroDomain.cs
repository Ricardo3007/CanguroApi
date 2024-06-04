using CanguroApi.Domain.Entities;
using CanguroApi.DTO;

namespace CanguroApi.Domain.Services.Contracts
{
    public interface ICanguroDomain
    {
        List<MovCanguro> GetCanguro();
        bool UpdateCanguro(MovCanguro movCanguroUpdate);
        bool DeleteCanguro(int codigo);
        bool AddCanguro(MovCanguro movCanguroAdd);
        bool GetUsuario(string nombre, string password);

    }
}
