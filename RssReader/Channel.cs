using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader
{
    public class Channel
    {
        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private bool selected;
        public bool IsSelected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Channel(string newUrl)
        {
            url = newUrl;
            selected = true;
        }


    }
}
