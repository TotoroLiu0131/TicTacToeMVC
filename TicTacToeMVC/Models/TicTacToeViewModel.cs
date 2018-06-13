using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToeMVC.Enum;

namespace TicTacToeMVC.Models
{
    public class TicTacToeViewModel
    {
        public string TopLeft { get; set; } 
        public string TopCenter { get; set; } 
        public string TopRight { get; set; } 
        public string MiddleLeft { get; set; } 
        public string MiddleCenter { get; set; } 
        public string MiddleRight { get; set; } 
        public string BottomLeft { get; set; } 
        public string BottomCenter { get; set; } 
        public string BottomRight { get; set; } 
        public List<SelectListItem> PositionList { get;  set; }
        public string Selected { get; set; }
        public string Result { get; set; }
    }
}