using MOTP.Model;

namespace MOTP.ViewModel
{
    class HimkiVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string ProductAvailability
        {
            get { return _pageModel.HimkiStatus; }
            set { _pageModel.HimkiStatus = value; OnPropertyChanged(); }
        }

        public HimkiVM()
        {
            _pageModel = new PageModel();
            ProductAvailability = "Out of Stock";
        }
    }
}
