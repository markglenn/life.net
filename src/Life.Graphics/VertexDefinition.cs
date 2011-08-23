using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Life.Graphics
{
	public class VertexDefinition
    {
        #region [ Private Members ]

        private readonly List<VertexElement> elements;

        #endregion [ Private Members ]

		#region [ Public Properties ]
		
        public IEnumerable<VertexElement> Elements
        {
            get { return this.elements; }
        }

        /// <summary>
        /// Size of the vertex
        /// </summary>
        public int Stride { get; private set; }
        
		#endregion

        public VertexDefinition( )
            : this( new List<VertexElement>( ) )
        {
        }

        public VertexDefinition( IEnumerable<VertexElement> elements)
        {
            this.elements = elements.ToList( );

            foreach( var element in this.elements )
            {
                element.Offset = this.Stride;
                this.Stride += element.Size;
            }
        }
        
        public void Add( VertexElement element )
        {
            // Set the element's offset in the entire list
            element.Offset = this.Stride;

            this.Stride += element.Size;
            this.elements.Add( element );
        }

        public void Sort( )
        {
            this.elements.Sort( );
            int offset = 0;

            foreach ( var element in this.elements )
            {
                element.Offset = offset;
                offset += element.Size;
            }
        }

        #region [ Object Overrides ]

        public override string ToString( )
        {
            var sb = new StringBuilder( "[ " );

            foreach ( var element in this.elements )
                sb.Append( element ).Append( " " );

            sb.Append( "]" );

            return sb.ToString( );
        }

        #endregion [ Object Overrides ]
    }
}

