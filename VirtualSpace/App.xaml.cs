﻿/* Copyright (C) 2021 Dylan Cheng (https://github.com/newlooper)

This file is part of VirtualSpace.

VirtualSpace is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

VirtualSpace is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with VirtualSpace. If not, see <https://www.gnu.org/licenses/>.
*/

using System.Windows;
using System;
using System.Threading;
using System.Windows.Forms;
using VirtualSpace.AppLogs;
using VirtualSpace.Commons;
using VirtualSpace.Log;
using VirtualSpace.VirtualDesktop;
using VirtualSpace.VirtualDesktop.Api;
using ConfigManager = VirtualSpace.Config.Manager;
using Application = System.Windows.Application;

namespace VirtualSpace
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex? _mutex;

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            LogManager.GorgeousDividingLine();
            _mutex = SingleInstanceCheck();

            if ( _mutex != null &&
                 AssemblyLoader.VersionCheck() &&
                 ConfigManager.Init() )
            {
                Current.MainWindow = CreateCanvas( e );
                Current.MainWindow.Show();
            }
            else
            {
                Current.Shutdown();
            }
        }

        protected override void OnExit( ExitEventArgs e )
        {
            base.OnExit( e );

            _mutex?.ReleaseMutex();
            IpcPipe.IsRunning = false;
        }

        private static Mutex? SingleInstanceCheck()
        {
            var m = new Mutex( true, "乱花渐欲迷人眼", out var createdNew );

            if ( createdNew )
            {
                IpcPipe.AsServer();
                return m;
            }

            IpcPipe.AsClient();
            return null;
        }

        private static MainWindow CreateCanvas( StartupEventArgs args )
        {
            var canvas = VirtualSpace.MainWindow.Create();
            Bootstrap( canvas );
            return canvas;
        }

        private static void Bootstrap( MainWindow mainWindow )
        {
            Logger.Info( "System Version: " + Environment.OSVersion );
            Logger.Info( "Total Screens: " + Screen.AllScreens.Length );
            Logger.Info( "Total Virtual Desktops: " + DesktopWrapper.Count );
            Logger.Info( "Start Screen: " + Screen.FromHandle( mainWindow.Handle ).DeviceName );
            Logger.Info( "Start Virtual Desktop: Desktop[" + DesktopWrapper.CurrentIndex + "]" );
            Logger.Info( "Start Position: " +
                         Screen.PrimaryScreen.Bounds.Location.X + ", " +
                         Screen.PrimaryScreen.Bounds.Location.Y );
            Logger.Info( "Start Size: " +
                         Screen.PrimaryScreen.Bounds.Width + "*" +
                         Screen.PrimaryScreen.Bounds.Height );
            Logger.Info( "Is Running On Administrator: " + Agent.IsAdministrator() );

            // ProcessWatcher.Start();
            // WindowWatcher.Start();
            Daemon.TakeWndHandleSnapshot();
        }
    }
}