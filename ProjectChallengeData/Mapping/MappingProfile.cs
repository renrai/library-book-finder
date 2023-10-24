using AutoMapper;
using ProjectChallengeData.Database.Entities;
using ProjectChallengeDomain.Models;
using ProjectLibraryData.Database.Entities;
using ProjectLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeData.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookEntity, Book>()
            .ReverseMap();
            CreateMap<UserEntity, User>()
            .ReverseMap();
        }
    }
}
