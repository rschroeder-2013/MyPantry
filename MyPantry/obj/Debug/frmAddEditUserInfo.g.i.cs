﻿#pragma checksum "..\..\frmAddEditUserInfo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "385F0228EA2C8244DB8324959B3F89BC4955BC4591F7DE2347B05F90FF5E26C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MyPantry;
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


namespace MyPantry {
    
    
    /// <summary>
    /// frmAddEditUserInfo
    /// </summary>
    public partial class frmAddEditUserInfo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 87 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbkTitle;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditSave;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblUserID;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserFirstName;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserLastName;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserEmail;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserPhoneNumber;
        
        #line default
        #line hidden
        
        
        #line 219 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstUnassignedRoles;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstAssignedRoles;
        
        #line default
        #line hidden
        
        
        #line 233 "..\..\frmAddEditUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkActiveStatus;
        
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
            System.Uri resourceLocater = new System.Uri("/MyPantry;component/frmaddedituserinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmAddEditUserInfo.xaml"
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
            
            #line 9 "..\..\frmAddEditUserInfo.xaml"
            ((MyPantry.frmAddEditUserInfo)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbkTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btnEditSave = ((System.Windows.Controls.Button)(target));
            
            #line 152 "..\..\frmAddEditUserInfo.xaml"
            this.btnEditSave.Click += new System.Windows.RoutedEventHandler(this.btnEditSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 161 "..\..\frmAddEditUserInfo.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblUserID = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.txtUserFirstName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtUserLastName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtUserEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtUserPhoneNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.lstUnassignedRoles = ((System.Windows.Controls.ListBox)(target));
            
            #line 223 "..\..\frmAddEditUserInfo.xaml"
            this.lstUnassignedRoles.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstUnassignedRoles_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.lstAssignedRoles = ((System.Windows.Controls.ListBox)(target));
            
            #line 229 "..\..\frmAddEditUserInfo.xaml"
            this.lstAssignedRoles.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstAssignedRoles_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 12:
            this.chkActiveStatus = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

