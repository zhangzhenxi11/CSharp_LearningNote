using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Solution_AndroidApp {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private void DoAdd(object sender, EventArgs e) {
            double.TryParse(Tb1.Text, out double x);
            double.TryParse(Tb2.Text, out double y);
            var z = x + y;
            Tb3.Text = z.ToString();
        }

        private void DoSub(object sender, EventArgs e) {
            double.TryParse(Tb1.Text, out double x);
            double.TryParse(Tb2.Text, out double y);
            var z = x - y;
            Tb3.Text = z.ToString();
        }

        private void DoMul(object sender, EventArgs e) {
            double.TryParse(Tb1.Text, out double x);
            double.TryParse(Tb2.Text, out double y);
            var z = x * y;
            Tb3.Text = z.ToString();
        }

        private void DoDiv(object sender, EventArgs e) {
            double.TryParse(Tb1.Text, out double x);
            double.TryParse(Tb2.Text, out double y);
            var z = x / y;
            Tb3.Text = z.ToString();
        }
    }
}
