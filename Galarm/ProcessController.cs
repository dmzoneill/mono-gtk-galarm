using System;
using System.Diagnostics;
using System.Threading;
using System.Collections; 


namespace Galarm
{
		
	public class ProcessController
	{
		
		private static ArrayList processControllerInstances = new ArrayList();
		private string processName = null;
		private string processArguments = null;
		private Boolean isActive = false;
		private Boolean isPersistant = true;
		private ThreadStart job = null;
		private Thread jobRunner = null;
		private int timeStarted = 0;
		private System.Diagnostics.Process processInstance;
		
		public ProcessController(String process, String arguments, Boolean persistant)
		{
			this.processName = process;
			this.processArguments = arguments;
			this.isPersistant = persistant;
			processControllerInstances.Add(this);
		}
		
		
		public Boolean getIsPersistant()
		{
			return this.isPersistant;	
		}
		
				
		public static ArrayList getProcessControllers()
		{
			return processControllerInstances;	
		}
		
		
		public static void clearProcessControllers()
		{
			for(int y =0; y < processControllerInstances.Count; y++)
			{
				ProcessController temp = (ProcessController) processControllerInstances[y];
				temp.stop();
			}
			
			processControllerInstances.Clear();
		}
		
		
		public void start()
		{			
			try
			{
				if(this.is_active()==false)
				{
					this.timeStarted = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
					this.isActive = true;
					this.job = new ThreadStart(process);
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
					this.processInstance.CloseMainWindow();
					this.processInstance.Dispose();
					this.processInstance.Close();		
					this.isActive = false;
					this.jobRunner.Abort();
				}
			}
			catch (Exception)
			{}
		}
		
		
		public Boolean is_active()
		{
			return this.isActive;	
		}
		
		
		public int getTimeStarted()
		{
			return this.timeStarted;	
		}
			
				
		private void process()
		{						
			if(this.isActive==true)
			{				
				this.processInstance = new Process();
				this.processInstance.StartInfo.FileName = this.processName;
				this.processInstance.StartInfo.Arguments = "\""+this.processArguments+"\"";
				this.processInstance.StartInfo.UseShellExecute = true;
				this.processInstance.Start();
				this.processInstance.WaitForExit();
								
				if(this.isActive==true && this.isPersistant==true)
				{
					// if it is a persistant alarm, we'll reopen the player if they have closed it 
					// the user must stop the alarm through the programs alarming stopping mechanism
					process();
				}				
				
				this.isActive = false;	
			}								
		}	
		
	}
}
