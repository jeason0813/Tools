﻿#include "stdafx.h"
#include "Exceptions.h"

namespace Tools {
    namespace TotalCommanderT {
        namespace ResourcesT {
    
    
    inline Exceptions::Exceptions() {
    }
    
    inline ::System::Object^  Exceptions::InternalSyncObject::get() {
        if (System::Object::ReferenceEquals(_internalSyncObject, nullptr)) {
            ::System::Threading::Interlocked::CompareExchange(_internalSyncObject, (gcnew ::System::Object()), nullptr);
        }
        return _internalSyncObject;
    }
    
    inline ::System::Resources::ResourceManager^  Exceptions::ResourceManager::get() {
        if (System::Object::ReferenceEquals(_resourceManager, nullptr)) {
            ::System::Threading::Monitor::Enter(InternalSyncObject);
            try {
                if (System::Object::ReferenceEquals(_resourceManager, nullptr)) {
                    ::System::Threading::Interlocked::Exchange(_resourceManager, (gcnew ::System::Resources::ResourceManager(L"ToolsTotalCommander.Exceptions.resources", 
                            Tools::TotalCommanderT::ResourcesT::Exceptions::typeid->Assembly)));
                }
            }
            finally {
                ::System::Threading::Monitor::Exit(InternalSyncObject);
            }
        }
        return _resourceManager;
    }
    
    inline ::System::Globalization::CultureInfo^  Exceptions::Culture::get() {
        return _resourceCulture;
    }
    inline System::Void Exceptions::Culture::set(::System::Globalization::CultureInfo^  value) {
        _resourceCulture = value;
    }
    
    inline System::String^  Exceptions::PluginNotInitialized::get() {
        return ResourceManager->GetString(L"PluginNotInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginNotInitializedFormat() {
        return PluginNotInitialized;
    }
        }
    }
}
 