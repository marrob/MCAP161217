

namespace Konvolucio.MCAP161217.Device
{
    using System;
    using System.Collections.Specialized;

    public enum DebugFunctionNames
    {
        SDBG_IDLE,                       //0x00
        SDBG_MAKE_HARDFAULT,             //0x01
        SDBG_HARD_RESET,                 //0x02
        SDBG_AUTH_REPEAT_DATA_MOVE_RESP  //0x03
    }


    public class Services
    {
        internal const byte SID_ATTR_CHANGED_NOTIFY         = 0x01;
        internal const byte SID_ATTR_READ                   = 0x02;
        internal const byte SID_ATTR_WRITE                  = 0x03;
        internal const byte SID_EXEC_DBG_FUNC               = 0x30;

        readonly Sppc2 _sppc;
        #region Constructor

        public Services(object device)
        {
            _sppc = new Sppc2(device);
         }
        #endregion 
        #region Debug

        public void ExecuteDebugFuinction(DebugFunctionNames func)
        { 
            Execute(SID_EXEC_DBG_FUNC, new byte[]{(byte)func});
        }
        #endregion 
        #region Execution

        void Execute(byte sid, byte[] arg, out byte[] res)
        { 
            byte[] tx;
            byte[] rx;
            MakeRequest(out tx, sid, arg);
            _sppc.MessageWriteRead(tx, out rx);
            ParseResponse(rx, sid, out res);
        }

        void Execute(byte sid, byte[] arg)
        {
            byte[] tx;
            byte[] rx;
            byte[] res;
            MakeRequest(out tx, sid, arg);
            _sppc.MessageWriteRead(tx, out rx);
            ParseResponse(rx, sid, out res);
        }

        void Execute(byte sid, out byte[] res)
        {
            byte[] tx;
            byte[] rx;
            MakeRequest(out tx, sid, null);
            _sppc.MessageWriteRead(tx, out rx);
            ParseResponse(rx, sid, out res);
        }

        void Execute(byte sid)
        {
            byte[] tx;
            byte[] rx;
            byte[] res;
            MakeRequest(out tx, sid, null);
            _sppc.MessageWriteRead(tx, out rx);
            ParseResponse(rx, sid, out res);
        }
        #endregion 
        #region Make Request Parse response

        private static int MakeRequest(out byte[] msg, byte sid, byte[] arg)
        {
            int offset = 0;

            UInt16 msgLength = Sppc2.FIELD_LENGTH_SIZE + Sppc2.FIELD_SID_SIZE + Sppc2.FIELD_CKS_SIZE;
            if (arg != null)
                msgLength += (UInt16)arg.Length;
            msg = new byte[msgLength];

            //Length
            UInt16 length = (UInt16)(msgLength - Sppc2.FIELD_CKS_SIZE);
            Buffer.BlockCopy(BitConverter.GetBytes(length), 0, msg, 0, Sppc2.FIELD_LENGTH_SIZE);
            offset += Sppc2.FIELD_LENGTH_SIZE;

            //Sid
            msg[offset] = sid;
            offset += Sppc2.FIELD_SID_SIZE;

            if (arg != null)
            {
                //arg
                Buffer.BlockCopy(arg, 0, msg, offset, arg.Length);
                offset += arg.Length;
            }

            //Cks
            UInt16 cks = Sppc2.Checksum(msg, 0, length);
            Buffer.BlockCopy(BitConverter.GetBytes(cks), 0, msg, offset, Sppc2.FIELD_CKS_SIZE);
            offset += Sppc2.FIELD_CKS_SIZE;

            return offset;
        }

        private static int ParseResponse(byte[] msg, byte sid, out byte[] arg)
        {
            int offset = 0;
            arg = new byte[0];

            //Length -> Ebben nincs benne már hossz!
            UInt16 length = BitConverter.ToUInt16(msg, 0);
            offset += Sppc2.FIELD_LENGTH_SIZE;

            byte rc = msg[offset];
            offset += Sppc2.FIELD_SID_SIZE;

            if (rc == 0x7F)
            { //Negative Response

                byte nrc = msg[offset];
                offset += Sppc2.FIELD_NRC_SIZE;

                byte nsid = msg[offset];
                offset += Sppc2.FIELD_SID_SIZE;

                var str = string.Empty;
                if (nsid != 0)
                {
                    str = " Service:" + GetServiceName(nsid) + ", Negative Response:" + Nrc.GetNrcName(nrc) + ".";
                }
                else
                {
                    str = "Negative Response:" + Nrc.GetNrcName(nrc) + ".";
                }
                throw new DeviceException(str);
            }
            else
            { //Positive Response
                //Sid
                if ((sid + 0x40) != rc)
                {
                    throw new DeviceException("Invalid Ack.");
                }
                //Service argumentum
                arg = new byte[length - Sppc2.FIELD_SID_SIZE - Sppc2.FIELD_CKS_SIZE];
                Buffer.BlockCopy(src: msg,                            
                                 srcOffset: Sppc2.FIELD_LENGTH_SIZE + Sppc2.FIELD_SID_SIZE,  
                                 dst: arg,                               
                                 dstOffset: 0,                              
                                 count: length - Sppc2.FIELD_SID_SIZE - Sppc2.FIELD_CKS_SIZE);
            }

            //Cks
            UInt16 calcCks = Sppc2.Checksum(msg, 0, length);
            UInt16 msgCks = BitConverter.ToUInt16(msg, length);
            if (calcCks != msgCks)
            {
                throw new DeviceException("Cheksum Error.");
            }
            return offset;
        }
        #endregion
        #region Tools
        internal static string GetServiceName(byte sid)
        {
            switch (sid)
            {
                case SID_ATTR_READ:             { return "SID_ATTR_READ"; }
                case SID_ATTR_WRITE:            { return "SID_ATTR_WRITE"; }
                case SID_EXEC_DBG_FUNC:         { return "SID_EXEC_DBG_FUNC"; }
                default:
                    {
                        return "Unknown Sid, " + string.Format("0x{0:X2}", sid);
                    }
            }
        }
        #endregion 
    }
}
