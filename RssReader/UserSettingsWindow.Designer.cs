namespace RssReader
{
    partial class UserSettingsWindow
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbThreadCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listChannels = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.listInFilters = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.listExFilters = new System.Windows.Forms.ListView();
            this.saveSettingBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbInMethod = new System.Windows.Forms.TextBox();
            this.tbExMethod = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thread count:";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(92, 6);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(100, 20);
            this.tbUserName.TabIndex = 2;
            // 
            // tbThreadCount
            // 
            this.tbThreadCount.Location = new System.Drawing.Point(92, 41);
            this.tbThreadCount.Name = "tbThreadCount";
            this.tbThreadCount.Size = new System.Drawing.Size(100, 20);
            this.tbThreadCount.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Channels:";
            // 
            // listChannels
            // 
            this.listChannels.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listChannels.ContextMenuStrip = this.contextMenuStrip1;
            this.listChannels.HoverSelection = true;
            this.listChannels.Location = new System.Drawing.Point(12, 110);
            this.listChannels.Name = "listChannels";
            this.listChannels.Size = new System.Drawing.Size(220, 389);
            this.listChannels.TabIndex = 5;
            this.listChannels.UseCompatibleStateImageBehavior = false;
            this.listChannels.View = System.Windows.Forms.View.List;
            this.listChannels.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Include filters:";
            // 
            // listInFilters
            // 
            this.listInFilters.ContextMenuStrip = this.contextMenuStrip1;
            this.listInFilters.Location = new System.Drawing.Point(264, 110);
            this.listInFilters.Name = "listInFilters";
            this.listInFilters.Size = new System.Drawing.Size(225, 389);
            this.listInFilters.TabIndex = 7;
            this.listInFilters.UseCompatibleStateImageBehavior = false;
            this.listInFilters.View = System.Windows.Forms.View.List;
            this.listInFilters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewMouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(514, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Exclude filters:";
            // 
            // listExFilters
            // 
            this.listExFilters.ContextMenuStrip = this.contextMenuStrip1;
            this.listExFilters.Location = new System.Drawing.Point(517, 110);
            this.listExFilters.Name = "listExFilters";
            this.listExFilters.Size = new System.Drawing.Size(225, 389);
            this.listExFilters.TabIndex = 9;
            this.listExFilters.UseCompatibleStateImageBehavior = false;
            this.listExFilters.View = System.Windows.Forms.View.List;
            this.listExFilters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewMouseDown);
            // 
            // saveSettingBtn
            // 
            this.saveSettingBtn.Location = new System.Drawing.Point(655, 525);
            this.saveSettingBtn.Name = "saveSettingBtn";
            this.saveSettingBtn.Size = new System.Drawing.Size(75, 23);
            this.saveSettingBtn.TabIndex = 10;
            this.saveSettingBtn.Text = "Save";
            this.saveSettingBtn.UseVisualStyleBackColor = true;
            this.saveSettingBtn.Click += new System.EventHandler(this.saveSetting);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Method include filters:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(261, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Method exclude filters:";
            // 
            // tbInMethod
            // 
            this.tbInMethod.Location = new System.Drawing.Point(389, 6);
            this.tbInMethod.Name = "tbInMethod";
            this.tbInMethod.Size = new System.Drawing.Size(100, 20);
            this.tbInMethod.TabIndex = 13;
            // 
            // tbExMethod
            // 
            this.tbExMethod.Location = new System.Drawing.Point(389, 41);
            this.tbExMethod.Name = "tbExMethod";
            this.tbExMethod.Size = new System.Drawing.Size(100, 20);
            this.tbExMethod.TabIndex = 14;
            // 
            // UserSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 560);
            this.Controls.Add(this.tbExMethod);
            this.Controls.Add(this.tbInMethod);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.saveSettingBtn);
            this.Controls.Add(this.listExFilters);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listInFilters);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listChannels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbThreadCount);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserSettingsWindow";
            this.Text = "UserSettingsWindow";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbThreadCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listChannels;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listInFilters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listExFilters;
        private System.Windows.Forms.Button saveSettingBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbInMethod;
        private System.Windows.Forms.TextBox tbExMethod;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}