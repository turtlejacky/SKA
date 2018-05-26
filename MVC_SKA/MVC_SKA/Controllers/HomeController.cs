using MVC_SKA.Models;
using MVC_SKA.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC_SKA.Controllers
{
    public class HomeController : Controller
    {
        private static Form _myFrom;

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public void ChoseGrid(int indexOfGrid)
        {
            _myFrom.Grids[indexOfGrid].Value = 'O';

            var emptyGrid = GetEmptyGrid();
            if (emptyGrid == -1)
            {
                CheckIsFinish();
                if (_myFrom.Result != GameResult.CircleWin)
                    _myFrom.Result = GameResult.Draw;
                Response.Write(JsonConvert.SerializeObject(new { IsFinish = true, GameRusult = _myFrom.Result.ToString() }));
                return;
            }

            _myFrom.Grids[emptyGrid].Value = 'X';
            CheckIsFinish();
            var response = new
            {
                IsFinish = _myFrom.IsFinsh,
                GameRusult = _myFrom.Result.ToString(),
                CrossStep = emptyGrid
            };
            Response.Write(JsonConvert.SerializeObject(response));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            _myFrom = new Form()
            {
                Grids = new List<Grid>()
                {
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                    new Grid(){ Value = ' '},
                }
            };
            return View(_myFrom);
        }

        private void CheckColumn()
        {
            if (CheckTheseGridsIsCircle(0, 3, 6) || CheckTheseGridsIsCircle(1, 4, 7) ||
                CheckTheseGridsIsCircle(2, 5, 8))
            {
                _myFrom.IsFinsh = true;
                _myFrom.Result = GameResult.CircleWin;
            }
            else if (CheckTheseGridsIsCross(0, 3, 6) || CheckTheseGridsIsCross(1, 4, 7) ||
                     CheckTheseGridsIsCross(2, 5, 8))
            {
                _myFrom.IsFinsh = true;
                _myFrom.Result = GameResult.CrossWin;
            }
        }

        private void CheckIsFinish()
        {
            CheckRow();
            CheckColumn();
            CheckSlant();
        }

        private void CheckRow()
        {
            if (CheckTheseGridsIsCircle(0, 1, 2) || CheckTheseGridsIsCircle(3, 4, 5) ||
                CheckTheseGridsIsCircle(6, 7, 8))
            {
                _myFrom.IsFinsh = true;
                _myFrom.Result = GameResult.CircleWin;
            }
            else if (CheckTheseGridsIsCross(0, 1, 2) || CheckTheseGridsIsCross(3, 4, 5) ||
                     CheckTheseGridsIsCross(6, 7, 8))
            {
                _myFrom.IsFinsh = true;
                _myFrom.Result = GameResult.CrossWin;
            }
        }

        private void CheckSlant()
        {
            if (CheckTheseGridsIsCircle(0, 4, 8) || CheckTheseGridsIsCircle(2, 4, 6))
            {
                _myFrom.IsFinsh = true;
                _myFrom.Result = GameResult.CircleWin;
            }
            else if (CheckTheseGridsIsCross(0, 4, 8) || CheckTheseGridsIsCross(2, 4, 6))
            {
                _myFrom.IsFinsh = true;
                _myFrom.Result = GameResult.CrossWin;
            }
        }

        private bool CheckTheseGridsIsCircle(int a, int b, int c)
        {
            return (_myFrom.Grids[a].Value == 'O' && _myFrom.Grids[b].Value == 'O'
                    && _myFrom.Grids[c].Value == 'O');
        }

        private bool CheckTheseGridsIsCross(int a, int b, int c)
        {
            return (_myFrom.Grids[a].Value == 'X' && _myFrom.Grids[b].Value == 'X'
                && _myFrom.Grids[c].Value == 'X');
        }

        private int GetEmptyGrid()
        {
            Random rand = new Random();
            var canPutList = _myFrom.Grids.Select((x, index) => new { x.Value, Index = index }).Where(x => x.Value == ' ')
                .ToList();
            return canPutList.Any() ? canPutList[rand.Next(canPutList.Count)].Index : -1;
        }
    }
}