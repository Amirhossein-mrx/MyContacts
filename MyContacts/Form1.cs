using System;

using System.Data;

using System.Linq;

using System.Windows.Forms;

namespace MyContacts
{
    public partial class Form1 : Form
    {
        


        public Form1()
        {
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGride();
        }

        private void BindGride()
        {
            using (Contact_DBEntities db = new Contact_DBEntities())
            {
                dgContacts.AutoGenerateColumns = false;
                dgContacts.DataSource = db.MyContacts.ToList();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGride();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAddOrEdit frm = new frmAddOrEdit();
            frm.ShowDialog();
            if(frm.DialogResult== DialogResult.OK)
            {
                BindGride();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEdit frm = new frmAddOrEdit();
                frm.contactId = contactId;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    BindGride();
                }
            } 
           
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            if(dgContacts.CurrentRow!=null)
            {
                string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string fullname = name +" "+ family;
                if (MessageBox.Show($"آیا از حذف کاربر {fullname}  مطمئین هستید","قابل توجه",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    int contactid = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                    using (Contact_DBEntities db = new Contact_DBEntities())
                    {
                        var countact = db.MyContacts.Single(p => p.ContactID == contactid);
                        db.MyContacts.Remove(countact); 
                    }
                    
                    BindGride();   
                }
            }
            else
            {
                MessageBox.Show("لطفا یک شخص را از لیست انتخاب کنید", "ارور", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (Contact_DBEntities db = new Contact_DBEntities())
            {
                dgContacts.DataSource = db.MyContacts.Where(p => p.Name.Contains(txtSearch.Text) || p.Family.Contains(txtSearch.Text)).ToList();
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
