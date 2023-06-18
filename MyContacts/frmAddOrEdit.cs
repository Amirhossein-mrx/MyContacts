using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class frmAddOrEdit : Form
    {
        Contact_DBEntities db = new Contact_DBEntities();
        public int contactId = 0;
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            if(contactId==0)
            {
                this.Text = "افزودن شخص جدید";
            }
            else
            {
                this.Text = "ویرایش اطلاعات شخص";

                //DataTable=یک جدول یک سطری است که شامل سطری است که سرچ شده
                var dt = db.MyContacts.Find(contactId);      
                txtName.Text = dt.Name;
                txtFamily.Text = dt.Family;
                txtAge.Text = dt.Age.ToString();
                txtEmail.Text = dt.Email;
                txtMobile.Text = dt.Mobile;
                txtAddres.Text = dt.Address;
                btnSubmit.Text = "ویرایش";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(ValidateInputs())
            {
                if (contactId==0)
                {
                    MyContact mycontact = new MyContact();
                    mycontact.Name = txtName.Text;
                    mycontact.Family = txtFamily.Text;
                    mycontact.Age = (int)txtAge.Value;
                    mycontact.Email = txtEmail.Text;
                    mycontact.Address = txtAddres.Text;
                    mycontact.Mobile = txtMobile.Text;
                    db.MyContacts.Add(mycontact);
                }
                else
                {
                    var mycontact = db.MyContacts.Find(contactId);
                    mycontact.Name = txtName.Text;
                    mycontact.Family = txtFamily.Text;
                    mycontact.Age = (int)txtAge.Value;
                    mycontact.Email = txtEmail.Text;
                    mycontact.Address = txtAddres.Text;
                    mycontact.Mobile = txtMobile.Text;
                }
                db.SaveChanges();

                MessageBox.Show("عملیات با موفقیت انجام شد ", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
              
                
                
            }
        }

        bool ValidateInputs()
        {
           if(txtName.Text=="")
            {
                MessageBox.Show("لطفا نام را وارد کنید", "هشتدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFamily.Text == "")
            {
                MessageBox.Show("لطفا نام خانوادگی را وارد کنید", "هشتدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           
            if (txtMobile.Text == "")
            {
                MessageBox.Show("لطفا شماره تماس را وارد کنید", "هشتدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtAge.Value == 0)
            {
                MessageBox.Show("لطفا سن را وارد کنید", "هشتدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("لطفا ایمیل را وارد کنید", "هشتدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }
    }
}
