#If Config <= Alpha Then 'Stage: Alpha
Namespace Collections.Generic
    ''' <summary>List that provides events when changed</summary>
    ''' <typeparam name="T">Type of items to be stored in the list</typeparam>
    ''' <remarks>If item of type tha implements the <see cref="IReportsChange"/> interface is passed to this list, than it's events <see cref="IReportsChange.Changed"/> are reported through <see cref="ListWithEvents.ItemValueChanged"/> event.</remarks>
    <Author("�onny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(ListWithEvents(Of String)), LastChange:="1/3/2007")> _
    Public Class ListWithEvents(Of T) : Implements IList(Of T)
        ''' <summary>CTor</summary>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        Public Sub New(Optional ByVal AddingReadOnly As Boolean = False)
            List = New List(Of T)
        End Sub
        ''' <summary>CTor - initializes from another <see cref="IEnumerable(Of T)"/></summary>
        ''' <param name="collection"><see cref="IEnumerable(Of T)"/> to initialize new instance of <see cref="ListWithEvents(Of T)"/> with</param>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <exception cref="System.ArgumentNullException">collection is null</exception>
        Public Sub New(ByVal collection As IEnumerable(Of T), Optional ByVal AddingReadOnly As Boolean = False)
            List = New List(Of T)(collection)
            AddAllItemHandlers()
        End Sub
        ''' <summary>Initializes a new instance of the <see cref="ListWithEvents(Of T)"/> class that is empty and has the specified initial capacity.</summary>
        ''' <param name="capacity">The number of elements that the new list can initially store.</param>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0</exception>
        Public Sub New(ByVal capacity As Integer, Optional ByVal AddingReadOnly As Boolean = False)
            List = New List(Of T)(capacity)
        End Sub

        ''' <summary>Internal list that is used for soring values</summary>
        Private List As List(Of T)

        ''' <summary>Contains value of the <see cref="AddingReadOnly"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _AddingReadOnly As Boolean = False
        ''' <summary>Determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</summary>
        Public ReadOnly Property AddingReadOnly() As Boolean
            Get
                Return _AddingReadOnly
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="AllowAddCancelableEventsHandlers"/> property</summary>
        Private _AllowAddCancelableEventsHandlers As Boolean = True
        ''' <summary>Determines if it is allowed to add handlers for events that supports cancellation</summary>
        ''' <exception cref="InvalidOperationException">Trying to set value to True when it if False</exception>
        ''' <remarks>
        ''' Value can be changed only from True (default) to False
        ''' <list>
        ''' <listheader>Those are events:</listheader>
        ''' <item><see cref="Adding"/></item>
        ''' <item><see cref="Removing"/></item>
        ''' <item><see cref="Clearing"/></item>
        ''' <item><see cref="ItemChanging"/></item>
        ''' </list>
        ''' </remarks>
        Public Property AllowAddCancelableEventsHandlers() As Boolean
            Get
                Return _AllowAddCancelableEventsHandlers
            End Get
            Set(ByVal value As Boolean)
                If value = False Then
                    _AllowAddCancelableEventsHandlers = value
                ElseIf value = True AndAlso AllowAddCancelableEventsHandlers = False Then
                    Throw New InvalidOperationException("AllowAddCancelableEventsHandlers  can be changed only from True to False")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Locked"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Locked As Boolean = False
        ''' <summary>Determines if the <see cref="ListWithEvents(Of T)"/> isn locked (being locked prevents if from being edited)</summary>
        ''' <remarks><para>
        ''' <see cref="ListWithEvents(Of T)"/> is usually locked while some events' handlers are being invoked.
        ''' </para><list>
        ''' <listheader><see cref="Locked"/> set to True blocks following methods and causes <see cref="InvalidOperationException"/> exception to be thrown there:</listheader>
        ''' <item><see cref="Add"/></item>
        ''' <item><see cref="Insert"/></item>
        ''' <item><see cref="Remove"/></item>
        ''' <item><see cref="RemoveAt"/></item>
        ''' <item><see cref="Clear"/></item>
        ''' <item><see cref="Item"/> (only setter)</item>
        ''' </list></remarks>
        Public ReadOnly Property Locked() As Boolean
            Get
                Return _Locked
            End Get
        End Property
        ''' <summary>Sets the <see cref="Locked"/> to True</summary>
        Protected Sub Lock()
            _Locked = True
        End Sub
        ''' <summary>Sets the <see cref="Locked"/> to False</summary>
        Protected Sub Unlock()
            _Locked = False
        End Sub
#Region "Add"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Adding"/> event is raised</summary>
        Private AddingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Delegate of handler of <see cref="Adding"/>, <see cref="Removing"/> and <see cref="ItemChanging"/> events</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ItemCancelEventHandler(ByVal sender As ListWithEvents(Of T), ByVal e As CancelableItemIndexEventArgs)
        ''' <summary>Raised before an item is added to the list. Raised by <see cref="Add"/> and <see cref="Insert"/> methods.</summary>
        ''' <remarks><para>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> can be changed if <see cref="AddingReadOnly"/> is False.
        ''' </para></remarks>
        Public Custom Event Adding As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    AddingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the Adding event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                AddingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As ListWithEvents(Of T).CancelableItemIndexEventArgs)
                For Each Handler As ItemCancelEventHandler In AddingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after an item is added to the list. Raised by <see cref="Add"/> and <see cref="Insert"/> methods</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Event Added(ByVal sender As ListWithEvents(Of T), ByVal e As ItemIndexEventArgs)

        ''' <summary>Adds an item to the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to add to the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        Public Overridable Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
            If Locked Then Throw New InvalidOperationException("List is locked")
            Dim e As New CancelableItemIndexEventArgs(item, List.Count, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                List.Add(item)
                AddItemHandler(List.Count - 1)
                OnAdded(New ItemIndexEventArgs(item, List.Count - 1))
            End If
        End Sub
        '        ''' <summary>Handles events for concre item (represented by index), attaches index to them and passes them to <see cref="ListWithEvents(Of T)"/></summary>
        '        Protected Class IndexEventHandler : Implements IDisposable
        '            ''' <summary>Index of item in <see cref="ListWithEvents(Of T)"/></summary>
        '            Private index As Integer
        '            ''' <summary>Item that produces events to be handled</summary>
        '            Private item As IReportsChange
        '            ''' <summary>CTor</summary>
        '            ''' <param name="index">Index of item in <see cref="ListWithEvents(Of T)"/></param>
        '            ''' <param name="item">Item that produces events to be hendled</param>
        '            Public Sub New(ByVal index As Integer, ByVal item As IReportsChange)
        '                Me.index = index
        '                Me.item = item
        '                AddHandler item.Changed, AddressOf item_ItemChanged
        '            End Sub
        '            ''' <summary>Handles event from <see cref="item"/></summary>
        '            Private Sub item_ItemChanged(ByVal sender As IReportsChange, ByVal e As EventArgs)
        '                RaiseEvent ItemChanged(sender, New IndexEventArgs(e, index))
        '            End Sub
        '            ''' <summary>Delegate of handler of <see cref="ItemChanged"/> event</summary>
        '            ''' <param name="sender">Object that originally produced the event</param>
        '            ''' <param name="e">Event parameters (contains index and original parameters)</param>
        '            Public Delegate Sub ItemChangedEventHandler(ByVal sender As IReportsChange, ByVal e As IndexEventArgs)
        '            ''' <summary>List of handlers of the <see cref="ItemChanged"/> event</summary>
        '            Private ItemChangedEventHandlerList As New List(Of ItemChangedEventHandler)
        '            ''' <summary>Raised when <see cref="item"/> raises the <see cref="IReportsChange.Changed"/> event</summary>
        '            Public Custom Event ItemChanged As ItemChangedEventHandler
        '                AddHandler(ByVal value As ItemChangedEventHandler)
        '                    ItemChangedEventHandlerList.Add(value)
        '                End AddHandler
        '                RemoveHandler(ByVal value As ItemChangedEventHandler)
        '                    ItemChangedEventHandlerList.Remove(value)
        '                End RemoveHandler
        '                RaiseEvent(ByVal sender As IReportsChange, ByVal e As ListWithEvents(Of T).IndexEventHandler.IndexEventArgs)
        '                    For Each Handler As ItemChangedEventHandler In ItemChangedEventHandlerList
        '                        Handler.Invoke(sender, e)
        '                    Next Handler
        '                End RaiseEvent
        '            End Event
        '            ''' <summary>Arguments of the <see cref="ItemChanged"/> event</summary>
        '            Public Class IndexEventArgs : Inherits EventArgs
        '                ''' <summary>Arguments of original event</summary>
        '                Public ReadOnly Internal As EventArgs
        '                ''' <summary>Index of item that originally caused the event</summary>
        '                Public ReadOnly Index As Integer
        '                ''' <summary>CTor</summary>
        '                ''' <param name="InternalEventArgs">Arguments of original event</param>
        '                ''' <param name="Index">Index of item that originally caused the event</param>
        '                Public Sub New(ByVal InternalEventArgs As EventArgs, ByVal Index As Integer)
        '                    Me.Internal = InternalEventArgs
        '                    Me.Index = Index
        '                End Sub
        '            End Class

        '#Region " IDisposable Support "
        '            ''' <summary>To detect redundant calls</summary>
        '            Private disposedValue As Boolean = False
        '            ''' <summary>IDisposable</summary>
        '            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        '                If Not Me.disposedValue Then
        '                    If disposing Then
        '                        FinalizeDispoze()
        '                    End If
        '                End If
        '                Me.disposedValue = True
        '            End Sub
        '            ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        '            ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        '            Public Sub Dispose() Implements IDisposable.Dispose
        '                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '                Dispose(True)
        '                GC.SuppressFinalize(Me)
        '            End Sub
        '#End Region
        '            ''' <summary>Allows an System.Object to attempt to free resources and perform other cleanup operations before the System.Object is reclaimed by garbage collection</summary>
        '            Protected Overrides Sub Finalize()
        '                MyBase.Finalize()
        '                FinalizeDispoze()
        '            End Sub
        '            ''' <summary>Removes all handlers</summary>
        '            Private Sub FinalizeDispoze()
        '                RemoveHandler item.Changed, AddressOf item_ItemChanged
        '                ItemChangedEventHandlerList.Clear()
        '            End Sub
        '        End Class

        ''' <summary>Inserts an item to the <see cref="ListWithEvents(Of T)"/> at the specified index.</summary>
        ''' <param name="item">The object to insert into the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <param name="index">The zero-based index at which item should be inserted.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
            If Locked Then Throw New InvalidOperationException("List is locked")
            Dim e As New CancelableItemIndexEventArgs(item, index, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                List.Add(item)
                AddItemHandler(index)
                OnAdded(New ItemIndexEventArgs(item, index))
            End If
        End Sub
        ''' <summary>Raises <see cref="Adding"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdding"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnAdding(ByVal e As CancelableItemIndexEventArgs)
            RaiseEvent Adding(Me, e)
        End Sub
        ''' <summary>Raises <see cref="Added"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdded"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnAdded(ByVal e As ItemIndexEventArgs)
            RaiseEvent Added(Me, e)
        End Sub
#End Region
#Region "Clear"
        ''' <summary>List of <see cref="ClearingEventHandler"/> delegates to be invoked when the <see cref="Clearing"/> event is raised</summary>
        Private ClearingEventHandlerList As New List(Of ClearingEventHandler)
        ''' <summary>Delegate of handler of <see cref="Clearing"/> event</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ClearingEventHandler(ByVal sender As ListWithEvents(Of T), ByVal e As CancelEventArgs)
        ''' <summary>Raised before the list is cleared. Raised by <see cref="Clear"/> method.</summary>
        ''' <remarks><para>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that<see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' <see cref="Removing"/> Event is not raised when clearing list.
        ''' </para></remarks>
        Public Custom Event Clearing As ClearingEventHandler
            AddHandler(ByVal value As ClearingEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    ClearingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the Clearig event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ClearingEventHandler)
                ClearingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As CancelEventArgs)
                For Each Handler As ClearingEventHandler In ClearingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after the list is cleared. Raised by <see cref="Clear"/> method.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><see cref="Removed"/> event is not raised when clearing list.</remarks>
        Public Event Cleared(ByVal sender As ListWithEvents(Of T), ByVal e As CountEventArgs)
        ''' <summary>Removes all items from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnClearing"/> before clearing of the list and <see cref="OnCleared"/> after clearing of the list,, do not forgot to check <see cref="CancelEventArgs.Cancel"/></remarks>
        Public Overridable Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
            If Locked Then Throw New InvalidOperationException("List is locked")
            Dim e As New CancelEventArgs
            OnClearing(e)
            If Not e.Cancel Then
                Dim e2 As New CountEventArgs(List.Count)
                RemoveAllItemHandlers()
                List.Clear()
                OnCleared(e2)
            End If
        End Sub
        ''' <summary>Raises <see cref="Clearing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnClearing"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnClearing(ByVal e As CancelEventArgs)
            RaiseEvent Clearing(Me, e)
        End Sub
        ''' <summary>Raises <see cref="Cleared"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnCleared"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnCleared(ByVal e As CountEventArgs)
            RaiseEvent Cleared(Me, e)
        End Sub
#End Region
        ''' <summary>Determines whether the <see cref="ListWithEvents(Of T)"/> contains a specific value.</summary>
        ''' <param name="item">The object to locate in the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>true if item is found in the <see cref="ListWithEvents(Of T)"/>; otherwise, false.</returns>
        Public Overridable Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            Return List.Contains(item)
        End Function
        ''' <summary>Copies the elements of the <see cref="ListWithEvents(Of T)"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="ListWithEvents(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source System.Collections.Generic.ICollection(Of T) is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        Public Overridable Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
            List.CopyTo(array, arrayIndex)
        End Sub
        ''' <summary>Gets the number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <returns>The number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</returns>
        Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count
            Get
                Return List.Count
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the <see cref="ListWithEvents(Of T)"/> is read-only (always false).</summary>
        ''' <returns>Always false because <see cref="ListWithEvents(Of T)"/> is not read-only</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
            Get
                Return False
            End Get
        End Property
#Region "Remove"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Removing"/> event is raised</summary>
        Private RemovingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Raised before item is removed from the list. Raised by <see cref="Remove"/> and <see cref="RemoveAt"/> methods.</summary>
        ''' <remarks><para>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that<see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Do not change content of list in handler! List is locked.
        ''' </para><para>
        ''' <see cref="Removing"/> event is not raised when list is being cleared.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> cannot be changed.
        ''' </para>
        ''' </remarks>
        Public Custom Event Removing As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    RemovingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the Removing event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                RemovingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As CancelableItemIndexEventArgs)
                For Each Handler As ItemCancelEventHandler In RemovingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after the list is cleared. Raised by <see cref="Remove"/> and <see cref="RemoveAt"/> methods.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><see cref="Removed"/> event is not raised when the list.</remarks>
        Public Event Removed(ByVal sender As ListWithEvents(Of T), ByVal e As ItemIndexEventArgs)
        ''' <summary>Raises <see cref="Removing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks><para>
        ''' Note for inheritors: Always call base class method <see cref="OnRemoving"/> in order the event to be raised
        ''' </para><para>
        ''' Do not change content of list in this method!
        ''' </para><para>
        ''' </para></remarks>
        Protected Overridable Sub OnRemoving(ByVal e As CancelableItemIndexEventArgs)
            RaiseEvent Removing(Me, e)
        End Sub
        ''' <summary>Raises <see cref="Removed"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnRemoved"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnRemoved(ByVal e As ItemIndexEventArgs)
            RaiseEvent Removed(Me, e)
        End Sub
        ''' <summary>Removes the first occurrence of a specific object from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to remove from the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>true if item was successfully removed from the <see cref="ListWithEvents(Of T)"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="ListWithEvents(Of T)"/>.</returns>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
            If Locked Then Throw New InvalidOperationException("List is locked")
            If Contains(item) Then
                Dim e As New CancelableItemIndexEventArgs(item, IndexOf(item), True)
                Lock()
                Try
                    OnRemoving(e)
                Finally
                    Unlock()
                End Try
                If Not e.Cancel Then
                    Dim i As Integer = IndexOf(item)
                    RemoveItemHandler(i)
                    If List.Remove(item) Then
                        OnRemoved(New ItemIndexEventArgs(item, i))
                        Return True
                    Else
                        AddItemHandler(i)
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        End Function
        ''' <summary>Removes the <see cref="ListWithEvents(Of T)"/> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
            If Locked Then Throw New InvalidOperationException("List is locked")
            Dim e As New CancelableItemIndexEventArgs(Me(index), index, True)
            If index >= 0 AndAlso index < Count Then
                Lock()
                Try
                    OnRemoving(e)
                Finally
                    Unlock()
                End Try
            End If
            If Not e.Cancel Then
                Dim itm As T = Me(index)
                RemoveItemHandler(index)
                Try
                    List.RemoveAt(index)
                Catch ex As Exception
                    AddItemHandler(index)
                    Throw ex
                End Try
                OnRemoved(New ItemIndexEventArgs(itm, index))
            End If
        End Sub
#End Region
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of T)"/> that can be used to iterate through the collection.</returns>
        Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return List.GetEnumerator
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe GetEnumerator instead")> _
        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return List.GetEnumerator
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to locate in the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>The index of item if found in the list; otherwise, -1.</returns>
        Public Overridable Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
            Return List.IndexOf(item)
        End Function
#Region "Item"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="ItemChanging"/> event is raised</summary>
        Private ItemChangingEventHandlerList As List(Of ItemCancelEventHandler)
        ''' <summary>Raised before an item is changed. Raised by setter of <see cref="Item"/> property.</summary>
        ''' <remarks><para>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> can be changed if <see cref="AddingReadOnly"/> is False.
        ''' </para><para>
        ''' Do not change content of list in handler! List is locked.
        ''' </para><para>
        ''' <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> contains new value. Use <see cref="Item"/> to determine old value.
        ''' </para></remarks>
        Public Custom Event ItemChanging As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    ItemChangingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the ItemChanging event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                ItemChangingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As ListWithEvents(Of T).CancelableItemIndexEventArgs)
                For Each Handler As ItemCancelEventHandler In ItemChangingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after item in the list is changed. Raised by setter of <see cref="Item"/> property.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters (<see cref="ItemIndexEventArgs.Item"/> contains old value, use <see cref="Item"/> to determine new value.)</param>
        Public Event ItemChanged(ByVal sender As ListWithEvents(Of T), ByVal e As ItemIndexEventArgs)
        ''' <summary>Raises <see cref="ItemChanging"/> event</summary>
        ''' <param name="e">Event argument</param>
        ''' <remarks><para>
        ''' Note for inheritors: Alway call base class method <see cref="OnItemChanging"/> in order the event to be raised.
        ''' </para><para>
        ''' Do not change the content of the list in this method!
        ''' </para></remarks>
        Protected Overridable Sub OnItemChanging(ByVal e As CancelableItemIndexEventArgs)
            RaiseEvent ItemChanging(Me, e)
        End Sub
        ''' <summary>Raises <see cref="ItemChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnItemChanged"/> in order the event to be raised.</remarks>
        Protected Overridable Sub OnItemChanged(ByVal e As ItemIndexEventArgs)
            RaiseEvent ItemChanged(Me, e)
        End Sub
        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <returns>The element at the specified index.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True (in setter)</exception>
        Default Public Overridable Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
            Get
                Return List(index)
            End Get
            Set(ByVal value As T)
                If Locked Then Throw New InvalidOperationException("List is locked")
                Dim e As New CancelableItemIndexEventArgs(value, index, AddingReadOnly)
                If index >= 0 AndAlso index < List.Count Then
                    Lock()
                    Try
                        OnItemChanging(e)
                    Finally
                        Unlock()
                    End Try
                End If
                If Not e.Cancel Then
                    Dim old As T = List(index)
                    RemoveItemHandler(index)
                    List(index) = e.Item
                    AddItemHandler(index)
                    OnItemChanged(New ItemIndexEventArgs(old, index))
                End If
            End Set
        End Property
#End Region
        ''' <summary>Gives access to underlying <see cref="List(Of T)"/></summary>
        Protected ReadOnly Property InternalList() As List(Of T)
            Get
                Return List
            End Get
        End Property
        ''' <summary>Gives read-only access to underlying <see cref="List(Of T)"/></summary>
        Public ReadOnly Property AsReadOnly() As IReadOnlyList(Of T)
            Get
                Return New ReadOnlyListAdapter(Of T)(List)
            End Get
        End Property
        ''' <summary>Adds handler to item at specified index if the item is <see cref="IReportsChange"/></summary>
        ''' <param name="Index">Index of item to try add handler</param>
        ''' <remarks>Call after item is added</remarks>
        Protected Overridable Sub AddItemHandler(ByVal Index As Integer)
            If TypeOf Me(Index) Is IReportsChange Then
                AddHandler CType(Me(Index), IReportsChange).Changed, AddressOf OnItemValueChanged
            End If
        End Sub
        ''' <summary>Removes handler from item at specified index if the item is <see cref="IReportsChange"/></summary>
        ''' <param name="Index">Index of item to try remove handler</param>
        ''' <remarks>Call before item is removed</remarks>
        Protected Overridable Sub RemoveItemHandler(ByVal Index As Integer)
            If TypeOf Me(Index) Is IReportsChange Then
                RemoveHandler CType(Me(Index), IReportsChange).Changed, AddressOf OnItemValueChanged
            End If
        End Sub
        ''' <summary>Removes handlers from all item that are of type <see cref="IReportsChange"/></summary>
        ''' <remarks>Call before clering list</remarks>
        Protected Overridable Sub RemoveAllItemHandlers()
            For i As Integer = 0 To Count - 1
                RemoveItemHandler(i)
            Next i
        End Sub
        ''' <summary>Adds ahndlers to all items that as of type <see cref="IReportsChange"/></summary>
        ''' <remarks>Call only from CTor when no handlers have been added</remarks>
        Protected Overridable Sub AddAllItemHandlers()
            For i As Integer = 0 To Count - 1
                AddItemHandler(i)
            Next i
        End Sub
        ''' <summary>Raises the <see cref="ItemValueChanged"/> event and handles the <see cref="IReportsChange.Changed"/> event for items</summary>
        ''' <param name="sender">Original source of the event</param>
        ''' <param name="e">Original event parameters</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnItemValueChanged"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnItemValueChanged(ByVal sender As IReportsChange, ByVal e As EventArgs)
            RaiseEvent ItemValueChanged(Me, New ItemValueChangedEventArgs(sender, e))
        End Sub
        ''' <summary>Raised when any of items that is of type <see cref="IReportsChange"/> raises <see cref="IReportsChange.Changed"/> event</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event params (contains original source (item) and original arguments</param>
        Public Event ItemValueChanged(ByVal sender As ListWithEvents(Of T), ByVal e As ItemValueChangedEventArgs)

#Region "EventArgs"
        ''' <summary>Parameter of cancelable item events</summary>
        Public Class CancelableItemEventArgs : Inherits CancelEventArgs
            ''' <summary>Contains value of the <see cref="Item"/> property</summary>
            Private _Item As T
            ''' <summary>Contains value of the <see cref="[ReadOnly]"/> property</summary>
            Private _ReadOnly As Boolean
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item associated with current event</param>
            ''' <param name="ReadOnly">True to disallow changing of the <see cref="Item"/> property</param>
            Public Sub New(ByVal Item As T, Optional ByVal [ReadOnly] As Boolean = False)
                _Item = Item
                _ReadOnly = [ReadOnly]
            End Sub
            ''' <summary>Item associated with current event</summary>
            ''' <exception cref="system.Data.ReadOnlyException">Using setter when <see cref="[ReadOnly]"/> is True</exception>
            Public Property Item() As T
                Get
                    Return _Item
                End Get
                Set(ByVal value As T)
                    If Not [ReadOnly] Then
                        _Item = value
                    Else
                        Throw New System.Data.ReadOnlyException("Cannot change Item property when ReadOnly is True")
                    End If
                End Set
            End Property
            ''' <summary>Indicates if this instance's property <see cref="Item"/> is read-only or not</summary>
            Public ReadOnly Property [ReadOnly]() As Boolean
                Get
                    Return _ReadOnly
                End Get
            End Property
        End Class
        ''' <summary>Argument of <see cref="Adding"/> event</summary>
        Public Class CancelableItemIndexEventArgs : Inherits CancelableItemEventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item associated with current event</param>
            ''' <param name="ReadOnly">True to disallow changing of the <see cref="Item"/> property</param>
            ''' <param name="Index">Index of newly added item</param>
            Public Sub New(ByVal Item As T, ByVal Index As Integer, Optional ByVal [ReadOnly] As Boolean = False)
                MyBase.New(Item, [ReadOnly])
                NewIndex = Index
            End Sub
            ''' <summary>Index of newly added item</summary>
            ''' <remarks>The index may be invalid when collecion-manipulation is done between raising <see cref="Adding"/> event and performing <see cref="List(Of T).Add"/> operation on underlying <see cref="List(Of T)"/>. (it's always valid when performing <see cref="List(Of T).Insert"/> - event raised through <see cref="Insert"/>.)</remarks>
            Public ReadOnly NewIndex As Integer
        End Class

        ''' <summary>Parameter od non-cancelable item events</summary>
        Public Class ItemEventArgs : Inherits EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item associated with current event</param>
            Public Sub New(ByVal Item As T)
                Me.Item = Item
            End Sub
            ''' <summary>Item associated with current event</summary>
            Public ReadOnly Item As T
        End Class
        ''' <summary>Parameter of the <see cref="Added"/> event</summary>
        Public Class ItemIndexEventArgs : Inherits ItemEventArgs
            ''' <summary>Index ow newly added item</summary>
            Public ReadOnly Index As Integer
            ''' <summary>CTor</summary>
            ''' <param name="Item">Newly added item</param>
            ''' <param name="index">Index of newly added item</param>
            Public Sub New(ByVal Item As T, ByVal index As Integer)
                MyBase.New(Item)
                Me.Index = index
            End Sub
        End Class
        ''' <summary>Parameter of events that reports count</summary>
        Public Class CountEventArgs : Inherits EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Count">Count to be reported</param>
            Public Sub New(ByVal Count As Integer)
                Me.Count = Count
            End Sub
            ''' <summary>Reported count</summary>
            Public ReadOnly Count As Integer
        End Class
        ''' <summary>Parameter of the <see cref="ItemValueChanged"/> event</summary>
        Public Class ItemValueChangedEventArgs : Inherits EventArgs
            ''' <summary>Item that caused the event</summary>
            Public ReadOnly Item As T
            ''' <summary>Original argument of item's <see cref="IReportsChange.Changed"/> event</summary>
            Public ReadOnly OriginalEventArgs As EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item that caused the event</param>
            ''' <param name="OriginalEventArgs">Original argument of item's <see cref="IReportsChange.Changed"/> event</param>
            Public Sub New(ByVal Item As T, ByVal OriginalEventArgs As EventArgs)
                Me.Item = Item
                Me.OriginalEventArgs = OriginalEventArgs
            End Sub
        End Class
#End Region
    End Class
End Namespace
#End If