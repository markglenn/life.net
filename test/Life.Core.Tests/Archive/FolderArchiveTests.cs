using System;
using System.IO;
using Life.Core.Archive;
using NUnit.Framework;

namespace Life.Core.Tests.Archive
{
    [TestFixture]
    public class FolderArchiveTests
    {
        private readonly string tempFolder = Path.GetTempPath( );
        private IArchive archive;

        [SetUp]
        public void Setup( )
        {
            this.archive = new FolderArchive( Path.GetTempPath( ) );
        }

        [Test]
        public void Ctor_SetsArchiveName( )
        {
            Assert.AreEqual( tempFolder, archive.ArchiveName );
        }

        [Test]
        public void IsReadOnly_ReturnsFalse( )
        {
            Assert.IsFalse( archive.IsReadOnly );
        }

        [Test]
        public void OpenStream_ReturnsFileStream( )
        {
            var filename = Guid.NewGuid( ).ToString( );
            using( var stream = File.CreateText( Path.Combine( tempFolder, filename ) ) )
            {
                stream.WriteLine( "test" );
                stream.Flush( );
            }

            try
            {
                using ( var stream = this.archive.OpenFile( filename ) )
                {
                    Assert.IsNotNull( stream );
                } 
            }
            finally
            {
                File.Delete( Path.Combine( tempFolder, filename ) );
            }
            
        }
    }
}
