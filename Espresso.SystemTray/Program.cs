﻿//
//  This file is part of Espresso <https://github.com/piedar/Espresso>.
//
//  Author(s):
//        Bennjamin Blast <bennjamin.blast@gmail.com>
//
//  Copyright (c) 2016 Bennjamin Blast
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
using System;
using System.IO;
using Gtk;

namespace Espresso.SystemTray
{
	interface IGraphicalSleepInhibitor : ISleepInhibitor
	{
		void Run();
	}

	static class GraphicalSleepInhibitor
	{
		public static IGraphicalSleepInhibitor CreateNew()
		{
			try
			{
				return new GtkTraySleepInhibitor(); // GTK works great wherever it's available.
			}
			catch (SystemException ex)
			{
				System.Diagnostics.Debug.WriteLine($"Failed to load GtkTraySleepInhibitor due to {ex}");
				return new WinformsTraySleepInhibitor(); // Winforms are OK on Windows, but look awful everywhere else.
			}
		}
	}

	static class Icons
	{
		public static readonly String EmptyCupFile = Path.Combine("Icons", "Empty_Cup.svg");
		public static readonly String FullCupFile = Path.Combine("Icons", "Full_Cup.svg");

		public static readonly String EmptyCupIconFile = Path.Combine("Icons", "Empty_Cup.ico");
		public static readonly String FullCupIconFile = Path.Combine("Icons", "Full_Cup.ico");
	}

	static class MainClass
	{
		[STAThread]
		static void Main(String[] args)
		{
			using (IGraphicalSleepInhibitor trayIcon = GraphicalSleepInhibitor.CreateNew())
			{
				trayIcon.Run();
			}
		}
	}
}
