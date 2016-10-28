using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace RssReader
{
    public partial class UserSettingsWindow : Form
    {
        public UserSettingsWindow()
        {
            InitializeComponent();
            addInListForm.Height = 130;
        }
        
        private Setting setting;
        Form addInListForm = new Form();
        TextBox textBox = new TextBox();
        ListView currentListView = new ListView();

        public void SetValueInForm(Setting current)
        {
            setting = current;
            tbUserName.Text = current.GetUserName;
            tbThreadCount.Text = current.GetThreadCount.ToString();
            tbExMethod.Text = current.excludeMethod;
            tbInMethod.Text = current.includeMethod;
            foreach (Channel channel in current.channels)
            {
                listChannels.Items.Add(channel.Url);
            }
            foreach (string inFilters in current.includeFilters)
            {
                listInFilters.Items.Add(inFilters);
            }
            foreach (string exFilters in current.excludeFilters)
            {
                listExFilters.Items.Add(exFilters);
            }
        }

        private void saveSetting(object sender, EventArgs e)
        {
            try
            {
                setting.SetUserName = tbUserName.Text;
                setting.SetThreadCount = Int32.Parse(tbThreadCount.Text);
                setting.excludeMethod = tbExMethod.Text;
                setting.includeMethod = tbInMethod.Text;
                setting.channels.Clear();
                setting.includeFilters.Clear();
                setting.excludeFilters.Clear();
                foreach(ListViewItem channel in listChannels.Items)
                {
                    setting.channels.Add(new Channel(channel.Text));
                }
                foreach (ListViewItem inFilter in listInFilters.Items)
                {
                    setting.includeFilters.Add(inFilter.Text);
                }
                foreach (ListViewItem exFilter in listExFilters.Items)
                {
                    setting.excludeFilters.Add(exFilter.Text);
                }
                Program.readerForm.currentSetting = setting;
                saveInXMLFile(setting);
                this.Close();
                Program.readerForm.updateUserData();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                tbThreadCount.Text = setting.GetThreadCount.ToString();
            }
        }

        private void saveInXMLFile(Setting set)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("root");
            XElement user = new XElement("user");
            XAttribute userName = new XAttribute("name", set.GetUserName);
            user.Add(userName);
            XElement thread = new XElement("thread");
            XAttribute threadCount = new XAttribute("count", set.GetThreadCount);
            thread.Add(threadCount);
            user.Add(thread);
            XElement channels = new XElement("channels");
            foreach(Channel channel in set.channels)
            {
                XElement chl = new XElement("channel");
                XAttribute url = new XAttribute("url", channel.Url);
                chl.Add(url);
                channels.Add(chl);
            }
            user.Add(channels);
            XElement filters = new XElement("filters"); 
            XElement include = new XElement("include");
            XAttribute method = new XAttribute("method", set.includeMethod);
            include.Add(method);
            foreach(string inFilter in set.includeFilters)
            {
                XElement item = new XElement("item");
                item.Add(inFilter);
                include.Add(item);
            }
            if (set.includeFilters.Count != 0)
                filters.Add(include);
            include = new XElement("exclude");
            method = new XAttribute("method", set.excludeMethod);
            include.Add(method);
            foreach (string exFilter in set.excludeFilters)
            {
                XElement item = new XElement("item");
                item.Add(exFilter);
                include.Add(item);
            }
            if (set.excludeFilters.Count != 0)
                filters.Add(include);
            if (set.includeFilters.Count != 0 || set.excludeFilters.Count != 0)
                user.Add(filters);
            root.Add(user);
            doc.Add(root);
            doc.Save(set.fileName);
        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (currentListView.SelectedItems.Count == 0 || currentListView.SelectedItems.Count > 1)
                deleteToolStripMenuItem.Enabled = false;
            else
                deleteToolStripMenuItem.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentListView.Items.Remove(currentListView.SelectedItems[0]); 
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Top = 20;
            textBox.Left = 40;
            textBox.Width = 200;
            textBox.Parent = addInListForm;
            Button btnOk = new Button();
            btnOk.Text = "Add";
            btnOk.Parent = addInListForm;
            Button btnCancel = new Button();
            btnOk.Top = 50;
            btnOk.Left = 40;
            btnOk.Click += btnOkClick;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancelClick;
            btnCancel.Top = 50;
            btnCancel.Left = 165;
            btnCancel.Parent = addInListForm;
            addInListForm.ShowDialog();
        }

        private void btnCancelClick(object sender, EventArgs e)
        {
            textBox.Clear();
            addInListForm.Close();
        }

        private void btnOkClick(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {
                currentListView.Items.Add(textBox.Text);
                textBox.Clear();
                addInListForm.Close();
            }
            else
                MessageBox.Show("You have not entered a value!");
        }

        private void listViewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                currentListView = (ListView)sender;
            }
        }
    }
}
