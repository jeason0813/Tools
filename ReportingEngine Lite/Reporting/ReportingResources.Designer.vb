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

Namespace ReportingT.ReportingEngineLite
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class ReportingResources
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Tools.ReportingResources", GetType(ReportingResources).Assembly)
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
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Zadejte sloupce, u nichž má být nastavena automatická šířka po vyplnění všech řádků. Zadejte:
        '''• * pro nastavení u všech vyplňovaných sloupců
        '''• čísla (např. 1,2,5).
        '''• rozmezí (např. 1-6, 12-22).
        '''• neomezené rozmezí (např. 12-) pro všechny vyplňované sloupce od 12 výše.
        '''• ponechte prázdné a nenastaví se nic.
        '''Všechna čísla sloupců jsou absolutní v rámci listu (1-based).
        '''</summary>
        Friend Shared ReadOnly Property AutoWidth_d() As String
            Get
                Return ResourceManager.GetString("AutoWidth_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Zadejte 1-based číslo sloupce, kam se budou data zapisovat (buňky vlevo budou tvořt záhlaví řádků).
        '''</summary>
        Friend Shared ReadOnly Property Col1_d() As String
            Get
                Return ResourceManager.GetString("Col1_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Číslo řádku (1-based) kam zapsat název sloupce z databáze (0 a nezapíše se nikam).
        '''</summary>
        Friend Shared ReadOnly Property ColumnNameRow_d() As String
            Get
                Return ResourceManager.GetString("ColumnNameRow_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to CSV.
        '''</summary>
        Friend Shared ReadOnly Property CSV() As String
            Get
                Return ResourceManager.GetString("CSV", resourceCulture)
            End Get
        End Property
        
        Friend Shared ReadOnly Property Excel() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("Excel", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Řádky budou vkládány místo aby byly přepisovány. Omožňuje zachovat zápatí..
        '''</summary>
        Friend Shared ReadOnly Property InsertRows_d() As String
            Get
                Return ResourceManager.GetString("InsertRows_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Zadejte název listu nebo 1-based index, kam se budou data zapisovat (nechte prázdné a použije se první list).
        '''</summary>
        Friend Shared ReadOnly Property List_d() As String
            Get
                Return ResourceManager.GetString("List_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Zadejte ve formátu:
        '''x1;x2-y1;y2
        '''• x1 - sloupec vzhledem k prvnímu vyplňovanému
        '''• x2 - sloupec vzhledem k poslednímu vyplňovanému
        '''• y1 - řádek vzhledem k prvnímu vyplňovanému
        '''• y2 - řádek vzhledem k poslednímu vyplňovanému
        '''Relativní čísla mají tento význam:
        '''• 0 - řádek/sloupec ke kterému se vztakují
        '''• +1, +2, ... posun dolů/doprava
        '''• -1, -2, ... posun nahoru/doleva
        '''• 1, 2, ... absolutní vzhledem k listu
        '''Ponechte prázdné a oblast tisku nebude nastavena..
        '''</summary>
        Friend Shared ReadOnly Property PrintArea_d() As String
            Get
                Return ResourceManager.GetString("PrintArea_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Zadejte 1-based číslo 1.řádku pro zápis (buňky výše budou tvořit záhlaví).
        '''</summary>
        Friend Shared ReadOnly Property Row1_d() As String
            Get
                Return ResourceManager.GetString("Row1_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Sloupce v nichž je vyplněna hodnota nebo vzorec budou přeskočeny místo aby byly zapsány (rozhodující je stav v 1. vyplňovaném řádku).
        '''</summary>
        Friend Shared ReadOnly Property SkipFilled_d() As String
            Get
                Return ResourceManager.GetString("SkipFilled_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Nezapisovat název sloupce, pokud je již buňka vyplněna.
        '''</summary>
        Friend Shared ReadOnly Property SkipFilledNames_d() As String
            Get
                Return ResourceManager.GetString("SkipFilledNames_d", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to XML.
        '''</summary>
        Friend Shared ReadOnly Property XML() As String
            Get
                Return ResourceManager.GetString("XML", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
