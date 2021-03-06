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
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Sharpex2D.Common
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public abstract class TaskWorker : IComponent, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get; set; }

        #endregion

        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    _task.Dispose();
                }
            }
        }

        #endregion

        private readonly Task _task;

        /// <summary>
        /// Initializes a new TaskComponent class.
        /// </summary>
        protected TaskWorker()
        {
            Guid = Guid.NewGuid();
            _task = new Task(Work);
        }

        /// <summary>
        /// Gets the ProgressPercentage.
        /// </summary>
        public abstract int ProgressPercentage { get; }

        /// <summary>
        /// Starts the
        /// </summary>
        public void Start()
        {
            if (_task.Status != TaskStatus.Running)
            {
                _task.Start();
            }
        }

        /// <summary>
        /// Starts the work.
        /// </summary>
        public virtual void Work()
        {
        }
    }
}