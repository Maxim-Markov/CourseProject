﻿#pragma checksum "..\..\..\windows\KeyWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "909B6E378CE6F7E411906D1AB7EB0D2235791C298DBE2C9048441E310C69AD13"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseProject.windows;
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


namespace CourseProject.windows {
    
    
    /// <summary>
    /// KeyWindow
    /// </summary>
    public partial class KeyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\windows\KeyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.LinearGradientBrush BackgroudGrad;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\windows\KeyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FullKeyBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\windows\KeyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button KeyAgreeButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\windows\KeyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock KeyErrors;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseProject;component/windows/keywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\windows\KeyWindow.xaml"
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
            this.BackgroudGrad = ((System.Windows.Media.LinearGradientBrush)(target));
            return;
            case 2:
            this.FullKeyBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\windows\KeyWindow.xaml"
            this.FullKeyBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FullKeyBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.KeyAgreeButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\windows\KeyWindow.xaml"
            this.KeyAgreeButton.Click += new System.Windows.RoutedEventHandler(this.KeyAgreeButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.KeyErrors = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

