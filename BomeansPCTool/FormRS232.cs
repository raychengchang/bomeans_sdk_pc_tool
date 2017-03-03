using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Bomeans;
using Bomeans.IRNet;
using BomeansPCTool.Properties;


namespace BomeansPCTool
{
    public partial class FormRS232 : Form
    {
        private IrEasy mMyIrEasy;

        public FormRS232(IrEasy irEasy)
        {
            mMyIrEasy = irEasy;

            InitializeComponent();
        }

        private void FormRS232_Load(object sender, EventArgs e)
        {
            refreshSerialPorts();
        }

        private void refreshSerialPorts()
        {
            cboComPort.Items.Clear();
            cboBaudRate.Items.Clear();

            // COM ports
            string[] serialPortNames = SerialPort.GetPortNames();
            cboComPort.Items.AddRange(serialPortNames);
            if (cboComPort.Items.Count > 0)
            {
                cboComPort.SelectedIndex = 0;
            }

            String comPort = Settings.Default.UART_COM_PORT;
            if ((comPort.Length > 0) && (serialPortNames.Length > 0))
            {
                for (int i = 0; i < cboComPort.Items.Count; i++)
                {
                    if (cboComPort.Items[i].Equals(comPort))
                    {
                        cboComPort.SelectedIndex = i;
                        break;
                    }
                }
            }

            // baud rates
            String[] baudRates = new String[] { "9600", "14400", "19200", "28800", "38400", "56000", "57600", "115200", "128000", "250000", "256000" };
            cboBaudRate.Items.AddRange(baudRates);
            if (cboBaudRate.Items.Count > 0)
            {
                cboBaudRate.SelectedIndex = 4;
            }

            String baudRate = Settings.Default.UART_BAUD_RATE;
            if ((baudRate.Length > 0) && (cboBaudRate.Items.Count > 0))
            {
                for (int i = 0; i < cboBaudRate.Items.Count; i++)
                {
                    if (cboBaudRate.Items[i].Equals(baudRate))
                    {
                        cboBaudRate.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((cboComPort.Text.Length == 0) || (cboBaudRate.Text.Length == 0))
            {
                if (DialogResult.Yes == Program.ShowQuestion("No COM port is set, continue?"))
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                else
                {
                    return;
                }
            }

            if (null != mMyIrEasy)
            {
                if (mMyIrEasy.Initialize(cboComPort.Text, Convert.ToInt32(cboBaudRate.Text)))
                {
                    if (mMyIrEasy.isConnection() == ConstValue.BIROK)
                    {
                        // save to settings
                        Settings.Default.UART_COM_PORT = cboComPort.Text;
                        Settings.Default.UART_BAUD_RATE = cboBaudRate.Text;
                        Settings.Default.Save();

                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (DialogResult.No == Program.ShowQuestion(String.Format("Failed to open {0}! \n Continue?", mMyIrEasy.PortName)))
                        {
                            return;
                        }
                        else
                        {
                            DialogResult = DialogResult.Cancel;
                        }
                    }
                }

            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshSerialPorts();
        }
    }
}
