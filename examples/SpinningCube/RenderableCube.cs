using System;
using Life.Graphics;
using Life;
using ExampleCore;
using System.Collections.Generic;
using Life.Math;
using Life.Graphics.Materials;

namespace SpinningCube
{
	public class RenderableCube : ResourceBase, IRenderable
	{
		#region [ Private Members ]
		
		private readonly IDevice device;
        private readonly TextureManager textureManager;
        private HardwareIndexBuffer indexBuffer;
        private HardwareVertexBuffer vertexBuffer;
        private RenderOperation renderOperation;
        private Texture texture;
        
		#endregion
 
		public RenderableCube( IDevice device )
			: base( "Renderable Cube" )
		{
			this.device = device;
			this.textureManager = new TextureManager( device );
            
			this.WorldPosition = new Vector3( 0, 0, 3 );
			this.WorldOrientation = Quaternion.FromAxisAngle( Vector3.UnitY, 0 );
		}

		#region [ implemented abstract members of Life.ResourceBase ]
		
		protected override bool DoLoad( )
		{
			this.vertexBuffer = device.CreateVertexBuffer( TexturedVertex.VertexDefinition, 4 );
            this.indexBuffer = device.CreateIndexBuffer( IndexBufferFormat.UShort, 4 );

            using ( vertexBuffer.Lock( BufferLock.Discard ) )
            {
            	vertexBuffer.Write( new TexturedVertex( -1, 1, 0, 0, 1 ) );
            	vertexBuffer.Write( new TexturedVertex( 1, 1, 0, 0, 1 ) );
            	vertexBuffer.Write( new TexturedVertex( 1, -1, 0, 0.5f, 0 ) );
            	vertexBuffer.Write( new TexturedVertex( -1, -1, 0, 1, 1 ) );
            }

            using ( indexBuffer.Lock( BufferLock.Discard ) )
                indexBuffer.Write( new ushort[] { 0, 1, 2, 3 } );

            this.renderOperation = new RenderOperation( OperationType.TriangleFan, 4, 
				this.vertexBuffer, this.indexBuffer );
			
			return true;
		}

		protected override bool DoUnload ()
		{
			this.vertexBuffer.Dispose( );
			this.indexBuffer.Dispose( );
			
			return true;
		}
		
		#endregion
		
		#region [ IRenderable implementation ]
		
		public RenderOperation RenderOperation
		{
			get { return this.renderOperation; }
		}

		public Quaternion WorldOrientation { get; set; }

		public Vector3 WorldPosition { get; set; }

		public IEnumerable<Matrix4> Matrices { get; set; }

		public Material Material { get; set; }
		
		#endregion

	}
}

