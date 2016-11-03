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
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RssReader
{
    public partial class Reader : Form
    {
        public Reader()
        {
            InitializeComponent();
        }

        Thread thr;
        Setting setting;
        MyThreadPool tp;
        public List<Setting> listSetting = new List<Setting>();
        public Setting currentSetting;
        private static string pattern = "\\<.*?\\>";
        private Regex regex = new Regex(pattern);
        Thread checkThread;

        private void checkButton()
        {
            while(true)
            {
                if (tp != null)
                {
                    if (tp.isEmpty())
                        resetChanelButton.Invoke(new Action(() => resetChanelButton.Enabled = true));
                    else
                        resetChanelButton.Invoke(new Action(() => resetChanelButton.Enabled = false));
                }
            }
        }

        private void initialTP(object param)
        {
            newsTable.Invoke(new Action(() =>
            {
                newsTable.Rows.Clear();
            }));
            Setting set = (Setting)param;
            foreach (Channel channel in set.channels)
            {
                if(channel.IsSelected)
                    tp.Execute(new Task(parse, channel.Url));
            }
        }

        private bool checkIncludeFilters(string str)
        {
            if (currentSetting.includeMethod == "or")
            {
                foreach (string inFilter in currentSetting.includeFilters)
                {
                    if (str.Contains(inFilter.ToUpper()))
                        return true;
                }
            }
            else if (currentSetting.includeMethod == "and")
            {
                int count = 0;
                foreach (string inFilter in currentSetting.includeFilters)
                {
                    if (str.Contains(inFilter.ToUpper()))
                        count++;
                }
                if (count == currentSetting.includeFilters.Count)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool checkExcludeFilters(string str)
        {
            if (currentSetting.excludeMethod == "or")
            {
                foreach (string exFilter in currentSetting.excludeFilters)
                {
                    if (str.Contains(exFilter.ToUpper()))
                        return true;
                }
            }
            else if(currentSetting.excludeMethod == "and")
            {
                int count = 0;
                foreach (string exFilter in currentSetting.excludeFilters)
                {
                    if (str.Contains(exFilter.ToUpper()))
                        count++;
                }
                if (count == currentSetting.excludeFilters.Count)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool checkString(string str)
        {
            if ((currentSetting.includeFilters.Count == 0 || checkIncludeFilters(str)) && 
                (currentSetting.excludeFilters.Count == 0 || checkExcludeFilters(str)))
                return true;
            else
                return false;
        }

        private bool checkItemOfFilters(XmlNode item)
        {
            if (currentSetting.excludeFilters.Count == 0 && currentSetting.includeFilters.Count == 0)
                return true;

            string title = item.SelectSingleNode("title") != null ? item.SelectSingleNode("title").InnerText.ToUpper() : "";
            string description = item.SelectSingleNode("description") != null ? item.SelectSingleNode("description").InnerText.ToUpper() : "";
            string category = item.SelectSingleNode("category") != null ? item.SelectSingleNode("category").InnerText.ToUpper() : "";

            if ((currentSetting.includeFilters.Count == 0 || checkIncludeFilters(title) || checkIncludeFilters(description) || checkIncludeFilters(category)) &&
                (currentSetting.excludeFilters.Count == 0 || (!checkExcludeFilters(title) && !checkExcludeFilters(description) && !checkExcludeFilters(category))))
                return true;
            else
                return false;
        }

        private bool checkInDataGridView(RssItem item)
        {
            foreach(DataGridViewRow row in newsTable.Rows)
            {
                if (((string)row.Cells["Title"].Value == item.title) && ((string)row.Cells["Link"].Value == item.link))
                    return false;
            }
            return true;
        }

        private List<RssItem> parse(string channelUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(channelUrl);
                XmlNode channel = doc.ChildNodes[1].FirstChild;
                List<RssItem> rssItems = new List<RssItem>();
                RssItem rssItem;
                foreach (XmlNode item in channel.SelectNodes("item"))
                {
                    if (checkItemOfFilters(item))
                    {
                        rssItem = new RssItem(
                            item.SelectSingleNode("title").InnerText,
                            item.SelectSingleNode("link").InnerText,
                            regex.Replace(item.SelectSingleNode("description").InnerText, ""),
                            item.SelectSingleNode("category") != null ? item.SelectSingleNode("category").InnerText : "",
                            item.SelectSingleNode("pubDate").InnerText
                        );

                        lock (this)
                        {
                            newsTable.Invoke(new Action(() =>
                            {
                                if (checkInDataGridView(rssItem))
                                {
                                    int rowNumber = newsTable.Rows.Add();
                                    newsTable.Rows[rowNumber].Cells["Title"].Value = rssItem.title;
                                    newsTable.Rows[rowNumber].Cells["Category"].Value = rssItem.category;
                                    newsTable.Rows[rowNumber].Cells["Description"].Value = rssItem.description;
                                    newsTable.Rows[rowNumber].Cells["PubDate"].Value = rssItem.publicDate;
                                    newsTable.Rows[rowNumber].Cells["Link"].Value = rssItem.link;
                                    newsTable.Rows[rowNumber].Cells["ID"].Value = newsTable.Rows.Count;
                                }
                            }));
                        }
                        rssItems.Add(rssItem);
                    }
                }
                return rssItems;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }
        }

        private void itemCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Process.Start((string)newsTable.Rows[e.RowIndex].Cells["Link"].Value);
            //webBrowser1.Navigate((string)dataGridView1.Rows[e.RowIndex].Cells["Link"].Value);
        }

        private void resetChannel(object sender, EventArgs e)
        {
            newsTable.Rows.Clear();
            int i = 0;
            foreach (ListViewItem item in channelList.Items)
            {
                if (item.Checked != true)
                {
                    setting.channels[i].IsSelected = false;
                }
                i++;
            }
            thr = new Thread(new ParameterizedThreadStart(initialTP)) { IsBackground = true };
            thr.Start(setting);
        }

        private void newUserCreate(object sender, EventArgs e)
        {
            try
            { 
                setting = new Setting();
                if (setting.GetUserName != null)
                {
                    if (!usersToolStripMenuItem.DropDownItems.ContainsKey(setting.GetUserName))
                    {
                        resetChanelButton.Enabled = true;
                        listSetting.Add(setting);
                        currentSetting = setting;
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Text = setting.GetUserName;
                        item.Name = setting.GetUserName;
                        usersToolStripMenuItem.DropDownItems.Add(item);
                        usersToolStripMenuItem.DropDownItems.Find(setting.GetUserName, false).First().Click += menuItemClick;
                        label1.Text = "Current User: " + setting.GetUserName;
                        channelList.Items.Clear();
                        ListViewItem lvi;
                        foreach (Channel channel in setting.channels)
                        {
                            lvi = new ListViewItem();
                            lvi.Text = channel.Url;
                            lvi.Checked = true;
                            channelList.Items.Add(lvi);
                        }
                        if(tp == null)
                        {
                            checkThread = new Thread(checkButton) { IsBackground = true };
                            checkThread.Start();
                        }
                        tp = new MyThreadPool(setting.GetThreadCount);
                        thr = new Thread(new ParameterizedThreadStart(initialTP)) { IsBackground = true };
                        thr.Start(setting);
                    }
                    else
                    {
                        MessageBox.Show("This file is already open!");
                    }
                }
                else
                {
                    MessageBox.Show("You choose the configuration file!");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void menuItemClick(object sender, EventArgs e)
        {
            setting = listSetting.Find(x => x.GetUserName == sender.ToString());
            currentSetting = setting;
            try
            {
                label1.Text = "Current User: " + setting.GetUserName;
                int i = 0;
                channelList.Items.Clear();
                foreach (Channel channel in setting.channels)
                {
                    channelList.Items.Add(channel.Url);
                    channelList.Items[i].Checked = true;
                    i++;
                }
                thr = new Thread(new ParameterizedThreadStart(initialTP)) { IsBackground = true };
                thr.Start(setting);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        public void updateUserData()
        {
            listSetting.Remove(listSetting.Find(x => x.GetUserName == currentSetting.GetUserName));
            listSetting.Add(currentSetting);
            menuItemClick(currentSetting.GetUserName, new EventArgs());
        }

        private void settingUserDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentSetting != null)
            {
                UserSettingsWindow form = new UserSettingsWindow();
                form.SetValueInForm(currentSetting);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("User was not selected!");
            }
        }
    }
}
