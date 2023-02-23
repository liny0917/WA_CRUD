using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WA_CRUD_Data
{
    public class CategoryModel
    {
        /// <summary>
        /// 取得所有類別
        /// </summary>
        /// <returns></returns>
        public List<Categorys> GetCategoryList()
        {
            List<Categorys> listData = new List<Categorys>();
            try
            {
                using (var context = new CategoryDataDataContext())
                {
                    var data = context.Categorys.AsEnumerable();//AsQueryable();
                    if(data != null)
                    {
                        data=data.Where(s=>s.Status).ToList();
                        listData.AddRange(data);
                    }
               
                   // return listData.to
                    //if (data.Any())
                    //{
                    //    listData = data.Where(s=>s.Status.Equals(true)).ToList();
                    //}
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
        public Categorys GetCategoryById(int cId)
        {
            Categorys cyData = null;
            if (cId <= 0)
            {
                throw new NullReferenceException("無可取得類別之條件");
            }
            try
            {
                using (var context= new CategoryDataDataContext())
                {
                    cyData = context.Categorys.Where(s => s.Id== cId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return cyData;
        }

        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="categoryItem"></param>
        /// <returns></returns>
        public bool AddCategory(Categorys categoryItem)
        {
            bool result = false;
            if (categoryItem == null) throw new NullReferenceException("無可供新增的項目");
            try
            {
                using (var context= new CategoryDataDataContext())
                {
                    if (context.Categorys.Where(s => s.Name.ToUpper() == categoryItem.Name.ToUpper()&&s.Status).Any())
                    {
                        throw new Exception("已有此類別，無法新增");
                    }
                    context.Categorys.InsertOnSubmit(categoryItem);
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
        public bool UpdateItemById(Categorys updateItem)
        {
            bool updateResult = false;
            if (updateItem == null) throw new NullReferenceException("查無可更新的項目");
            try
            {
                using (var context= new CategoryDataDataContext())
                {
                    var oriItem = context.Categorys.Where(s => s.Id == updateItem.Id);
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
        public bool DeleteCategoryData(List<Categorys> deleteList)
        {
            var deleteResult = true;
            try
            {
                using (var context=new CategoryDataDataContext())
                {
                    var deleteData = new List<Categorys>();
                    foreach (var item in deleteList)
                    {
                        //var deleteItem = new Categorys();
                        var deleteItem = context.Categorys.First(s => s.Id == item.Id);
                        deleteData.Add(deleteItem);
                    }
                    context.Categorys.DeleteAllOnSubmit(deleteData);
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






    //public  class Category:Categorys
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string SimpleName { get; set; }
    //    public string IsActive { get; set; }
    //    public List<Category> Categories { get; set; } 
    //}
}
