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
#include "stdafx.h"

#include "DXGIException.h"

#include "KeyedMutex.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{
	KeyedMutex::KeyedMutex( IComObject^ resource )
	{
		IDXGIKeyedMutex* result = 0;

		IUnknown *ptr = reinterpret_cast<IUnknown*>(resource->ComPointer.ToPointer());
		if( RECORD_DXGI( ptr->QueryInterface( IID_IDXGIKeyedMutex, reinterpret_cast<void**>( &result ) ) ).IsFailure )
			throw gcnew DXGIException( Result::Last );
		Construct( result );
	}

	Result KeyedMutex::Acquire(System::Int64 key, int milliseconds)
	{
		HRESULT hr = InternalPointer->AcquireSync(key, milliseconds);
		return RECORD_DXGI(hr);
	}

	Result KeyedMutex::Release(System::Int64 key)
	{
		HRESULT hr = InternalPointer->ReleaseSync(key);
		return RECORD_DXGI(hr);
	}
}
}
