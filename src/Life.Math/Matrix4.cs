using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Life.Math
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    public struct Matrix4 : IEquatable<Matrix4>
    {
        public readonly float[ ] M;

        public Matrix4( float v )
        {
            this.M = new[ ]{
                v, v, v, v,
                v, v, v, v,
                v, v, v, v,
                v, v, v, v
            };
        }

        public Matrix4(
           float r1c1, float r2c1, float r3c1, float r4c1,
           float r1c2, float r2c2, float r3c2, float r4c2,
           float r1c3, float r2c3, float r3c3, float r4c3,
           float r1c4, float r2c4, float r3c4, float r4c4 )
        {
            this.M = new[ ]
            {
                r1c1, r2c1, r3c1, r4c1,
                r1c2, r2c2, r3c2, r4c2,
                r1c3, r2c3, r3c3, r4c3,
                r1c4, r2c4, r3c4, r4c4,
            };
        }

        public Matrix4( float[ ] values )
        {
            if ( values.Length != 16 )
                throw new ArgumentOutOfRangeException( "values" );

            this.M = values;
        }

        public bool Equals( Matrix4 other )
        {
            return Enumerable.SequenceEqual( this.M, other.M );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            return obj.GetType( ) == typeof( Matrix4 ) && this.Equals( ( Matrix4 )obj );
        }

        public override int GetHashCode( )
        {
            return ( this.M != null ? this.M.GetHashCode( ) : 0 );
        }

        public static bool operator ==( Matrix4 left, Matrix4 right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Matrix4 left, Matrix4 right )
        {
            return !left.Equals( right );
        }

        public Matrix4 Transpose( )
        {
            return new Matrix4(
                this.M[ 0 ], this.M[ 4 ], this.M[ 8 ], this.M[ 12 ],
                this.M[ 1 ], this.M[ 5 ], this.M[ 9 ], this.M[ 13 ],
                this.M[ 2 ], this.M[ 6 ], this.M[ 10 ], this.M[ 14 ],
                this.M[ 3 ], this.M[ 7 ], this.M[ 11 ], this.M[ 15 ] );
        }

        public static Matrix4 Add( Matrix4 left, Matrix4 right )
        {
            return new Matrix4(
                left.M[ 0 ] + right.M[ 0 ], left.M[ 1 ] + right.M[ 1 ], left.M[ 2 ] + right.M[ 2 ], left.M[ 3 ] + right.M[ 3 ],
                left.M[ 4 ] + right.M[ 4 ], left.M[ 5 ] + right.M[ 5 ], left.M[ 6 ] + right.M[ 6 ], left.M[ 7 ] + right.M[ 7 ],
                left.M[ 8 ] + right.M[ 8 ], left.M[ 9 ] + right.M[ 9 ], left.M[ 10 ] + right.M[ 10 ], left.M[ 11 ] + right.M[ 11 ],
                left.M[ 12 ] + right.M[ 12 ], left.M[ 13 ] + right.M[ 13 ], left.M[ 14 ] + right.M[ 14 ], left.M[ 15 ] + right.M[ 15 ]
            );
        }

        public static Matrix4 Subtract( Matrix4 left, Matrix4 right )
        {
            return new Matrix4(
                left.M[ 0 ] - right.M[ 0 ], left.M[ 1 ] - right.M[ 1 ], left.M[ 2 ] - right.M[ 2 ], left.M[ 3 ] - right.M[ 3 ],
                left.M[ 4 ] - right.M[ 4 ], left.M[ 5 ] - right.M[ 5 ], left.M[ 6 ] - right.M[ 6 ], left.M[ 7 ] - right.M[ 7 ],
                left.M[ 8 ] - right.M[ 8 ], left.M[ 9 ] - right.M[ 9 ], left.M[ 10 ] - right.M[ 10 ], left.M[ 11 ] - right.M[ 11 ],
                left.M[ 12 ] - right.M[ 12 ], left.M[ 13 ] - right.M[ 13 ], left.M[ 14 ] - right.M[ 14 ], left.M[ 15 ] - right.M[ 15 ]
            );
        }

        public static Matrix4 Multiply( Matrix4 m1, Matrix4 m2 )
        {
            return new Matrix4(
                m1.M[ 0 ] * m2.M[ 0 ] + m1.M[ 4 ] * m2.M[ 1 ] + m1.M[ 8 ] * m2.M[ 2 ] + m1.M[ 12 ] * m2.M[ 3 ],
                m1.M[ 1 ] * m2.M[ 0 ] + m1.M[ 5 ] * m2.M[ 1 ] + m1.M[ 9 ] * m2.M[ 2 ] + m1.M[ 13 ] * m2.M[ 3 ],
                m1.M[ 2 ] * m2.M[ 0 ] + m1.M[ 6 ] * m2.M[ 1 ] + m1.M[ 10 ] * m2.M[ 2 ] + m1.M[ 14 ] * m2.M[ 3 ],
                m1.M[ 3 ] * m2.M[ 0 ] + m1.M[ 7 ] * m2.M[ 1 ] + m1.M[ 11 ] * m2.M[ 2 ] + m1.M[ 15 ] * m2.M[ 3 ],
                m1.M[ 0 ] * m2.M[ 4 ] + m1.M[ 4 ] * m2.M[ 5 ] + m1.M[ 8 ] * m2.M[ 6 ] + m1.M[ 12 ] * m2.M[ 7 ],
                m1.M[ 1 ] * m2.M[ 4 ] + m1.M[ 5 ] * m2.M[ 5 ] + m1.M[ 9 ] * m2.M[ 6 ] + m1.M[ 13 ] * m2.M[ 7 ],
                m1.M[ 2 ] * m2.M[ 4 ] + m1.M[ 6 ] * m2.M[ 5 ] + m1.M[ 10 ] * m2.M[ 6 ] + m1.M[ 14 ] * m2.M[ 7 ],
                m1.M[ 3 ] * m2.M[ 4 ] + m1.M[ 7 ] * m2.M[ 5 ] + m1.M[ 11 ] * m2.M[ 6 ] + m1.M[ 15 ] * m2.M[ 7 ],
                m1.M[ 0 ] * m2.M[ 8 ] + m1.M[ 4 ] * m2.M[ 9 ] + m1.M[ 8 ] * m2.M[ 10 ] + m1.M[ 12 ] * m2.M[ 11 ],
                m1.M[ 1 ] * m2.M[ 8 ] + m1.M[ 5 ] * m2.M[ 9 ] + m1.M[ 9 ] * m2.M[ 10 ] + m1.M[ 13 ] * m2.M[ 11 ],
                m1.M[ 2 ] * m2.M[ 8 ] + m1.M[ 6 ] * m2.M[ 9 ] + m1.M[ 10 ] * m2.M[ 10 ] + m1.M[ 14 ] * m2.M[ 11 ],
                m1.M[ 3 ] * m2.M[ 8 ] + m1.M[ 7 ] * m2.M[ 9 ] + m1.M[ 11 ] * m2.M[ 10 ] + m1.M[ 15 ] * m2.M[ 11 ],
                m1.M[ 0 ] * m2.M[ 12 ] + m1.M[ 4 ] * m2.M[ 13 ] + m1.M[ 8 ] * m2.M[ 14 ] + m1.M[ 12 ] * m2.M[ 15 ],
                m1.M[ 1 ] * m2.M[ 12 ] + m1.M[ 5 ] * m2.M[ 13 ] + m1.M[ 9 ] * m2.M[ 14 ] + m1.M[ 13 ] * m2.M[ 15 ],
                m1.M[ 2 ] * m2.M[ 12 ] + m1.M[ 6 ] * m2.M[ 13 ] + m1.M[ 10 ] * m2.M[ 14 ] + m1.M[ 14 ] * m2.M[ 15 ],
                m1.M[ 3 ] * m2.M[ 12 ] + m1.M[ 7 ] * m2.M[ 13 ] + m1.M[ 11 ] * m2.M[ 14 ] + m1.M[ 15 ] * m2.M[ 15 ]

            );
        }
        
        public override string ToString( )
        {
            var sb = new StringBuilder( );

            sb.Append( "[" ).Append( M[ 0 ] ).Append( ", " ).Append( M[ 4 ] )
                .Append( ", " ).Append( M[ 8 ] ).Append( ", " ).Append( M[ 12 ] ).AppendLine( "]" );
            sb.Append( "[" ).Append( M[ 1 ] ).Append( ", " ).Append( M[ 5 ] )
                .Append( ", " ).Append( M[ 9 ] ).Append( ", " ).Append( M[ 13 ] ).AppendLine( "]" );
            sb.Append( "[" ).Append( M[ 2 ] ).Append( ", " ).Append( M[ 6 ] )
                .Append( ", " ).Append( M[ 10 ] ).Append( ", " ).Append( M[ 14 ] ).AppendLine( "]" );
            sb.Append( "[" ).Append( M[ 3 ] ).Append( ", " ).Append( M[ 7 ] )
                .Append( ", " ).Append( M[ 11 ] ).Append( ", " ).Append( M[ 15 ] ).Append( "]" );

            return sb.ToString( );
        }

        public static Matrix4 Scale( float x, float y, float z )
        {
            return new Matrix4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1 );
        }

        public static Matrix4 Scale( Vector3 v )
        {
            return Scale( v.X, v.Y, v.Z );
        }

        public static readonly Matrix4 Zero = new Matrix4 ( 0 );
        public static readonly Matrix4 Identity = Scale( 1, 1, 1 );

        public static Matrix4 Translation( float x, float y, float z )
        {

            return new Matrix4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                x, y, z, 1 );
        }
    }
}
