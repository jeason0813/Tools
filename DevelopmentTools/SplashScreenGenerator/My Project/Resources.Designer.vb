﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.208
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Tools.SplashScreenGenerator.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Assembly info extensions must be same.
        '''</summary>
        Friend ReadOnly Property AssemblyInfoExtensionsMustBeSame() As String
            Get
                Return ResourceManager.GetString("AssemblyInfoExtensionsMustBeSame", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Assembly info missing.
        '''</summary>
        Friend ReadOnly Property AssemblyInfoMissing() As String
            Get
                Return ResourceManager.GetString("AssemblyInfoMissing", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to At least one assembly info is required.
        '''</summary>
        Friend ReadOnly Property AtLeastOneAssemblyInfoIsRequired() As String
            Get
                Return ResourceManager.GetString("AtLeastOneAssemblyInfoIsRequired", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Format was null.
        '''</summary>
        Friend ReadOnly Property FormatWasNull() As String
            Get
                Return ResourceManager.GetString("FormatWasNull", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error {0}: {1}.
        '''</summary>
        Friend ReadOnly Property GeneralError() As String
            Get
                Return ResourceManager.GetString("GeneralError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to {0} is invalid format for {1}.
        '''</summary>
        Friend ReadOnly Property InvalidFormat() As String
            Get
                Return ResourceManager.GetString("InvalidFormat", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Option missing for {0}.
        '''</summary>
        Friend ReadOnly Property OptionMissing() As String
            Get
                Return ResourceManager.GetString("OptionMissing", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to {0} not suppported: {1}.
        '''</summary>
        Friend ReadOnly Property SomethingNotSuppported() As String
            Get
                Return ResourceManager.GetString("SomethingNotSuppported", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Unknown option: {0}.
        '''</summary>
        Friend ReadOnly Property UnknownOption() As String
            Get
                Return ResourceManager.GetString("UnknownOption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Usage:
        '''{0} infile outfile arguments   
        '''- infile - path to original image
        '''- outfile - path to store generated image
        '''- arguments - specifiedas -argName, arg name is case insensitive
        '''    -assemblyInfo assemblyInfo (must be specified at least once, can be specified multiple times, short form is -a)
        '''        assemblyInfo - path to source file containing assembly info
        '''            if specified multiple times all files must have same extension
        '''    -type fontSize offset [foregroundColor [backgroundColor [font [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Usage() As String
            Get
                Return ResourceManager.GetString("Usage", resourceCulture)
            End Get
        End Property
    End Module
End Namespace