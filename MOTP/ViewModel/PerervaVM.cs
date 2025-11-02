using MOTP.Model;

namespace MOTP.ViewModel
{
    class PerervaVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.PerervaStatus; }
        set { _pageModel.PerervaStatus = value; OnPropertyChanged(); }
    }

    public PerervaVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
