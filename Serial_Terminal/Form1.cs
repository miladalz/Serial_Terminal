using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Serial_Terminal
{
    public partial class Form1 : Form
    {
        string strReceive;
        string strBaud;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBaud = comboBox2.Text;
            serialPort1.Close();
            try
            {
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.StopBits = StopBits.One;
                serialPort1.BaudRate = int.Parse(strBaud);
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
                serialPort1.DiscardInBuffer();
                label1.BackColor = System.Drawing.Color.Green;
                label1.ForeColor = System.Drawing.Color.White;
                label1.Text = "(" + comboBox1.Text + ")" + " Connceted ";
            }
            catch
            {
                label1.Text = "Disconnected.";
                label1.BackColor = System.Drawing.Color.Red;
                label1.ForeColor = System.Drawing.Color.White;
                MessageBox.Show("Cannt access " + "(" + comboBox1.Text + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBaud = comboBox2.Text;
            try
            {
                serialPort1.BaudRate = int.Parse(strBaud);
                serialPort1.DiscardInBuffer();
                label1.BackColor = System.Drawing.Color.Green;
                label1.ForeColor = System.Drawing.Color.White;
                label1.Text = "( " + comboBox1.Text + " ) " + "Connected.";
            }
            catch
            {
                label1.Text = "Disconnected.";
                label1.BackColor = System.Drawing.Color.Red;
                label1.ForeColor = System.Drawing.Color.White;
                MessageBox.Show("Can't Access " + "(" + comboBox1.Text + ")", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            label1.Text = "Disconnected.";
            label1.BackColor = System.Drawing.Color.Red;
            label1.ForeColor = System.Drawing.Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
                serialPort1.WriteLine(textBox1.Text);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            strReceive = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(DisplayText));
        }

        private void DisplayText(object sender, EventArgs e)
        {
            textBox2.AppendText(strReceive);
        }

        private void btnClearSend_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void btnClearRecieve_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strReceive=serialPort1.ReadExisting();
        }

    }
}
