using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_New.Markup.Models
{
    internal class ImageModel : BindableBase
    {

        public ImageSource Image {  get; set; }
    }
}
