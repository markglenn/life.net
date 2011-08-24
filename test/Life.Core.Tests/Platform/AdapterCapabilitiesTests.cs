using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;

namespace Life.Core.Platform.Tests
{
	[TestFixture]
	public class AdapterCapabilitiesTests
	{
		[Test]
		public void Ctor_SetsDisplays( )
		{
			var displays = A.CollectionOfFake<DisplayCapabilities>( 2 );
			
			var adapter = new AdapterCapabilities( displays );
			
			Assert.IsTrue( adapter.Displays.SequenceEqual( displays ) );
		}
	}
}

