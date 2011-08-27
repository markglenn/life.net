using System;
using TaoSdl = Tao.Sdl.Sdl;

namespace Life.Input.Sdl
{
	public class SdlInputManager : InputManager
	{
		public SdlInputManager ()
		{
			TaoSdl.SDL_Init( TaoSdl.SDL_INIT_EVENTTHREAD );
		}

		#region implemented abstract members of Life.Input.InputManager
		
		public override Keyboard GetKeyboard ()
		{
			throw new NotImplementedException ();
		}

		public override Mouse GetMouse ()
		{
			return new SdlMouse( );
		}
		
		protected override void Dispose (bool disposing)
		{
			if ( disposing )
				TaoSdl.SDL_Quit( );
		}
		
		#endregion
		
	}
}

