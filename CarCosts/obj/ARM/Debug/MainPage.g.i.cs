﻿

#pragma checksum "C:\Users\Christoph\Documents\Development\CarCosts\CarCosts\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FFCB49B8C801DC5CA16ABA5CD2D879A7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarCosts
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton bAddRefueling; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton bEditRefuelings; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.PivotItem piRefuelings; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox tbGoodFuelConsumption; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox tbAverageFuelConsumption; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox tbBadFuelConsumption; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart chart; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ListView lvRefulings; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///MainPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            bAddRefueling = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("bAddRefueling");
            bEditRefuelings = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("bEditRefuelings");
            piRefuelings = (global::Windows.UI.Xaml.Controls.PivotItem)this.FindName("piRefuelings");
            tbGoodFuelConsumption = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("tbGoodFuelConsumption");
            tbAverageFuelConsumption = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("tbAverageFuelConsumption");
            tbBadFuelConsumption = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("tbBadFuelConsumption");
            chart = (global::WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart)this.FindName("chart");
            lvRefulings = (global::Windows.UI.Xaml.Controls.ListView)this.FindName("lvRefulings");
        }
    }
}


