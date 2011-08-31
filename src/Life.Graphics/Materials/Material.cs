using System;
using System.Linq;
using System.Collections.Generic;

namespace Life.Graphics.Materials
{
	public class Material : ResourceBase
	{
		public IEnumerable<Pass> Passes { get; set; }
		
		public Material( string name )
			: base( name )
		{
		}
		
		protected override bool DoLoad( )
		{
			foreach( var texture in GetTextures( ) )
				texture.Load( );
			
			return true;
		}
		
		protected override bool DoUnload ()
		{
			foreach( var texture in GetTextures( ) )
				texture.Unload( );
				
			return true;
		}
		
		#region [ Private Methods ]
		
		private IEnumerable<Texture> GetTextures( )
		{
			return Passes.SelectMany( p => 
				p.TextureUnits.Select( tu => tu.Texture ) );
		}
		
		#endregion
	}
}

