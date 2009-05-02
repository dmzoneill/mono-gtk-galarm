using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading;
using System.Globalization;
using System.Collections;
using System.Diagnostics;
using System.Timers;


namespace Galarm
{	
	public class UserAlarms
	{		
		private static ArrayList alarms = new ArrayList();
		private System.Timers.Timer alarmTimer;
		private static FileStream fs;
		private string alarmName = "";
		private string processName = "";
		private string processArguments = "";
		private int hour = 0;
		private int minute = 0;
		private int second = 0;
		private int monday = 0;
		private int tuesday = 0;
		private int wednesday = 0;
		private int thursday = 0;
		private int friday = 0;
		private int saturday = 0;
		private int sunday = 0;
		private int active = 0;
		private int initialVolume = 50;
		private Boolean isRunning = false;
		
		private ProcessController processController = null;
		private VolumeController volumeController = null;
		
						
		// gets 
		public int getActive()
		{
			return this.active;	
		}
		public int getVolume()
		{
			return this.initialVolume;	
		}
		public String getName()
		{
			return this.alarmName;
		}
		public String getProcessName()
		{
			return this.processName;	
		}
		public String getProcessArguments()
		{
			return this.processArguments;
		}
		public int getHour()
		{
			return this.hour;
		}
		public int getMin()
		{
			return this.minute;	
		}
      	public int getSec()
		{
			return this.second;
		}
		public int getMonday()
		{
			return this.monday;	
		}
		public int getTuesday()
		{
			return this.tuesday;	
		}
		public int getWednesday()
		{
			return this.wednesday;	
		}
		public int getThursday()
		{
			return this.thursday;	
		}
		public int getFriday()
		{
			return this.friday;	
		}
		public int getSaturday()
		{
			return this.saturday;	
		}
		public int getSunday()
		{
			return this.sunday;	
		}
		public ProcessController getProcessController()
		{
			return this.processController;	
		}		
		public VolumeController getVolumeController()
		{
			return this.volumeController;	
		}		
		public Boolean getRunning()
		{
			return this.isRunning;	
		}
		
		
		// sets 
		public void setActive(int x)
		{
			this.active = x;
		}
		public void setVolume(int x)
		{
			this.initialVolume = x;
		}
		public void setName(String name)
		{
			this.alarmName = name;
		}
		public void setProcessName(String player)
		{
			this.processName = player;	
		}
		public void setProcessArguments(String arguments)
		{
			this.processArguments = arguments;
		}
		public void setHour(int x)
		{
			this.hour = x;
		}
		public void setMin(int x)
		{
			this.minute = x;
		}
      	public void setSec(int x)
		{
			this.second = x;
		}
		public void setMonday(int x)
		{
			this.monday = x;	
		}
		public void setTuesday(int x)
		{
			this.tuesday = x;	
		}
		public void setWednesday(int x)
		{
			this.wednesday = x;	
		}
		public void setThursday(int x)
		{
			this.thursday = x;	
		}
		public void setFriday(int x)
		{
			this.friday = x;	
		}
		public void setSaturday(int x)
		{
			this.saturday = x;	
		}
		public void setSunday(int x)
		{
			this.sunday = x;	
		}
		public void setRunning(Boolean val)
		{
			this.isRunning = val;
		}
		
		
		// static gets
		public static ArrayList getAlarms()
		{
			return UserAlarms.alarms;
		}
		public static int getNumAlarms()
		{
			return UserAlarms.alarms.Count;
		}
		
		
		// constructors
		public UserAlarms()
		{		
			Console.WriteLine("Loading : " + System.Environment.CurrentDirectory.ToString() + "/.alarms.xml");
			UserAlarms.loadAlarms();			
		}
		public UserAlarms(String name, int hour, int min, int sec, int mon, int tue, int wed, int thur, int fri, int sat, int sun, int act, String player, String argument, int vol)
		{			
			this.alarmName = name;
			this.hour = hour;
			this.minute = min;
			this.second = sec;
			this.monday = mon;
			this.tuesday = tue;
			this.wednesday = wed;
			this.thursday = thur;
			this.friday = fri;
			this.saturday = sat;
			this.sunday = sun;
			this.active = act;
			this.processName = player;
			this.processArguments = argument;
			this.initialVolume = vol;
			
			UserAlarms.alarms.Add(this);
			this.alarmTimer = new System.Timers.Timer();
	   		this.alarmTimer.Elapsed += new ElapsedEventHandler( checkAlarm );
			this.alarmTimer.Interval = 1000;
			this.alarmTimer.Start();
		}

		
		// Xml Worker
		private static void loadAlarms()
		{						
			try 
			{
				if(!File.Exists(System.Environment.CurrentDirectory.ToString() + "/.alarms.xml"))
				{
					alarmsInit();
				}
			}
			catch(Exception)
			{
				Console.WriteLine("Unable to initialize ~/.alarms.xml");				
			}
			
			try 
			{
			
				UserAlarms.fs = new FileStream(System.Environment.CurrentDirectory.ToString() + "/.alarms.xml", FileMode.Open);
      			XmlTextReader xtr = new XmlTextReader(fs);
        		xtr.WhitespaceHandling = WhitespaceHandling.None;
        		XmlDocument xd = new XmlDocument();
        		xd.Load(xtr);
						
				XmlNodeList alarms = xd.DocumentElement.SelectNodes("/alarms/alarm"); 
			
				foreach(XmlNode alarm in alarms)
				{
					XmlNode name = alarm.SelectSingleNode("name");
					XmlNode hour = alarm.SelectSingleNode("hour");
					XmlNode minute = alarm.SelectSingleNode("minute");
					XmlNode second = alarm.SelectSingleNode("second");
					XmlNode monday = alarm.SelectSingleNode("monday");
				
					XmlNode tuesday = alarm.SelectSingleNode("tuesday");
					XmlNode wednesday = alarm.SelectSingleNode("wednesday");
					XmlNode thursday = alarm.SelectSingleNode("thursday");
					XmlNode friday = alarm.SelectSingleNode("friday");
					XmlNode saturday = alarm.SelectSingleNode("saturday");
				
					XmlNode sunday = alarm.SelectSingleNode("sunday");
					XmlNode activate = alarm.SelectSingleNode("activate");
					XmlNode player = alarm.SelectSingleNode("player");
					XmlNode argument = alarm.SelectSingleNode("argument");
					XmlNode volume = alarm.SelectSingleNode("volume");
			
					Boolean dmon = false;
					Boolean dtue = false;
					Boolean dwed = false;
					Boolean dthur = false;
					Boolean dfri = false;
					Boolean dsat = false;
					Boolean dsun = false;				
					Boolean dact = false;
			
					if(int.Parse(monday.InnerText)==1)	dmon = true;
					if(int.Parse(tuesday.InnerText)==1)	dtue = true;
					if(int.Parse(wednesday.InnerText)==1)	dwed = true;
					if(int.Parse(thursday.InnerText)==1) dthur = true;
					if(int.Parse(friday.InnerText)==1)	dfri = true;
					if(int.Parse(saturday.InnerText)==1)	dsat = true;
					if(int.Parse(sunday.InnerText)==1)	dsun = true;
					if(int.Parse(activate.InnerText)==1)	dact = true;
				
					UserAlarms.add(name.InnerText, int.Parse(hour.InnerText), int.Parse(minute.InnerText), int.Parse(second.InnerText), dmon, dtue, dwed, dthur, dfri, dsat, dsun, dact, player.InnerText, argument.InnerText, int.Parse(volume.InnerText), false);
	
				}

        		xtr.Close();
				Console.WriteLine("Done loading Alarms");
				
			}
			catch(Exception)
			{
				Console.WriteLine("~/.alarms.xml caused an exception in Galarm (Most likey been corrupted), please delete it.");				
			}			
				
		}
		private static void saveAlarms()
		{
			try
			{				
				UserAlarms.fs = new FileStream(System.Environment.CurrentDirectory.ToString() + "/.alarms.xml", FileMode.Create);
       			XmlWriter w = XmlWriter.Create(fs);

       	 		w.WriteStartDocument();
        		w.WriteStartElement("alarms");
			
				ArrayList alarm = UserAlarms.getAlarms();

				for(int k = 0 ; k < alarm.Count; k++)
				{
					UserAlarms a = (UserAlarms) alarm[k];
				
        			w.WriteStartElement ("alarm");
        			w.WriteElementString("name", a.getName());
        			w.WriteElementString("hour", a.getHour().ToString());
					w.WriteElementString("minute", a.getMin().ToString());
        			w.WriteElementString("second", a.getSec().ToString());
					w.WriteElementString("monday", a.getMonday().ToString());
        			w.WriteElementString("tuesday", a.getTuesday().ToString());
					w.WriteElementString("wednesday", a.getWednesday().ToString());
        			w.WriteElementString("thursday", a.getThursday().ToString());
					w.WriteElementString("friday", a.getFriday().ToString());
        			w.WriteElementString("saturday", a.getSaturday().ToString());
					w.WriteElementString("sunday", a.getSunday().ToString());
        			w.WriteElementString("activate", a.getActive().ToString());
					w.WriteElementString("player", a.getProcessName());
        			w.WriteElementString("argument", a.getProcessArguments());
					w.WriteElementString("volume", a.getVolume().ToString());
        			w.WriteEndElement();
				}

        		w.WriteEndElement();
        		w.WriteEndDocument();
       	 		w.Flush();
        		fs.Close();				
				Console.WriteLine("Alarms saved to ~/.alarms.xml");
			}
			catch(Exception)
			{
				Console.WriteLine("Exception writing ~/.alarms.xml, your changes will not be saved");	
			}
		}
		private static void alarmsInit()
		{
			UserAlarms.fs = new FileStream(System.Environment.CurrentDirectory.ToString() + "/.alarms.xml", FileMode.Create);

       		XmlWriter w = XmlWriter.Create(fs);

       	 	w.WriteStartDocument();
        	w.WriteStartElement("alarms");

        	w.WriteStartElement ("alarm");
        	w.WriteElementString("name", "My Alarm");
        	w.WriteElementString("hour", "8");
			w.WriteElementString("minute", "8");
        	w.WriteElementString("second", "8");
			w.WriteElementString("monday", "1");
        	w.WriteElementString("tuesday", "1");
			w.WriteElementString("wednesday", "1");
        	w.WriteElementString("thursday", "1");
			w.WriteElementString("friday", "1");
        	w.WriteElementString("saturday", "1");
			w.WriteElementString("sunday", "1");
        	w.WriteElementString("activate", "1");
			w.WriteElementString("player", " ");
        	w.WriteElementString("argument", " ");
			w.WriteElementString("volume", "50");
        	w.WriteEndElement();

			w.WriteEndElement();
        	w.WriteEndDocument();
       	 	w.Flush();
        	fs.Close();
			Console.WriteLine("Created ~/.alarms.xml");
		}
			
		
		// Alarm Mutators
		public static void add(String name, int hour, int min, int sec, bool mon, bool tue, bool wed, bool thur, bool fri, bool sat, bool sun, bool act, String player, String argument, int vol, bool saveAlarm)
		{		
			int dmon = 0;
			int dtue = 0; 
			int dwed = 0; 
			int dthur = 0; 
			int dfri = 0; 
			int dsat = 0; 
			int dsun = 0; 					
			int dact = 0;
			
			if(mon)	dmon = 1;
			if(tue)	dtue = 1;
			if(wed)	dwed = 1;
			if(thur)dthur = 1;
			if(fri)	dfri = 1;
			if(sat)	dsat = 1;
			if(sun)	dsun = 1;
			if(act)	dact = 1;	
			
			new UserAlarms(name,hour,min,sec,dmon,dtue,dwed,dthur,dfri,dsat,dsun,dact,player,argument,vol);
			
			if(saveAlarm==true)
			{				
				UserAlarms.saveAlarms();			
			}
		}
		public static void update(int index, String name, int hour, int min, int sec, bool mon, bool tue, bool wed, bool thur, bool fri, bool sat, bool sun, bool act, String player, String argument, int vol)
		{
			object[] alarms = Galarm.UserAlarms.getAlarms().ToArray();
			Galarm.UserAlarms alarm = (Galarm.UserAlarms) alarms[index];
			
			int dmon = 0;
			int dtue = 0; 
			int dwed = 0; 
			int dthur = 0; 
			int dfri = 0; 
			int dsat = 0; 
			int dsun = 0; 					
			int dact = 0;
			
			if(mon)	dmon = 1;
			if(tue)	dtue = 1;
			if(wed)	dwed = 1;
			if(thur)dthur = 1;
			if(fri)	dfri = 1;
			if(sat)	dsat = 1;
			if(sun)	dsun = 1;
			if(act)	dact = 1;	
			
			alarm.setActive(dact);
			alarm.setName(name);
			alarm.setHour(hour);
			alarm.setMin(min);
			alarm.setSec(sec);
			alarm.setMonday(dmon);
			alarm.setTuesday(dtue);
			alarm.setWednesday(dwed);
			alarm.setThursday(dthur);
			alarm.setFriday(dfri);
			alarm.setSaturday(dsat);
			alarm.setSunday(dsun);
			alarm.setVolume(vol);
			alarm.setProcessName(player);
			alarm.setProcessArguments(argument);
						
			saveAlarms();			
		}
		public static void delete(int index)
		{
			Galarm.UserAlarms.alarms.RemoveAt(index);
			saveAlarms();
		}	
		
		
		public static void stopAllTimers()
		{
			foreach(UserAlarms alarm in UserAlarms.getAlarms())
			{
				alarm.stopTimer();
			}			
			
			UserAlarms.saveAlarms();
		}		
		public void stopTimer()
		{
			this.alarmTimer.Stop();
		}		

		
		public static void stopRunningAlarm(UserAlarms selectedAlarm)
		{
			selectedAlarm.setRunning(false);
			if(selectedAlarm.getProcessController()!=null && selectedAlarm.getVolumeController()!=null)
			{
				selectedAlarm.getProcessController().stop();
				selectedAlarm.getVolumeController().stop();
			}
		}			
		
		public static void stopRunningAlarms(Boolean internalOverride)
		{			
			foreach(UserAlarms alarm in Galarm.UserAlarms.getAlarms())
			{						
				stopRunningAlarm(alarm);
			}						
			Galarm.ProcessController.clearProcessControllers();
			Galarm.VolumeController.clearVolumeControllers();			
		}
				
		
		public static DateTime getNextAlarmCountdown()
		{
			
			DateTime nextTime = new DateTime(3000,12,12,12,12,12,12);
			
			int hour = 0;
			int min = 0;
			
			foreach(object j in UserAlarms.getAlarms())
			{
				UserAlarms alarm = (UserAlarms) j;
				
				if(alarm.getActive() == 1)
				{
					if(DateTime.Now.Hour > alarm.getHour())
					{
						hour = (alarm.getHour() + 24) - DateTime.Now.Hour;
					}
					else if (DateTime.Now.Hour < alarm.getHour())
					{
						hour = alarm.getHour() - DateTime.Now.Hour;
					}	
					else
					{
						hour = 0;	
					}
					
					if(DateTime.Now.Minute > alarm.getMin())
					{
						min = (alarm.getMin() + 60) - DateTime.Now.Minute;
						hour-=1;
					}
					else if (DateTime.Now.Minute < alarm.getMin())
					{
						min = alarm.getMin() - DateTime.Now.Minute;
					}	
					else
					{
						min = 0;	
					}
					
					
					DateTime poch = new DateTime(new DateTime(2000, 1, 1,hour,min,0).Ticks - new DateTime(2000,1,1,0,0,0).Ticks);
					
					DateTime alarmDay = new DateTime(DateTime.Now.Ticks + poch.Ticks);
			
					String today = alarmDay.DayOfWeek.ToString();
					
					Boolean submit = false;			
				
					if(today == "Monday" && alarm.getMonday() == 1)
						submit = true;
					if(today == "Tuesday" && alarm.getTuesday() == 1)
						submit = true;
					if(today == "Wednesday" && alarm.getWednesday() == 1)
						submit = true;
					if(today == "Thursday" && alarm.getThursday() == 1)
						submit = true;
					if(today == "Friday" && alarm.getFriday() == 1)
						submit = true;
					if(today == "Saturday" && alarm.getSaturday() == 1)
						submit = true;
					if(today == "Sunday" && alarm.getSunday() == 1)
						submit = true;
							
					if(submit==true)
					{
						DateTime nt = new DateTime(2000, 1, 1,hour,min,0);
						if(nt.Ticks < nextTime.Ticks)
						{
							nextTime = nt;	
						}
					}
				
				}
			}			
			
			return nextTime;				
		}
		
		// timer
		protected void checkAlarm( object source, ElapsedEventArgs e )
		{					
			try
			{						
				String today = DateTime.Now.DayOfWeek.ToString();
				
				int tMon = 0;
				int tTue = 0;
				int tWed = 0;
				int tThur = 0;
				int tFri = 0;
				int tSat = 0;
				int tSun = 0;
				
				
				if(today == "Monday")
					tMon = 1;
				if(today == "Tuesday")
					tTue = 1;
				if(today == "Wednesday")
					tWed = 1;
				if(today == "Thursday")
					tThur = 1;
				if(today == "Friday")
					tFri = 1;
				if(today == "Saturday")
					tSat = 1;
				if(today == "Sunday")
					tSun = 1;
								
				
				if(this.getActive() == 1)
				{
					if(DateTime.Now.Second == this.getSec() && DateTime.Now.Minute == this.getMin() && DateTime.Now.Hour == this.getHour())
					{
						if((this.getMonday() + tMon == 2) || (this.getMonday() + tTue == 2) || (this.getWednesday() + tWed == 2) || (this.getThursday() + tThur == 2) || (this.getFriday() + tFri == 2) || (this.getSaturday() + tSat == 2) || (this.getSunday() + tSun == 2))
						{
							Galarm.UserAlarms.stopRunningAlarms(true);						
							
							this.isRunning = true;
							
							this.volumeController = new Galarm.VolumeController(this.getVolume());
							this.volumeController.start();

							this.processController = new Galarm.ProcessController(this.getProcessName(), this.getProcessArguments(),true);
							this.processController.start();	
							
							Console.WriteLine(" >>>> Alarm ( " + this.alarmName + " : " +  DateTime.Now.ToString() + " ) <<<< ");
						}
					}
				}					
								
			}
			catch(Exception)
			{}
		}
	}
}
