using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RssReader
{
    public class Setting
    {
        public List<Channel> channels = new List<Channel>();
        public List<string> includeFilters = new List<string>();
        public List<string> excludeFilters = new List<string>();
        public string includeMethod = "";
        public string excludeMethod = "";
        public string fileName = "";

        public Setting()
        {
            XmlDocument doc = new XmlDocument();
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\Users\Александр\Documents\Visual Studio 2015\Projects\SPP\Lab4\RssReader\RssReader\bin\Debug";
            openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            doc.Load(openFileDialog1.FileName);
                            fileName = openFileDialog1.FileName;
                            XmlNode root = doc.SelectNodes("root")[0];
                            XmlNode user = root.FirstChild;
                            foreach (XmlAttribute attr in user.Attributes)
                            {
                                userName = attr.Value;
                            }
                            foreach(XmlNode node in user.ChildNodes)
                            {
                                switch (node.Name)
                                {
                                    case "thread":
                                    {
                                        threadCount = Int32.Parse(node.Attributes[node.Attributes.Count - 1].Value);
                                        break;
                                    }

                                    case "channels":
                                    {
                                        foreach(XmlNode temp in node.ChildNodes)
                                        {
                                            channels.Add(new Channel(temp.Attributes[0].Value));
                                        }
                                        break;
                                    }

                                    case "filters":
                                    {
                                        foreach(XmlNode temp in node.SelectNodes("include"))
                                        {
                                            includeMethod = temp.Attributes[temp.Attributes.Count - 1].Value;
                                            foreach (XmlNode item in temp.ChildNodes)
                                            {
                                                includeFilters.Add(item.InnerText);
                                            }
                                        }
                                        foreach (XmlNode temp in node.SelectNodes("exclude"))
                                        {
                                            excludeMethod = temp.Attributes[temp.Attributes.Count - 1].Value;
                                            foreach (XmlNode item in temp.ChildNodes)
                                            {
                                                excludeFilters.Add(item.InnerText);
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private string userName;
        public string GetUserName
        {
            get { return userName; }
        }
        public string SetUserName
        {
            set { userName = value; }
        }

        private int threadCount;
        public int GetThreadCount
        {
            get { return threadCount; }
        }
        public int SetThreadCount
        {
            set { threadCount = value; }
        }
    }
}
