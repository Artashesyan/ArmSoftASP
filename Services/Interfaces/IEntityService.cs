using Homework1.DTOs.Entity;

namespace Homework1.Services.Interfaces
{
	public interface IEntityService
	{
		IEnumerable<EntityReadDTO> GetAll();
		EntityReadDTO? GetById(int id);
		EntityReadDTO Create(EntityCreateDTO dto);
		EntityReadDTO? Update(int id, EntityUpdateDTO dto);
		public EntityReadDTO? UpdatePartial(int id, EntityPatchDTO dto);
		bool Delete(int id);
	}
}
