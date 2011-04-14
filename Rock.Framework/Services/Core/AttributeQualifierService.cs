//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the T4\Model.tt template.
//
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rock.Models.Core;
using Rock.Repository.Core;

namespace Rock.Services.Core
{
    public partial class AttributeQualifierService : Rock.Services.Service
    {
        private IAttributeQualifierRepository _repository;

        public AttributeQualifierService()
			: this( new EntityAttributeQualifierRepository() )
        { }

        public AttributeQualifierService( IAttributeQualifierRepository AttributeQualifierRepository )
        {
            _repository = AttributeQualifierRepository;
        }

        public IQueryable<Rock.Models.Core.AttributeQualifier> Queryable()
        {
            return _repository.AsQueryable();
        }

        public Rock.Models.Core.AttributeQualifier GetAttributeQualifier( int id )
        {
            return _repository.FirstOrDefault( t => t.Id == id );
        }
		
        public void AddAttributeQualifier( Rock.Models.Core.AttributeQualifier AttributeQualifier )
        {
            if ( AttributeQualifier.Guid == Guid.Empty )
                AttributeQualifier.Guid = Guid.NewGuid();

            _repository.Add( AttributeQualifier );
        }

        public void DeleteAttributeQualifier( Rock.Models.Core.AttributeQualifier AttributeQualifier )
        {
            _repository.Delete( AttributeQualifier );
        }

        public void Save( Rock.Models.Core.AttributeQualifier AttributeQualifier, int? personId )
        {
            List<Rock.Models.Core.EntityChange> entityChanges = _repository.Save( AttributeQualifier, personId );

			if ( entityChanges != null )
            {
                Rock.Services.Core.EntityChangeService entityChangeService = new Rock.Services.Core.EntityChangeService();

                foreach ( Rock.Models.Core.EntityChange entityChange in entityChanges )
                {
                    entityChange.EntityId = AttributeQualifier.Id;
                    entityChangeService.AddEntityChange ( entityChange );
                    entityChangeService.Save( entityChange, personId );
                }
            }
        }
    }
}