
namespace Konvolucio.MCAP161217.Device
{
    using System;
    using System.IO;

    public static class Tools
    {
        /// <summary>
        /// Az bájt tömb érték konvertálása string.
        /// </summary>
        /// <param name="byteArray">byte array</param>
        /// <param name="offset">az ofszettől kezdődően kezdődik a konvertálás</param>
        /// <returns>string pl.: (00 FF AA) </returns>
        public static string ByteArrayLogString(byte[] byteArray, int offset, int length)
        {
            string retval = string.Empty;
            try
            {
                for (int i = offset; i < offset + length; i++)
                    retval += string.Format("{0:X2} ", byteArray[i]);
                
                //Az utolsó vessző törlése
                if (byteArray.Length > 1)
                    retval = retval.Remove(retval.Length - 1, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (retval);
        }

        /// <summary>
        /// ByteArray To C-Style String.
        /// </summary>
        /// <param name="data">byte[] data</param>
        /// <returns>0x00,0x01</returns>
        public static string ByteArrayToCStyleString(byte[] data)
        {
            string retval = string.Empty;
            for (int i = 0; i < data.Length; i++)
                retval += string.Format("0x{0:X2},", data[i]);
            //Az utolsó vessző törlése
            if (data.Length > 1)
                retval = retval.Remove(retval.Length - 1, 1);
            return retval;
        }

        /// <summary>
        /// C-Style String To Byte Array
        /// </summary>
        /// <param name="cstyleByteArrayString">0x56,0x8D,0x85,0x5E</param>
        /// <returns>byte[]</returns>
        public static byte[] CStyleStringStringToByteArray(string cstyleByteArrayString)
        {
            if (cstyleByteArrayString.Length < 2)
                return new byte[0];
            string[] byteStrArray = cstyleByteArrayString.Split(',');
            byte[] data = new byte[byteStrArray.Length];

            for (int i = 0; i < byteStrArray.Length; i++)
            {
                byteStrArray[i] = byteStrArray[i].Trim();
                if (byteStrArray[i].Contains("0x"))
                    byteStrArray[i] = byteStrArray[i].Substring(2);

                if (byteStrArray[i].Length != 0)
                    data[i] = byte.Parse(byteStrArray[i], System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return data;
        }


        /// <summary>
        /// Ez az időfomrátum a fájlnevekhez ajánlott
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTimeToFileNameString(DateTime dt)
        {
            return dt.ToString("yyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Bináris fájl létrehozása
        /// </summary>
        /// <param name="fullPath">Teljes elérési utvonal.</param>
        /// <param name="databytes">Adatbájtok</param>
        public static void CreateFile(string fullPath, byte[] databytes)
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                fs.Write(databytes, 0, databytes.Length);
            }
        }

        /// <summary>
        /// Bináris beolvasása bájt tömbe
        /// </summary>
        /// <param name="fullPath">Teljes elérési utvonal.</param>
        /// <param name="databytes">Adatbájtok</param>
        public static void OpenFile(string fullPath, out byte[] databytes)
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                databytes = new byte[fs.Length];
                fs.Read(databytes, 0, databytes.Length);
            }
        }

        /// <summary>
        /// CRC-16-ANSI
        /// xx16 + x15 + x2 + 1 => 0x8005
        /// Test:
        /// 0x48,0x65,0x6C,0x6C,0x6F,0x20,0x57,0x6F,0x72,0x6C,0x64 -> CRC:0x70C3
        /// </summary>
        /// <param name="data">Erre a tömbre számolja a CRC-t.</param>
        /// <returns>Ez a CRC eredménye.</returns>
        public static UInt16 CalcCrc16Ansi(UInt16 initValue, byte[] data)
        {
            UInt16 remainder = initValue;
            UInt16 polynomial = 0x8005;
            for (long i = 0; i < data.LongLength; ++i)
            {
                remainder ^= (UInt16)(data[i] << 8);
                for (byte bit = 8; bit > 0; --bit)
                {
                    if ((remainder & 0x8000) != 0)
                        remainder = (UInt16)((remainder << 1) ^ polynomial);
                    else
                        remainder = (UInt16)(remainder << 1);
                }
            }
            return (remainder);
        }

        /// <summary>
        /// Timestamp for log.
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeNowString()
        {
            return DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
