
using System;

namespace Galarm
{	
	
	public partial class AwakeTest : Gtk.Window
	{
		
		private String word = "";
		private String reverseWord = "";
		
		public AwakeTest() : base(Gtk.WindowType.Toplevel)
		{			
			this.Build();	
			this.createRandomString();
		}
		
		
		public void createRandomString()
		{
			char[] letters = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
			Random rand = new Random();
			Random letter = new Random();
			String test = "";
			String testbackwards = "";
			
			for(int j = 0; j < rand.Next(5,12); j++)
			{
				Char character = letters[letter.Next(0,25)];
				test +=  " " + character.ToString();
				testbackwards = character.ToString() + testbackwards;
			}
			
			this.word = test;
			this.reverseWord = testbackwards;
			
			this.label3.Text = this.word;
			
		}

		protected virtual void OnEntry2TextInserted (object o, Gtk.TextInsertedArgs args)
		{
			if(this.entry2.Text.CompareTo(this.reverseWord) == 0)
			{
				Galarm.UserAlarms.stopRunningAlarms(false);
				this.Destroy();
			}
		}
		
	}
}
