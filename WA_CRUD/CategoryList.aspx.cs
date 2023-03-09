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
		CategoryModel model = new CategoryModel();
		//Alert alert = new Alert();
		// MessageAlertModel msg = new MessageAlertModel();
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
			//var model = new CategoryModel();
			pl_List.Visible = true;
			lblNodata.Visible = false;
			//model.
			var dataList = model.GetCategoryList();
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
				Label idCategory = (Label)e.Row.FindControl("lblId");
				LinkButton editItem = (LinkButton)e.Row.FindControl("lBtnEdit");
				//LinkButton deleteItem = (LinkButton)e.Row.FindControl("lBtnDelete");
				if (idCategory.Text != null)
				{
					editItem.Attributes.Add("href", "EditCategory.aspx?id=" + idCategory.Text);
				}

			}
		}

		protected void LBtnDelete_OnClick(object sender, EventArgs e)
		{
			LinkButton delCategory = (LinkButton)sender;
			GridViewRow gvr = (GridViewRow)delCategory.NamingContainer;
			//try
			//{
			var dateKey = GVCategory.DataKeys[gvr.RowIndex];
			var deleteId = 0;
			if (dateKey != null && dateKey.Values != null)
			{
				var keyId = dateKey.Values[0].ToString();
				if (keyId != null)
				{
					deleteId = Convert.ToInt32(keyId);
					var categoryLsit = model.GetCategoryList().Where(s => s.Id == deleteId);
					if (categoryLsit != null)
					{
						var listDelete = new List<Categories>();
						foreach (var item in categoryLsit)
						{
							var listItem = new Categories();
							listItem = categoryLsit.First(s => s.Id == item.Id);
							listDelete.Add(listItem);
						}
						var deleteQuery = model.DeleteCategoryData(listDelete);
						if (deleteQuery)
						{
							//this.Controls.Add(alert.Msg, alert.JsCode);

							//alert.Msg = "刪除成功";//;
							//alert.JsCode = "self.location.href='CategoryList.aspx';";
							Alert("刪除成功", "self.location.href='CategoryList.aspx';");
						}
					}
				}
			}

		}

		private void Alert(string msg, string jsCode = "")
		{
			string script = "alert(\"" + msg + "\");" + jsCode;
			Page.Controls.Add(new LiteralControl(("<script>" + script + "</script>")));
		}

		protected void BtnAdd_OnClick(object sender, EventArgs e)
		{
			if (CheckInput())
			{
				AddCategory();
			}
			else { Alert("全名請勿空白"); }
		}

		private void AddCategory()
		{
			var queryAdd = model.AddCategory(new Categories()
			{
				Name = txtName.Text.Trim(),
				SimpleName = string.IsNullOrEmpty(txtSimple.Text.Trim()) ? "" : txtSimple.Text.Trim(),
				Status = true
			});
			if (queryAdd)
			{
				Alert("新增成功", "self.location.href='CategoryList.aspx';");
			}
		}

		private bool CheckInput()
		{
			return !string.IsNullOrEmpty(txtName.Text.Trim());
		}
	}
}