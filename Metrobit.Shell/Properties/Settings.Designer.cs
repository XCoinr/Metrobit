﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metrobit.Shell.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"&lt;?xml version=""1.0"" encoding=""utf-16""?&gt;
&lt;WINDOWPLACEMENT xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""&gt;
  &lt;length&gt;0&lt;/length&gt;
  &lt;flags&gt;0&lt;/flags&gt;
  &lt;showCmd&gt;0&lt;/showCmd&gt;
  &lt;minPosition&gt;
    &lt;X&gt;0&lt;/X&gt;
    &lt;Y&gt;0&lt;/Y&gt;
  &lt;/minPosition&gt;
  &lt;maxPosition&gt;
    &lt;X&gt;0&lt;/X&gt;
    &lt;Y&gt;0&lt;/Y&gt;
  &lt;/maxPosition&gt;
  &lt;normalPosition&gt;
    &lt;Left&gt;0&lt;/Left&gt;
    &lt;Top&gt;0&lt;/Top&gt;
    &lt;Right&gt;0&lt;/Right&gt;
    &lt;Bottom&gt;0&lt;/Bottom&gt;
  &lt;/normalPosition&gt;
&lt;/WINDOWPLACEMENT&gt;")]
        public global::Metrobit.Shell.Utils.WINDOWPLACEMENT WindowPlacement {
            get {
                return ((global::Metrobit.Shell.Utils.WINDOWPLACEMENT)(this["WindowPlacement"]));
            }
            set {
                this["WindowPlacement"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/FirstFloor.ModernUI;component/Assets/ModernUI.Dark.xaml")]
        public global::System.Uri ThemeSource {
            get {
                return ((global::System.Uri)(this["ThemeSource"]));
            }
            set {
                this["ThemeSource"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FF647687")]
        public global::System.Windows.Media.Color AccentColor {
            get {
                return ((global::System.Windows.Media.Color)(this["AccentColor"]));
            }
            set {
                this["AccentColor"] = value;
            }
        }
    }
}
