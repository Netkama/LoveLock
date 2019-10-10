using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TestTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            Writelog("感谢使用：" + DateTime.Now);
            MessageBox.Show("您已开启间隔60分钟自动锁屏功能，锁屏时间为3分钟。工作再忙，也要注意休息哦~");
            string strPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] files = Directory.GetFiles(@strPath + "imgs", "*");
            pictureBox1.Image = Image.FromFile(files[0]);
            timer1.Start();
            SelfStarting selfStarting = new SelfStarting();
            selfStarting.SetMeAutoStart();

        }




        /// <summary>
        /// 随机生成背景图片
        /// </summary>
        private void RandomBackgroundImage()
        {
            string strPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] files = Directory.GetFiles(@strPath + "imgs", "*");
            Random rd = new Random();
            int i = rd.Next(files.Length);
            ImgsEffect imgsEffect = new ImgsEffect();
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(Image.FromFile(files[i]));
            //imgsEffect.Effect_BaiYeH(bmp1, bmp2, pictureBox1);
            // imgsEffect.DanRu( bmp2, pictureBox1);
            //imgsEffect.Effect_U2D(bmp1, bmp2, pictureBox1);
            //imgsEffect.Effect_D2U(bmp1, bmp2, pictureBox1);
            imgsEffect.Effect_L2R(bmp1, bmp2, pictureBox1);
            pictureBox1.Image = bmp2;
            //imgsEffect.Effect_R2L(bmp1, bmp2, pictureBox1);
            //this.BackgroundImage = Image.FromFile(files[i]);
        }


        //屏蔽任务管理器及显示隐藏逻辑
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (DateTime.Now.Minute % 60 > 3)
                //if (DateTime.Now.Second % 60 < 30)
                {
                    this.Hide(); Hook_Clear();
                }
                else
                {
                    if (DateTime.Now.Second % 15 == 0)
                    {
                        //MessageBox.Show((DateTime.Now.Second % 10)  +"秒 时间刷新图片");
                        RandomBackgroundImage();
                    }
                    this.Show();
                    Hook_Start();
                    System.Diagnostics.Process[] killprocess = System.Diagnostics.Process.GetProcessesByName("taskmgr");
                    foreach (System.Diagnostics.Process p in killprocess)
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Writelog("异常" + ex.ToString());
            }
        }
        private void killProcess(string name)
        {
            //name为要杀的程序进程名称
            Process[] prc = Process.GetProcesses();
            foreach (Process pr in prc) //遍历整个进程
            {
                if (name == pr.ProcessName)  //如果进程存在
                {
                    pr.Kill();
                    pr.Dispose();
                }
            }
        }


        #region 屏蔽组合键的钩子
        //委托 
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        static int hHook = 0;
        public const int WH_KEYBOARD_LL = 13;

        //LowLevel键盘截获，如果是WH_KEYBOARD＝2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。 
        HookProc KeyBoardHookProcedure;

        //键盘Hook结构函数 
        [StructLayout(LayoutKind.Sequential)]
        public class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        //设置钩子 
        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //抽掉钩子 
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll")]
        //调用下一个钩子 
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public void Hook_Start()
        {
            // 安装键盘钩子 
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);

                //hHook = SetWindowsHookEx(2, 
                //            KeyBoardHookProcedure, 
                //          GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), GetCurrentThreadId()); 

                hHook = SetWindowsHookEx(WH_KEYBOARD_LL,
                          KeyBoardHookProcedure,
                        GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

                //如果设置钩子失败. 
                if (hHook == 0)
                {
                    Hook_Clear();
                    //throw new Exception("设置Hook失败!"); 
                }
            }
        }

        //取消钩子事件 
        public void Hook_Clear()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
            //如果去掉钩子失败. 
            if (!retKeyboard) throw new Exception("UnhookWindowsHookEx failed.");
        }

        //这里可以添加自己想要的信息处理 
        public static int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        {


            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                if (kbh.vkCode == 91)  // 截获左win(开始菜单键) 
                {
                    return 1;
                }
                if (kbh.vkCode == 92)// 截获右win 
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control) //截获Ctrl+Esc 
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.F4 && (int)Control.ModifierKeys == (int)Keys.Alt)  //截获alt+f4 
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Tab && (int)Control.ModifierKeys == (int)Keys.Alt) //截获alt+tab 
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Shift) //截获Ctrl+Shift+Esc 
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Alt)  //截获alt+空格 
                {
                    return 1;
                }
                if (kbh.vkCode == 241)                  //截获F1 
                {
                    return 1;
                }
                if ((int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt + (int)Keys.Delete)      //截获Ctrl+Alt+Delete 
                {
                    return 1;
                }
                //if ((int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Shift)      //截获Ctrl+Shift 
                //{ 
                //    return 1; 
                //} 

                //if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)  //截获Ctrl+Alt+空格 
                //{ 
                //    return 1; 
                //} 
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        #endregion
        //end


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Writelog(string msg)
        {
            StreamWriter stream;
            //写入日志内容
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //检查上传的物理路径是否存在，不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            stream = new StreamWriter(path + "\\log.txt", true, Encoding.Default);
            stream.Write(DateTime.Now.ToString() + ":" + msg);
            stream.Write("\r\n");
            stream.Flush();
            stream.Close();
        }
    }
}
