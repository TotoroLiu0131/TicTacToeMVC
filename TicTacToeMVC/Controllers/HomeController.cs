using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicTacToeMVC.Models;

namespace TicTacToeMVC.Controllers
{
    public class HomeController : Controller
    {
        private static List<string> _selectedDropDownList = new List<string>();

        private static Dictionary<string, string> _selectedViewLookup = new Dictionary<string, string>();

        private List<List<int>> _winningOptions = new List<List<int>>
        {
            new List<int> {0,1,2},
            new List<int> {3,4,5},
            new List<int> {6,7,8},
            new List<int> {0,3,6},
            new List<int> {1,4,7},
            new List<int> {2,5,8},
            new List<int> {0,4,8},
            new List<int> {2,4,6}
        };

        private List<string> _dropDownList = new List<string>()
        {
            "TopLeft",
            "TopCenter",
            "TopRight",
            "MiddleLeft",
            "MiddleCenter",
            "MiddleRight",
            "BottomLeft",
            "BottomCenter",
            "BottomRight",
        };

        private string _result = string.Empty;

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

        private bool _submitEnable = true;

        public ActionResult Index()
        {
            _selectedDropDownList = _dropDownList;
            _selectedViewLookup = _viewLookup;
            return View("Index", GetViewModel());
        }

        [HttpPost]
        public ActionResult Input(TicTacToeViewModel model)
        {
            SetPlayerUi(model.Selected);

            if (CheckWinner())
                return View("Index", GetViewModel());

            if (_selectedDropDownList.Count == 0)
            {
                _submitEnable = false;
                _result = "Tie";
                return View("Index", GetViewModel());
            }

            SetAIUi();

            CheckWinner();

            return View("Index", GetViewModel());
        }

        private static void SetAIUi()
        {
            var value = _selectedDropDownList[new Random().Next(0, _selectedDropDownList.Count)];
            _selectedDropDownList.Remove(value);
            _selectedViewLookup[value] = "X";
        }

        private void SetPlayerUi(string selected)
        {
            _selectedDropDownList.Remove(selected);
            _selectedViewLookup[selected] = "O";
        }

        private bool CheckWinner()
        {
            foreach (var list in _winningOptions)
            {
                if (_selectedViewLookup.ElementAt(list[0]).Value == string.Empty ||
                    _selectedViewLookup.ElementAt(list[1]).Value == string.Empty ||
                    _selectedViewLookup.ElementAt(list[2]).Value == string.Empty)
                    continue;

                if (_selectedViewLookup.ElementAt(list[0]).Value == _selectedViewLookup.ElementAt(list[1]).Value &&
                    _selectedViewLookup.ElementAt(list[1]).Value == _selectedViewLookup.ElementAt(list[2]).Value)
                {
                    _result = _selectedViewLookup.ElementAt(list[0]).Value == "O" ? "You Win" : "You Lose";
                    _submitEnable = false;
                    return true;
                }
            }

            return false;
        }

        private List<SelectListItem> GetDropDownList()
        {
            var dropItems = new List<SelectListItem>();
            foreach (var value in _selectedDropDownList)
            {
                dropItems.Add(new SelectListItem()
                {
                    Text = value,
                    Value = value,
                });
            }
            return dropItems;
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
                SubmitEnable = _submitEnable,
                Result = _result
            };
        }
    }
}