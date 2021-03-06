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
using System.Threading;

namespace Espresso.Console
{
	class MainClass
	{
		public static void Main(String[] args)
		{
			CancellationTokenSource cancellation = new CancellationTokenSource();

			System.Console.CancelKeyPress +=
				(Object sender, ConsoleCancelEventArgs e) =>
				{
					e.Cancel = true;
					cancellation.Cancel();
				};

			using (ISleepInhibitor inhibitor = SleepInhibitor.StartNew())
			{
				System.Console.WriteLine("Espresso is keeping the computer awake.");
				System.Console.WriteLine("Press Ctrl+C to exit.");
				cancellation.Token.WaitHandle.WaitOne();
			}
		}
	}
}
