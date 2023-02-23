using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WA_CRUD_Data;

namespace WA_CRUD
{
    public partial class EditCategory : System.Web.UI.Page
    {
        CategoryModel ct = new CategoryModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void InitForm()
        {
            if (!IsPostBack)
            {
                var _cId = 0;
                var cId = string.IsNullOrEmpty(Request["id"]) ? _cId : int.TryParse(Request["id"], out _cId) ?
                                  int.Parse(Request["id"]) : _cId;
                if (cId != 0)
                {
                    var cData = ct.GetCategoryById(cId);
                    if (cData != null)
                    {
                        txtName.Text = cData.Name;
                        txtSimple.Text = cData.SimpleName;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (CheckInput(txtName.Text))
            {
                UpdateCategory(txtName.Text, txtSimple.Text);
            }

        }

        private void UpdateCategory(string updateName, string updateSimple)
        {
            var _cId = 0;
            var cId = string.IsNullOrEmpty(Request["id"]) ? _cId : int.TryParse(Request["id"], out _cId) ?
                              int.Parse(Request["id"]) : _cId;

            if (cId != 0)
            {
                var inputName = txtName.Text;
                var inputSimpleName = txtSimple.Text;
                if (ct.GetCategoryList().Any(s => s.Name == inputName && s.Id != cId))
                {
                    Alert("已有重複全名，請修正");
                }
                else
                {
                    var updateItem = ct.UpdateItemById(new Categorys()
                    {
                        Id = cId,
                        Name = inputName,
                        SimpleName = inputSimpleName
                    });
                    if (updateItem)
                    {
                        Alert("儲存成功", "window.location.href='CategoryList.aspx';");
                    }
                    else
                    {
                        Alert("儲存失敗");
                    }
                }


            }
        }

        private void Alert(string strMsg, string jsCode = "")
        {
            string script = "alert(\"" + strMsg + "\");" + jsCode;
            Page.Controls.Add(new LiteralControl(("<script>" + script + "</script>")));
        }

        private bool CheckInput(string strName)
        {
            return !string.IsNullOrEmpty(strName);
        }

		protected void BtnCanel_Click(object sender, EventArgs e)
		{
            Response.Redirect("~/CategoryList.aspx");
		}
	}
}