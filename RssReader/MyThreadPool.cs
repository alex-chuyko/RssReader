using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace RssReader
{
    class MyThreadPool : IDisposable
    {
        private List<Task> _tasks = new List<Task>();
        private List<Thread> _workers;
        private int count = 0;
        private int threadCount;
        private bool _disallowAdd; 
        private bool _disposed;

        public MyThreadPool(int count)
        {
            if (count <= 0)
                throw new ArgumentException("count", "Количество потоков должно быть больше нуля.");
            threadCount = count;

            this._workers = new List<Thread>();
            for (var i = 0; i < threadCount; ++i)
            {
                var worker = new Thread(this.ThreadWork) { IsBackground = true };
                worker.Start();
                this._workers.Add(worker);
            }
        }

        private void ThreadWork()
        {
            Task task = null;
            while (true) 
            {
                lock (this._tasks) 
                {
                    while (true)
                    {
                        if (this._disposed)
                        {
                            return;
                        }
                        if (this._workers.First() != null && object.ReferenceEquals(Thread.CurrentThread, this._workers.First()) && this._tasks.Count > 0) 
                        {
                            task = this._tasks.First();
                            this._tasks.RemoveAt(0);
                            this._workers.RemoveAt(0);
                            Monitor.PulseAll(this._tasks); 
                            break; 
                        }
                        Monitor.Wait(this._tasks); 
                    }
                }

                task.Execute();
                lock (this._tasks)
                {
                    this._workers.Add(Thread.CurrentThread);
                }
                task = null;
                count--;
            }
        }

        public bool isEmpty()
        {
            if (this.count == 0)
                return true;
            else
                return false;
        }

        public void Dispose()
        {
            var waitForThreads = false;
            lock (this._tasks)
            {
                if (!this._disposed)
                {
                    GC.SuppressFinalize(this);

                    while (this._tasks.Count > 0)
                    {
                        Monitor.Wait(this._tasks); 
                    }

                    this._disposed = true;
                    Monitor.PulseAll(this._tasks); 
                    waitForThreads = true;
                }
            }
            if (waitForThreads)
            {
                foreach (var worker in this._workers)
                {
                    worker.Join();
                }
            }
        }

        public bool Execute(Task task)
        {
            lock (this._tasks)
            {
                this._tasks.Add(task);
                count++;
                Monitor.PulseAll(this._tasks); 
                return true;
            }
        }
    }
}
