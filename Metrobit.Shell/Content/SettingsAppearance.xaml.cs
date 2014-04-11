using System.Diagnostics.CodeAnalysis;
using Metrobit.Shell.ViewModel;

namespace Metrobit.Shell.Content
{
    /// <summary>
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class SettingsAppearance
    {
        public SettingsAppearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            DataContext = new SettingsAppearanceViewModel();
        }
    }
}
