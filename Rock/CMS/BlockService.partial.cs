﻿//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rock.CMS
{
	public partial class BlockService
	{
		/// <summary>
		/// Gets a list of Blocks on the filesystem that have not yet been registered in the repository.
		/// </summary>
		/// <param name="physWebAppPath">the physical path of the web application</param>
		/// <returns>a collection of <see cref="Rock.CMS.Block">Blocks</see> that are not yet registered</returns>
        public IEnumerable<Rock.CMS.Block> GetUnregisteredBlocks( string physWebAppPath )
        {
            List<string> list = new List<string>();

            // Find all the blocks in the Blocks folder...
            FindAllBlocksInPath( physWebAppPath, list, "Blocks" );

            // Now do the exact same thing for the Plugins folder...
            FindAllBlocksInPath( physWebAppPath, list, "Plugins" );

            // Now remove from the list any that are already registered (via the path)
            var registered = from r in Repository.GetAll() select r.Path;
            return ( from u in list.Except( registered ) select new Block { Path = u, Guid = Guid.NewGuid() } );
        }

        private static void FindAllBlocksInPath( string physWebAppPath, List<string> list, string folder )
        {
            // Determine the physical path (it will be something like "C:\blahblahblah\Blocks\" or "C:\blahblahblah\Plugins\")
            string physicalPath = string.Format( @"{0}{1}{2}\", physWebAppPath, ( physWebAppPath.EndsWith( @"\" ) ) ? "" : @"\", folder );
            // Determine the virtual path (it will be either "~/Blocks/" or "~/Plugins/")
            string virtualPath = string.Format( "~/{0}/", folder );

            // search for all blocks under the physical path 
            string[] allBlockNames = Directory.GetFiles( physicalPath, "*.ascx", SearchOption.AllDirectories );
            string fileName = string.Empty;

            // Convert them to virtual file/path: ~/<folder>/foo/bar.ascx
            for  (int i = 0; i < allBlockNames.Length; i++ )
            {
                fileName = allBlockNames[i].Replace( physicalPath, virtualPath );
                list.Add( fileName.Replace(@"\", "/") );
            }
        }
	}
}