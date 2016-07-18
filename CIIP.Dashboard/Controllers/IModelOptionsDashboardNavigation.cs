using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace CIIP.Win.General.DashBoard.Controllers
{
    public interface IModelOptionsDashboardNavigation : IModelNode {
        [Category("Navigation")]
        [DefaultValue("�Ǳ���")]
        String DashboardGroupCaption { get; set; }

        [DefaultValue(true)]
        [Category("Navigation")]
        bool DashboardsInGroup { get; set; }
    }
}