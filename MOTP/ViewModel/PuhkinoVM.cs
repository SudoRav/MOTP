using MOTP.Model;

namespace MOTP.ViewModel
{
    class PuhkinoVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.PuhkinoStatus; }
        set { _pageModel.PuhkinoStatus = value; OnPropertyChanged(); }
    }

    public PuhkinoVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
