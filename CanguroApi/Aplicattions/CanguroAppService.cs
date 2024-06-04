using AutoMapper;
using CanguroApi.Aplicattions.Contracts;
using CanguroApi.Domain.Entities;
using CanguroApi.Domain.Services.Contracts;
using CanguroApi.DTO;
using CanguroApi.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace CanguroApi.Aplicattions
{
    public class CanguroAppService:ICanguroAppService
    {
        private readonly ICanguroDomain _canguroDomain;
        private readonly IMapper _mapper;
        private readonly ILogger<CanguroAppService> _logger;

        public CanguroAppService(ICanguroDomain canguroDomain, IMapper mapper, ILogger<CanguroAppService> logger) {
            _canguroDomain = canguroDomain;
            _mapper = mapper;
            _logger = logger;
        }


        public Request<List<MovCanguroDTO>> GetCanguro()
        {
            try
            {

                List<MovCanguro> movCanguroDTO =  _canguroDomain.GetCanguro();
                if (!movCanguroDTO.Any())

                {
                    return Request<List<MovCanguroDTO>>.NoSucces("No existen Registros");   
                }

                return Request<List<MovCanguroDTO>>.Succes(_mapper.Map<List<MovCanguroDTO>>(movCanguroDTO));

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en GetCanguro" + ex);
                return Request<List<MovCanguroDTO>>.Error(ex.ToString());
            }
        }

        public Request<MovCanguroDTO> UpdateCanguro(MovCanguroDTO movCanguroDTO)
        {
            try
            {
                if (movCanguroDTO.Codigo.ToString().IsNullOrEmpty())
                {
                    return Request<MovCanguroDTO>.NoSucces("El codigo es obligatorio");
                }
                MovCanguro movCanguro = _mapper.Map<MovCanguro>(movCanguroDTO);
                bool rstaCanguro = _canguroDomain.UpdateCanguro(movCanguro);
                if (!rstaCanguro)

                {
                    return Request<MovCanguroDTO>.NoSucces("No se pudo actualizar el regsitro.");
                }

                return Request<MovCanguroDTO>.Succes(movCanguroDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en UpdateCanguro" + ex);
                return Request<MovCanguroDTO>.Error(ex.ToString());
            }
        }

        public Request<string> DeleteCanguro(int codigo)
        {
            try
            {
                if (codigo.ToString().IsNullOrEmpty())
                {
                    return Request<string>.NoSucces("El codigo es obligatorio");
                }

                bool rstaCanguro = _canguroDomain.DeleteCanguro(codigo);
                if (!rstaCanguro)

                {
                    return Request<string>.NoSucces("No se pudo eliminar el regsitro.");
                }

                return Request<string>.Succes("Se elimino el registro correctamente");

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en DeleteCanguro" + ex);
                return Request<string>.Error(ex.ToString());
            }
        }



        public Request<MovCanguroDTO> AddCanguro(MovCanguroDTO movCanguroDTO)
        {
            try
            {
                if (movCanguroDTO.Descripcion.IsNullOrEmpty() || movCanguroDTO.Identificacion.IsNullOrEmpty())
                {
                    return Request<MovCanguroDTO>.NoSucces("Existen campos obligatorios por llenar");
                }

                MovCanguro movCanguro = _mapper.Map<MovCanguro>(movCanguroDTO);
                bool rstaCanguro = _canguroDomain.AddCanguro(movCanguro);
                if (!rstaCanguro)

                {
                    return Request<MovCanguroDTO>.NoSucces("No se pudo insertar el regsitro.");
                }

                return Request<MovCanguroDTO>.Succes(movCanguroDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en AddCanguro" + ex);
                return Request<MovCanguroDTO>.Error(ex.ToString());
            }
        }


        public Request<bool> GetUsuario(string nombre, string password)
        {
            try
            {

                if (nombre.IsNullOrEmpty() || password.IsNullOrEmpty())
                {
                    return Request<bool>.NoSucces("Usuario o contraseña incorrecta.");
                }

                bool usuarioDTO = _canguroDomain.GetUsuario(nombre, password);
                if (!usuarioDTO)

                {
                    return Request<bool>.NoSucces("Usuario o contraseña incorrecta.");
                }

                return Request<bool>.Succes(usuarioDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception en GetUsuario" + ex);
                return Request<bool>.Error(ex.ToString());
            }
        }

    }
}
