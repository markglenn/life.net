using System;
using TaoSdl = Tao.Sdl.Sdl;

namespace Life.Input.Sdl
{
	public class SdlMouse : Mouse
	{
		#region implemented abstract members of Life.Input.Mouse
		
		public override void Update( GameTime gameTime )
		{
			TaoSdl.SDL_PumpEvents( );
			
			int x, y;
			var buttons = TaoSdl.SDL_GetRelativeMouseState( out x, out y );
			
			this.SetMouseButtonState( MouseButton.Left, ( buttons & TaoSdl.SDL_BUTTON_LEFT ) != 0 );
			this.SetMouseButtonState( MouseButton.Middle, ( buttons & TaoSdl.SDL_BUTTON_MIDDLE ) != 0 );
			this.SetMouseButtonState( MouseButton.Right, ( buttons & TaoSdl.SDL_BUTTON_RIGHT ) != 0 );
		}

		public override string Name
		{
			get { return "SDL Mouse Service"; }
		}

		#endregion
	}
}

