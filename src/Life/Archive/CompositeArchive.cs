using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Life.Archive
{
    public class CompositeArchive : IArchive, ICollection<IArchive>
    {
        #region [ Private Members ]

        private readonly IList<IArchive> archives = new List<IArchive>( );

        #endregion [ Private Members ]

        /// <summary>
        /// Returns a list of archives that have this fileName
        /// </summary>
        /// <param name="fileName">Filename to search for</param>
        /// <returns>Enumerable of archives that contain the file</returns>
        public IEnumerable<IArchive> GetArchivesWithFile( string fileName )
        {
            return this.archives.Where( a => a.Exists( fileName ) );
        }

        #region [ IArchive Members ]

        public string ArchiveName
        {
            get { return "CompositeArchive"; }
        }

        public Stream OpenFile( string fileName )
        {
            return 
                this.archives.Where( a => a.Exists( fileName ) )
                .Select( a => a.OpenFile( fileName ) )
                .FirstOrDefault( );
        }

        public bool Exists( string fileName )
        {
            return this.archives.Any( a => a.Exists( fileName ) );
        }

        public bool IsReadOnly
        {
            get { return this.archives.All( a => a.IsReadOnly ); }
        }

        public Stream CreateWritableStream( string fileName )
        {
            return 
                this.archives.Where( a => !a.IsReadOnly )
                .Select( a => a.CreateWritableStream( fileName ) )
                .FirstOrDefault( );
        }

        public IEnumerable<string> FileNames
        {
            get { return this.archives.SelectMany( a => a ); }
        }

        public void DeleteFile( string fileName )
        {
            throw new NotSupportedException( );
        }

        #endregion [ IArchive Members ]

        #region [ ICollection<IArchive> Members ]

        public void Add( IArchive item )
        {
            this.archives.Add( item );
        }

        public void Clear( )
        {
            this.archives.Clear( );
        }

        public bool Contains( IArchive item )
        {
            return this.archives.Contains( item );
        }

        public void CopyTo( IArchive[ ] array, int arrayIndex )
        {
            this.archives.CopyTo( array, arrayIndex );
        }

        public int Count
        {
            get { return this.archives.Count; }
        }

        bool IArchive.IsReadOnly
        {
            get { return false; }
        }

        public bool Remove( IArchive item )
        {
            return this.archives.Remove( item );
        }

        #endregion [ ICollection<IArchive> Members ]

        #region [ IEnumerable<IArchive> Members ]

        public IEnumerator<IArchive> GetEnumerator( )
        {
            return this.archives.GetEnumerator( );
        }

        #endregion [ IEnumerable<IArchive> Members ]

        #region [ IEnumerable Members ]

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return this.archives.GetEnumerator( );
        }

        #endregion [ IEnumerable Members ]

        #region [ IDisposable Members ]

        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                foreach ( var archive in this.archives )
                    archive.Dispose( );
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~CompositeArchive( )
        {
            Dispose( false );
        }

        #endregion [ IDisposable Members ]

        #region [ IEnumerable<string> Members ]

        IEnumerator<string> IEnumerable<string>.GetEnumerator( )
        {
            var fileNames = new List<string>( );

            foreach ( var archive in this.archives )
                fileNames.AddRange( archive );

            return fileNames.GetEnumerator( );
        }

        #endregion [ IEnumerable<string> Members ]

    }
}
