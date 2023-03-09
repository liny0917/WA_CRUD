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
        CategoryModel model = new CategoryModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void InitForm()
        {
            if (!IsPostBack)
            {
                var _id = 0;
                var id = string.IsNullOrEmpty(Request["id"]) ? _id : int.TryParse(Request["id"], out _id) ?
                                  int.Parse(Request["id"]) : _id;
                if (_id != 0)
                {
                    var cData = model.GetCategoryById(id);
                    if (cData != null)
                    {
                        txtName.Text = cData.Name;
                        txtSimple.Text = cData.SimpleName;
                    }
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            if (CheckInput())
            {
                UpdateCategory();
            }

        }

        private void UpdateCategory()
        {
            var _id = 0;
            var id = string.IsNullOrEmpty(Request["id"]) ? _id : int.TryParse(Request["id"], out _id) ?
                              int.Parse(Request["id"]) : _id;

            if (id != 0)
            {
				var inputName = txtName.Text;
				var inputSimpleName = txtSimple.Text;
				if (model.GetCategoryList().Any(s => s.Name.ToUpper() == inputName.ToUpper() && s.Id != id))
                {
                    Alert("已有重複全名，請修正");
                }
                else
                {
                    var updateItem = model.UpdateItemById(new Categories()
                    {
                        Id = id,
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

        private bool CheckInput()
        {
            return !string.IsNullOrEmpty(txtName.Text.Trim());
        }

		protected void BtnCanel_Click(object sender, EventArgs e)
		{
            Response.Redirect("~/CategoryList.aspx");
		}
	}
}