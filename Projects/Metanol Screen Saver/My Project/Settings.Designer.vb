﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<Sys:FileNameWO:{0}><Exif:DateTimeDigitized: {0}>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<IPTC:ObjectName:{0}"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"><IPTC:C"& _ 
            "ountryPrimaryLocationCode:{0}, ><IPTC:ProvinceState:{0}, ><IPTC:City:{0}, ><IPTC"& _ 
            ":SubLocation:{0}>")>  _
        Public Property InfoText() As String
            Get
                Return CType(Me("InfoText"),String)
            End Get
            Set
                Me("InfoText") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Folder() As String
            Get
                Return CType(Me("Folder"),String)
            End Get
            Set
                Me("Folder") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Random")>  _
        Public Property Alghoritm() As Global.Metanol.SSaver.SSAverAlghoritm
            Get
                Return CType(Me("Alghoritm"),Global.Metanol.SSaver.SSAverAlghoritm)
            End Get
            Set
                Me("Alghoritm") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<ArrayOfString xmlns:xsi=""http://www.w3."& _ 
            "org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <s"& _ 
            "tring>*.jpeg</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <string>*.jpg</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"</ArrayOfString>")>  _
        Public Property Mask() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("Mask"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("Mask") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("10")>  _
        Public Property Timer() As UShort
            Get
                Return CType(Me("Timer"),UShort)
            End Get
            Set
                Me("Timer") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Arial, 9.75pt")>  _
        Public Property Font() As Global.System.Drawing.Font
            Get
                Return CType(Me("Font"),Global.System.Drawing.Font)
            End Get
            Set
                Me("Font") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property BgColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("BgColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("BgColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Lime")>  _
        Public Property FgColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("FgColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("FgColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Transparent")>  _
        Public Property InfoBgColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("InfoBgColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("InfoBgColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("TopLeft")>  _
        Public Property InfoAlign() As Global.System.Drawing.ContentAlignment
            Get
                Return CType(Me("InfoAlign"),Global.System.Drawing.ContentAlignment)
            End Get
            Set
                Me("InfoAlign") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Metanol.SSaver.My.MySettings
            Get
                Return Global.Metanol.SSaver.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
