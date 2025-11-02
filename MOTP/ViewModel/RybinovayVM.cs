using MOTP.Model;

namespace MOTP.ViewModel
{
    class RybinovayVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.RybinovayStatus; }
        set { _pageModel.RybinovayStatus = value; OnPropertyChanged(); }
    }

    public RybinovayVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
