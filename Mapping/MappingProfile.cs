using AutoMapper;
using Homework1.DTOs.Entity;
using Homework1.DTOs.Post;
using Homework1.DTOs.User;
using Homework1.Models;

namespace Homework1.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Post, PostReadDTO>();
			CreateMap<PostCreateDTO, Post>();
			CreateMap<PostUpdateDTO, Post>();

			CreateMap<User, UserReadDTO>();
			CreateMap<UserCreateDTO, User>();
			CreateMap<UserUpdateDTO, User>();

			CreateMap<Entity, EntityReadDTO>();
			CreateMap<EntityCreateDTO, Entity>();
			CreateMap<EntityUpdateDTO, Entity>();
		}
	}
}
