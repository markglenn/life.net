using System;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using Life.Archive;
using System.IO;

namespace Life.Core.Tests.Archive
{
	[TestFixture]
	public class CompositeArchiveTests
	{
		private IArchive archive;
		private CompositeArchive composite;
		
		[SetUp]
		public void Setup( )
		{
			this.archive = A.Fake<IArchive>( );
			this.composite = new CompositeArchive( );
			
			this.composite.Add( archive );
		}
		
		[Test]
		public void GetArchivesWithFile_ReturnsArchivesThatHaveFile( )
		{
			A.CallTo( ( ) => archive.Exists( "dummy.txt" ) ).Returns( true );
			Assert.AreSame( archive, composite.GetArchivesWithFile( "dummy.txt" ).First( ) );
		}
		
		[Test]
		public void GetArchivesWithFile_ReturnsEmptyIfNoFile( )
		{
			Assert.IsEmpty( composite.GetArchivesWithFile( "missing_file.txt" ).ToArray( ) );	
		}
		
		[Test]
		public void LoadFile_LoadsFileFromChild( )
		{
			var stream = A.Dummy<Stream>( );
			A.CallTo( ( ) => archive.Exists( "dummy.txt" ) ).Returns( true );
			A.CallTo( ( ) => archive.OpenFile( A<string>.Ignored ) ).Returns( stream );
			
			Assert.AreSame( stream, composite.OpenFile( "dummy.txt" ) );
		}

	}
}

