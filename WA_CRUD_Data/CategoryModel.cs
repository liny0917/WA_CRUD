using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace WA_CRUD_Data
{
    public class CategoryModel
    {        
        /// <summary>
        /// 取得所有類別
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetCategoryList()
        {
            List<Categories> listData = new List<Categories>();
            try
            {
                using (var context = new CategoryDataDataContext())
                {
                    var data = context.Categories.AsEnumerable();//AsQueryable();
                    if(data != null)
                    {
                        data=data.Where(s=>s.Status).ToList();
                        listData.Add((Categories)data);
                    }
               
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return listData;
        }


        /// <summary>
        /// 依照類別Id取得類別資料
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        public Categories GetCategoryById(int id)
        {
            Categories categoryData = null;
            if (id <= 0)
            {
                throw new NullReferenceException("無可取得類別之條件");
            }
            try
            {
                using (var context= new CategoryDataDataContext())
                {
                    categoryData = context.Categories.Where(s => s.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return categoryData;
        }

        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="categoryItem"></param>
        /// <returns></returns>
        public bool AddCategory(Categories categoryItem)
        {
            bool result = false;
            if (categoryItem == null) throw new NullReferenceException("無可供新增的項目");
            try
            {
                using (var context= new CategoryDataDataContext())
                {
                    if (context.Categories.Where(s => s.Name.ToUpper() == categoryItem.Name.ToUpper()&&s.Status).Any())
                    {
                        throw new Exception("已有此類別，無法新增");
                    }
                    context.Categories.InsertOnSubmit(categoryItem);
                    context.SubmitChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 編輯類別
        /// </summary>
        /// <param name="updateItem"></param>
        /// <returns></returns>
        public bool UpdateItemById(Categories updateItem)
        {
            bool updateResult = false;
            if (updateItem == null) throw new NullReferenceException("查無可更新的項目");
            try
            {
                using (var context= new CategoryDataDataContext())
                {
                    var oriItem = context.Categories.Where(s => s.Id == updateItem.Id);
                    if (oriItem.Any())
                    {
                        oriItem.First().Name = updateItem.Name;
                        oriItem.First().SimpleName = updateItem.SimpleName;
                        //oriItem.First().PMCACTI = updateItem.PMCACTI;
                        context.SubmitChanges();

                        updateResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return updateResult;
        }


        /// <summary>
        /// 刪除類別
        /// </summary>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        public bool DeleteCategoryData(List<Categories> deleteList)
        {
            var deleteResult = true;
            try
            {
                using (var context=new CategoryDataDataContext())
                {
                    var deleteData = new List<Categories>();
                    foreach (var item in deleteList)
                    {
                        //var deleteItem = new Categorys();
                        var deleteItem = context.Categories.First(s => s.Id == item.Id);
                        deleteData.Add(deleteItem);
                    }
                    context.Categories.DeleteAllOnSubmit(deleteData);
                    context.SubmitChanges();
      
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return deleteResult;
        }
    }

	//public class Alert
	//{
	//	public string Msg { get; set; }
	//	public string JsCode { get; set; }
	//	//public object Controls { get; private set; }

	//	public Alert()
	//	{
 //           string script = "alert(\"" + Msg + "\");" + JsCode;
 //           new LiteralControl(("<script>" + script + "</script>"));
 //         //  this.Controls.Add(new LiteralControl(("<script>" + script + "</script>")))
 //       }

	//	//public void Alert() { }
	//}
 
   
}
