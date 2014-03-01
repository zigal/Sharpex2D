﻿using System;
using System.Windows.Forms;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Surface
{
    public class RenderTarget : IComponent
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("0F73D6D0-7CE8-4A77-A184-BE93E77E86B5"); }
        }

        #endregion

        /// <summary>
        /// Initializes a new RenderTarget class.
        /// </summary>
        /// <param name="handle">The WindowHandle.</param>
        internal RenderTarget(IntPtr handle)
        {
            Handle = handle;
            SurfaceControl = new WindowController(this);
        }

        /// <summary>
        /// Gets the WindowHandle.
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// Gets the ISurfaceControl.
        /// </summary>
        public ISurfaceControl SurfaceControl { private set; get; }
        /// <summary>
        /// A value indicating whether the surface is running in fullscreen.
        /// </summary>
        public bool IsFullscreen
        {
            get { return ((WindowController) SurfaceControl).IsFullscreen(); }
        }
        /// <summary>
        /// Create a new RenderTarget from a specified handle.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        /// <returns>RenderTarget</returns>
        public static RenderTarget FromHandle(IntPtr handle)
        {
            if (Control.FromHandle(handle) is Form)
            {
                return new RenderTarget(handle);
            }

            throw new InvalidOperationException("The given Handle is not a window.");
        }
    }
}
