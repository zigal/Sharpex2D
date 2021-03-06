﻿// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Batch<T> : IBatch where T : IContent
    {
        /// <summary>
        /// BatchEventHandler.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void BatchEventHandler(object sender, BatchEventArgs<T> e);

        /// <summary>
        /// Initializes a new Batch class.
        /// </summary>
        /// <param name="asset">The Asset.</param>
        public Batch(string asset)
        {
            Asset = asset;
        }

        /// <summary>
        /// Gets the Type.
        /// </summary>
        public Type Type
        {
            get { return typeof (T); }
        }

        /// <summary>
        /// Gets the Asset.
        /// </summary>
        public string Asset { private set; get; }

        /// <summary>
        /// Raises the completed event.
        /// </summary>
        /// <param name="data">The Data.</param>
        void IBatch.RaiseEvent(object data)
        {
            RaiseEvent((T) data);
        }

        /// <summary>
        /// Completed event.
        /// </summary>
        public event BatchEventHandler Completed;

        /// <summary>
        /// Raises the completed event.
        /// </summary>
        /// <param name="data">The Data.</param>
        internal void RaiseEvent(T data)
        {
            if (Completed != null)
            {
                Completed(this, new BatchEventArgs<T>(Asset, data));
            }
        }
    }
}