
namespace Konvolucio.MCAP161217.Device
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Attributes
    {
        #region Attributes Id
        internal const UInt32 ATTR_TEST_U32         = 0x80000000;
        internal const UInt32 ATTR_FIRMWARE_REV     = 0x81000001;
        internal const UInt32 ATTR_PCB_REV          = 0x81000002;
        internal const UInt32 ATTR_DEVICE_NAME      = 0x81000003;
        internal const UInt32 ATTR_DEVICE_SERIAL    = 0x81000004;
      
        #endregion

        public string FirmwareRev
        {
            get
            {
                return AttributeReadString(ATTR_FIRMWARE_REV);
            }
        }
        public string PcbRev
        {
            get
            {
                return AttributeReadString(ATTR_PCB_REV);
            }
        }
        public string DeviceName
        {
            get
            {
                return AttributeReadString(ATTR_DEVICE_NAME);
            }
        }
        /// <summary>
        /// The adapters unique number.
        /// Default eg.:387536633133.
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return AttributeReadString(ATTR_DEVICE_SERIAL);
            }
        }
        /// <summary>
        /// The Konvolucio.MCAN120803.dll Assembly Version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                return assembly.GetName().Version.ToString();
            }
        }
        /// <summary>
        /// Mindig rossz, a NRC_ATTR_BAD_ID
        /// </summary>
        public int BadAttrId
        {
            get { return (int)AttributeReadUInt32(0x0000001); }
        }
        //------------------------------------------------------------       
        /// <summary>
        /// Teszt attribútum olvasás és írás
        /// </summary>
        public UInt32 AttrTestU32
        {
            get { return AttributeReadUInt32(ATTR_TEST_U32); }
            set { AttributeWrite(Services.SID_ATTR_WRITE, ATTR_TEST_U32, BitConverter.GetBytes(value)); }
        }
        /// <summary>
        /// Tachográf ezen a címen küld.
        /// </summary>
    

        readonly Sppc2 _sppc;

        #region Constructor
        public Attributes(object device)
        {
            _sppc = new Sppc2(device);
        }
        #endregion
        #region Attribute Read Methods
        internal void AttributeRead(byte sid, UInt32 attrId, out byte[] data)
        {
            data = new byte[0];
            byte[] req;
            byte[] resp;
            MakeRequest(out req, sid, attrId, null);
            _sppc.MessageWriteRead(req, out resp);
            ParseResponse(resp, sid, out data);
        }

        private string AttributeReadString(UInt32 attrId)
        {
            byte[] data;
            AttributeRead(Services.SID_ATTR_READ, attrId, out data);
            string str = System.Text.Encoding.ASCII.GetString(data).Trim('\0');
            return str;
        }

        private UInt64 AttributeReadUInt64(UInt32 attrId)
        {
                byte[] data;
                AttributeRead(Services.SID_ATTR_READ, attrId, out data);
                var retval = BitConverter.ToUInt64(data, 0);
                return retval;
        }

        private UInt32 AttributeReadUInt32(UInt32 attrId)
        {
            byte[] data;
            AttributeRead(Services.SID_ATTR_READ, attrId, out data);
            var retval = BitConverter.ToUInt32(data, 0);
            return retval;
        }

        private byte AttributeReadUInt8(UInt32 attrId)
        {
            byte[] data;
            AttributeRead(Services.SID_ATTR_READ, attrId, out data);
            var retval = data[0];
            return retval;
        }
        #endregion
        #region Attribute Write Methods
        private void AttributeWrite(byte sid, UInt32 attrId, byte[] data)
        {
            byte[] req;
            byte[] resp;
            MakeRequest(out req, sid, attrId, data);
            _sppc.MessageWriteRead(req, out resp);
            ParseResponse(resp, sid);

        }
        #endregion
        #region Make Reques and Parse Response
        internal static int MakeRequest(out byte[] msg, byte sid, UInt32 attrId, byte[] attrValue)
        {
            int offset = 0;

            UInt16 msgLength = Sppc2.FIELD_LENGTH_SIZE + Sppc2.FIELD_SID_SIZE + Sppc2.FIELD_ATTR_ID_SIZE + Sppc2.FIELD_CKS_SIZE;
            if (attrValue != null)
                msgLength += (UInt16)attrValue.Length;
            msg = new byte[msgLength];

            //Length
            UInt16 length = (UInt16)(msgLength - Sppc2.FIELD_CKS_SIZE);
            Buffer.BlockCopy(BitConverter.GetBytes(length), 0, msg, 0, Sppc2.FIELD_LENGTH_SIZE);
            offset += Sppc2.FIELD_LENGTH_SIZE;

            //Sid
            msg[offset] = sid;
            offset += Sppc2.FIELD_SID_SIZE;

            //Attribuet id
            Buffer.BlockCopy(BitConverter.GetBytes(attrId), 0, msg, offset, Sppc2.FIELD_ATTR_ID_SIZE);
            offset += Sppc2.FIELD_ATTR_ID_SIZE;

            if (attrValue != null)
            {
                //Attribuet Value
                Buffer.BlockCopy(attrValue, 0, msg, offset, attrValue.Length);
                offset += attrValue.Length;
            }
            //Cks
            UInt16 cks = Sppc2.Checksum(msg, 0, length);
            Buffer.BlockCopy(BitConverter.GetBytes(cks), 0, msg, offset, Sppc2.FIELD_CKS_SIZE);
            offset += Sppc2.FIELD_CKS_SIZE;

            return offset;
        }

        internal static int ParseResponse(byte[] msg, byte sid, out byte[] data)
        {
            int offset = 0;
            data = new byte[0];

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

                string str = string.Empty;
                if (nsid != 0)
                {
                    str = " Service:" + Services.GetServiceName(nsid) + ", Negative Response:" + Nrc.GetNrcName(nrc) + ".";
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

                UInt32 readAttr = BitConverter.ToUInt32(msg, Sppc2.FIELD_LENGTH_SIZE + Sppc2.FIELD_SID_SIZE);

                //Data
                data = new byte[length - Sppc2.FIELD_ATTR_ID_SIZE - Sppc2.FIELD_SID_SIZE - Sppc2.FIELD_CKS_SIZE];
                Buffer.BlockCopy(msg,                              //Source
                                Sppc2.FIELD_LENGTH_SIZE + Sppc2.FIELD_SID_SIZE + Sppc2.FIELD_ATTR_ID_SIZE,  //Source Offset
                                data,                              //Dest
                                0,                                 //Dest Offset
                                length - Sppc2.FIELD_SID_SIZE - Sppc2.FIELD_CKS_SIZE - Sppc2.FIELD_ATTR_ID_SIZE);  //Count 
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

        /// <summary>
        /// Attributum olvasása után csak Pozitiv nyugta ellenörzésére.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        private static int ParseResponse(byte[] msg, byte sid)
        {
            int offset = 0;
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

                string str = string.Empty;
                if (nsid != 0)
                {
                    str = " Service:" + Services.GetServiceName(nsid) + ", Negative Response:" + Nrc.GetNrcName(nrc) + ".";
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
        #region Property Tools

        public List<string> GetAttriubtes()
        {
            List<string> i = new List<string>();
            foreach (PropertyInfo prop in this.GetType().GetProperties())
                i.Add(prop.Name);
            return i;
        }

        public void SetAttributeValueByName(string name, UInt32 value)
        {
            var prop = this.GetType().GetProperties().First(n => n.Name == name);
            prop.SetValue(this, value, null);
        }

        public string GetAttributeStringValueByName(string name)
        {
            var prop = this.GetType().GetProperties().First(n => n.Name == name);
            return prop.GetValue(this, null).ToString();
        }

        public UInt32 GetAttributeUint32ValueByName(string name)
        {
            var prop = this.GetType().GetProperties().First(n => n.Name == name);
            return (UInt32)prop.GetValue(this, null);
        }
        #endregion 
    }
}
