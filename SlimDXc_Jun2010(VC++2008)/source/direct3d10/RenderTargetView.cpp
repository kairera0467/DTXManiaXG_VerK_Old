#include "stdafx.h"
/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

#include <d3d10.h>

#include "Direct3D10Exception.h"

#include "Device10.h"
#include "RenderTargetView.h"
#include "RenderTargetViewDescription.h"
#include "Resource10.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	RenderTargetView::RenderTargetView( SlimDX::Direct3D10::Device^ device, SlimDX::Direct3D10::Resource^ resource )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( resource == nullptr )
			throw gcnew ArgumentNullException( "resource" );
		
		ID3D10RenderTargetView *view = 0;
		if( RECORD_D3D10( device->InternalPointer->CreateRenderTargetView( resource->InternalPointer, NULL, &view ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( view );
	}
	
	RenderTargetView::RenderTargetView( SlimDX::Direct3D10::Device^ device, SlimDX::Direct3D10::Resource^ resource, RenderTargetViewDescription description )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( resource == nullptr )
			throw gcnew ArgumentNullException( "resource" );
		
		ID3D10RenderTargetView *view = 0;
		D3D10_RENDER_TARGET_VIEW_DESC nativeDescription = description.CreateNativeVersion();
		if( RECORD_D3D10( device->InternalPointer->CreateRenderTargetView( resource->InternalPointer, &nativeDescription, &view ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( view );
	}
	
	RenderTargetViewDescription RenderTargetView::Description::get()
	{
		D3D10_RENDER_TARGET_VIEW_DESC nativeDescription;
		InternalPointer->GetDesc( &nativeDescription );
		return RenderTargetViewDescription( nativeDescription );
	}
}
}
