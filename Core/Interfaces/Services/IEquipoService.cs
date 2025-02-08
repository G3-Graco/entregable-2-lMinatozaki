using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IEquipoService : IBaseService<Equipo>
    {
        Task<Equipo> Equipar(int equipoId, string tipo, string objeto);
        Task<Equipo> Desequipar(int equipoId, string tipo);
    }
}