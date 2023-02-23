using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WA_CRUD_Data;

namespace WA_CRUD
{
    
    public partial class CategoryList : System.Web.UI.Page
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
                GenerateList();
                
            }
        }

        private void GenerateList()
        {
            pl_List.Visible = true;
            lblNodata.Visible = false;
            var dataList = ct.GetCategoryList();
            if (dataList.Any())
            {
                GVCategory.DataSource = dataList;
                GVCategory.DataBind();
                GVCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                pl_List.Visible = false;
                lblNodata.Visible = true;
                GVCategory.DataSource = "";
                GVCategory.DataBind();
          
            }

        }

        protected void GVCategory_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label idCategory = (Label) e.Row.FindControl("lblId");
                LinkButton editItem = (LinkButton) e.Row.FindControl("lBtnEdit");
                LinkButton deleteItem = (LinkButton)e.Row.FindControl("lBtnDelete");
                if (idCategory.Text != null)
                {
                    editItem.Attributes.Add("href","EditCategory.aspx?id="+idCategory.Text);
                }
                
            }
        }

        protected void lBtnDelete_OnClick(object sender, EventArgs e)
        {
            LinkButton delCategory = (LinkButton) sender;
            GridViewRow gvr = (GridViewRow) delCategory.NamingContainer;
            //try
            //{
                var dateKey = GVCategory.DataKeys[gvr.RowIndex];
                var delId = 0;
                if (dateKey != null && dateKey.Values != null)
                {
                    var keyId = dateKey.Values[0].ToString();
                    if (keyId != null)
                    {
                        delId = Convert.ToInt32(keyId);
                        var categoryLsit = ct.GetCategoryList().Where(s=>s.Id==delId);
                        if (categoryLsit != null)
                        {
                            var listDelete = new List<Categorys>();
                            foreach (var item in categoryLsit)
                            {
                                var listItem = new Categorys();
                                listItem = categoryLsit.First(s => s.Id == item.Id);
                                listDelete.Add(listItem);
                            }
                            var deleteQuery = ct.DeleteCategoryData(listDelete);
                            if (deleteQuery)
                            {
                                Alert("刪除成功", "self.location.href='CategoryList.aspx';");
                            }
                        }
                    }
                }
       
        }

        private void Alert(string strMsg, string jsCode="")
        {
            string script = "alert(\"" + strMsg + "\");" + jsCode;
            Page.Controls.Add(new LiteralControl(("<script>" + script + "</script>")));
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
           if(CheckInput(txtName.Text))
            {
                AddCategory(txtName.Text, txtSimple.Text);
			}
			else { Alert("全名請勿空白"); }
        }

        private void AddCategory(string strName, string strSimple="")
        {
            var queryAdd = ct.AddCategory(new Categorys()
            {
                Name = strName,
                SimpleName = string.IsNullOrEmpty(strSimple)?"":strSimple,
                Status =true
            });
            if (queryAdd)
            {
                Alert("新增成功", "self.location.href='CategoryList.aspx';");
            }
        }

        private bool CheckInput(string strName)
        {
            return !string.IsNullOrEmpty(strName);
        }
    }
}