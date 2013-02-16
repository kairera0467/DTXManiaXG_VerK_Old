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
#include "stdafx.h"

#include "FastFourierTransformBufferRequirements11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{
	FastFourierTransformBufferRequirements::FastFourierTransformBufferRequirements( const D3DX11_FFT_BUFFER_INFO &bufferInfo )
	{
		TemporaryBufferCount = bufferInfo.NumTempBufferSizes;
		PrecomputeBufferCount = bufferInfo.NumPrecomputeBufferSizes;

		TemporaryBufferSizes = gcnew array<int>(TemporaryBufferCount);
		PrecomputeBufferSizes = gcnew array<int>(PrecomputeBufferCount);

		for (int i = 0; i < TemporaryBufferCount; i++)
			TemporaryBufferSizes[i] = bufferInfo.TempBufferFloatSizes[i];

		for (int i = 0; i < PrecomputeBufferCount; i++)
			PrecomputeBufferSizes[i] = bufferInfo.PrecomputeBufferFloatSizes[i];
	}
}
}