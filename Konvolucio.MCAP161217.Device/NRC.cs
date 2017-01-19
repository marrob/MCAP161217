
namespace Konvolucio.MCAP161217.Device
{
    static class Nrc
    {
        private const byte NRC_GENERAL                 = 0x01;
        private const byte NRC_BAD_CKS                 = 0x02;
        private const byte NRC_ATTR_BAD_ID             = 0x03;
        private const byte NRC_BAD_DATA_LEN            = 0x04;
        private const byte NRC_NOT_SUPPORTED           = 0x05;
        private const byte NRC_VU_NOT_RESP             = 0x06;
        private const byte NRC_VU_BUS_LOST             = 0x07;
        private const byte NRC_BUSY_REPEAT_REQ         = 0x08;
        private const byte NRC_BAD_ARG                 = 0x09;
        private const byte NRC_STORAGE_FULL            = 0x0A;
        private const byte NRC_FRAME_TIMEOUT           = 0x0B;
        private const byte NRC_RESERVED2               = 0x0C;
        private const byte NRC_STORAGE_WR_ERR          = 0x0D;
        private const byte NRC_GENERAL_REJECT          = 0x10;
        private const byte NRC_CONDITION_NOT_CORRECT   = 0x22;
        private const byte NRC_VLTAGE_TOO_HIGH         = 0x92;
        private const byte NRC_VLTAGE_TOO_LOW          = 0x93;


        public static string GetNrcName(byte nrc)
        {
            switch (nrc)
            {

                case NRC_GENERAL:               { return "NRC_GENERAL"; }
                case NRC_BAD_CKS:               { return "NRC_BAD_CKS"; }
                case NRC_ATTR_BAD_ID:           { return "NRC_ATTR_BAD_ID"; }
                case NRC_BAD_DATA_LEN:          { return "NRC_BAD_DATA_LEN"; }
                case NRC_NOT_SUPPORTED:         { return "NRC_NOT_SUPPORTED"; }
                case NRC_VU_NOT_RESP:           { return "NRC_VU_NOT_RESP"; }
                case NRC_VU_BUS_LOST:           { return "NRC_VU_BUS_LOST"; }
                case NRC_BUSY_REPEAT_REQ:       { return "NRC_BUSY_REPEAT_REQ"; }
                case NRC_BAD_ARG:               { return "NRC_BAD_ARG"; }
                case NRC_STORAGE_FULL:          { return "NRC_STORAGE_FULL"; }
                case NRC_FRAME_TIMEOUT:         { return "NRC_FRAME_TIMEOUT"; }
                case NRC_RESERVED2:             { return "NRC_RESERVED2"; }
                case NRC_STORAGE_WR_ERR:        { return "NRC_STORAGE_WR_ERR"; }
                case NRC_GENERAL_REJECT:        { return "NRC_GENERAL_REJECT"; }
                case NRC_CONDITION_NOT_CORRECT: { return "NRC_CONDITION_NOT_CORRECT"; }
                case NRC_VLTAGE_TOO_HIGH:       { return "NRC_VLTAGE_TOO_HIGH"; }
                case NRC_VLTAGE_TOO_LOW:        { return "NRC_VLTAGE_TOO_LOW"; }
                default: { return "NRC_UNKNOWN:" + " 0x" + nrc.ToString("X02"); }
            }
        }
    }
}
