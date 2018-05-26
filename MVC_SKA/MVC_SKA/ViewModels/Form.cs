using MVC_SKA.Models;
using System.Collections.Generic;

namespace MVC_SKA.ViewModels
{
    public enum GameResult
    {
        CrossWin = 1,
        CircleWin = 2,
        Draw = 3
    }

    public class Form
    {
        public List<Grid> Grids { get; set; }
        public bool IsFinsh { get; set; }
        public GameResult Result { get; set; }
    }
}