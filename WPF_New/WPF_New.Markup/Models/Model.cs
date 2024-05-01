using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_New.Models
{
   public class Model : ReactiveObject
    {
        [Reactive] public int K { get; set; }
    }
}
