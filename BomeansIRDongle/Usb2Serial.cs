using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Bomeans
{
    public class Usb2Serial
    {
        private SerialPort fSerialPort;

        private const int DEFAULT_BAUD_RATE = 38400;

        private CommandDataQueue mUartDataQueue = new CommandDataQueue();

        private OnIrDongleReceivedDataCallback mPackageReceiveCallback;

        public Boolean Initialize(String portName, int baudRate = DEFAULT_BAUD_RATE)
        {
            if (fSerialPort == null)
            {
                fSerialPort = new SerialPort();
            }
            else
            {
                try
                {
                    fSerialPort.Close();
                }
                catch
                {
                    return false;
                }
            }

            fSerialPort.PortName = portName;
            fSerialPort.BaudRate = baudRate;
            fSerialPort.DataBits = 8;
            fSerialPort.Parity = Parity.None;
            fSerialPort.StopBits = StopBits.One;

            try
            {
                fSerialPort.Open();
                fSerialPort.DataReceived += new SerialDataReceivedEventHandler(UartDataReceivedHandler);

                // initial read buffer/queue
                Flush();

                return true;
            }
            catch
            {
            }

            return false;
        }

        public Boolean IsConnected
        {
            get { return null == fSerialPort ? false : fSerialPort.IsOpen; }
        }

        private void UartDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            int bytesToRead;
            byte[] tmpData = null;


            byte[] tmpAll = null;
            byte[] all = null;
            int totalBytes = 0;
            int readBytes = 0;
            while ((bytesToRead = sp.BytesToRead) > 0)
            {
                tmpData = new byte[bytesToRead];
                readBytes = sp.Read(tmpData, 0, bytesToRead);

                totalBytes += readBytes;

                if (null != all)
                {
                    tmpAll = all;
                    all = new byte[tmpAll.Length + tmpData.Length];
                    Array.Copy(tmpAll, 0, all, 0, tmpAll.Length);
                    Array.Copy(tmpData, 0, all, tmpAll.Length, tmpData.Length);
                }
                else
                {
                    all = tmpData;
                }
            }

            // debug
            if (null != all)
            {
                String info = String.Format("Data Received: {0} bytes:", all.Length);
                for (int i = 0; i < all.Length; i++)
                {
                    info += String.Format("{0:X2} ", all[i]);
                }
                Console.WriteLine(info);
            }

            mUartDataQueue.AppendData(all);

            while (true)
            {
                IRUARTCommand uartCommand = mUartDataQueue.GetCommandData(true);
                if ((null != uartCommand) && uartCommand.IsValidCommand)
                {
                    if (mPackageReceiveCallback != null)
                    {
                        mPackageReceiveCallback.OnReceivedPackageData(uartCommand.CommandBytes);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        // clear the read buffer
        public void Flush()
        {
            int bytesToRead = 0;
            byte[] tmpData;
            while ((bytesToRead = fSerialPort.BytesToRead) > 0)
            {
                tmpData = new byte[bytesToRead];
                fSerialPort.Read(tmpData, 0, bytesToRead);
            }

            mUartDataQueue.Flush();
        }

        public void RegisterPackageReceiveCallback(OnIrDongleReceivedDataCallback callback)
        {
            mPackageReceiveCallback = callback;
        }

        public String PortName
        {
            get { return fSerialPort.PortName; }
        }

        public int BaudRate
        {
            get { return fSerialPort.BaudRate; }
        }

        public void Write(byte[] dataBytes)
        {
            if (null != fSerialPort)
            {
                fSerialPort.Write(dataBytes, 0, dataBytes.Length);
            }
        }
    }

    public interface OnIrDongleReceivedDataCallback
    {
        void OnReceivedPackageData(byte[] receivedDataBytes);
    }
}
