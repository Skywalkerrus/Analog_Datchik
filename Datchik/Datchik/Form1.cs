using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rtChart;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Datchik
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

           

            //чтение портов доступных в системе
            //бесполезное говно
            string[] ports = SerialPort.GetPortNames();
            //Добавление найденных портов в бокс
            //comboBox1.Items.AddRange(ports);
            // timer1.Interval = 60000; //1 min
               timer1.Interval = 3600000; // 1 hour
            //  timer1.Interval = 1800000; 30 min

            // timer1.Enabled = true;

            // timer1.Tick += new EventHandler(timer1_Tick);

        }

        kayChart serialDataChart;
        kayChart serialDataChart1;
        kayChart serialDataChart2;
       

        

        private void Form1_Load(object sender, EventArgs e)
        {

            chart1.ChartAreas[0].AxisY.Maximum = 3.0;
            chart1.ChartAreas[0].AxisY.Minimum = -3.0;
           
            Thread.Sleep(5000);

            serialDataChart = new kayChart(chart1, 300);
            serialDataChart.serieName = "X";

            chart2.ChartAreas[0].AxisY.Maximum = 3.0;
            chart2.ChartAreas[0].AxisY.Minimum = -3.0;
         
            serialDataChart1 = new kayChart(chart2, 300);
            serialDataChart1.serieName = "Y";

            chart3.ChartAreas[0].AxisY.Maximum = 3.0;
            chart3.ChartAreas[0].AxisY.Minimum = -3.0;
           
            serialDataChart2 = new kayChart(chart3 , 300);
            serialDataChart2.serieName = "Z";

        }

        MemoryStream userInput = new MemoryStream();


        private void timer1_Tick(object sender, System.EventArgs e)
        {
           // richTextBox3.Clear();
        }

        static List<byte> sBuffer = new List<byte>();


        
        public void button1_Click(object sender, EventArgs e)
        {
            string  a = textBox1.Text;
           
           SerialPort aSerialPort = new SerialPort ("COM" + a);



            aSerialPort.BaudRate = 115200;
            aSerialPort.Parity = Parity.None;
            aSerialPort.StopBits = StopBits.One;
            aSerialPort.DataBits = 8;
            aSerialPort.Encoding = Encoding.UTF8;
              aSerialPort.ReceivedBytesThreshold = 1;
            aSerialPort.DataReceived += new SerialDataReceivedEventHandler(serialDataReceivedEventHandler);
            aSerialPort.DataReceived += new SerialDataReceivedEventHandler(button1_DoubleClick);



            // if(textBox.Text == "a")  textBox3 = a;




            //aSerialPort.DataReceived += new SerialDataReceivedEventHandler(serialDataReceivedEventHandler2);//po prikoly
            // aSerialPort.DataReceived += new SerialDataReceivedEventHandler(serialDataReceivedEventHandler3);//po prikoly
            aSerialPort.Open();
           
            richTextBox3.Visible = false;
           
        }
        public void button1_DoubleClick(object sender, EventArgs e)
        {
            serialPort1.Close();
        }



        private void serialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
          
            NumberFormatInfo provider = new NumberFormatInfo();
/*
            provider.NumberDecimalSeparator = ",";
            provider.NumberGroupSeparator = ".";
            provider.NumberGroupSizes = new int[] { 3 };
            */
            NumberFormatInfo provider1 = new NumberFormatInfo();
            /*
            provider1.NumberDecimalSeparator = ",";
            provider1.NumberGroupSeparator = ".";
            provider1.NumberGroupSizes = new int[] { 3 };
            */

            NumberFormatInfo provider2 = new NumberFormatInfo();
            /*
            provider2.NumberDecimalSeparator = ",";
            provider2.NumberGroupSeparator = ".";
            provider2.NumberGroupSizes = new int[] { 3 };
            */

            SerialPort sData = sender as SerialPort;
          
                string recvData1 = sData.ReadLine();
                double b = Convert.ToDouble(recvData1, provider);
                double a;
         
                if (b > 512)
                {

                a = (b / 1023.0) * 5.0 / 2.0;
                    serialData.Invoke((MethodInvoker)delegate { serialData.AppendText("\n" + "X:" + a.ToString("0.000") + "\n"); });
                    serialDataChart.TriggeredUpdate(a);
                }
                else if (b < 487)

                {
                a = (b / 1023.0) * 5.0 / (-2.0);
                    serialData.Invoke((MethodInvoker)delegate { serialData.AppendText("\n" + "X:" + a.ToString("0.000") + "\n"); });

                    serialDataChart.TriggeredUpdate(a);
                }
                else
                {
                    a = b * 0;
                    serialData.Invoke((MethodInvoker)delegate { serialData.AppendText("\n" + "X:" + a.ToString("0.000") + "\n"); });

                    serialDataChart.TriggeredUpdate(a);
                }

           


                SerialPort sData1 = sender as SerialPort;
            string recvDataShit = sData1.ReadLine();
            double b2 = Convert.ToDouble(recvDataShit, provider);
            //  string recvData2 = sData1.ReadLine();

            double a1;
            if (b2 > 512)
            {

                a1 = (b2 / 1023.0) * 5.0 / 2.0;
               richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText("\n" + "Y:" + a1.ToString("0.000") + "\n"); });
                serialDataChart1.TriggeredUpdate(a1);
            }
            else if (b2 < 487)

            {
                a1 = (b2 / 1023.0) * 5.0 / (-2.0);
                richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText("\n" + "Y:" + a1.ToString("0.000") + "\n"); });

                serialDataChart1.TriggeredUpdate(a1);
            }
            else
            {
                a1 = b2 * 0;
                richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText("\n" + "Y:" + a1.ToString("0.000") + "\n"); });
               
                serialDataChart1.TriggeredUpdate(a1);
            }


            //    }

            SerialPort sData2 = sender as SerialPort;

            string recvData3 = sData2.ReadLine();
            double b3 = Convert.ToDouble(recvData3, provider);
            //  string recvData2 = sData1.ReadLine();
            double a2;

            if (b3 > 512)
            {

                a2 = (b3 / 1023.0) * 5.0 / 2.0;
                richTextBox2.Invoke((MethodInvoker)delegate { richTextBox2.AppendText("\n" + "Z:" + a2.ToString("0.000") + "\n"); });
                serialDataChart2.TriggeredUpdate(a2);
            }
            else if (b3 < 487)

            {
                a2 = (b3 / 1023.0) * 5.0 / (-2.0);
                richTextBox2.Invoke((MethodInvoker)delegate { richTextBox2.AppendText("\n" + "Z:" + a2.ToString("0.000") + "\n"); });

                serialDataChart2.TriggeredUpdate(a2);
            }
            else
            {
                 a2 = b3 * 0;
                richTextBox2.Invoke((MethodInvoker)delegate { richTextBox2.AppendText("\n" + "Z:" + a2.ToString("0.000") + "\n"); });

                serialDataChart2.TriggeredUpdate(a2);
            }
                //   }
             
                DateTime localDate = DateTime.Now;
                richTextBox3.Invoke((MethodInvoker)delegate { richTextBox3.AppendText( "\n" +  "  "   + a.ToString("0.000") + "  " +  a1.ToString("0.000") + "  " + a2.ToString("0.000") + "  ");  });
            // richTextBox3.Invoke((MethodInvoker)delegate { richTextBox3.AppendText("\n" + "Y:" + a1 + "\n"); });
            // richTextBox3.Invoke((MethodInvoker)delegate { richTextBox3.AppendText("\n" + "Z:" + a2 + "\n"); });
          

        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            // no
        }

        private void serialData_TextChanged(object sender, EventArgs e)
        {
            serialData.SelectionStart = serialData.Text.Length;

            serialData.ScrollToCaret();

            richTextBox1.SelectionStart = richTextBox1.Text.Length;

            richTextBox1.ScrollToCaret();

            richTextBox2.SelectionStart = richTextBox2.Text.Length;

            richTextBox2.ScrollToCaret();
           // richTextBox3.Visible = false;
           
     //       richTextBox3.SelectionStart = richTextBox3.Text.Length;

       //     richTextBox3.ScrollToCaret();

        }



       private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sFile = new SaveFileDialog();



            string[] lines = { serialData.Text, richTextBox1.Text, richTextBox2.Text };


            sFile.Filter = "rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (sFile.ShowDialog() == DialogResult.OK &&
               sFile.FileName.Length > 0)
            {

                StreamWriter wr = new StreamWriter(sFile.FileName);
                DateTime ThToday = DateTime.Now;

                string[] readText = richTextBox3.Lines;
               
                foreach (var c in readText)
                {

                    wr.WriteLine(c);
                }

                wr.Close();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            richTextBox3.Visible = true;
            serialDataChart.TriggeredUpdate(0);
            serialDataChart1.TriggeredUpdate(0);
            serialDataChart2.TriggeredUpdate(0);

        }
    }

      
    }




        
 
