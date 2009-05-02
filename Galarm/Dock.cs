using System;
using System.Diagnostics;
using System.Timers;
using System.Threading;
using System.IO;
using Gtk;
using Gdk;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


public partial class Dock : Gtk.Window
{		
	private StatusIcon trayIcon;
	private MainWindow alarmControllerGui;
	private System.Timers.Timer statusIconUpdater;
	private delegate void updateStatusIcon(object sender, EventArgs e);
			
	public Dock() : base(Gtk.WindowType.Toplevel)
	{
		new Galarm.UserAlarms();
		
		this.trayIcon = new StatusIcon(getStatusIconPixbuf());
		this.trayIcon.Tooltip = "Right click for menu (" + toolTipNextAlarm() + ")";		
		this.trayIcon.Visible = true;
		this.trayIcon.PopupMenu += OnStatusIconPopupMenu;	
		this.trayIcon.Activate += delegate { OnShowEvent(this,new EventArgs()); };
		
		this.Build();		
	}

	
	private String toolTipNextAlarm()
	{
		DateTime nextAlarm = Galarm.UserAlarms.getNextAlarmCountdown();
		String tooltipString = "Next Alarm  " + nextAlarm.Hour + ":" + nextAlarm.Minute + ":" + nextAlarm.Second;  
		return tooltipString;
	}
	
	
	private Pixbuf getStatusIconPixbuf()
	{
		Bitmap image = new Bitmap("/home/dave/projects/Galarm/Galarm/bin/Release/icon2.png");
		int width = image.Width;
		int height = image.Height;
		
		Graphics graphicImage = Graphics.FromImage( image );
		graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
		
		DateTime nextAlarm = Galarm.UserAlarms.getNextAlarmCountdown();
		
		int xPointHour = 0;
		
		if(nextAlarm.Hour<10)
		{
			xPointHour = (int) (width / 2.5);
		}
		else
		{
			xPointHour = (int) (width / 3.35);
		}
		
		
		int xPointMinute = 0;
		
		if(nextAlarm.Minute<10)
		{
			xPointMinute = (int) (width / 2.5);
		}
		else
		{
			xPointMinute = (int) (width / 3.35);
		}
		
		
		graphicImage.DrawString( nextAlarm.Hour.ToString(), new System.Drawing.Font("Arial", (height / 5),FontStyle.Bold ), SystemBrushes.WindowText, new System.Drawing.Point( xPointHour, (int) (height / 3.3) ) );
		graphicImage.DrawString( nextAlarm.Minute.ToString(), new System.Drawing.Font("Arial", (height / 5),FontStyle.Bold ), SystemBrushes.WindowText, new System.Drawing.Point( xPointMinute, (int) (height / 1.7) ) ); 
		
		if ( image != null ) 
		{
            using ( System.IO.MemoryStream stream = new MemoryStream() ) 
			{
               image.Save( stream, System.Drawing.Imaging.ImageFormat.Png );
               stream.Position = 0;
               Gdk.Pixbuf pixbuf = new Gdk.Pixbuf( stream );
               return pixbuf;
            }
        } 
		else 
		{
            return null;
        }
	}
	
	
	protected void OnStatusIconPopupMenu(object sender, EventArgs e)
	{
		Menu popupMenu = new Menu();
			
		ImageMenuItem alarmItem = new ImageMenuItem("Alarm Settings");
		alarmItem.Image = new Gtk.Image("/home/dave/projects/Galarm/Galarm/bin/Release/icon2-small.png");
		alarmItem.Show();
		alarmItem.Activated += new EventHandler(OnShowEvent);
		popupMenu.Append(alarmItem);	
		
		if(Galarm.ProcessController.getProcessControllers().Count>0)
		{
			ImageMenuItem stopItem = new ImageMenuItem("Stop Alarms");
			stopItem.Image = new Gtk.Image(Stock.Stop);
			stopItem.Show();
			stopItem.Activated += new EventHandler(stopRunningAlarms);		
			popupMenu.Append(stopItem);						
		}
		
		ImageMenuItem quitItem = new ImageMenuItem("Quit");
		quitItem.Image = new Gtk.Image(Stock.Quit);
		quitItem.Show();
		quitItem.Activated += new EventHandler(OnUserDestroyEvent);
		popupMenu.Append(quitItem);		
		
		popupMenu.Popup(null,null,null,3,Gtk.Global.CurrentEventTime);
	}
			
	
	private void stopRunningAlarms(object sender, EventArgs e)
	{
		Galarm.AwakeTest test = new Galarm.AwakeTest();
		test.Move(Screen.Width - 430, 40);
		test.Stick();
		test.Show();		
	}
	
		
	protected void OnShowEvent (object sender, EventArgs e )
	{			
		
		if(this.alarmControllerGui==null)
		{
			
			this.alarmControllerGui = new MainWindow();
			this.alarmControllerGui.Move(Screen.Width - 430, 40);
			this.alarmControllerGui.Show();				
		}
		else
		{
			this.alarmControllerGui.Destroy();
			this.alarmControllerGui = null;	
		}
	}
				
		
	protected void OnUserDestroyEvent(object sender, EventArgs a)
	{
		try 
		{
			Galarm.ProcessController.clearProcessControllers();
			Galarm.VolumeController.clearVolumeControllers();
			Galarm.UserAlarms.stopAllTimers();
			this.statusIconUpdater.Stop();
			System.Threading.Thread.Sleep(500);
			Application.Quit ();
		} 
		catch (Exception) 
		{
			Console.WriteLine("Dirty Termination");
			Application.Quit ();	
		}
		finally
		{
			Application.Quit ();	
		}			
	}
	
}
