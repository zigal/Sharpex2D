﻿using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Network.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PackageSentExceptionEvent : IEvent
    {
        /// <summary>
        ///     Initializes a new PackageSentExceptionEvent class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public PackageSentExceptionEvent(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Gets the exception message.
        /// </summary>
        public string Message { private set; get; }
    }
}