﻿using System;
using System.Diagnostics;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Debug;
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Game.Services;
using Sharpex2D.Framework.Game.Services.Achievements;
using Sharpex2D.Framework.Game.Services.Availability;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Media;
using Sharpex2D.Framework.Media.Sound;
using Sharpex2D.Framework.Media.Video;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.Devices;
using Sharpex2D.Framework.Rendering.Scene;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class SGL
    {
        /// <summary>
        ///     Current Game Instance.
        /// </summary>
        public static Game GameInstance;

        /// <summary>
        ///     Initializes a new SGL class.
        /// </summary>
        static SGL()
        {
            State = SGLState.NotInitialized;
        }

        /// <summary>
        ///     Current GraphicsDevice.
        /// </summary>
        internal static GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        ///     The Current Renderer.
        /// </summary>
        internal static RenderDevice RenderDevice { set; get; }

        /// <summary>
        ///     Gets the Version of SGL.
        /// </summary>
        public static string Version
        {
            get { return "0.5.840"; }
        }

        /// <summary>
        ///     Gets the current state.
        /// </summary>
        public static SGLState State { private set; get; }

        /// <summary>
        ///     ComponentManager Instance.
        /// </summary>
        public static ComponentManager Components { private set; get; }

        /// <summary>
        ///     Initializes SGL.
        /// </summary>
        public static void Initialize()
        {
            Game gameInstance = InitializeHelper.GetGameClass();
            RenderTarget renderTarget;

            try
            {
                renderTarget = RenderTarget.Default;
            }
            catch (InvalidOperationException)
            {
                renderTarget = RenderTarget.Create();
            }

            var sglInitializer = new SGLInitializer(gameInstance, renderTarget);

            Initialize(sglInitializer);
        }

        /// <summary>
        ///     Initializes SGL.
        /// </summary>
        /// <param name="initializer">The Initializer.</param>
        public static void Initialize(SGLInitializer initializer)
        {
            if (State != SGLState.NotInitialized)
            {
                return;
            }

            State = SGLState.Initializing;
            Components = new ComponentManager();
            GameInstance = initializer.GameInstance;
            Components.AddComponent(initializer.RenderTarget);
            Components.AddComponent(new EventManager());
            initializer.RenderTarget.Window.Size = new Vector2(initializer.Width, initializer.Height);
            initializer.GameInstance.Input = new InputManager(initializer.RenderTarget.Handle);
            GraphicsDevice = new GraphicsDevice(initializer.RenderTarget)
            {
                BackBuffer = new BackBuffer(initializer.Width, initializer.Height)
            };
            initializer.GameInstance.Content = new ContentManager();
            initializer.GameInstance.SceneManager = new SceneManager();
            Components.AddComponent(initializer.GameLoop);
            GraphicsDevice.RefreshRate = initializer.GameLoop.TargetFramesPerSecond;
            Components.AddComponent(initializer.GameInstance.Content);
            Components.AddComponent(GraphicsDevice);
            Components.AddComponent(initializer.GameInstance);
            Components.AddComponent(initializer.GameInstance.SceneManager);
            Components.AddComponent(initializer.GameInstance.Input);
            Components.Get<IGameLoop>().Subscribe((IDrawable) GameInstance);
            Components.Get<IGameLoop>().Subscribe((IUpdateable) GameInstance);

            //prepare game services
            var gameServices = new GameServiceContainer();

            gameServices.Add(new AchievementProvider());
            gameServices.Add(new AvailabilityProvider());
            gameServices.Add(new Gamer());
            gameServices.Add(new LaunchParameters());

            GameInstance.GameServices = gameServices;

            Components.AddComponent(new ExceptionHandler());
            Components.AddComponent(new ContentStorage());

            State = SGLState.Initialized;

            EngineConfiguration engineConfiguration =
                GameInstance.OnInitialize(GameInstance.GameServices.GetService<LaunchParameters>());

            Run(engineConfiguration.Renderer, engineConfiguration.MediaInitializer);
        }

        /// <summary>
        ///     Runs SGL based on the specific initialized options.
        /// </summary>
        /// <param name="renderDevice">The RenderDevice.</param>
        /// <param name="mediaInitializer">The MediaInitializer.</param>
        private static void Run(RenderDevice renderDevice, MediaInitializer mediaInitializer)
        {
            if (State != SGLState.Initialized)
                throw new InvalidOperationException("SGL must be initialized in the first place.");

            if (State == SGLState.Running) return;

            RenderDevice = renderDevice;
            RenderDevice.GraphicsDevice = GraphicsDevice;
            RenderDevice.InitializeDevice();
            if (mediaInitializer != null)
            {
                GameInstance.SoundManager = mediaInitializer.SoundInitializer == null
                    ? null
                    : new SoundManager(mediaInitializer.SoundInitializer);
                GameInstance.VideoManager = mediaInitializer.VideoInitializer == null
                    ? null
                    : new VideoManager(mediaInitializer.VideoInitializer);
            }
            Components.AddComponent(renderDevice);
            Components.AddComponent(GameInstance.SoundManager);
            Components.AddComponent(GameInstance.VideoManager);
            Components.Construct();
            GameInstance.OnLoadContent();
            Components.Get<IGameLoop>().Start();

            Log.Next("SGL ({0}) is sucessfully running.", LogLevel.Engine, Version);
            Log.Next("CLR: {0}", LogLevel.Engine, Platform.IsMonoRuntime() ? "Mono" : "Windows");

            State = SGLState.Running;
        }

        /// <summary>
        ///     Closes the current session.
        /// </summary>
        internal static void Shutdown()
        {
            State = SGLState.NotInitialized;

            Components.Get<IGameLoop>().Stop();
            GameInstance.OnUnload();
            GC.Collect();
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        ///     Restarts the game.
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        internal static void Restart(string parameters)
        {
            var gameProcess = new Process
            {
                StartInfo =
                {
                    FileName = Environment.GetCommandLineArgs()[0],
                    Arguments = parameters,
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory
                }
            };

            try
            {
                gameProcess.Start();
                Shutdown();
            }
            catch (Exception)
            {
                Log.Next("Failed to restart the process.", LogLevel.Engine);
            }
        }
    }
}