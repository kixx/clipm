namespace ClipM
{
    partial class ClipListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbClipItems = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbClipItems
            // 
            this.lbClipItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbClipItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbClipItems.FormattingEnabled = true;
            this.lbClipItems.Location = new System.Drawing.Point(0, 0);
            this.lbClipItems.Name = "lbClipItems";
            this.lbClipItems.Size = new System.Drawing.Size(284, 262);
            this.lbClipItems.TabIndex = 0;
            this.lbClipItems.Click += new System.EventHandler(this.clipListBox_Click);
            this.lbClipItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbClipItems_KeyPress);
            // 
            // ClipListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.lbClipItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ClipListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbClipItems;
    }
}