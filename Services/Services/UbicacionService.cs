using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Responses;
using Services.Validators;

namespace Services.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UbicacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ubicacion> Create(Ubicacion newUbicacion)
        {
            UbicacionValidators validator = new UbicacionValidators();
            var validationResult = await validator.ValidateAsync(newUbicacion);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors[0].ErrorMessage);
            }

            await _unitOfWork.UbicacionRepository.AddAsync(newUbicacion);
            await _unitOfWork.CommitAsync();

            return newUbicacion;
        }

        public async Task Delete(int ubicacionId)
        {
            var ubicacion = await _unitOfWork.UbicacionRepository.GetByIdAsync(ubicacionId);
            if (ubicacion == null)
            {
                throw new Exception("La ubicación no existe");
            }

            _unitOfWork.UbicacionRepository.Remove(ubicacion);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Ubicacion>> GetAll()
        {
            return await _unitOfWork.UbicacionRepository.GetAllAsync();
        }

        public async Task<Ubicacion> GetById(int id)
        {
            var ubicacion = await _unitOfWork.UbicacionRepository.GetByIdAsync(id);
            if (ubicacion == null)
            {
                throw new Exception("La ubicación no existe");
            }

            return ubicacion;
        }

        public async Task<Ubicacion> Update(int ubicacionId, Ubicacion updatedUbicacion)
        {
            var ubicacion = await _unitOfWork.UbicacionRepository.GetByIdAsync(ubicacionId);
            if (ubicacion == null)
            {
                throw new Exception("La ubicación no existe");
            }

            ubicacion.nombre = updatedUbicacion.nombre;
            ubicacion.descripcion = updatedUbicacion.descripcion;
            ubicacion.clima = updatedUbicacion.clima;

            await _unitOfWork.CommitAsync();
            return ubicacion;
        }

        public async Task<string> Moverse(int personajeNivel, int nuevaUbicacionId)
        {
            var ubicacion = await _unitOfWork.UbicacionRepository.GetByIdAsync(nuevaUbicacionId);
            if (ubicacion == null)
            {
                throw new Exception("La nueva ubicación no existe :/");
            }

            Random random = new Random();
            int nivelDeAmenaza = random.Next(personajeNivel, personajeNivel + 5); //esto hace que el nivel de amenaza >= nivel del personaje

            return $"El personaje se ha movido a la ubicacion '{ubicacion.nombre}' con el clima '{ubicacion.clima}'. Se encontró con un enemigo de nivel de amenaza {nivelDeAmenaza}";
        }
    }
}