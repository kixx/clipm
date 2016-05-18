using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipM
{
    class ClipMApplicationContext: ApplicationContext
    {

        private NotifyIcon notifyIcon;
        private Config configWindow;

        public ClipMApplicationContext() {
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            this.notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.ApplicationIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
            { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

            this.configWindow = new Config();
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
            notifyIcon.Visible = false;
            Application.Exit();
        }



    }
}
