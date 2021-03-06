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
using System.Diagnostics;

namespace Espresso
{
	public static class SleepInhibitor
	{
		/// <summary>
		/// Creates and activates a new <see cref="ISleepInhibitor"/> for the current platform.
		/// </summary>
		public static ISleepInhibitor StartNew()
		{
			ISleepInhibitor inhibitor = CreateNew();
			inhibitor.IsInhibited = true;
			return inhibitor;
		}

		/// <summary>
		/// Creates a new <see cref="ISleepInhibitor"/> for the current platform.
		/// </summary>
		public static ISleepInhibitor CreateNew()
		{
			try
			{
				return new Win32SleepInhibitor();
			}
			catch (TypeLoadException ex)
			{
				Debug.WriteLine(String.Format("Failed to load Win32SleepInhibitor because '{0}'", ex));
				return new DBusSleepInhibitor();
			}
		}
	}
}
