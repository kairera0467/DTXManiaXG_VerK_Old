#include "stdafx.h"
/*
* Copyright (c) 2007-2010 SlimDX Group
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

#include "DXGIException.h"

#include "Device1.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{
	Device1::Device1( IComObject^ device )
	{
		IDXGIDevice1* result = 0;

		IUnknown *ptr = reinterpret_cast<IUnknown*>(device->ComPointer.ToPointer());
		if( RECORD_DXGI( ptr->QueryInterface( IID_IDXGIDevice1, reinterpret_cast<void**>( &result ) ) ).IsFailure )
			throw gcnew DXGIException( Result::Last );

		Construct( result );
	}

	int Device1::MaximumFrameLatency::get()
	{
		UINT latency = 0;

		HRESULT hr = InternalPointer->GetMaximumFrameLatency(&latency);
		RECORD_DXGI(hr);

		return latency;
	}

	void Device1::MaximumFrameLatency::set(int value)
	{
		RECORD_DXGI(InternalPointer->SetMaximumFrameLatency(value));
	}
}
}
