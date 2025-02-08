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
    public class EquipoService : IEquipoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EquipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Equipo> Create(Equipo newEquipo)
        {
            EquipoValidators validator = new EquipoValidators();
            var validationResult = await validator.ValidateAsync(newEquipo);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors[0].ErrorMessage);
            }

            await _unitOfWork.EquipoRepository.AddAsync(newEquipo);
            await _unitOfWork.CommitAsync();

            return newEquipo;
        }

        public async Task Delete(int equipoId)
        {
            var equipo = await _unitOfWork.EquipoRepository.GetByIdAsync(equipoId);
            if (equipo == null)
            {
                throw new Exception("El equipo no existe");
            }

            _unitOfWork.EquipoRepository.Remove(equipo);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Equipo>> GetAll()
        {
            return await _unitOfWork.EquipoRepository.GetAllAsync();
        }

        public async Task<Equipo> GetById(int id)
        {
            var equipo = await _unitOfWork.EquipoRepository.GetByIdAsync(id);
            if (equipo == null)
            {
                throw new Exception("El equipo no existe");
            }

            return equipo;
        }

        public async Task<Equipo> Update(int equipoId, Equipo updatedEquipo)
        {
            var equipo = await _unitOfWork.EquipoRepository.GetByIdAsync(equipoId);
            if (equipo == null)
            {
                throw new Exception("El equipo no existe");
            }

            EquipoValidators validator = new EquipoValidators();
            var validationResult = await validator.ValidateAsync(updatedEquipo);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors[0].ErrorMessage);
            }

            equipo.casco = updatedEquipo.casco;
            equipo.armadura = updatedEquipo.armadura;
            equipo.arma1 = updatedEquipo.arma1;
            equipo.arma2 = updatedEquipo.arma2;
            equipo.guanteletes = updatedEquipo.guanteletes;
            equipo.grebas = updatedEquipo.grebas;

            await _unitOfWork.CommitAsync();

            return equipo;
        }

        public async Task<Equipo> Equipar(int equipoId, string tipo, string objeto)
        {
            var equipo = await _unitOfWork.EquipoRepository.GetByIdAsync(equipoId);
            if (equipo == null)
            {
                throw new Exception("El equipo no existe");
            }

            switch (tipo.ToLower())
            {
                case "casco":
                    equipo.casco = objeto;
                    break;
                case "armadura":
                    equipo.armadura = objeto;
                    break;
                case "arma1":
                    equipo.arma1 = objeto;
                    break;
                case "arma2":
                    equipo.arma2 = objeto;
                    break;
                case "guanteletes":
                    equipo.guanteletes = objeto;
                    break;
                case "grebas":
                    equipo.grebas = objeto;
                    break;
                default:
                    throw new ArgumentException("Tipo de objeto invalido. Los tipos validos son: casco, armadura, arma1, arma2, guanteletes, grebas");
            }

            await _unitOfWork.CommitAsync();
            return equipo;
        }

        public async Task<Equipo> Desequipar(int equipoId, string tipo)
        {
            var equipo = await _unitOfWork.EquipoRepository.GetByIdAsync(equipoId);
            if (equipo == null)
            {
                throw new Exception("El equipo no existe");
            }

            switch (tipo.ToLower())
            {
                case "casco":
                    equipo.casco = string.Empty;
                    break;
                case "armadura":
                    equipo.armadura = string.Empty;
                    break;
                case "arma1":
                    equipo.arma1 = string.Empty;
                    break;
                case "arma2":
                    equipo.arma2 = string.Empty;
                    break;
                case "guanteletes":
                    equipo.guanteletes = string.Empty;
                    break;
                case "grebas":
                    equipo.grebas = string.Empty;
                    break;
                default:
                    throw new ArgumentException("Tipo de objeto invalido. Los tipos validos son: casco, armadura, arma1, arma2, guanteletes, grebas");
            }

            await _unitOfWork.CommitAsync();
            return equipo;
        }
    }
}