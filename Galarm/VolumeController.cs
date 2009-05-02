using System;
using System.Diagnostics;
using System.Threading;
using System.Collections; 


namespace Galarm 
{		
	public class VolumeController
	{
		private static ArrayList volumeControllerInstances = new ArrayList();
		private int startVolume = 0;
		private Boolean isActive = false;
		private int currentVolume = 0;
		private ThreadStart job;
		private Thread jobRunner;
		
		
		public VolumeController(int volume)
		{
			this.startVolume = volume;		
			volumeControllerInstances.Add(this);
			
			/*
			new Timer(delegate(object s) {
                            Console.WriteLine("{0} : {1}",
                            DateTime.Now.ToString("HH:mm:ss.ffff"), s);
                        }
                  , null,1000, 1000);
                  */
			
		}
		
		public static ArrayList getVolumeControllers()
		{
			return volumeControllerInstances;	
		}
		
		
		public static void clearVolumeControllers()
		{
			for(int y =0; y < volumeControllerInstances.Count; y++)
			{
				VolumeController temp = (VolumeController) volumeControllerInstances[y];
				temp.stop();
			}
			
			volumeControllerInstances.Clear();
		}
		
		
		public void start()
		{			
			try
			{
				if(this.is_active()==false)
				{
					this.job = new ThreadStart(adjustVolume);
        			this.jobRunner = new Thread(this.job);
        			this.jobRunner.Start();	
				}										
			}
			catch(Exception)
			{}
		}
		
		
		public void stop()
		{
			try
			{
				if(this.is_active()==true)
				{
					this.jobRunner.Abort();
					this.isActive = false;
				}
			}
			catch (Exception)
			{}
		}
		
		
		public Boolean is_active()
		{
			return this.isActive;	
		}
		
		
		public int getCurrentVolume()
		{
			return this.currentVolume;	
		}
		
				
		private void adjustVolume()
		{			
			this.isActive = true;	
			
			int p = 1000;
			String command = "amixer";
			String argument = " -D hw:0 -q sset Master Playback Volume 50% unmute";
			
			Process unmute = new Process();
			unmute.StartInfo.FileName = command;
			unmute.StartInfo.Arguments = argument;
			unmute.StartInfo.UseShellExecute = true;
			unmute.Start();							

			for (int i=this.startVolume; i < 101; i++)
        	{
				argument = " -D hw:0 -q sset Master Playback Volume " + i + "%";
				
				Process increaseVol = new Process();
				increaseVol.StartInfo.FileName = command;
				increaseVol.StartInfo.Arguments = argument;
				increaseVol.StartInfo.UseShellExecute = true;
				increaseVol.Start();
				
				this.currentVolume = i;			
            	Thread.Sleep(p);
				p = p + 1000;
        	}				
				
			this.isActive = false;			
		}
		
	}
}
