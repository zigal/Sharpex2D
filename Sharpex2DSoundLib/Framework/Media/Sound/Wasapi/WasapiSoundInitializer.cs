﻿using System;

namespace Sharpex2D.Framework.Media.Sound.Wasapi
{
    public class WasapiSoundInitializer : ISoundInitializer
    {
        /// <summary>
        /// Creates a new SoundProvider class.
        /// </summary>
        /// <returns>SoundProvider</returns>
        public ISoundProvider CreateProvider()
        {
            if (!CSCore.SoundOut.WasapiOut.IsSupportedOnCurrentPlatform)
                throw new InvalidOperationException("Wasapi is not supported for your system. (Vista and higher)");
            return new WasapiSoundProvider();
        }
    }
}