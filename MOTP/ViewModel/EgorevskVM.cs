using MOTP.Model;

namespace MOTP.ViewModel
{
    class EgorevskVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string ProductAvailability
        {
            get { return _pageModel.EgorevskStatus; }
            set { _pageModel.EgorevskStatus = value; OnPropertyChanged(); }
        }

        public EgorevskVM()
        {
            _pageModel = new PageModel();
            ProductAvailability = "Out of Stock";
        }
    }
}
