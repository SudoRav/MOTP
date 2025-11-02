using MOTP.Model;

namespace MOTP.ViewModel
{
    class BUhunskayVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string ProductAvailability
        {
            get { return _pageModel.BUhunskayStatus; }
            set { _pageModel.BUhunskayStatus = value; OnPropertyChanged(); }
        }

        public BUhunskayVM()
        {
            _pageModel = new PageModel();
            ProductAvailability = "Out of Stock";
        }
    }
}
