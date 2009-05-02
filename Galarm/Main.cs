using System;
using Gtk;

namespace Galarm
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				Application.Init ();
				Dock alarm = new Dock ();
				alarm.Show ();
				Application.Run ();
			}
			catch(Exception fail)
			{
				Console.WriteLine("Total Failure + " + fail.StackTrace.ToString());	
			}
		}
	}
}