using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipM
{
    class ClipMApplicationContext: ApplicationContext
    {
        static TraceSource _trace = new TraceSource("ClipM");

        private ConfigForm configWindow;
        private ClipListForm clipListWindow;

        private NotifyIcon notifyIcon;
        private ClipboardMonitor monitor;

        private BindingList<string> clipList { get; }

        public ClipMApplicationContext() {

            MenuItem listMenuItem = new MenuItem("Clipboard", new EventHandler(ToggleList));
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            listMenuItem.DefaultItem = true;

            this.notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.ApplicationIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
            { listMenuItem, configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

            this.clipList = new BindingList<string>();
            this.monitor = new ClipboardMonitor();
            this.monitor.ClipboardContentChanged += Monitor_ClipboardContentChanged;

            this.configWindow = new ConfigForm();
            this.clipListWindow = new ClipListForm();
            this.clipListWindow.SetBinding(this.clipList);

            TextWriterTraceListener textListener = new TextWriterTraceListener("C:\\Users\\Kiki\\clipm.log");
            _trace.Listeners.Remove("Default");
            _trace.Listeners.Add(textListener);
            _trace.Switch.Level = SourceLevels.All;
        }


        private void Monitor_ClipboardContentChanged(object sender, ClipboardChangedEventArgs e)
        {
            if(Clipboard.ContainsText()) {
                String clipContent = Clipboard.GetText();
                _trace.TraceEvent(TraceEventType.Information, 2000, "Clipboard changed seq {0}, content '{1}'", e.seqNo, clipContent);
                clipList.Add(clipContent);

            }
        }

        private void ToggleList(object sender, EventArgs e)
        {
            if(clipListWindow.Visible)
            {
                clipListWindow.Hide();
            }
            else
            {
                clipListWindow.ShowDialog();
            }
        }

        void ShowConfig(object sender, EventArgs e)
        {
            if(configWindow.Visible)
            {
                configWindow.Activate();
            }
            else
            {
                configWindow.ShowDialog();
            }
        }

        void Exit(object sender, EventArgs e)
        {
            _trace.Close();
            notifyIcon.Visible = false;
            Application.Exit();
        }



    }
}
