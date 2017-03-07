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

            GenerateService service = new GenerateService(sourceFile);
            service.Running();

            Console.ReadKey();
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

            if (dialogResult == DialogResult.OK)
            {
                path = fileDialog.FileName;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                Thread.CurrentThread.Abort();
            }

            return path;
        }
    }
}
