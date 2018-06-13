using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicTacToeMVC.Models;
using WebGrease.Css.Extensions;

namespace TicTacToeMVC.Controllers
{
    public class HomeController : Controller
    {
        private Dictionary<string, int> _dropDownLookup = new Dictionary<string, int>()
        {
            {"TopLeft",0},
            {"TopCenter",1},
            {"TopRight",2},
            {"MiddleLeft",3},
            {"MiddleCenter",4},
            {"MiddleRight",5},
            {"BottomLeft",6},
            {"BottomCenter",7},
            {"BottomRight",8},
        };

        private Dictionary<string, string> _viewLookup = new Dictionary<string, string>()
        {
            {"TopLeft", string.Empty},
            {"TopCenter", string.Empty},
            {"TopRight", string.Empty},
            {"MiddleLeft", string.Empty},
            {"MiddleCenter", string.Empty},
            {"MiddleRight", string.Empty},
            {"BottomLeft", string.Empty},
            {"BottomCenter", string.Empty},
            {"BottomRight", string.Empty},
        };

        private static int[,] _winningOptions = {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };

        private static Dictionary<string, int> _selectedDropDownLookup = new Dictionary<string, int>();
        private static Dictionary<string, string> _selectedViewLookup = new Dictionary<string, string>();
        private string _result = string.Empty;

        public ActionResult Index()
        {
            _selectedDropDownLookup = _dropDownLookup;
            _selectedViewLookup = _viewLookup;
            return View("Index", GetViewModel());
        }

        [HttpPost]
        public ActionResult Input(TicTacToeViewModel model)
        {
            
            _selectedDropDownLookup.Remove(model.Selected);
            _selectedViewLookup[model.Selected] = "O";

            if (_selectedDropDownLookup.Count > 1)
            {
                var list = new List<int>();
                _selectedDropDownLookup.ForEach(x => list.Add(x.Value));
                var rand = new Random();
                var value = list[rand.Next(0, list.Count)];
                var key = _selectedDropDownLookup.FirstOrDefault(x => x.Value == value).Key;
                _selectedDropDownLookup.Remove(key);
                _selectedViewLookup[key] = "X";
            }
            else
            {
                //for (int i = 0; i < _winningOptions.Length; i++)
                //{
                //    if (_selectedViewLookup)
                //    {
                        
                //    }
                //}

                //if (_selectedViewLookup["TopLeft"] == _selectedViewLookup["TopCenter"] &&
                //    _selectedViewLookup["TopCenter"] == _selectedViewLookup["TopRight"])
                //{
                //    _result = _selectedViewLookup["TopCenter"] == "O" ? "You Win" : "You Lose";
                //}
                //else if (_selectedViewLookup["MiddleLeft"] == _selectedViewLookup["MiddleCenter"] &&
                //         _selectedViewLookup["MiddleCenter"] == _selectedViewLookup["MiddleRight"])
                //{
                //    _result = _selectedViewLookup["MiddleLeft"] == "O" ? "You Win" : "You Lose";
                //}
                //else if (_selectedViewLookup["BottomLeft"] == _selectedViewLookup["BottomCenter"] &&
                //         _selectedViewLookup["BottomCenter"] == _selectedViewLookup["BottomRight"])
                //{
                    
                //}
                //if ( ||
                //    (_selectedViewLookup["BottomLeft"] == _selectedViewLookup["BottomCenter"] &&
                //     _selectedViewLookup["BottomCenter"] == _selectedViewLookup["BottomRight"]) ||
                //    (_selectedViewLookup["TopLeft"] == _selectedViewLookup["MiddleLeft"] &&
                //     _selectedViewLookup["MiddleLeft"] == _selectedViewLookup["BottomLeft"]) ||
                //    (_selectedViewLookup["TopCenter"] == _selectedViewLookup["MiddleCenter"] &&
                //     _selectedViewLookup["MiddleCenter"] == _selectedViewLookup["BottomCenter"]) ||
                //    (_selectedViewLookup["TopRight"] == _selectedViewLookup["MiddleRight"] &&
                //     _selectedViewLookup["MiddleRight"] == _selectedViewLookup["BottomRight"]) ||
                //    (_selectedViewLookup["TopLeft"] == _selectedViewLookup["MiddleCenter"] &&
                //     _selectedViewLookup["MiddleCenter"] == _selectedViewLookup["BottomRight"]) ||
                //    (_selectedViewLookup["TopRight"] == _selectedViewLookup["MiddleCenter"] &&
                //     _selectedViewLookup["MiddleCenter"] == _selectedViewLookup["BottomLeft"]) 
                //    )
                //{
                //    _result = "";
                //}
                //else
                //{
                //    _result = "Tie";
                //}
            }

            return View("Index", GetViewModel());
        }

        private TicTacToeViewModel GetViewModel()
        {
            return new TicTacToeViewModel()
            {
                TopLeft = _selectedViewLookup["TopLeft"],
                TopCenter = _selectedViewLookup["TopCenter"],
                TopRight = _selectedViewLookup["TopRight"],
                MiddleLeft = _selectedViewLookup["MiddleLeft"],
                MiddleCenter = _selectedViewLookup["MiddleCenter"],
                MiddleRight = _selectedViewLookup["MiddleRight"],
                BottomLeft = _selectedViewLookup["BottomLeft"],
                BottomCenter = _selectedViewLookup["BottomCenter"],
                BottomRight = _selectedViewLookup["BottomRight"],
                PositionList = GetDropDownList(),
                Result = _result
            };
        }

        private List<SelectListItem> GetDropDownList()
        {
            var dropItems = new List<SelectListItem>();
            foreach (var i in _selectedDropDownLookup)
            {
                dropItems.Add(new SelectListItem()
                {
                    Text = i.Key,
                    Value = i.Key,
                });
            }
            return dropItems;
        }
    }
}