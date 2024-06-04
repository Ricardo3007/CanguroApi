using CanguroApi.DataAccess.Context;
using CanguroApi.Domain.Entities;
using CanguroApi.Domain.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CanguroApi.Domain.Services
{
    public class CanguroDomain:ICanguroDomain
    {

        private readonly CanguroContext _context;
        public CanguroDomain(CanguroContext context)
        {
            _context = context;
        }

        public List<MovCanguro> GetCanguro()
        {
            List<MovCanguro> result = _context.MovCanguro.Where(x => x.Estado == true).OrderByDescending(x => x.FechaCreacion).ToList();
            //List<MovCanguro> result =  _context.MovCanguro.ToList();

            return result;
        }

        public bool UpdateCanguro(MovCanguro movCanguroUpdate)
        {
            MovCanguro movCanguroExistente = GetCanguroExistById(movCanguroUpdate.Codigo);
            if (movCanguroExistente == null)
                return false;

            // Actualizar registros
            _context.Entry(movCanguroExistente).CurrentValues.SetValues(movCanguroUpdate);

            // Aplicar los cambios a la base de datos
            _context.SaveChanges();

            return true;
        }

        public bool DeleteCanguro(int codigo)
        {

            MovCanguro movCanguroExistente = GetCanguroExistById(codigo);
            if (movCanguroExistente != null)
            {
                movCanguroExistente.Estado = false;
                // Actualizar estado
                _context.Entry(movCanguroExistente).CurrentValues.SetValues(movCanguroExistente);

                // Aplicar los cambios a la base de datos
                _context.SaveChanges();
            } else
            {
                return false;
            }

          

            return true;

        }


        public bool AddCanguro(MovCanguro movCanguroAdd)
        {

            // Agregar registros
            _context.Add(movCanguroAdd);

            // Aplicar los cambios a la base de datos
            _context.SaveChanges();

            return true;
        }


        public bool GetUsuario(string nombre, string password)
        {
            return  _context.Usuario.Any(x => x.Estado == true && x.Nombre == nombre && x.Password == password);

        }


        private MovCanguro GetCanguroExistById(int canguroId)
        {
            return _context.MovCanguro.FirstOrDefault(x => x.Codigo.Equals(canguroId));

        }
    }
}
