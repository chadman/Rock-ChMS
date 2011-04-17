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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

using Rock.Models;

namespace Rock.Models.Cms
{
    [Table( "cmsHtmlContent" )]
    public partial class HtmlContent : ModelWithAttributes, IAuditable
    {
		public Guid Guid { get; set; }
		
		public int BlockId { get; set; }
		
		public string Content { get; set; }
		
		[MaxLength( 50 )]
		public string EntityValue { get; set; }
		
		public int VersionLabel { get; set; }
		
		public bool Approved { get; set; }
		
		public int? ApprovedByPersonId { get; set; }
		
		public DateTime? ApprovedDateTime { get; set; }
		
		public DateTime? CreatedDateTime { get; set; }
		
		public DateTime? ModifiedDateTime { get; set; }
		
		public int? CreatedByPersonId { get; set; }
		
		public int? ModifiedByPersonId { get; set; }
		
		public DateTime? StartDateTime { get; set; }
		
		public DateTime? ExpireDateTime { get; set; }
		
		[NotMapped]
		public override string AuthEntity { get { return "Cms.HtmlContent"; } }

		public virtual BlockInstance Block { get; set; }

		public virtual Crm.Person ApprovedByPerson { get; set; }

		public virtual Crm.Person CreatedByPerson { get; set; }

		public virtual Crm.Person ModifiedByPerson { get; set; }
    }

    public partial class HtmlContentConfiguration : EntityTypeConfiguration<HtmlContent>
    {
        public HtmlContentConfiguration()
        {
			this.HasRequired( p => p.Block ).WithMany( p => p.HtmlContents ).HasForeignKey( p => p.BlockId );
			this.HasOptional( p => p.ApprovedByPerson ).WithMany().HasForeignKey( p => p.ApprovedByPersonId );
			this.HasOptional( p => p.CreatedByPerson ).WithMany().HasForeignKey( p => p.CreatedByPersonId );
			this.HasOptional( p => p.ModifiedByPerson ).WithMany().HasForeignKey( p => p.ModifiedByPersonId );
		}
    }
}
