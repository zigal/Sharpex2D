﻿using System;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.Streams;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D.Framework.Media.Sound.WaveOut
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class WaveOutSoundProvider : ISoundProvider
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CA814AC6-6B43-4B34-9713-8CDB3F8E5602"); }
        }

        #endregion

        private bool _disposed;
        private PanSource _panSource;
        private ISoundOut _soundOut;

        /// <summary>
        ///     Initializes a new WaveOutSoundProvider class.
        /// </summary>
        internal WaveOutSoundProvider()
        {
            _soundOut = new WaveOutWindow(SGL.Components.Get<RenderTarget>().Handle);
        }

        /// <summary>
        ///     Gets the PlaybackState.
        /// </summary>
        private PlaybackState PlaybackState
        {
            get { return _soundOut.PlaybackState; }
        }

        /// <summary>
        ///     Disposes the SoundProvider.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Clones the SoundProvider.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        /// <param name="soundFile">The Soundfile.</param>
        /// <param name="playMode">The PlayMode.</param>
        public void Play(Sound soundFile, PlayMode playMode)
        {
            Play(CodecFactory.Instance.GetCodec(soundFile.ResourcePath), playMode);
        }

        /// <summary>
        ///     Resumes a sound.
        /// </summary>
        public void Resume()
        {
            _soundOut.Resume();
        }

        /// <summary>
        ///     Pause a sound.
        /// </summary>
        public void Pause()
        {
            _soundOut.Pause();
            IsPlaying = false;
        }

        /// <summary>
        ///     Seeks a sound to a specified position.
        /// </summary>
        /// <param name="position">The Position.</param>
        public void Seek(long position)
        {
            if (_soundOut.WaveSource != null)
                _soundOut.WaveSource.Position = position;
        }

        /// <summary>
        ///     Sets or gets the Position.
        /// </summary>
        public long Position
        {
            get { return _soundOut.WaveSource != null ? _soundOut.WaveSource.Position : 0; }
            set { Seek(value); }
        }

        /// <summary>
        ///     A value indicating whether the SoundProvider is playing.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        ///     Gets the sound length.
        /// </summary>
        public long Length
        {
            get { return _soundOut.WaveSource != null ? _soundOut.WaveSource.Length : 0; }
        }

        /// <summary>
        ///     Sets or gets the Balance.
        /// </summary>
        public float Balance
        {
            get { return _panSource.Pan; }
            set { _panSource.Pan = value; }
        }

        /// <summary>
        ///     Sets or gets the Volume.
        /// </summary>
        public float Volume
        {
            get { return _soundOut.Volume; }
            set { _soundOut.Volume = value; }
        }

        /// <summary>
        ///     Disposes the SoundProvider.
        /// </summary>
        /// <param name="disposing">The State.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_soundOut != null)
                {
                    Stop();
                    _soundOut.Dispose();
                    _soundOut = null;
                    _panSource = null;
                }
            }
            _disposed = true;
        }

        /// <summary>
        ///     Deconstructs the SoundProvider.
        /// </summary>
        ~WaveOutSoundProvider()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Plays the sound.
        /// </summary>
        /// <param name="waveSource">The WaveSource.</param>
        /// <param name="playMode">The PlayMode.</param>
        private void Play(IWaveSource waveSource, PlayMode playMode)
        {
            Stop();
            if (playMode == PlayMode.Loop)
                waveSource = new LoopStream(waveSource);

            var panSource = new PanSource(waveSource);
            _soundOut.Initialize(panSource.ToWaveSource(16));
            _panSource = panSource;
            _soundOut.Play();
            IsPlaying = true;
        }

        /// <summary>
        ///     Stops the sound.
        /// </summary>
        private void Stop()
        {
            if (PlaybackState != PlaybackState.Stopped)
            {
                _soundOut.Stop();
                IsPlaying = false;
            }
        }
    }
}