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

#include <d2d1.h>
#include <d2d1helper.h>

#include "Direct2DException.h"

#include "Layer.h"

const IID IID_ID2D1Layer = __uuidof(ID2D1Layer);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	Layer::Layer( RenderTarget^ renderTarget )
	{
		ID2D1Layer *layer = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateLayer( &layer );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( layer );
	}

	Layer::Layer( RenderTarget^ renderTarget, System::Drawing::SizeF size )
	{
		ID2D1Layer *layer = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateLayer( reinterpret_cast<D2D1_SIZE_F*>( &size ), &layer );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( layer );
	}

	System::Drawing::SizeF Layer::Size::get()
	{
		D2D1_SIZE_F size = InternalPointer->GetSize();
		return System::Drawing::SizeF( size.width, size.height );
	}
}
}