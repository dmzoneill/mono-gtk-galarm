// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Galarm {
    
    
    public partial class AwakeTest {
        
        private Gtk.VBox vbox1;
        
        private Gtk.HBox hbox4;
        
        private Gtk.Label label1;
        
        private Gtk.HBox hbox1;
        
        private Gtk.Label label2;
        
        private Gtk.Label label3;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Entry entry2;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Galarm.AwakeTest
            this.Name = "Galarm.AwakeTest";
            this.Title = Mono.Unix.Catalog.GetString("AwakeTest");
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.DestroyWithParent = true;
            this.SkipPagerHint = true;
            this.SkipTaskbarHint = true;
            // Container child Galarm.AwakeTest.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 6;
            this.vbox1.BorderWidth = ((uint)(6));
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox4 = new Gtk.HBox();
            this.hbox4.Name = "hbox4";
            this.hbox4.Spacing = 6;
            // Container child hbox4.Gtk.Box+BoxChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("<b>Are You really Awake?</b>");
            this.label1.UseMarkup = true;
            this.hbox4.Add(this.label1);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.hbox4[this.label1]));
            w1.Position = 0;
            w1.Expand = false;
            w1.Fill = false;
            this.vbox1.Add(this.hbox4);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox4]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("Type this backwards : ");
            this.hbox1.Add(this.label2);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox1[this.label2]));
            w3.Position = 0;
            w3.Expand = false;
            w3.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("label3");
            this.label3.UseMarkup = true;
            this.hbox1.Add(this.label3);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.hbox1[this.label3]));
            w4.PackType = ((Gtk.PackType)(1));
            w4.Position = 1;
            w4.Expand = false;
            w4.Fill = false;
            this.vbox1.Add(this.hbox1);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            w5.Padding = ((uint)(6));
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            // Container child hbox2.Gtk.Box+BoxChild
            this.entry2 = new Gtk.Entry();
            this.entry2.CanFocus = true;
            this.entry2.Name = "entry2";
            this.entry2.IsEditable = true;
            this.entry2.InvisibleChar = '●';
            this.hbox2.Add(this.entry2);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox2[this.entry2]));
            w6.Position = 0;
            this.vbox1.Add(this.hbox2);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
            w7.Position = 2;
            w7.Expand = false;
            w7.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 279;
            this.DefaultHeight = 110;
            this.Show();
            this.entry2.TextInserted += new Gtk.TextInsertedHandler(this.OnEntry2TextInserted);
        }
    }
}
