using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Timers;
using System.IO;
using Gtk;
using Gdk;


public partial class MainWindow: Gtk.Window
{
	private Boolean create = false;
	private int comboBoxNumEntries = 1;
	private int selectedIndex = 0;
	private String firstFoundMusicFile = "";

	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{				
		Build ();
		
		dayControlsVisible(false);
		alarmSettingsVisible(false);	
		
		FileFilter filter1 = new FileFilter();
		filter1.Name = "Audio Files";
		filter1.AddPattern("*.mp3");
		filter1.AddPattern("*.wav");
		filter1.AddPattern("*.ogg");
		filter1.AddPattern("*.flac");
				
		FileFilter filter2 = new FileFilter();
		filter2.Name = "Audio Playlists";
		filter2.AddPattern("*.m3u");
		filter2.AddPattern("*.pls");
		
		this.filechooserbutton1.AddFilter(filter1);
		this.filechooserbutton1.AddFilter(filter2);
		
		this.button1.Visible = false;
		
		this.loadAlarms();

	}
	
	
	private void alarmSettingsVisible(Boolean vis)
	{				
		this.hbox12.Visible = vis;
		this.hbox13.Visible = vis;
		this.vbox14.Visible = vis;		
		this.hbox2.Visible = vis;
		this.hbox3.Visible = vis;
		this.hbox4.Visible = vis;
		this.hbox5.Visible = vis;
		this.hbox6.Visible = vis;
		this.hbox7.Visible = vis;
		this.hseparator3.Visible = vis;	
		this.hseparator2.Visible = vis;
		this.hseparator1.Visible = vis;
		this.hseparator4.Visible = vis;
		this.image2.Visible = false;
		
		if(vis==false)
			this.Resize(420,55);
		else
			this.Resize(420,226);
	}
		
	private void dayControlsVisible(Boolean vis)
	{				
		this.checkbutton11.Visible = vis;
		this.checkbutton12.Visible = vis;
		this.checkbutton13.Visible = vis;
		this.checkbutton15.Visible = vis;
		this.checkbutton16.Visible = vis;
		this.checkbutton17.Visible = vis;
		this.checkbutton18.Visible = vis;	
		this.vbox10.Visible = vis;
		this.vbox11.Visible = vis;
		this.vbox12.Visible = vis;
		this.vbox13.Visible = vis;
		this.hbox14.Visible = vis;
		this.hbox1.Visible = vis;
		this.hseparator3.Visible = vis;
		
		if(vis==false)
			this.Resize(420,226);
		else
			this.Resize(420,310);
	}	
			
	private void loadAlarms()
	{	
		if(this.comboBoxNumEntries>1)
		{
			for(int h=this.comboBoxNumEntries; h > 0; h--)
			{
				this.combobox3.RemoveText(h);				
			}
			this.comboBoxNumEntries = 1;
		}
		
		ArrayList savedAlarms = Galarm.UserAlarms.getAlarms();
						
				
		try
		{		
			foreach (object j in savedAlarms)
			{						
				Galarm.UserAlarms alarm = (Galarm.UserAlarms) j;
				
				String name = alarm.getName();
				int Ahour = alarm.getHour();
				int Amin = alarm.getMin();
				int Asec = alarm.getSec();
				int mon = alarm.getMonday();
				int tue = alarm.getTuesday();
				int wed = alarm.getWednesday();
				int thur = alarm.getThursday();
				int fri = alarm.getFriday();
				int sat = alarm.getSaturday();
				int sun = alarm.getSunday();
				
				int days = 0;
				
				String dayShow = "";
						
				if(mon==1)
				{
					days++;
					dayShow += "M,";
				}
				if(tue==1)
				{
					days++;		
					dayShow += "Tue,";
				}
				if(wed==1)
				{
					days++;
					dayShow += "W,";
				}
				if(thur==1)
				{
					days++;	
					dayShow += "Thur,";
				}
				if(fri==1)
				{
					days++;
					dayShow += "F,";
				}
				if(sat==1)
				{
					days++;	
					dayShow += "Sat,";
				}
				if(sun==1)
				{
					days++;		
					dayShow += "Sun";
				}
				
				
				if(days==7)
					dayShow = "Everyday";

				String entry = name + " - " + Ahour + ":" + Amin + ":" + Asec + " - ( " + dayShow + " )";	
				this.combobox3.AppendText(entry);	
				this.comboBoxNumEntries++;
			}
		}
		catch(Exception e)
		{
			Console.WriteLine(e.StackTrace.ToString());	
		}
		
		this.combobox3.Active = 0;
	}
	
	public void addAlarm()
	{		
		try 
		{
			Galarm.UserAlarms.add(this.entry1.Text,(int)this.spinbutton7.Value,(int)this.spinbutton6.Value,(int)this.spinbutton5.Value,this.checkbutton11.Active,this.checkbutton12.Active,this.checkbutton13.Active,this.checkbutton18.Active,this.checkbutton15.Active,this.checkbutton16.Active,this.checkbutton17.Active,this.checkbutton9.Active,this.filechooserbutton2.Filename.ToString(),this.filechooserbutton1.Filename.ToString(),(int)this.hscale2.Value, true);
			this.loadAlarms();
		} 
		catch (Exception b) 
		{
			Console.WriteLine(b.Message);
		}			
	}
	
	public void updateAlarm()
	{
		int index = combobox3.Active - 1;
		Galarm.UserAlarms.update(index,this.entry1.Text,(int)this.spinbutton7.Value,(int)this.spinbutton6.Value,(int)this.spinbutton5.Value,this.checkbutton11.Active,this.checkbutton12.Active,this.checkbutton13.Active,this.checkbutton18.Active,this.checkbutton15.Active,this.checkbutton16.Active,this.checkbutton17.Active,this.checkbutton9.Active,this.filechooserbutton2.Filename.ToString(),this.filechooserbutton1.Filename.ToString(),(int)this.hscale2.Value);
		this.loadAlarms();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{		
		this.Destroy();
	}

	protected virtual void OnCheckbutton10Clicked (object sender, System.EventArgs e)
	{
		if(checkbutton10.Active==true)
		{
			this.checkbutton11.Active = true;
			this.checkbutton12.Active = true;
			this.checkbutton13.Active = true;
			this.checkbutton15.Active = true;
			this.checkbutton16.Active = true;
			this.checkbutton17.Active = true;
			this.checkbutton18.Active = true;
			dayControlsVisible(false);			
		}
		else
		{
			dayControlsVisible(true);
		}
	}
	
	protected virtual void OnButton3Clicked (object sender, System.EventArgs e)
	{
		if(button3.Label == "Set Alarm")
		{			
			this.button4.Visible = false;
			this.button3.Label = "Create new";
			dayControlsVisible(false);
			alarmSettingsVisible(false);	
			
			if(this.create==true)
			{
				addAlarm();
				this.create = false;
			}
			else
			{
				updateAlarm();
			}	
			
			this.combobox3.Visible = true;
			this.button1.Visible = false;
			
		}
		else
		{		
			this.create = true;
			checkbutton10.Active = true;
			checkbutton9.Active = true;
			this.button4.Visible = true;
			this.button3.Label = "Set Alarm";
			dayControlsVisible(false);
			alarmSettingsVisible(true);			
			
			if (File.Exists("/usr/bin/vlc"))
			{
				this.filechooserbutton2.SetFilename("/usr/bin/vlc");
			}
			
			String music = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Music";			
			DirectoryInfo di = new DirectoryInfo(music);
			ShowDirectory(di);
			this.filechooserbutton1.SetFilename(this.firstFoundMusicFile);
		}
	}

	
	public void ShowDirectory(DirectoryInfo di) 
	{
    	try
    	{     		
			if(this.firstFoundMusicFile.Length<2)
			{
			
				String[] files = Directory.GetFiles(di.FullName,"*.mp3");
				if(files.Length>0)
				{
						for(int y =0; y < files.Length; y++)
						{
							this.firstFoundMusicFile = files[y];						
						}
				}
				else
				{						
      				foreach (DirectoryInfo diChild in di.GetDirectories())
					{		
        				ShowDirectory(diChild);					
					}
				}
    		} 
		}
    	catch {} 
    	finally{}
  }
	
	
	protected virtual void OnButton4Clicked (object sender, System.EventArgs e)
	{
		this.button3.Label = "Create new";
		this.button4.Visible = false;
		dayControlsVisible(false);
		alarmSettingsVisible(false);	
		this.combobox3.Active = 0;
		this.create = false;
		this.combobox3.Visible = true;
		this.button1.Visible = false;
	}
	
	
	protected virtual void OnCombobox3Changed (object sender, System.EventArgs e)
	{				
		int index = this.combobox3.Active -1;
		this.selectedIndex = index + 1;
		this.combobox3.Visible = false;
		this.button1.Visible = true;
		if(index<=-1)
		{
			this.button4.Visible = false;
			this.button3.Label = "Create new";
			dayControlsVisible(false);
			alarmSettingsVisible(false);
			this.create = false;
			return;
		}
		
		object[] alarms = Galarm.UserAlarms.getAlarms().ToArray();
		Galarm.UserAlarms alarm = (Galarm.UserAlarms) alarms[index];
					
		String name = alarm.getName();
		int Ahour = alarm.getHour();
		int Amin = alarm.getMin();
		int Asec = alarm.getSec();
		int mon = alarm.getMonday();
		int tue = alarm.getTuesday();
		int wed = alarm.getWednesday();
		int thur = alarm.getThursday();
		int fri = alarm.getFriday();
		int sat = alarm.getSaturday();
		int sun = alarm.getSunday();
		int active = alarm.getActive();
		String command = alarm.getProcessName();		
		String argument =alarm.getProcessArguments();
		int vol = alarm.getVolume();
		
		this.filechooserbutton2.SetFilename(command);
		this.filechooserbutton1.SetFilename(argument);		
		this.hscale2.Value = vol;
				
		
		this.checkbutton11.Active = false;
		this.checkbutton12.Active = false;
		this.checkbutton13.Active = false;
		this.checkbutton15.Active = false;
		this.checkbutton16.Active = false;
		this.checkbutton17.Active = false;
		this.checkbutton18.Active = false;
				
		this.entry1.Text = name;
		this.spinbutton7.Value = Ahour;
		this.spinbutton6.Value = Amin;
		this.spinbutton5.Value = Asec;
		
		int days = 0;
		
		if(mon==1)
		{
			this.checkbutton11.Active = true;
			days++;
		}
		if(tue==1)
		{
			this.checkbutton12.Active = true;
			days++;
		}
		if(wed==1)
		{
			this.checkbutton13.Active = true;
			days++;
		}
		if(thur==1)
		{
			this.checkbutton18.Active = true;
			days++;
		}
		if(fri==1)
		{
			this.checkbutton15.Active = true;
			days++;
		}
		if(sat==1)
		{
			this.checkbutton16.Active = true;
			days++;
		}
		if(sun==1)
		{
			this.checkbutton17.Active = true;
			days++;
		}
		
		
		alarmSettingsVisible(true);
		this.button4.Visible = true;
		this.button3.Label = "Set Alarm";
		
		if(active==1)
			this.checkbutton9.Active = 	true;
		else
			this.checkbutton9.Active = 	false;
		
		if(days==7)
		{
			checkbutton10.Active = true;
			dayControlsVisible(false);
		}
		else
		{
			checkbutton10.Active = false;
			dayControlsVisible(true);
		}
				
	}
	
	
	public int getSum(int n)
	{
		if(n==0)
			return 0;
		else
			return n + getSum(n -1);
	}
		
	
	public int getPercent(int breakerSeconds)
	{
		int count = 0;
		int iter = 0;
		while(iter < breakerSeconds)
		{
			count++;
			iter = iter + count;		
		}
		return count;
	}
	
	
	
	public Bitmap graph(int width, int height, int initial, int soundIncrement)
	{
		Bitmap bmp = new Bitmap(width,height,PixelFormat.Format32bppArgb);
				
		int mins  = this.getSum(((100 - initial) * 1000) / soundIncrement) / 60;
		
		int timeDividers = (int) System.Math.Round(new Decimal(width / mins));
		int percentDividers = (int) System.Math.Round(new Decimal(height / ((100 - initial) / 10)));
		
		for(int w = 0; w < width; w++)
		{
			for(int h = 0; h < height; h++)
			{
				if(h>0)
				{
					if(h % percentDividers == 0)
					{
						bmp.SetPixel(w,h,System.Drawing.Color.Blue);	
					}
					else
					{
						bmp.SetPixel(w,h,System.Drawing.Color.White);	
					}
				}	
				if(w>0)
				{
					if(w % timeDividers == 0)
					{						
						bmp.SetPixel(w,h,System.Drawing.Color.FromArgb(2,255,0,0));	
					}					
				}	
			}
		}		
					
		
		int y = percentDividers - 12;
		
		Graphics graphicImage = Graphics.FromImage( bmp );
		graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
		for(int i = 9; i >= (int) 10 - System.Math.Round(new Decimal((100 - initial) / 10)); i--)
		{
			int percent = i * 10;
			String percentString = percent.ToString() + "%";
			int x = 10;			
			graphicImage.DrawString( percentString, new System.Drawing.Font("Arial", 8,FontStyle.Bold ), SystemBrushes.WindowText, new System.Drawing.Point( x, y ) ); 
			y += percentDividers;
		}
		
		
		int xd = timeDividers - 5;
		
		for(int i = 1; i < timeDividers * 2 + 1; i++)
		{
			if(i%2 > 0)
			{
				xd += timeDividers;
				continue;				
			}
			
			String min = i.ToString();
			int yd = 10;		
			graphicImage.DrawString( min, new System.Drawing.Font("Arial", 8,FontStyle.Bold ), SystemBrushes.WindowText, new System.Drawing.Point( xd, yd ) ); 
			xd += timeDividers;
		}
			
		
		int px = 1;		
		int thePercentageAtInterval = 0;
		int multiplier = (int) Math.Round(new Decimal(height / initial));
		System.Drawing.Point a = new System.Drawing.Point(0,height);
		System.Drawing.Point b = new System.Drawing.Point(0,height);
		
		for(int k = 0 ; k <= mins; k++)
		{			
			thePercentageAtInterval = this.getPercent(k  * 60);
			int percentageToPoint = height - (multiplier * thePercentageAtInterval);
			b = new System.Drawing.Point(px,percentageToPoint);					
			graphicImage.DrawLine(new Pen(System.Drawing.Color.Red),a.X,a.Y,b.X,b.Y);
			a = b;                      
			px += width / mins;
		}		
		
		return bmp;		
	}
	
	/*
	
	private Gdk.Pixbuf ImageToPixbuf( System.Drawing.Bitmap image ) 
	{
        if ( image != null ) 
		{
            using ( System.IO.MemoryStream stream = new MemoryStream() ) 
			{
               image.Save( stream, System.Drawing.Imaging.ImageFormat.Bmp );
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
    
    */

	protected virtual void OnHscale2ValueChanged (object sender, System.EventArgs e)
	{
		//Pixbuf bmp = ImageToPixbuf(this.graph(350,180,(int) this.hscale2.Value,1000));		    
		//this.image2.Visible = true;
		//this.image2.Pixbuf = bmp;		
	}

	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		if(this.selectedIndex > 0)
		{
			this.button3.Label = "Create new";
			this.button4.Visible = false;
			dayControlsVisible(false);
			alarmSettingsVisible(false);	
			this.combobox3.Active = 0;
			this.create = false;
			this.combobox3.Visible = true;
			this.button1.Visible = false;			
			
			Galarm.UserAlarms.delete(this.selectedIndex + 1);
			this.selectedIndex = 0;

			this.loadAlarms();			
						
		}
		
	} 
}
