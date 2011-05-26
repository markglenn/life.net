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
        private string existingFile;
        private IArchive archive;

        [TestFixtureSetUp]
        public void FixtureSetup( )
        {
            this.existingFile = Path.Combine( tempFolder, Guid.NewGuid( ).ToString( ) );
            using ( var stream = File.CreateText( this.existingFile ) )
                stream.Flush( );
        }

        [TestFixtureTearDown]
        public void FixtureTearDown( )
        {
            File.Delete( this.existingFile );
        }

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
    }
}
