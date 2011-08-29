using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace Life.Archive
{
    public class ZipArchive : IArchive
    {
        #region [ Private Members ]

        private readonly string path;
        private readonly ZipFile zipFile;

        #endregion [ Private Members ]

        /// <summary>
        /// Opens a standard zip archive
        /// </summary>
        /// <param name="path">Path to zip file</param>
        /// <exception cref="ArgumentNullException">Thrown when the path is null</exception>
        public ZipArchive( string path )
        {
            if ( path == null )
                throw new ArgumentNullException( "path" );

            this.path = path;
            this.zipFile = new ZipFile( path );
        }

        /// <summary>
        /// Opens a zip archive stored inside a stream
        /// </summary>
        /// <param name="stream">Stream containing the zip file</param>
        public ZipArchive( Stream stream )
        {
            this.zipFile = new ZipFile( stream );
            this.path = String.Empty;
        }

        #region [ IArchive Members ]

        /// <summary>
        /// Gets the name of the archive
        /// </summary>
        public string ArchiveName
        {
            get { return this.path; }
        }

        /// <summary>
        /// Opens a file within the zip file
        /// </summary>
        /// <param name="fileName">File to open</param>
        /// <returns>Stream to that file</returns>
        /// <exception cref="InvalidOperationException">Thrown when the file does not exist in the zip file</exception>
        public Stream OpenFile( string fileName )
        {
            int index = this.zipFile.FindEntry( fileName.Replace( '\\', '/' ), true );

            // File was not found
            if ( index == -1 )
                throw new FileNotFoundException( "File not found in zip file", fileName );

            return this.zipFile.GetInputStream( index );
        }

        /// <summary>
        /// Checks if a file exists within the open zip file
        /// </summary>
        /// <param name="fileName">File to check</param>
        /// <returns>True if the file exists</returns>
        public bool Exists( string fileName )
        {
            return this.zipFile.FindEntry( fileName.Replace( '\\', '/' ), true ) != -1;
        }

        /// <summary>
        /// States whether the zip file is in write only mode
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Creates a writable stream within the zip file
        /// </summary>
        /// <param name="fileName">File to write within the zip file</param>
        /// <returns>A writable stream to write within the zip file</returns>
        /// <exception cref="NotSupportedException"></exception>
        public Stream CreateWritableStream( string fileName )
        {
            throw new NotSupportedException( );
        }

        /// <summary>
        /// List of all the files within the zip file
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when a write only zip file is being used</exception>
        public IEnumerable<string> FileNames
        {
            get { return this.zipFile.Cast<ZipEntry>( ).Select( z => z.Name ); }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="fileName">File to delete</param>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public void DeleteFile( string fileName )
        {
            throw new NotSupportedException( );
        }

        #endregion [ IArchive Members ]

        #region [ IDisposable Members ]

        /// <summary>
        /// Deconstructor
        /// </summary>
        ~ZipArchive( )
        {
            Dispose( false );
        }

        /// <summary>
        /// Disposes the archive
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Disposes the archive
        /// </summary>
        /// <param name="disposing">True if Dispose was called, false if the destructor calls</param>
        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
                this.zipFile.Close( );
        }

        #endregion [ IDisposable Members ]

        #region [ IEnumerable<string> Members ]

        /// <summary>
        /// Gets an enumerator of all the files
        /// </summary>
        /// <returns>Enumerator for all the file names</returns>
        /// <exception cref="InvalidOperationException">Thrown when a write only zip file is being used</exception>
        public IEnumerator<string> GetEnumerator( )
        {
            return this.zipFile.Cast<ZipEntry>( ).Select( z => z.Name ).GetEnumerator( );
        }

        #endregion [ IEnumerable<string> Members ]

        #region [ IEnumerable Members ]

        /// <summary>
        /// Gets an enumerator of all the files
        /// </summary>
        /// <returns>Enumerator for all the file names</returns>
        /// <exception cref="InvalidOperationException">Thrown when a write only zip file is being used</exception>
        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        #endregion [ IEnumerable Members ]
    }
}
