using System;
using System.Windows.Forms;
using static METH.meth;
using System.Threading;

public struct program
{

    public string n{get;set;}
    public string v{get;set;}
    public string p{get;set;}
    public string u{get;set; }
    public program(string n,string v,string p,string u)
    {
        this.n = n;
        this.v = v;
        this.p = p;
        this.u = u;
    }
    
}

namespace UI
{
    public partial class mainUI : Form
    {
        public mainUI()
        {
            InitializeComponent();
        }
        public void Setting(ListView listView1)
        {
            listView1.View = View.Details;
            listView1.CheckBoxes = true;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns[0].Width = 50;
            listView1.Columns[1].Width = 150;
            listView1.Columns[2].Width = -2;
            listView1.Columns[3].Width = -2;
            listView1.Columns[4].Width = 300;
        }
        private void SizeColumn(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }
        private void mainUI_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            SizeColumn(listView1);
            listpgr();
            Setting(listView1);
            try
            {
                int i = 0;
                foreach (program pro in plist)
                {
                    
                    i++;
                    string[] str = {i.ToString(), pro.n, pro.v, pro.p, pro.u };
                    ListViewItem obj = new ListViewItem(str);
                    listView1.Items.Add(obj);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uninstall()
        {
            try
            {
                foreach (ListViewItem item in this.listView1.CheckedItems)
                {
                    var s = item.SubItems[4].Text;
                    func(s);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            uninstall();
            MessageBox.Show("The program will now restart", "NOTICE");
            Thread.Sleep(3000);
            Application.Restart();
            Environment.Exit(1);
        }
        private void listView1_Resize(object sender, EventArgs e)
        {
            SizeColumn((ListView) sender);
        }
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            button1.Enabled = listView1.CheckedItems.Count > 0;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("K15DCPM04\n2018110007", "Made by");
        }

        private void reloadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(1);
        }
    }
}
