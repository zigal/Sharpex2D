// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Sharpex2D.Audio;
using Sharpex2D.Rendering;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class EngineConfiguration : IComponent
    {
        /// <summary>
        /// Initializes a new EngineConfiguration class.
        /// </summary>
        /// <param name="graphicsManager">The GraphicsManager.</param>
        public EngineConfiguration(GraphicsManager graphicsManager)
            : this(graphicsManager, null)
        {
        }

        /// <summary>
        /// Initializes a new EngineConfiguration class.
        /// </summary>
        /// <param name="graphicsManager">The GraphicsManager.</param>
        /// <param name="soundInitializer">The SoundInitializer.</param>
        public EngineConfiguration(GraphicsManager graphicsManager, ISoundInitializer soundInitializer)
        {
            GraphicsManager = graphicsManager;
            SoundInitializer = soundInitializer;
        }

        /// <summary>
        /// Gets the IRenderer.
        /// </summary>
        internal GraphicsManager GraphicsManager { set; get; }

        /// <summary>
        /// Gets the SoundInitializer.
        /// </summary>
        internal ISoundInitializer SoundInitializer { set; get; }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("C3D1DFE3-80A0-4543-AE19-355F09C45DB0"); }
        }
    }
}