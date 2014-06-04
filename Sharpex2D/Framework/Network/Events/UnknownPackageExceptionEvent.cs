﻿using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Network.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class UnknownPackageExceptionEvent : IEvent
    {
        /// <summary>
        ///     Initializes a new UnknownPackageExceptionEvent class.
        /// </summary>
        public UnknownPackageExceptionEvent()
        {
            Message = "";
        }

        /// <summary>
        ///     Initializes a new UnknownPackageExceptionEvent class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public UnknownPackageExceptionEvent(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Gets the event message.
        /// </summary>
        public string Message { private set; get; }
    }
}