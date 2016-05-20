using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipM
{
    public partial class ClipListForm : Form
    {
        public ClipListForm()
        {
            InitializeComponent();
        }

        public void SetBinding(BindingList<string> items)
        {
            lbClipItems.DataSource = items;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Escape)
            {
                Hide();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void clipListBox_Click(object sender, EventArgs e)
        {
            if(sender is ListBox) { 
                ListBox lb = (ListBox) sender;
                string selectedItem = lb.GetItemText(lb.SelectedItem);
                Clipboard.SetText(selectedItem);
            }
        }
    }
}
