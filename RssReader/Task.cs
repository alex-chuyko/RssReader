using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RssReader
{
    class Task
    {
        private Func<string, List<RssItem>> work;
        private string param; 
        private bool isRunned;

        public Task(Func<string, List<RssItem>> work, string channelUrl)
        {
            this.work = work;
            this.param = channelUrl;
        }

        public List<RssItem> Execute()
        {
            lock (this)
            {
                isRunned = true;
            }
            return work(param);
        }

        public bool IsRunned
        {
            get
            {
                return isRunned;
            }
        }

    }
}
