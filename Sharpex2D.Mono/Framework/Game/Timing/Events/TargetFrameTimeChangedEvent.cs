﻿using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Game.Timing.Events
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class TargetFrameTimeChangedEvent : IEvent
    {
        /// <summary>
        ///     Initializes a new TargetFrameTimeChangedEvent class.
        /// </summary>
        public TargetFrameTimeChangedEvent()
        {
        }

        /// <summary>
        ///     Initializes a new TargetFrameTimeChangedEvent class.
        /// </summary>
        /// <param name="fps">The FramesPerSecond.</param>
        /// <param name="targetFrameTime">The TargetFrameTime.</param>
        public TargetFrameTimeChangedEvent(float fps, float targetFrameTime)
        {
            FramesPerSecond = fps;
            TargetFrameTime = targetFrameTime;
        }

        /// <summary>
        ///     Gets the changed fps amount.
        /// </summary>
        public float FramesPerSecond { private set; get; }

        /// <summary>
        ///     Gets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime { private set; get; }
    }
}