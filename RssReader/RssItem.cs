using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader
{
    class RssItem
    {
        public string title;
        public string link;
        public string description;
        public string category;
        public string publicDate;

        public RssItem(string newTitle, string newLink, string newDescription, string newCategory, string newPublicDate)
        {
            title = newTitle;
            link = newLink;
            description = newDescription;
            category = newCategory;
            publicDate = newPublicDate;
        }
    }
}
