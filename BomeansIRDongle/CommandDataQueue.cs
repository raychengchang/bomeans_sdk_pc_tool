using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomeans
{
    public class CommandDataQueue
    {
        private List<Byte> mDataList;

        private Object mLock = new Object();

        public CommandDataQueue()
        {
            mDataList = new List<Byte>();
        }

        public void AppendData(byte[] data)
        {
            if (null == data)
            {
                return;
            }

            // if expecting new package data, check the prefix byte(s)
            if (mDataList.Count == 0)
            {
                if (data.Length == 1 && data[0] != 0xFF)
                {
                    return;
                }

                if ((data.Length > 1) && ((data[0] != 0xFF) || (data[1] != 0x61)))
                {
                    return;
                }
            }

            lock (mLock)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    mDataList.Add(data[i]);
                }
            }
        }

        public void Flush()
        {
            lock (mLock)
            {
                mDataList.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bClearBuffer">true: remove the returned command data from the queue if the command data is valid (a complete IRUARTCommand data)</param>
        /// <returns></returns>
        public IRUARTCommand GetCommandData(Boolean bClearBuffer = false)
        {
            lock (mLock)
            {
                Byte[] data = mDataList.Cast<Byte>().ToArray<Byte>();
                IRUARTCommand uartCmd = new IRUARTCommand(data);
                if (uartCmd.IsValidCommand)
                {
                    if (bClearBuffer)
                    {
                        RemoveHead(uartCmd.CommandBytes.Length);
                    }
                    return uartCmd;
                }

                return null;
            }
        }

        public void RemoveHead(int numBytes)
        {
            if (numBytes >= mDataList.Count)
            {
                mDataList.Clear();
            }
            else
            {
                for (int i = 0; i < numBytes; i++)
                {
                    mDataList.RemoveAt(0);
                }
            }

        }
    }
}
