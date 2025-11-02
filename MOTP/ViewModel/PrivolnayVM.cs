using MOTP.Model;

namespace MOTP.ViewModel
{
    class PrivolnayVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string ProductAvailability
        {
            get { return _pageModel.PrivolnayStatus; }
            set { _pageModel.PrivolnayStatus = value; OnPropertyChanged(); }
        }

        public PrivolnayVM()
        {
            _pageModel = new PageModel();
            ProductAvailability = "Out of Stock";
        }
    }
}
