﻿#pragma checksum "..\..\..\visao\VisualizarBurndown.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0EE351D93C7333B8C034AB6FCAA62A5A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GEP_DE611.visao {
    
    
    /// <summary>
    /// VisualizarBurndown
    /// </summary>
    public partial class VisualizarBurndown : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupFiltro;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblProjeto;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbProjeto;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSprint;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbSprint;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGerarBurndown;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tblBurndown;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\visao\VisualizarBurndown.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataVisualization.Charting.Chart lineChart;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GEP_DE611;component/visao/visualizarburndown.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\visao\VisualizarBurndown.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.groupFiltro = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.lblProjeto = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.cmbProjeto = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\visao\VisualizarBurndown.xaml"
            this.cmbProjeto.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbProjeto_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblSprint = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.cmbSprint = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnGerarBurndown = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\visao\VisualizarBurndown.xaml"
            this.btnGerarBurndown.Click += new System.Windows.RoutedEventHandler(this.btnGerarBurndown_Click_1);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tblBurndown = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            this.lineChart = ((System.Windows.Controls.DataVisualization.Charting.Chart)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

