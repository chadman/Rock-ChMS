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
using System.ServiceModel;

namespace Rock.REST.CMS
{
	/// <summary>
	/// Represents a REST WCF service for PageRoutes
	/// </summary>
	[ServiceContract]
    public partial interface IPageRouteService
    {
		/// <summary>
		/// Gets a PageRoute object
		/// </summary>
		[OperationContract]
        Rock.CMS.DTO.PageRoute Get( string id );

		/// <summary>
		/// Gets a PageRoute object
		/// </summary>
		[OperationContract]
        Rock.CMS.DTO.PageRoute ApiGet( string id, string apiKey );

		/// <summary>
		/// Updates a PageRoute object
		/// </summary>
        [OperationContract]
        void UpdatePageRoute( string id, Rock.CMS.DTO.PageRoute PageRoute );

		/// <summary>
		/// Updates a PageRoute object
		/// </summary>
        [OperationContract]
        void ApiUpdatePageRoute( string id, string apiKey, Rock.CMS.DTO.PageRoute PageRoute );

		/// <summary>
		/// Creates a new PageRoute object
		/// </summary>
        [OperationContract]
        void CreatePageRoute( Rock.CMS.DTO.PageRoute PageRoute );

		/// <summary>
		/// Creates a new PageRoute object
		/// </summary>
        [OperationContract]
        void ApiCreatePageRoute( string apiKey, Rock.CMS.DTO.PageRoute PageRoute );

		/// <summary>
		/// Deletes a PageRoute object
		/// </summary>
        [OperationContract]
        void DeletePageRoute( string id );

		/// <summary>
		/// Deletes a PageRoute object
		/// </summary>
        [OperationContract]
        void ApiDeletePageRoute( string id, string apiKey );
    }
}
