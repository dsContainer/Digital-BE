using AutoMapper;
using Digital.Data.Entities;
using Digital.Infrastructure.Model.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region DocumentType
            CreateMap<DocumentType, DocumentTypeViewModel>();
            CreateMap< DocumentTypeCreateModel, DocumentType>();
            #endregion
        }
    }
}
