using FMCG.Utility.RedisCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Utility;
using Utility.Cache;
using cache = Utility.Cache.CacheHelper;
namespace OperateForm
{
    public partial class GenerateTwoCode : Form
    {
        public GenerateTwoCode()
        {
            InitializeComponent();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog();
            //filedlg.Multiselect = true;
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                fileurl.Text = filedlg.FileName;
            }
        }

        private void GenerateTwoCode_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cache.Get("SavePath") == null)
                {
                    MessageBox.Show("请选择保存路径");
                }
                else
                {
                    if (content.Text.Length > 0)
                    {
                        var list = content.Text.Split(',');

                        for (int i = 0; i < list.Length; i++)
                        {
                            if(!string.IsNullOrWhiteSpace(list[i]))
                            {
                                Image img = GenerateTwoCodeHelper.Create_ImgCode(list[i], 100);
                                img.Save(cache.Get("SavePath").ToString() + "/" + HttpUtility.UrlEncode(list[i]) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                        MessageBox.Show("生成完毕");
                    }
                    else
                    {
                        MessageBox.Show("请填写输出内容");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void 保存路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pathdlg = new FolderBrowserDialog();
            if (pathdlg.ShowDialog() == DialogResult.OK)
            {
                cache.AddPermanent("SavePath", pathdlg.SelectedPath);
            }
        }
        //全选
        private void content_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (e.KeyChar == (char)1)       // Ctrl-A 相当于输入了AscII=1的控制字符
            {
                textBox.SelectAll();
                e.Handled = true;      // 不再发出“噔”的声音
            }
        }
    }
}
