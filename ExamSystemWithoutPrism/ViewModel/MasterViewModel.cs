using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.ViewModel
{
	public class MasterViewModel : BaseViewModel
	{
		private ViewContainerViewModel _containerViewModel;

		public ViewContainerViewModel ContainerViewModel
		{
			get
			{
				return _containerViewModel == null ? _containerViewModel = new ViewContainerViewModel() : _containerViewModel;
			}
			set
			{
				_containerViewModel = value;
				OnPropertyChanged("ContainerViewModel");
			}
		}
	}
}
