using FMCG.Utility.RedisCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var list = content.Text.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    Image img = GenerateTwoCodeHelper.Create_ImgCode(list[i], 100);
                    img.Save(cache.Get("SavePath").ToString() + "/" + list[i] + ".jpg", System.Drawing.Imaging.ImageFormat.Png);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace);
            }
        }

        private void 保存路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pathdlg = new FolderBrowserDialog();
            if (pathdlg.ShowDialog() == DialogResult.OK)
            {
                cache.AddPermanent("SavePath",pathdlg.SelectedPath);
            }
        }
    }
}
