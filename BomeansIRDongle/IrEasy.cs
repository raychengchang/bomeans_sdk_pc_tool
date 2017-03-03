using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bomeans.IRNet;

namespace Bomeans
{
    public class IrEasy : IRBlaster
    {
        private Usb2Serial mUsb2Serial;
        private static ReceiveDataCallback mSendReceivedDataCallback;

        public IrEasy()
        {
            mUsb2Serial = new Usb2Serial();
            mUsb2Serial.RegisterPackageReceiveCallback(new Usb2SerialCallback());
        }

        public String PortName
        {
            get { return mUsb2Serial == null ? "" : mUsb2Serial.PortName; }
        }

        public int BaudRate
        {
            get { return mUsb2Serial == null ? 0 : mUsb2Serial.BaudRate; }
        }

        public Boolean Initialize(String portName, int baudRate = 38400)
        {
            if (null != mUsb2Serial)
            {
                return mUsb2Serial.Initialize(portName, baudRate);
            }
            else
            {
                return false;
            }
        }

        public void Flush()
        {
            if (null != mUsb2Serial)
            {
                mUsb2Serial.Flush();
            }
        }

        public int isConnection()
        {
            if (null != mUsb2Serial)
            {
                return mUsb2Serial.IsConnected ? ConstValue.BIROK : ConstValue.BIRNotConnect;
            }
            else
            {
                return ConstValue.BIRNotConnect;
            }
        }

        public int sendData(byte[] irBlasterData)
        {
            if (mUsb2Serial.IsConnected)
            {
                mUsb2Serial.Write(irBlasterData);
                return ConstValue.BIROK;
            }

            return ConstValue.BIRNotConnect;
        }

        public void setReceiveDataCallback(ReceiveDataCallback callback)
        {
            mSendReceivedDataCallback = callback;
        }

        class Usb2SerialCallback : OnIrDongleReceivedDataCallback
        {
            public void OnReceivedPackageData(byte[] receivedDataBytes)
            {
                if (null != mSendReceivedDataCallback)
                {
                    mSendReceivedDataCallback.onDataReceived(receivedDataBytes);
                }
            }
        }
    }
}
