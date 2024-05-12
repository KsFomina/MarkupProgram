using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AutomaticMarkup.Layout.Models
{
    public class Model : ReactiveObject
    {
        [Reactive] public int K { get; set; }
    }
}
