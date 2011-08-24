using System;
using System.Linq;
using System.IO;
using Life.Archive;
using NUnit.Framework;

namespace Life.Tests.Archive
{
    [TestFixture]
    public class FolderArchiveTests
    {
        private readonly string tempFolder = Path.Combine( Path.GetTempPath( ), Guid.NewGuid( ).ToString( ) );
        private string existingFile;
        private IArchive archive;

        [TestFixtureSetUp]
        public void FixtureSetup( )
        {
            Directory.CreateDirectory( tempFolder );
            this.existingFile = Path.Combine( tempFolder, Guid.NewGuid( ).ToString( ) );
            using ( var stream = File.CreateText( this.existingFile ) )
                stream.Flush( );
        }

        [TestFixtureTearDown]
        public void FixtureTearDown( )
        {
            Directory.Delete( tempFolder, true );
        }

        [SetUp]
        public void Setup( )
        {
            this.archive = new FolderArchive( this.tempFolder );
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
            using ( var stream = this.archive.OpenFile( this.existingFile ) )
            {
                Assert.IsNotNull( stream );
            }
        }

        [Test]
        public void OpenStream_WithNonExistantFile_ThrowsFileNotFoundException( )
        {
            Assert.Throws<FileNotFoundException>( 
                ( ) => this.archive.OpenFile( Guid.NewGuid( ).ToString( ) ) );
        }

        [Test]
        public void Exists_IsFalseWhenFileDoesntExist( )
        {
            Assert.IsFalse( this.archive.Exists( Guid.NewGuid( ).ToString( ) ) );
        }

        [Test]
        public void Exists_IsTrueWhenFileExists( )
        {
            Assert.IsTrue( this.archive.Exists( this.existingFile ) );
        }

        [Test]
        public void CreateWritableStream_ReturnsNewWritableStream( )
        {
            string filename = Guid.NewGuid( ).ToString( );
            using( var stream = this.archive.CreateWritableStream( filename ) )
            {
                Assert.IsNotNull( stream );
                stream.Flush( );
            }

            Assert.IsTrue( this.archive.Exists( filename ) );
            File.Delete( Path.Combine( this.archive.ArchiveName, filename ) );
        }

        [Test]
        public void DeleteFile_DeletesFile( )
        {
            string filename = Guid.NewGuid( ).ToString( );
            using ( var stream = this.archive.CreateWritableStream( filename ) )
                stream.Flush( );

            this.archive.DeleteFile( filename );

            Assert.IsFalse( this.archive.Exists( filename ) );
        }

        [Test]
        public void GetEnumerator_GetsEnumeratorOfFiles( )
        {
            Assert.That( this.archive.Any( i => i == Path.GetFileName( this.existingFile ) ) );
        }
    }
}
