﻿using System;

namespace Sharpex2D.Framework.Content
{
    public interface IContentSerializer : IContentReader, IContentWriter
    {
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        Type ContentType { get; }
        /// <summary>
        /// The ContentManager.
        /// </summary>
        ContentManager Content { get; }
    }
}