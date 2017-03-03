using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomeans
{
    public class IRUARTCommand
    {
        private static byte PREFIX0_CODE = (byte)0xFF;
        private static byte PREFIX1_CODE = (byte)0x61;
        private static byte VERSION_CODE = (byte)0x00;
        private static byte POSTFIX_CODE = (byte)0xF0;

        private static byte UNKNOWN_COMMAND_ID = 0xFF;

        private byte fCommandID;
        private byte[] fPayload;
        private byte[] fCmdBytes;

        public static byte IR_UART_COMMAND_ACK = (byte)0x7F;
        public static byte IR_UART_COMMAND_NAK = (byte)0x7E;

        public static byte IR_UART_COMMAND_LEARNING_DATA_TX = 0x01;
        public static byte IR_UART_COMMAND_FRAME_DATA_TX = 0x02;

        public static byte IR_UART_COMMAND_FIRMWARE_VERSION = 0x10;
        public static byte IR_UART_COMMAND_FIRMWARE_VERSION_ACK = 0x83;

        public static byte IR_UART_COMMAND_MODE_LEARNING = 0x30;
        public static byte IR_UART_COMMAND_MODE_NORMAL = 0x31;  // exit learning mode
        public static byte IR_UART_COMMAND_MODE_SLEEP = 0x03;

        public static byte IR_UART_COMMAND_IR_TRANSMISSION_DONE = 0x81;
        //public static byte IR_UART_COMMAND_LEARNING_OK = 0x90;
        public static byte IR_UART_COMMAND_LEARNING_FAIL = 0x91;
        public static byte IR_UART_COMMAND_LEARNING_DATA_RX = 0x92;

        public static byte IR_UART_COMMAND_SEND_FIRMWARE_DATA = 0x0E;
        public static byte IR_UART_COMMAND_SWITCH_TO_NORMAL_MODE = 0x0F;
        public static byte IR_UART_COMMAND_FIRMWARE_PROGRAM_DONE = 0x82;    // ACK

        public IRUARTCommand(byte commandID, byte[] data)
        {
            fCommandID = commandID;
            fPayload = data;

            int dataLength = 0;
            if (data != null)
            {
                dataLength = data.Length;
            }

            fCmdBytes = new byte[dataLength + 8];

            // prefix
            fCmdBytes[0] = PREFIX0_CODE;
            fCmdBytes[1] = PREFIX1_CODE;

            // version
            fCmdBytes[2] = VERSION_CODE;

            // length
            int length = dataLength + 2; // command (1-byte) + data length + checksum (1-byte)
            fCmdBytes[3] = (byte)(length & 0xFF);
            fCmdBytes[4] = (byte)((length >> 8) & 0xFF);

            // command
            fCmdBytes[5] = commandID;

            // data/payload
            for (int i = 0; i < dataLength; i++)
            {
                fCmdBytes[6 + i] = data[i];
            }

            // checksum
            int checksum = 0;
            for (int i = 2; i < 6 + dataLength; i++)
            {
                checksum += fCmdBytes[i];
            }
            checksum &= 0xFF;
            fCmdBytes[6 + dataLength] = (byte)checksum;

            // postfix
            fCmdBytes[7 + dataLength] = POSTFIX_CODE;
        }

        public IRUARTCommand(byte[] cmdBytes)
        {
            fCmdBytes = cmdBytes;

            if (cmdBytes.Length < 8)
            {
                fCommandID = UNKNOWN_COMMAND_ID;
                return;
            }

            // prefix & postfix
            if ((cmdBytes[0] != PREFIX0_CODE) || (cmdBytes[1] != PREFIX1_CODE) || (cmdBytes[cmdBytes.Length - 1] != POSTFIX_CODE))
            {
                fCommandID = UNKNOWN_COMMAND_ID;
                return;
            }

            // version
            byte versionCode = cmdBytes[2];

            // length
            int length = cmdBytes[3] + (((int)cmdBytes[4]) << 8);   // command (1-byte) + data length + checksum (1-byte)
            if (length + 6 != cmdBytes.Length)
            {
                fCommandID = UNKNOWN_COMMAND_ID;
                return;
            }
            int payloadLength = length - 2; // data length

            // command ID
            fCommandID = cmdBytes[5];

            // payload data
            if (payloadLength > 0)
            {
                fPayload = new byte[payloadLength];
                Array.Copy(cmdBytes, 6, fPayload, 0, payloadLength);
            }

            // checksum
            int checksum = 0;
            for (int i = 2; i < 6 + payloadLength; i++)
            {
                checksum += cmdBytes[i];
            }
            checksum &= 0xFF;
            if (cmdBytes[6 + payloadLength] != (byte)checksum)
            {
                fCommandID = UNKNOWN_COMMAND_ID;
                fPayload = null;
            }
        }

        public byte CommandID
        {
            get { return fCommandID; }
        }

        public byte[] Payload
        {
            get { return fPayload; }
        }

        public byte[] CommandBytes
        {
            get { return fCmdBytes; }
        }

        public Boolean IsValidCommand
        {
            get { return fCommandID != UNKNOWN_COMMAND_ID; }
        }
    }
}
