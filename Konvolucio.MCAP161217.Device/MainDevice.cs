

namespace Konvolucio.MCAP161217.Device
{
    using System;
    using System.Text;
    using System.IO.Ports;


    public sealed class MainDevice : IDisposable
    {
        public class TrancingEventArgs : EventArgs
        {
            public string Message { get; private set; }

            public TrancingEventArgs(string msg)
            {
                Message = new StringBuilder(msg).ToString();
            }
        };
        public delegate void TracingEventHandler(object obj, TrancingEventArgs e);
        public TracingEventHandler TracingEvent;
        public Attributes Attributes { get; private set; }
        public Services Services { get; private set; }
        public bool IsOpen { get; private set; }
        public bool IsResponseOk
        {
            get 
            {
                return (Attributes.SerialNumber != string.Empty); 
            }
        }
        public int RetryCount { get; private set; }

        bool _disposed = false;
        internal SerialPort Port;

        internal readonly object LocObject = new object();

        public MainDevice()
        {
            Attributes = new Attributes(this);
            Services = new Services(this);
            IsOpen = false;
            RetryCount = 3;
        }

        public void Open(string comPort)
        {
            Port = new SerialPort(comPort, /*460800*/ 115200, Parity.None, 8, StopBits.Two) {ReadTimeout = 5000};
            Port.Open();
            IsOpen = true;
        }

        public void Close()
        {
            if (Port != null && Port.IsOpen)
            {
                Port.Close();
                Port = null;
                IsOpen = false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                /*Managed dispose write here*/
               
                Close();
            }
            _disposed = true;
        }

        internal void OnTracingHandler(string msg)
        {
            if (TracingEvent != null)
                TracingEvent(this, new TrancingEventArgs(Tools.GetDateTimeNowString() + ";" + msg));
        
        }
    }
}
