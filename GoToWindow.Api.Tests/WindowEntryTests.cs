﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoToWindow.Api.Tests
{
	[TestClass]
	public class WindowEntryTests
	{
		[TestMethod]
		public void CanGiveFocusToAWindow()
		{
			using (var app1 = new GivenAnApp("GoToWindow.GetGetWindowEntry_FromTestWindow1"))
			{
				var window1 = WindowEntryFactory.Create(app1.Process.MainWindowHandle);

				using (var app2 = new GivenAnApp("GoToWindow.GetGetWindowEntry_FromTestWindow2"))
				{
					var window2 = WindowEntryFactory.Create(app2.Process.MainWindowHandle);

					Assert.IsFalse(window1.IsForeground());
					Assert.IsTrue(window2.IsForeground());

					window1.Focus();

					Assert.IsTrue(window1.IsForeground());
				}
				
			}

			Assert.IsTrue(true, "This test just ensures the code doesn't crash");
		}

		[TestMethod]
		public void ToString_GetsTheWindowTitle()
		{
			Assert.AreEqual("process (1234): \"Title\"", new WindowEntry { Title = "Title", ProcessId = 1234, ProcessName = "process" }.ToString());
		}
	}
}
