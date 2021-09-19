using Ecommerce.Models.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class MonofilamentLineViewModel
    {
        public IEnumerable<MonofilamentLine> monofilamentLines { get; set; }
        public int Unwinding { get; set; }            //Фильтр по размотке

    }
}
