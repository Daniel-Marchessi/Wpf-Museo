﻿#pragma checksum "..\..\..\..\Views\Home.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "315DEF349F57900621A5A930EE0734E1DDD0D978"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfAppTEST.Views;


namespace WpfAppTEST.Views {
    
    
    /// <summary>
    /// Home
    /// </summary>
    public partial class Home : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfAppTEST.Views.Home Home1;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Libro;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CrearLibro;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ListaLi;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Coleccion;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CrearColeccion;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ListaCo;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Autor;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Material;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Museoapp;V1.0.0.0;component/views/home.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Home.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Home1 = ((WpfAppTEST.Views.Home)(target));
            return;
            case 2:
            this.Libro = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 3:
            this.CrearLibro = ((System.Windows.Controls.MenuItem)(target));
            
            #line 17 "..\..\..\..\Views\Home.xaml"
            this.CrearLibro.Click += new System.Windows.RoutedEventHandler(this.CrearLibro_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ListaLi = ((System.Windows.Controls.MenuItem)(target));
            
            #line 18 "..\..\..\..\Views\Home.xaml"
            this.ListaLi.Click += new System.Windows.RoutedEventHandler(this.CrearListaLibro_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Coleccion = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 6:
            this.CrearColeccion = ((System.Windows.Controls.MenuItem)(target));
            
            #line 22 "..\..\..\..\Views\Home.xaml"
            this.CrearColeccion.Click += new System.Windows.RoutedEventHandler(this.CrearColeccion_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ListaCo = ((System.Windows.Controls.MenuItem)(target));
            
            #line 23 "..\..\..\..\Views\Home.xaml"
            this.ListaCo.Click += new System.Windows.RoutedEventHandler(this.CrearListaColeccion_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Autor = ((System.Windows.Controls.MenuItem)(target));
            
            #line 26 "..\..\..\..\Views\Home.xaml"
            this.Autor.Click += new System.Windows.RoutedEventHandler(this.CrearAutor_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Material = ((System.Windows.Controls.MenuItem)(target));
            
            #line 27 "..\..\..\..\Views\Home.xaml"
            this.Material.Click += new System.Windows.RoutedEventHandler(this.CrearMaterial_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

