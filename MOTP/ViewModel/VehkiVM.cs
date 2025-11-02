using MOTP.Model;

namespace MOTP.ViewModel
{
    class VehkiVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.VehkiStatus; }
        set { _pageModel.VehkiStatus = value; OnPropertyChanged(); }
    }

    public VehkiVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
