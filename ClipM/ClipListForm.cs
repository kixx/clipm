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

        private BindingOrderedSet<string> clipList;

        public ClipListForm()
        {
            InitializeComponent();
        }

        public void SetBinding(BindingOrderedSet<string> clipList)
        {
            this.clipList = clipList;
            lbClipItems.DataSource = clipList.getBindingList();
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
                selectOrAddItemFromClipList(lb);
            }
        }


        private void lbClipItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(sender is ListBox)
            {
                ListBox lb = (ListBox)sender;
                if(e.KeyChar == (char)Keys.Return)
                {
                    selectOrAddItemFromClipList(lb);
                }
            }
        }

        private void selectOrAddItemFromClipList(ListBox list)
        {
            string selectedItem = list.GetItemText(list.SelectedItem);
            if (selectedItem != "")
            {
                clipList.Remove(selectedItem);
                clipList.Insert(selectedItem);
                list.SetSelected(0, true);
                Clipboard.SetText(selectedItem);
            }

        }

    }
}
