﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BomeansPCTool.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string API_KEY {
            get {
                return ((string)(this["API_KEY"]));
            }
            set {
                this["API_KEY"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool USE_CHINA_SERVER {
            get {
                return ((bool)(this["USE_CHINA_SERVER"]));
            }
            set {
                this["USE_CHINA_SERVER"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string UART_COM_PORT {
            get {
                return ((string)(this["UART_COM_PORT"]));
            }
            set {
                this["UART_COM_PORT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("38400")]
        public string UART_BAUD_RATE {
            get {
                return ((string)(this["UART_BAUD_RATE"]));
            }
            set {
                this["UART_BAUD_RATE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ALWAYS_PULL_DATA_FROM_CLOUD {
            get {
                return ((bool)(this["ALWAYS_PULL_DATA_FROM_CLOUD"]));
            }
            set {
                this["ALWAYS_PULL_DATA_FROM_CLOUD"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string M_SELECTED_CATEGORY_ID {
            get {
                return ((string)(this["M_SELECTED_CATEGORY_ID"]));
            }
            set {
                this["M_SELECTED_CATEGORY_ID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string M_SELECTED_BRAND_ID {
            get {
                return ((string)(this["M_SELECTED_BRAND_ID"]));
            }
            set {
                this["M_SELECTED_BRAND_ID"] = value;
            }
        }
    }
}
