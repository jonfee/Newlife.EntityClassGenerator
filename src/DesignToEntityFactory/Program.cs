using System;
using System.Threading;
using System.Windows.Forms;

namespace DesignToEntityFactory
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //源文件
            string sourceFile = SetSoure();

            //定义一个生成服务对象
            GenerateService service = new GenerateService(sourceFile);
            //运行服务
            service.Running();
            
            Console.WriteLine("按任意键退出！");
            Console.ReadKey();  //达到程序等待的目的

            //退出程序
            Exit();
        }

        /// <summary>
        /// 设置文件来源路径
        /// </summary>
        static string SetSoure()
        {
            Console.WriteLine("请选择源文件：");

            string path = string.Empty;

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "HTML文件(*.html;*.htm)|*.html;*.htm|文本文件(*.txt)|*.txt";
            var dialogResult = fileDialog.ShowDialog();

            //根据用户操作结果处理
            //  1、选择文件后点击“确定”，则接收文件址
            //  2、用户点击“取消”，则退出程序
            if (dialogResult == DialogResult.OK)
            {
                path = fileDialog.FileName;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                //退出程序
                Exit();
            }

            return path;
        }

        /// <summary>
        /// 退出程序
        /// 中止线程
        /// </summary>
        static void Exit()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
