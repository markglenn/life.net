using System;
using Life.Math;
using System.Collections.Generic;

namespace Life.Graphics
{
    /// <summary>
    /// Renderable object
    /// </summary>
    public interface IRenderable : IDisposable
    {
        /// <summary>
        /// Gets the render operation that displays this renderable object
        /// </summary>
        RenderOperation RenderOperation { get; }

        /// <summary>
        /// Orientation of this renderable object relative to the world
        /// </summary>
        Quaternion WorldOrientation { get; set; }

        /// <summary>
        /// Position within the world of the object
        /// </summary>
        Vector3 WorldPosition { get; set; }

        /// <summary>
        /// Gets and sets the matrices required to draw
        /// </summary>
        IEnumerable<Matrix4> Matrices { get; set; }
    }
}

