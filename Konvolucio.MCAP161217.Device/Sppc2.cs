

namespace Konvolucio.MCAP161217.Device
{
    using System;
    using System.IO.Ports;

    class Sppc2
    {
        private const int FIELD_ST_SIZE                 = 1;
        internal const int FIELD_LENGTH_SIZE            = 2;
        internal const int FIELD_SID_SIZE               = 1;
        internal const int FIELD_RC_SIZE                = 1;
        internal const int FIELD_NRC_SIZE               = 1;
        internal const int FIELD_CKS_SIZE               = 2;
        internal const int FIELD_ATTR_ID_SIZE           = 4;
        internal const int FIELD_AUTH_STATUS_SIZE       = 1;

        readonly MainDevice _device;
        SerialPort Port { get { return  _device.Port; } }

        #region Constructor
        public Sppc2(object device)
        {
            _device = (MainDevice)device;
        }
        #endregion
        #region Tools
        public static UInt16 Checksum(byte[] data, int offset, int length)
        {
            UInt16 sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += data[i + offset];
            }
            return (UInt16)(0xFFFF - sum);
        }
        #endregion
        #region Message Write Read Methods...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">LEN | SID | DATA| CKS</param>
        /// <param name="response">LEN| SID | DATA| CKS</param>
        public void MessageWriteRead(byte[] request, out byte[] response)
        {
            lock (_device.LocObject)
            {
                var isComplete = false;
                var retry = _device.RetryCount;
                do
                {
                    try
                    {
                         GabrageParse();
                         Port.DiscardInBuffer();
                         Port.DiscardOutBuffer();
                         DoMessageWriteRead(request, out response);
                         isComplete = true;
                    }
                    catch (Exception)
                    {
                        if (retry-- == 0)
                            throw;
                        else
                        {
                            Port.DiscardInBuffer();
                            Port.DiscardOutBuffer();
                            _device.OnTracingHandler("Retry:");
                            response = new byte[0];
                        }
                        
                    }
                } while (!isComplete);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">DATA|CKS</param>
        /// <param name="response">DATA|CKS</param>
        /// <returns></returns>
        private void DoMessageWriteRead(byte[] request, out byte[] response)
        {
            var tracingTx = "Tx:";

            response = new byte[0];

            if (Port == null || !Port.IsOpen)
                throw new DeviceException("Port Closed.");

            var requestTimestamp = DateTime.Now;

            var tx = new byte[request.Length + Sppc2.FIELD_ST_SIZE];
            tx[0] = 0xFE;
            Buffer.BlockCopy(request, 0, tx, Sppc2.FIELD_ST_SIZE, request.Length);
            Port.Write(tx, 0, tx.Length);
            tracingTx += Tools.ByteArrayLogString(tx, 0, tx.Length);
            _device.OnTracingHandler(tracingTx);

            bool isCplt;
            do
            {
                isCplt = false;

                /*Egy kérésre több válasz is megengedett.*/
                var tracingRx = "Rx:";

                var timestamp = DateTime.Now;
                do
                {
                    if ((DateTime.Now.Ticks - timestamp.Ticks) > (Port.ReadTimeout*10000))
                    {
                        throw new DeviceException("Read Error:Start Field Timeout.");
                    }
                } while (Port.BytesToRead < Sppc2.FIELD_ST_SIZE + 1);

                //Start Field
                response = new byte[Sppc2.FIELD_ST_SIZE];
                Port.Read(response, 0, Sppc2.FIELD_ST_SIZE);
                if (response[0] != 0xFE)
                {
                    throw new DeviceException("Read Error:Start Field Wrong.");
                }
                tracingRx += Tools.ByteArrayLogString(response, 0, Sppc2.FIELD_ST_SIZE);

                //Length Filed
                timestamp = DateTime.Now;
                do
                {
                    if ((DateTime.Now.Ticks - timestamp.Ticks) > (Port.ReadTimeout*10000))
                    {
                        throw new DeviceException("Read Error:Length Field Timeout.");
                    }
                } while (Port.BytesToRead < Sppc2.FIELD_LENGTH_SIZE + 1);

                response = new byte[Sppc2.FIELD_LENGTH_SIZE];
                Port.Read(response, 0, Sppc2.FIELD_LENGTH_SIZE);
                int length = BitConverter.ToUInt16(response, 0);
                //Data Field
                timestamp = DateTime.Now;
                do
                {
                    if ((DateTime.Now.Ticks - timestamp.Ticks) > (Port.ReadTimeout*10000))
                    {
                        throw new DeviceException("Read Error: Data Field Timeout.");
                    }
                } while (Port.BytesToRead < length);

                Array.Resize(ref response, length + Sppc2.FIELD_LENGTH_SIZE);
                Port.Read(response, Sppc2.FIELD_LENGTH_SIZE, length);
                tracingRx += Tools.ByteArrayLogString(response, 0, response.Length);

                /*---*/
                var offset = 0;

                length = BitConverter.ToUInt16(response, 0);
                offset += Sppc2.FIELD_LENGTH_SIZE;

                byte rc = response[offset];
                offset += Sppc2.FIELD_SID_SIZE;

                if (rc == Services.SID_ATTR_CHANGED_NOTIFY + 0x40)
                {
                    _device.OnTracingHandler(tracingRx + "-> Attribute Changed");
                    isCplt = false;
                }
                else
                {
                    _device.OnTracingHandler(tracingRx);
                    isCplt = true;
                    break;
                }

                do
                {
                    if ((DateTime.Now.Ticks - requestTimestamp.Ticks) > (Port.ReadTimeout * 20000))
                    {
                        throw new DeviceException("Read Error: Request Timeout");
                    }
                } while (Port.BytesToRead < Sppc2.FIELD_LENGTH_SIZE + 1);
         

            } while (!isCplt);
        }
        /// <summary>
        /// 
        /// </summary>
        void GabrageParse()
        {
            if (Port.BytesToRead == 0)
                return;
            do
            {
                var tracingRx = "Rx:";

                var timestamp = DateTime.Now;
                do
                {
                    if ((DateTime.Now.Ticks - timestamp.Ticks) > (Port.ReadTimeout * 10000))
                    {
                        throw new DeviceException("GabrageParse Read Error:Start Field Timeout.");
                    }
                } while (Port.BytesToRead < Sppc2.FIELD_ST_SIZE + 1);

                //Start Field
                var  response = new byte[Sppc2.FIELD_ST_SIZE];
                Port.Read(response, 0, Sppc2.FIELD_ST_SIZE);
                if (response[0] != 0xFE)
                {
                    throw new DeviceException("GabrageParse Read Error:Start Field Wrong.");
                }
                tracingRx += Tools.ByteArrayLogString(response, 0, Sppc2.FIELD_ST_SIZE);

                //Length Filed
                timestamp = DateTime.Now;
                do
                {
                    if ((DateTime.Now.Ticks - timestamp.Ticks) > (Port.ReadTimeout * 10000))
                    {
                        throw new DeviceException("GabrageParse Read Error:Length Field Timeout.");
                    }
                } while (Port.BytesToRead < Sppc2.FIELD_LENGTH_SIZE + 1);

                response = new byte[Sppc2.FIELD_LENGTH_SIZE];
                Port.Read(response, 0, Sppc2.FIELD_LENGTH_SIZE);
                int length = BitConverter.ToUInt16(response, 0);
                //Data Field
                timestamp = DateTime.Now;
                do
                {
                    if ((DateTime.Now.Ticks - timestamp.Ticks) > (Port.ReadTimeout * 10000))
                    {
                        throw new DeviceException("GabrageParse Read Error: Data Field Timeout.");
                    }
                } while (Port.BytesToRead < length);

                Array.Resize(ref response, length + Sppc2.FIELD_LENGTH_SIZE);
                Port.Read(response, Sppc2.FIELD_LENGTH_SIZE, length);
                tracingRx += Tools.ByteArrayLogString(response, 0, response.Length);

                /*---*/
                var offset = 0;

                length = BitConverter.ToUInt16(response, 0);
                offset += Sppc2.FIELD_LENGTH_SIZE;

                byte rc = response[offset];
                offset += Sppc2.FIELD_SID_SIZE;

                if (rc == Services.SID_ATTR_CHANGED_NOTIFY + 0x40)
                {
                    _device.OnTracingHandler(tracingRx + "-> GabrageParse Attribute Changed");
                }
            } while (Port.BytesToRead != 0);
        }
        #endregion
    }
}
