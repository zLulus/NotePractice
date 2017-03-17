using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.AngularjsStudy;
using Newtonsoft.Json;

namespace NotePractice.Views.AngularjsStudy
{
    public class AngularjsStudyController : Controller
    {
        public ActionResult Calculate()
        {
            return View();
        }

        public ActionResult ControllerDemo()
        {
            return View();
        }

        public ActionResult RepeatAndRootScope()
        {
            return View();
        }

        public ActionResult Validation()
        {
            return View();
        }

        public ActionResult InvalidData()
        {
            return View();
        }

        public ActionResult CurrencyFilter()
        {
            return View();
        }

        public ActionResult OrderByFilter()
        {
            return View();
        }

        public ActionResult ServiceDemo()
        {
            return View();
        }

        public ActionResult DependencyInjection()
        {
            return View();
        }

        #region Binding
        public ActionResult Binding()
        {
            return View();
        }

        public ActionResult GetResource()
        {
            return Json(new AResource()
            {
                productName = "资源名称",
                model= "型号",
                standard= "执行标准",
                uploadTime=DateTime.Now,
                resourceCode= "资源编号",
                specification= "规格",
                drawingno= "图号",
                resourceLocation= "资源位置",
                technicalParameters= "技术参数",
                material= "材质",
                singleWeight=123,
                descriptionRemark="描述"
            });
        }

        public ActionResult GetFirstSort()
        {
            List<FirstSort> firstSorts = new List<FirstSort>();
            firstSorts.Add(new FirstSort() {id=1,displayName= "绑定分类1" });
            firstSorts.Add(new FirstSort() { id = 1, displayName = "绑定分类2" });
            firstSorts.Add(new FirstSort() { id = 1, displayName = "绑定分类3" });
            firstSorts.Add(new FirstSort() { id = 1, displayName = "绑定分类4" });
            firstSorts.Add(new FirstSort() { id = 1, displayName = "绑定分类5" });
            return Json(firstSorts);
        }

        public ActionResult GetProvinceSort()
        {
            List<District> districts = new List<District>();
            districts.Add(new District() {id=1,fatherID=0,name="湖南省" });
            districts.Add(new District() { id =2, fatherID = 0, name = "湖北省" });
            districts.Add(new District() { id =3, fatherID = 0, name = "四川省" });
            return Json(districts);
        }

        public ActionResult GetChildrenSort(int fatherID)
        {
            List<District> districts = new List<District>();
            switch (fatherID)
            {
                case 1:
                    districts.Add(new District() { id = 4, fatherID = 1, name = "长沙市" });
                    districts.Add(new District() { id = 5, fatherID = 1, name = "岳阳市" });
                    districts.Add(new District() { id = 6, fatherID = 1, name = "株洲市" });
                    return Json(districts);
                case 2:
                    districts.Add(new District() { id = 7, fatherID = 2, name = "武汉市" });
                    districts.Add(new District() { id = 8, fatherID = 2, name = "宜昌市" });
                    return Json(districts);
                case 3:
                    districts.Add(new District() { id = 9, fatherID = 3, name = "成都市" });
                    districts.Add(new District() { id = 10, fatherID = 3, name = "遂宁市" });
                    districts.Add(new District() { id = 11, fatherID = 3, name = "巴中市" });
                    districts.Add(new District() { id = 12, fatherID = 3, name = "绵阳市" });
                    districts.Add(new District() { id = 13, fatherID = 3, name = "南充市" });
                    return Json(districts);
                default:
                    districts.Add(new District() { id = 14, fatherID = -1, name = "不知道你选了什么∑q|ﾟДﾟ|p" });
                    return Json(districts);
            }
        }

        public ActionResult SaveForm(AEditResource editResource)
        {
            return Json(JsonConvert.SerializeObject(editResource));
        }
        #endregion
    }
}