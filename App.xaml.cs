﻿using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

namespace BikeMessenger
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>

    public sealed partial class App : Application
    {
        private static readonly TransferVar LvrTransferVar = new TransferVar();
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;

            // Get theme choice from LocalSettings.
            object value = ApplicationData.Current.LocalSettings.Values["themeSetting"];

            if (value != null)
            {
                // Apply theme choice.
                Current.RequestedTheme = (ApplicationTheme)(int)value;
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        /// 
        
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
        
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }


        /*
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame
                {
                    CacheSize = 0
                };
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                // rootFrame.Navigate(typeof(PageEmpresa), e.Arguments);
                // IniciarBaseDeDatos LvrIniciarBaseDeDatos = new IniciarBaseDeDatos(LvrTransferVar.DIRECTORIO_BASE_LOCAL);
                // LvrIniciarBaseDeDatos = null;
                // Empresa
                // LvrTransferVar.VerificarSQLite();
                LvrTransferVar.EMP_PENTALPHA = "";
                LvrTransferVar.EMP_RUTID = "";
                LvrTransferVar.EMCLI_DIGVER = "";
                // Personal
                LvrTransferVar.PER_PENTALPHA = "";
                LvrTransferVar.PER_RUTID = "";
                LvrTransferVar.PER_DIGVER = "";
                // Recursos
                LvrTransferVar.REC_PENTALPHA = "";
                LvrTransferVar.REC_RUTID = "";
                LvrTransferVar.REC_DIGVER = "";
                LvrTransferVar.REC_PAT_SER = "";
                // Clientes
                LvrTransferVar.CLI_PENTALPHA = "";
                LvrTransferVar.CLI_RUTID = "";
                LvrTransferVar.CLI_DIGVER = "";
                // Servicios
                LvrTransferVar.SER_PENTALPHA = "";
                LvrTransferVar.SER_PENTALPHA = "";

                rootFrame.CacheSize = 0;
                rootFrame.Navigate(typeof(PageEmpresa), LvrTransferVar, new SuppressNavigationTransitionInfo());
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }
        */
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            // Cierre de XMPP
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
