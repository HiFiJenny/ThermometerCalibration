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
using System.IO;
using System.Threading;
using NPOI;                 //基础辅助库
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;  //使用正则表达式


namespace ThermometerCalibration
{
    public partial class Form1 : Form
    {
        System.Threading.Timer threadtimer;     //定义timer类
        System.Threading.Timer TimeOut;         //串口读取超时定时器；

        private Thread SearchThread;            //定义串口检索线程
        globvalue PV = new globvalue();         //实例化PV类
        ZOGLAB[] device = new ZOGLAB[50];       //实例化结构体数组，用于存储温湿度计读取的数据

        public Form1()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// 变量初始化
        /// </summary>
        private void var_Init()
        {
            PV.sp_search = false;
            PV.receive_finish = false;
            PV.sp_th_read = false;
            PV.overflag = false;    //初始化超时标志位；

            PV.device_num = 0;
            PV.overcount = 0;
            PV.steady_count = 0;  //定时器一分钟触发一次，温箱稳定计数5次，即5分钟温度稳定认为温箱稳定
        }

        /// <summary>
        /// 窗口组件初始化
        /// </summary>
        private void com_Init()
        {
            serialPort1.BaudRate = 9600;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialport1_DataReceived);
            serialPort2.BaudRate = 9600;
            serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialport2_DataReceived);
            serialPort2.ReadTimeout = 5000;  //串口2读取超时5s

            progressBar1.Maximum = 100;
            //环形条组件
            TempBar.Text = "0.00℃";
            humibar.Text = "0.00%";
            TempBar.Minimum = 10;
            TempBar.Maximum = 50;
            TempBar.Value = 10;
            humibar.Value = 0;
            TempBar.Update();
            humibar.Update();

            threadtimer = new System.Threading.Timer(new TimerCallback(Timerup), null, Timeout.Infinite, 1000);   //初始化Timer类
            TimeOut = new System.Threading.Timer(new TimerCallback(Overtime), null, Timeout.Infinite, 1000);   //初始化Timer类

            this.Size = this.MaximumSize = this.MinimumSize = new Size(505, 285);   //隐藏进度条
            button2.Enabled = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var_Init(); //全局变量初始化
            com_Init(); //程序功能，线程，委托，串口等控件初始化
            //this.Size = this.MaximumSize = this.MinimumSize = new Size(325, 325);
            PV.temp = 0;
            label1.Text = "程序待初始化";

            TS1.ReadOnly = true;
            TS2.ReadOnly = true;
            TS3.ReadOnly = true;
            HS1.ReadOnly = true;
            HS2.ReadOnly = true;
            HS3.ReadOnly = true;

            if(!Directory.Exists(@".\DataSave\"))
            {
                Directory.CreateDirectory(@".\DataSave\");
            }
        }

        /// <summary>
        /// 定时器启动，间隔1分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            calibrationpoints_init();   // 将文本框内温湿度的设定值存入数组

            PV.wait_steady_humidity = Convert.ToDouble(Hsteady.Text);       //设置温箱稳定的等待时间
            PV.wait_steady_temperature = Convert.ToDouble(Tsteady.Text);
            PV.wait_steady_time_H = Convert.ToInt32(TimeHSteady.Text);
            PV.wait_steady_time_T = Convert.ToInt32(TimeTSteady.Text);

            threadtimer.Change(0, 60000);           //60,000 ms即1分钟计入一次定时器   
            
            this.Invoke((MethodInvoker)delegate {
                progressBar1.Value = 2;
                this.Size = this.MaximumSize = this.MinimumSize = new Size(505, 325);           //显示进度条                
            }); //更新进度条                                                                
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="valuse"></param>
        private void Timerup(object valuse)
        {
            if (PV.temcal[0] == false)              //通过else if 实现先后顺序
            {
                temperature_record(0);
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "温度点1的校准";
                });
            }
            else if (PV.temcal[1] == false)
            {
                temperature_record(1);
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "温湿度点2的校准";
                });
            }
            else if (PV.temcal[2] == false)
            {
                temperature_record(2);
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "温度点3的校准";
                });
            }
            else if (PV.humcal[0] == false)
            {
                humidity_record(0);
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "湿度点1的校准";
                });
            }
            else if (PV.humcal[1] == false)
            {
                humidity_record(1);
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "湿度点2的校准";
                });
            }
            else if (PV.humcal[2] == false)
            {
                humidity_record(2);
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "湿度点3的校准";
                });

            }
            if ( PV.temcal[0] == true && PV.temcal[1] == true && PV.temcal[2] == true && PV.humcal[0] == true && PV.humcal[1] == true && PV.humcal[2] == true)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "正在生成excel";
                    progressBar1.Value = 0;
                });

                threadtimer.Change(-1, 60000);
                threadtimer.Dispose();
                writetoexcel();
                this.Invoke((MethodInvoker)delegate
                {
                    this.Size = this.MaximumSize = this.MinimumSize = new Size(505, 285);
                });
                MessageBox.Show("校准完成");
                
            }
        }

        /// <summary>
        /// 在定时器中等待温箱稳定，记录温湿度计的温度值
        /// j表示第n+1个校准点
        /// </summary>
        /// <param name="j"></param>
        private void temperature_record(int j)  
        {
            string tempstr;     //记录温箱返回的值，方便调试
            ask_chamber("set");
            if(Convert.ToDouble(PV.Set_Temperature)!=PV.standard_temperature[j])
            {
                serialPort2.Open();
                serialPort2.Write("=AS " + PV.standard_temperature_str[j] + "\r");
                tempstr = serialPort2.ReadLine();
                serialPort2.Close();
            }
            if (Convert.ToDouble(PV.Set_Humidity) != 60)
            {
                serialPort2.Open();
                serialPort2.Write("=RS 60.00\r" );
                tempstr = serialPort2.ReadLine();
                serialPort2.Close();
            }
            ask_chamber("now");
            if(Math.Abs(Convert.ToDouble(PV.Temperature)-PV.standard_temperature[j])<=PV.wait_steady_temperature && Math.Abs(Convert.ToDouble(PV.Humidity) - 60) <= PV.wait_steady_humidity)   //温度稳定在设定值的设定范围内，湿度稳定在60的设定稳定范围内
            {
                PV.steady_count++;
                if(PV.steady_count > PV.wait_steady_time_T) //保持设定时间内稳定
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.Clear(); //稳定时，清空Listbox的内容，准备填入本次记录的数值
                        listBox1.Items.Add(PV.Set_Temperature + "℃记录情况");
                    });
                    for (int i = 0; i < PV.device_num; i++)
                    {
                        ask_ZOGLAB_temperature(device[i].portname);
                        device[i].temperature[j] = Convert.ToDouble(PV.tempT.Substring(0, PV.tempT.Length - 2));  //读取并记录每个温湿度计的温度。
                        if(Convert.ToDouble(PV.Temperature) == 20)  //在温度20℃时，可以把湿度一并记录
                        {
                            device[i].humidity[1] = Convert.ToDouble(PV.tempH.Substring(0, PV.tempH.Length - 2));  
                            PV.humcal[1] = true;    //跳过第二个湿度的校准
                        } 
                        this.Invoke((MethodInvoker)delegate {
                            listBox1.Items.Add(device[i].serialnum + "  " + device[i].temperature[j]);                            
                        });  //记录值列在listbox中
                    }
                    PV.steady_count = 0; //计数器归0
                    PV.temcal[j] = true; //第一个温度计量点完成标志位
                    this.Invoke((MethodInvoker)delegate { progressBar1.Value = progressBar1.Value + 15; });
                }
            }
            else
            {
                PV.steady_count = 0;
            }

        }

        /// <summary>
        /// 在定时器中等待温箱稳定，记录温湿度计的湿度值
        /// j表示第n+1个校准点
        /// 湿度的稳定时间存疑
        /// </summary>
        /// <param name="j"></param>
        private void humidity_record(int j)
        {
            string tempstr;
            ask_chamber("set");
            if (Convert.ToDouble(PV.Set_Humidity) != PV.standard_humidity[j])
            {
                serialPort2.Open();
                serialPort2.Write("=RS " + PV.standard_humidity_str[j] + "\r");
                tempstr = serialPort2.ReadLine();
                serialPort2.Close();
            }
            if (Convert.ToDouble(PV.Set_Temperature) != 20)
            {
                serialPort2.Open();
                serialPort2.Write("=AS 20.00\r");
                tempstr = serialPort2.ReadLine();
                serialPort2.Close();
            }
            ask_chamber("now");
            if (Math.Abs(Convert.ToDouble(PV.Humidity) - PV.standard_humidity[j]) <= PV.wait_steady_humidity && Math.Abs(Convert.ToDouble(PV.Temperature) - 15) <= PV.wait_steady_temperature)
            {
                PV.steady_count++;
                if (PV.steady_count > PV.wait_steady_time_H) //保持设定时间内稳定
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.Clear(); //稳定时，清空Listbox的内容，准备填入本次记录的数值
                        listBox1.Items.Add(PV.Set_Humidity + "%记录情况");
                    });
                    for (int i = 0; i < PV.device_num; i++)
                    {
                        ask_ZOGLAB_temperature(device[i].portname);
                        device[i].humidity[j] = Convert.ToDouble(PV.tempH.Substring(0, PV.tempH.Length - 2));  //读取并记录每个温湿度计的温度。 
                        this.Invoke((MethodInvoker)delegate {
                            listBox1.Items.Add(device[i].serialnum + "  " + device[i].humidity[j]);
                        });
                    }
                    PV.steady_count = 0; //计数器归0
                    PV.humcal[j] = true; //第一个温度计量点完成标志位
                    this.Invoke((MethodInvoker)delegate { progressBar1.Value = progressBar1.Value + 15; });
                }
            }
            else
            {
                PV.steady_count = 0;
            }

        }

        /// <summary>
        /// 环形控件数据文字更新函数
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="humidity"></param>
        private void circleupdate(string temperature, string humidity)
        {
            double t = Convert.ToDouble(temperature.Substring(0,temperature.Length-2));
            double h = Convert.ToDouble(humidity.Substring(0, humidity.Length - 2));
            int tt = (int)Math.Round(t);
            int hh = (int)Math.Round(h);
            this.Invoke((MethodInvoker)delegate
            {
                TempBar.Value = tt;
                humibar.Value = hh;
                TempBar.Text = temperature + "℃";
                humibar.Text = humidity + "%";
                TempBar.Update();
                humibar.Update();
            });
        }

        /// <summary>
        /// 超时计数函数
        /// </summary>
        /// <param name="valuse"></param>
        private void Overtime(object valuse)
        {
            PV.overcount++;
            if(PV.overcount == 9)  //9s没有完成读取动作认为超时；
            {
                PV.overcount = 0;
                PV.overflag = true;
            }
        }

        /// <summary>
        /// 读取ZOGLAB的温湿度（未完成）
        /// 需要将稳定值存入excel的功能
        /// </summary>
        /// <param name="value"></param>
        private void ask_ZOGLAB_temperature(string portname)
        {
            serialPort1.PortName = portname;
            serialPort1.Open();
            PV.sp_th_read = true;
            serialPort1.WriteLine("AT*ReadSensor:0");
            while (!PV.receive_finish) ;
            PV.receive_finish = false;
            PV.sp_th_read = false;
            this.Invoke((MethodInvoker)delegate {
                textBox1.Text = PV.tempT + " " + PV.tempH;
            });
            serialPort1.Close();
        }

        /// <summary>
        /// 温箱询问函数
        /// mission = now 询问温箱当前的温湿度
        /// mission = set 询问温箱设定的温湿度
        /// </summary>
        /// <param name="mission"></param>
        private void ask_chamber(string mission)
        {
            try
            {
                serialPort2.Open();
                switch(mission)
                {
                    case "now":
                        serialPort2.Write("?AP\r");
                        Thread.Sleep(100);
                        PV.Temperature = serialPort2.ReadExisting();
                        PV.Temperature = Regex.Replace(PV.Temperature, @"[^\d.\d]", "");    //提取字符串中的数字
                        serialPort2.Write("?RP\r");
                        Thread.Sleep(100);
                        PV.Humidity = serialPort2.ReadExisting();
                        PV.Humidity = Regex.Replace(PV.Humidity, @"[^\d.\d]", "");
                        circleupdate(PV.Temperature, PV.Humidity);
                        break;
                    case "set":
                        serialPort2.Write("?AS\r");
                        Thread.Sleep(100);
                        PV.Set_Temperature = serialPort2.ReadExisting();
                        PV.Set_Temperature = Regex.Replace(PV.Set_Temperature, @"[^\d.\d]", "");    //提取字符串中的数字
                        serialPort2.Write("?RS\r");
                        Thread.Sleep(100);
                        PV.Set_Humidity = serialPort2.ReadExisting();
                        PV.Set_Humidity = Regex.Replace(PV.Set_Humidity, @"[^\d.\d]", "");    //提取字符串中的数字
                        break;
                }

                serialPort2.Close();
            }
            catch
            {
                if (serialPort2.IsOpen == true)
                {
                    serialPort2.DiscardInBuffer();
                    serialPort2.DiscardInBuffer();
                    serialPort2.Close();
                }

            //    MessageBox.Show("温箱通信错误");
            }
        }

        /// <summary>
        /// 串口接收函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialport1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {           
            string str,str1;
            if(PV.sp_search == true)                                            //检测连接的设备数量，ID
            {
                str = serialPort1.ReadLine();                                  //从串口接收缓存中读取字符串，截止到“换行”
                if(str.Contains("DeviceID:"))                                   //记录检测到的温湿度计的产品编号
                {
                    str1 = str.Substring(10);
                                                            //记录检测到的温湿度计数量
                    this.Invoke((MethodInvoker)delegate
                   {                      
                        label1.Text = serialPort1.PortName;
                        listBox1.Items.Add(serialPort1.PortName + "      " + str1);
                    });
                    device[PV.device_num].portname =serialPort1.PortName;       //串口名和设备编号存入device类数组中
                    device[PV.device_num].serialnum = str1;
                    PV.device_num++;
                }
                else if(str.Contains("INFO"))                                     //检测到温箱 温箱接收到错误信号会返回“INVALID REQUEST”
                {
                    serialPort2.PortName = serialPort1.PortName;
                    this.Invoke((MethodInvoker)delegate
                    {
                        label1.Text = serialPort1.PortName;
                        listBox1.Items.Add(serialPort2.PortName + "      " + "Chamb Box");
                    });
                    serialPort1.DiscardInBuffer();
                    PV.receive_finish = true;
                }
                else if(str.Contains("OK"))
                {
                    PV.receive_finish = true;                                   //读取完成
                    serialPort1.DiscardInBuffer();
                }
                               
            }
            else if(PV.sp_th_read == true)                                      //接收读取温湿度的命令
            {
                str = serialPort1.ReadLine();
                if(str.Contains("Temperature"))
                {
                    PV.tempT = str.Substring(14);
                }
                if(str.Contains("Humidity"))
                {
                    PV.tempH = str.Substring(11);
                }
                if (str.Contains("OK"))
                {
                    PV.receive_finish = true;                                   //读取完成
                }
            }
            
        }
        
        /// <summary>
        /// 串口2接收函数，温箱的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialport2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /*
            string str = serialPort2.ReadExisting();
            if(str.Contains("AP"))
            {
                if(str.Contains("."))
                {
                    PV.Temperature = Regex.Replace(str, @"[^\d.\d]", "");
                }
            }
            */
        }

        /// <summary>
        /// 开启检索串口的线程，避免主界面卡死
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SearchThread = new Thread(new ThreadStart(PortSearch));
            SearchThread.IsBackground = true;
            PV.sp_search = true;
            SearchThread.Start();
        }

        /// <summary>
        /// 检索连通的串口函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortSearch()
        {
            int i = 0;

            PV.device_num = 0;          //每次检索时，清空存储设备ID，端口名的List和设备数量
            
            this.Invoke((MethodInvoker)delegate
            {
                this.Cursor = Cursors.WaitCursor;
                this.Size = this.MaximumSize = this.MinimumSize = new Size(505, 325);           //显示进度条
                listBox1.Items.Clear();
                label1.Text = "正在寻找连接设备";
            });

            for (i = 1; i <= 100; i++)                         //检索COM口上限100
            {
                serialPort1.PortName = "COM" + Convert.ToString(i);
                try
                {
                    serialPort1.Open();
                    serialPort1.WriteLine("AT##Info");          //查询温湿度计的ID号 
                    while (!PV.receive_finish) ;                //等待读取完成
                    PV.receive_finish = false;                  //检测到的设备总数在串口接收中断中更新
                    serialPort1.Close();
                }
                catch
                {

                }
                this.Invoke((MethodInvoker)delegate { progressBar1.Value = i; }); //更新进度条
                Thread.Sleep(5);
            }
            PV.sp_search = false;                               //检索完成

            SN_to_CN();                                         //转换计量编号
            structnewarray();
            try
            {
                serialPort2.Open();
                serialPort2.Write("?AP\r");
                serialPort2.ReadExisting();
                serialPort2.Close();
                this.Invoke((MethodInvoker)delegate {
                    button2.Enabled = true;
                    button1.Enabled = false;
                });
                ask_chamber("now");
            }
            catch
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listBox1.Items.Add("未发现温箱");
                });
            }          
            this.Invoke((MethodInvoker)delegate 
            {
                label1.Text = "接口测试完毕,检测到" + Convert.ToString(PV.device_num) + "个ZOGLAB温湿度计"; 
                this.Size = this.MaximumSize = this.MinimumSize = new Size(505, 285);
                this.Cursor = Cursors.Default;
            });            
            SearchThread.Abort();
        }

        /// <summary>
        /// SN码根据表格转换位计量编号
        /// </summary>
        private void SN_to_CN()
        {
            string SN;
            string url = @".\calnum.xlsx";
            IWorkbook workbook = null;                      //新建workbook对象
            try
            {
                FileStream fs = new FileStream(url, FileMode.Open, FileAccess.Read);  //创建读取文件流
                workbook = new XSSFWorkbook(fs);                //xlsx数据读入workbook
                ISheet sheet = workbook.GetSheetAt(0);          //获取第一个工作表
                IRow row;                                       //新建当前工作表行数据的类
                for (int j = 0; j < PV.device_num; j++)
                {
                    for (int i = 0; i < sheet.LastRowNum; i++)       //读取工作表的每一行
                    {
                        row = sheet.GetRow(i);
                        if (row != null)
                        {
                            SN = row.GetCell(0).ToString();  //读取的i行第一列的数据
                            if (device[j].serialnum.Contains(SN))   //表格中保存的SN码和设备读取的SN码对应上了
                            {
                                device[j].serialnum = row.GetCell(1).ToString();   //将device类中对应的SN码替换为计量编号
                                break;
                            }
                        }
                    }
                    //   this.Invoke((MethodInvoker)delegate { progressBar1.Value = 100 + j; });
                }
                fs.Close();
                workbook.Close();
            }
            catch
            {
                MessageBox.Show("未能找到calnum.xlsx的对照表");
            }        
        }

        /// <summary>
        /// 实例化结构体数组
        /// </summary>
        private void structnewarray()
        {
            for(int i = 0; i < PV.device_num; i++)
            {
                device[i].temperature = new double[3];
                device[i].humidity = new double[3];
            }
        }

        /// <summary>
        /// 将文本框内温湿度的设定值存入数组
        /// </summary>
        private void calibrationpoints_init()
        {
            PV.standard_temperature[0] = Convert.ToDouble(TS1.Text);
            PV.standard_temperature[1] = Convert.ToDouble(TS2.Text);
            PV.standard_temperature[2] = Convert.ToDouble(TS3.Text);

            PV.standard_humidity[0] = Convert.ToDouble(HS1.Text);
            PV.standard_humidity[1] = Convert.ToDouble(HS2.Text);
            PV.standard_humidity[2] = Convert.ToDouble(HS3.Text);

            PV.standard_temperature_str[0] = TS1.Text;
            PV.standard_temperature_str[1] = TS2.Text;
            PV.standard_temperature_str[2] = TS3.Text;

            PV.standard_humidity_str[0] = HS1.Text;
            PV.standard_humidity_str[1] = HS2.Text;
            PV.standard_humidity_str[2] = HS3.Text;
        }

        /// <summary>
        /// 生成excel表
        /// </summary>
        private void writetoexcel()
        {
            double per;
            for(int i = 0; i<PV.device_num; i++)
            {

                if (device[i].serialnum.Contains("\r"))
                {
                    device[i].serialnum = device[i].serialnum.Substring(0, device[i].serialnum.Length - 1);
                }
                //生成表格文件流
                PV.save = new FileStream(@".\DataSave\" + device[i].serialnum + ".xlsx", FileMode.Create, FileAccess.Write);
                PV.workbook = new XSSFWorkbook();
                
                PV.sheet = PV.workbook.CreateSheet();
                PV.row = PV.sheet.CreateRow(0);//第一行基本信息
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("日期");
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(DateTime.Now.ToString());
                PV.cell = PV.row.CreateCell(2); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("温度：");
                PV.cell = PV.row.CreateCell(3); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("20℃");
                PV.cell = PV.row.CreateCell(4); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("湿度：");
                PV.cell = PV.row.CreateCell(5); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("50%");
                PV.cell = PV.row.CreateCell(6); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("计量编号");
                PV.cell = PV.row.CreateCell(7); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].serialnum);
                //空一行
                PV.row = PV.sheet.CreateRow(2);//第三行 项目
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("温度校准");

                PV.row = PV.sheet.CreateRow(3);//第四行 表头
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("标准值/℃");
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("测量值/℃");

                PV.row = PV.sheet.CreateRow(4);//5,6,7行记录温度
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(PV.standard_temperature_str[0]);
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].temperature[0].ToString());
                PV.row = PV.sheet.CreateRow(5);
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(PV.standard_temperature_str[1]);
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].temperature[1].ToString());
                PV.row = PV.sheet.CreateRow(6);
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(PV.standard_temperature_str[2]);
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].temperature[2].ToString());
                //空一行
                PV.row = PV.sheet.CreateRow(8);//第九行 项目
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("湿度校准");

                PV.row = PV.sheet.CreateRow(9);//第10行 表头
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("标准值/%");
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue("测量值/%");

                PV.row = PV.sheet.CreateRow(10);//11,12,13行记录温度
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(PV.standard_humidity_str[0]);
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].humidity[0].ToString());
                PV.row = PV.sheet.CreateRow(11);
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(PV.standard_humidity_str[1]);
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].humidity[1].ToString());
                PV.row = PV.sheet.CreateRow(12);
                PV.cell = PV.row.CreateCell(0); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(PV.standard_humidity_str[2]);
                PV.cell = PV.row.CreateCell(1); PV.cell.SetAsActiveCell(); PV.cell.SetCellValue(device[i].humidity[2].ToString());
                PV.workbook.Write(PV.save);
                PV.save.Close();
                per = (Convert.ToDouble(i) / Convert.ToDouble(PV.device_num-1)) * 100;
                this.Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = (int)Math.Round(per);
                });
            }
        }
    }
    public class globvalue
    {
        public bool sp_search;          //端口数量搜索任务标志位
        public bool sp_th_read;         //读取温湿度计温湿度任务标志位
        public bool receive_finish;     //串口1接收完成标志位
        public bool receive_finish2;    //串口2接收完成标志位
        public bool overflag;           //超时标志位
        public bool[] temcal = new bool[4]{ false, false, false, false };       //记录校准进度，从第0个元素表示第1，2，3个温度点的完成情况
        public bool[] humcal = new bool[4]{ false, false, false, false };

        public int device_num;          //待测设备数量
        public int overcount;           //超时秒数
        public int steady_count;        //等待温箱稳定计数

        public string Temperature;      //温箱实时温湿度
        public string Humidity;
        public string Set_Temperature;  //温箱设置的温湿度
        public string Set_Humidity;
        public string status;
        public string tempT;            //串口读取的临时温湿度保存字符串
        public string tempH;

        public int temp;
        public double[] standard_temperature = new double[3] { 15.0, 20.0, 30.0};
        public double[] standard_humidity = new double[3] {40.0, 60.0, 80.0 };
        public string[] standard_temperature_str = new string[3] { "15.00", "20.00", "30.00" };
        public string[] standard_humidity_str = new string[3] { "40.00", "60.00", "80.00" };

        public FileStream save;         //excel文件流
        public XSSFWorkbook workbook;   //workbook                    
        public ISheet sheet;            //sheet表
        public IRow row;                //表格某一行
        public ICell cell;              //某一行的某一列

        public double wait_steady_temperature;
        public double wait_steady_humidity;
        public int wait_steady_time_T;
        public int wait_steady_time_H;
    }

    public struct ZOGLAB   //定义数组类的话，每个类都要进行一次实例化似乎
    {
        //读取的温度
        public double[] temperature;
        //读取的湿度
        public double[] humidity ;
        //设备信息
        public string portname;     //串口名
        public string serialnum;     //SN号
        public string calnum;       //计量编号
    }

}
