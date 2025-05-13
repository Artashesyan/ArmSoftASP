using Homework1.DTOs.Entity;
using Homework1.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Homework1.Services.Interfaces
{
	public interface IEntityService
	{
		IEnumerable<EntityReadDTO> GetAll();
		EntityReadDTO? GetById(int id);
		public Entity? GetEntityById(int id);
		EntityReadDTO Create(EntityCreateDTO dto);
		EntityReadDTO? Update(int id, EntityUpdateDTO dto);
		bool Delete(int id);
	}
}
