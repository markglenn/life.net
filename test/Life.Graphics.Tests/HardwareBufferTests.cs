using System;
using NUnit.Framework;
using FakeItEasy;
using Life.Graphics;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class HardwareBufferTests
	{
		class MockHardwareBuffer : HardwareBuffer
		{
			public override bool DoLoad( )
			{
				return false;
			}
			
			public override bool DoUnload( )
			{
				return false;
			}

		}
	}
}

