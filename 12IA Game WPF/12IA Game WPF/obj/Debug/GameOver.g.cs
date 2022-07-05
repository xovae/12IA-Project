﻿#pragma checksum "..\..\GameOver.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3BC738C943194347D3ED5B0554869C45FDBBE02B4E3817210D02627DD2841F02"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using _12IA_Game_WPF;


namespace _12IA_Game_WPF {
    
    
    /// <summary>
    /// GameOver
    /// </summary>
    public partial class GameOver : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgBackground;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtOver;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSummary;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtReset;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtExit;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtMenu;
        
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
            System.Uri resourceLocater = new System.Uri("/12IA Game WPF;component/gameover.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GameOver.xaml"
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
            this.imgBackground = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.txtOver = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtSummary = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtReset = ((System.Windows.Controls.TextBlock)(target));
            
            #line 13 "..\..\GameOver.xaml"
            this.txtReset.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TextHighlight);
            
            #line default
            #line hidden
            
            #line 13 "..\..\GameOver.xaml"
            this.txtReset.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TextDehighlight);
            
            #line default
            #line hidden
            
            #line 13 "..\..\GameOver.xaml"
            this.txtReset.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Reset);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtExit = ((System.Windows.Controls.TextBlock)(target));
            
            #line 14 "..\..\GameOver.xaml"
            this.txtExit.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TextHighlight);
            
            #line default
            #line hidden
            
            #line 14 "..\..\GameOver.xaml"
            this.txtExit.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TextDehighlight);
            
            #line default
            #line hidden
            
            #line 14 "..\..\GameOver.xaml"
            this.txtExit.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Exit);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtMenu = ((System.Windows.Controls.TextBlock)(target));
            
            #line 15 "..\..\GameOver.xaml"
            this.txtMenu.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Menu);
            
            #line default
            #line hidden
            
            #line 15 "..\..\GameOver.xaml"
            this.txtMenu.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TextHighlight);
            
            #line default
            #line hidden
            
            #line 15 "..\..\GameOver.xaml"
            this.txtMenu.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TextDehighlight);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

