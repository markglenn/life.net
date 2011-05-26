using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Life.Core.Archive
{
    public class FolderArchive : IArchive
    {
        private readonly DirectoryInfo directory;

        public FolderArchive( string folder )
        {
            this.directory = new DirectoryInfo( folder );
        }
        
        ~FolderArchive( )
        {
            Dispose( false );
        }

        #region [ Implementation of IArchive ]

        public string ArchiveName
        {
            get { return this.directory.FullName; }
        }

        public Stream OpenFile( string fileName )
        {
            return File.OpenRead( Path.Combine( this.directory.FullName, fileName ) );
        }

        public bool Exists( string fileName )
        {
            return File.Exists( Path.Combine( this.directory.FullName, fileName ) );
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public Stream CreateWritableStream( string fileName )
        {
            return File.Create( Path.Combine( this.directory.FullName, fileName ) );
        }

        public void DeleteFile( string fileName )
        {
            File.Delete( Path.Combine( this.directory.FullName, fileName ) );
        }

        #endregion [ Implementation of IArchive ]

        #region [ Implementation of IEnumerable ]

        public IEnumerator<string> GetEnumerator( )
        {
            throw new NotImplementedException( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        #endregion [ Implementation of IEnumerable ]

        #region [ Implementation of IDisposable ]

        public void Dispose( )
        {
            GC.SuppressFinalize( this );
            Dispose( true );
        }

        protected virtual void Dispose( bool disposing )
        {
                
        }

        #endregion [ Implementation of IDisposable ]
    }
}
